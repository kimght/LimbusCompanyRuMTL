import fnmatch
import re
from functools import cache
from pathlib import Path

from jsonpath_ng import JSONPath
from jsonpath_ng.ext import parse
from loguru import logger

from .assets import ReplacementsMap
from .json_data import JsonObject, JsonValue
from .models import CloseHighlight, FontRule, XmlEscape

FONT_MACRO = re.compile(r"^\[font=(?P<font>[^\]]+)\]")
OPEN_TAG = re.compile(r"<(?P<keyword_id>[A-z]+)[^>]*>")
SHORT_KEYWORD = re.compile(r"\[(?P<keyword_id>[A-z]+)\]")
PLACEHOLDER = re.compile(r"\{\d*\}")


@cache
def json_path(expression: str) -> JSONPath:
    return parse(expression)


def get_markup_positions(
    text: str,
    singular_keywords: list[str],
    escape_short: bool = True,
    escape_keywords: bool = True,
) -> list[tuple[int, int]]:
    if not escape_keywords:
        return []

    ranges: list[tuple[int, int]] = []
    for match in OPEN_TAG.finditer(text):
        keyword_id = match.group("keyword_id")
        close_tag = re.compile(rf"</{re.escape(keyword_id)}\s*>")
        close_tags = list(close_tag.finditer(text, match.end()))
        if close_tags or keyword_id.lower() in singular_keywords:
            ranges.append(match.span())
            ranges.extend(tag.span() for tag in close_tags)

    if escape_short:
        ranges.extend(match.span() for match in SHORT_KEYWORD.finditer(text))
    ranges.extend(match.span() for match in PLACEHOLDER.finditer(text))

    merged: list[tuple[int, int]] = []
    for start, end in sorted(ranges):
        if merged and start <= merged[-1][1]:
            previous_start, previous_end = merged[-1]
            merged[-1] = (previous_start, max(previous_end, end))
        else:
            merged.append((start, end))
    return merged


def convert_font(
    text: str,
    replacements: dict[str, str],
    singular_keywords: list[str],
    escape_short: bool = True,
    escape_keywords: bool = True,
) -> str:
    parts: list[str] = []
    position = 0
    for start, end in get_markup_positions(
        text, singular_keywords, escape_short, escape_keywords
    ):
        parts.extend(replacements.get(char, char) for char in text[position:start])
        parts.append(text[start:end])
        position = end
    parts.extend(replacements.get(char, char) for char in text[position:])
    return "".join(parts)


class FontConverter:
    def __init__(
        self,
        rules: dict[str, list[FontRule]],
        replacements_map: ReplacementsMap,
        xml_escape: XmlEscape,
    ) -> None:
        self.rules: dict[str, list[FontRule]] = rules
        self.replacements_map: ReplacementsMap = replacements_map
        self.xml_escape: XmlEscape = xml_escape

    def process(self, data: JsonObject, file: Path) -> None:
        updated: set[str] = set()
        for match in json_path("$..*").find(data):
            if not isinstance(match.value, str):
                continue
            macro = FONT_MACRO.match(match.value)
            if macro is None:
                continue
            font = macro.group("font")
            if font == "default":
                updated.add(str(match.full_path))
                continue
            if font not in self.replacements_map:
                logger.warning(f"Font {font} not found in replacements map!")
                continue
            converted = convert_font(
                match.value[macro.end() :].lstrip(),
                self.replacements_map[font],
                self.xml_escape.singular_keywords,
            )
            _ = match.full_path.update(data, converted)
            updated.add(str(match.full_path))

        for pattern, rules in self.rules.items():
            if not fnmatch.fnmatch(file.as_posix(), pattern):
                continue
            for rule in rules:
                if rule.font not in self.replacements_map:
                    logger.warning(f"Font {rule.font} not found in replacements map!")
                    continue
                for match in json_path(rule.path).find(data):
                    location = str(match.full_path)
                    if not isinstance(match.value, str) or location in updated:
                        continue
                    converted = convert_font(
                        match.value,
                        self.replacements_map[rule.font],
                        self.xml_escape.singular_keywords,
                        rule.escape_short_keywords,
                        rule.escape_keywords,
                    )
                    _ = match.full_path.update(data, converted)
                    updated.add(location)



def replace_shorthands(
    text: str,
    keyword_colors: dict[str, str],
    keyword_pattern: re.Pattern[str],
    converting_tuple: bool
) -> str:
    def replacement(match: re.Match[str]) -> str:
        if converting_tuple:
            def format_keyword(value: str) -> str:
                link_id, separator, sprite_id = value.partition(":")
                if separator == "": sprite_id = link_id
                return f'<u><link="{link_id}"><sprite name="{sprite_id}"></link></u>'
            keyword_ids = [format_keyword(value) for value in match.group("keyword_ids").split(",")]
            color = match.group("color")
            label = match.group("text")
            return f'<color={color}>({"/".join(keyword_ids)})<u>{label}</u></color>'
        else:
            keyword_id = match.group("keyword_id")
            sprite_id = match.group("sprite_id") or keyword_id
            color = match.group("color") or keyword_colors.get(keyword_id, "#f8c200")
            label = match.group("text")
            if match.group("cancel_link") is None: label = f'<link="{keyword_id}">{label}</link>'
            return f'<sprite name="{sprite_id}"><color={color}><u>{label}</u></color>'
    return keyword_pattern.sub(replacement, text)


def convert_keywords(
    data: JsonValue,
    keyword_colors: dict[str, str],
    keyword_pattern: re.Pattern[str],
    converting_tuple: bool
) -> JsonValue:
    if isinstance(data, dict):
        for key, value in data.items():
            data[key] = convert_keywords(value, keyword_colors, keyword_pattern, converting_tuple)
    elif isinstance(data, list):
        for index, value in enumerate(data):
            data[index] = convert_keywords(value, keyword_colors, keyword_pattern, converting_tuple)
    elif isinstance(data, str):
        return replace_shorthands(data, keyword_colors, keyword_pattern, converting_tuple)
    return data



def replace_small_hints(
    text: str,
    hints_pattern: re.Pattern[str]
) -> str:
    def replacement(match: re.Match[str]) -> str:
        keyword_id = match.group("keyword_id")
        label = match.group("text")
        return f'<link=\"{keyword_id}\"><u>{label[0]}</u>{label[1:]}</link>'
    return hints_pattern.sub(replacement, text)

def convert_small_hints(
    data: JsonValue,
    hints_pattern: re.Pattern[str]
) -> JsonValue:
    if isinstance(data, dict):
        for key, value in data.items():
            data[key] = convert_small_hints(value, hints_pattern)
    elif isinstance(data, list):
        for index, value in enumerate(data):
            data[index] = convert_small_hints(value, hints_pattern)
    elif isinstance(data, str):
        return replace_small_hints(data, hints_pattern)
    return data



def replace_illegal_square_brackets(
    text: str,
    tweak_pattern: re.Pattern[str],
    open_skilltag: str,
    close_skilltag: str
) -> str:
    def replacement(match: re.Match[str]) -> str:
        color = match.group("color")
        label = match.group("text")
        if color is None: color = "#ebcaa2"
        return f'<color={color}>[{open_skilltag}]<color={color}>{label}[{close_skilltag}]'
    return tweak_pattern.sub(replacement, text)

def convert_illegal_square_brackets(
    data: JsonValue,
    tweak_pattern: re.Pattern[str],
    open_skilltag: str,
    close_skilltag: str
) -> JsonValue:
    if isinstance(data, dict):
        for key, value in data.items():
            data[key] = convert_illegal_square_brackets(value, tweak_pattern, open_skilltag, close_skilltag)
    elif isinstance(data, list):
        for index, value in enumerate(data):
            data[index] = convert_illegal_square_brackets(value, tweak_pattern, open_skilltag, close_skilltag)
    elif isinstance(data, str):
        return replace_illegal_square_brackets(data, tweak_pattern, open_skilltag, close_skilltag)
    return data



def close_highlights(data: JsonObject, rule: CloseHighlight) -> None:
    for match in json_path(rule.path).find(data):
        value = match.value
        if isinstance(value, str) and value and '<style="highlight">' not in value:
            _ = match.full_path.update(data, f'{value}<style="highlight"></style>')
