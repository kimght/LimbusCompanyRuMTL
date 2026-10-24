import argparse
import fnmatch
import json
import re
import shutil
from collections.abc import Sequence
from dataclasses import dataclass
from pathlib import Path

from loguru import logger

from .assets import (
    download_included_fonts,
    load_keyword_colors,
    load_replacements_map,
    prepare_reference,
)
from .json_data import JsonObject, load_document, object_list
from .models import Config
from .story import load_story_entities, translate_rpg, translate_story
from .text import FontConverter, close_highlights, convert_keywords, convert_small_hints, convert_illegal_square_brackets

LOCALIZATION_PATH = Path("localize")
STORY_PATH = Path("story")
KEYWORD_COLORS_PATH = Path("data/build/keyword_colors.txt")


def entry_id(entry: JsonObject, key: str = "id") -> str | int | float | None:
    value = entry.get(key)
    if isinstance(value, (dict, list)):
        raise ValueError(f"Invalid entry ID: {value}")
    return value


def merge_by_id(
    reference: list[JsonObject],
    localized: list[JsonObject],
    file: Path,
    *,
    key: str = "id",
) -> list[JsonObject]:
    by_id = {entry_id(entry, key): entry for entry in localized}
    if len(by_id) != len(localized):
        logger.debug(f"Duplicate ID in {file}")
    missing_ids = [
        entry_id(entry, key) for entry in reference if entry_id(entry, key) not in by_id
    ]
    if missing_ids:
        logger.debug(f"Unknown IDs in {file}: {missing_ids}")
    return [by_id.get(entry_id(entry, key), entry) for entry in reference]


def merge_by_order(
    reference: list[JsonObject], localized: list[JsonObject]
) -> list[JsonObject]:
    return [
        localized[index] if index < len(localized) else entry
        for index, entry in enumerate(reference)
    ]


def matches_any(path: Path, patterns: Sequence[str]) -> bool:
    return any(fnmatch.fnmatch(path.as_posix(), pattern) for pattern in patterns)


def build(
    config: Config,
    reference_path: Path,
    output_path: Path,
    *,
    localization_path: Path = LOCALIZATION_PATH,
    story_path: Path = STORY_PATH,
    keyword_colors_path: Path = KEYWORD_COLORS_PATH,
    include_fonts: bool = True,
) -> None:
    font_converter = FontConverter(
        config.font_rules, load_replacements_map(config.font), config.xml_escape
    )
    keyword_colors = load_keyword_colors(keyword_colors_path)
    keyword_regex = re.compile(config.keyword_shorthands.regex)
    keyword_tuple_regex = re.compile(config.keyword_shorthands.regex_tuple)
    small_hints_regex = re.compile(config.keyword_shorthands.regex_small_hints)
    illegal_square_brackets_tweak_regex = re.compile(config.illegal_square_brackets_tweak.regex)
    entities = load_story_entities(story_path / "entities")
    output_path.mkdir(parents=True, exist_ok=True)
    if include_fonts:
        download_included_fonts(config.font, output_path / "Font")

    for source in reference_path.rglob("*.json"):
        if not source.is_file():
            continue
        relative_path = source.relative_to(reference_path)
        localized_file = localization_path / relative_path
        output_file = output_path / relative_path
        chapter = story_path / "chapters" / relative_path.with_suffix(".txt").name
        is_story = relative_path.parts[0] == "StoryData" and chapter.is_file()
        is_rpg = matches_any(relative_path, config.rpg.dialogue_files)
        output_file.parent.mkdir(parents=True, exist_ok=True)

        if not is_story and not is_rpg and not localized_file.exists():
            _ = shutil.copyfile(source, output_file)
            continue
        reference = load_document(source)
        if not reference:
            _ = shutil.copyfile(source, output_file)
            continue

        if is_rpg:
            localized = translate_rpg(
                reference, story_path / "rpg" / relative_path.stem, entities
            )
        elif is_story:
            localized = translate_story(reference, chapter, entities)
        else:
            localized = load_document(localized_file)

        if matches_any(relative_path, ["Skill*.json", "Passive*.json"]):
            _ = convert_illegal_square_brackets(
                localized,
                illegal_square_brackets_tweak_regex,
                config.illegal_square_brackets_tweak.open_skilltag,
                config.illegal_square_brackets_tweak.close_skilltag
            )

        if matches_any(relative_path, config.keyword_shorthands.apply_for):
            _ = convert_keywords(localized, keyword_colors, keyword_regex, converting_tuple=False)
            _ = convert_keywords(localized, keyword_colors, keyword_tuple_regex, converting_tuple=True)
            _ = convert_small_hints(localized, small_hints_regex)
        for rule in config.close_highlight:
            if fnmatch.fnmatch(relative_path.as_posix(), rule.file_pattern):
                close_highlights(localized, rule)

        reference_rows = object_list(reference["dataList"])
        localized_rows = object_list(localized["dataList"])
        if is_rpg:
            rows = localized_rows
        elif matches_any(relative_path, config.priority.order):
            rows = merge_by_order(reference_rows, localized_rows)
        else:
            rows = merge_by_id(
                reference_rows,
                localized_rows,
                output_file,
                key="key" if relative_path.parts[0] == "RPGSystem" else "id",
            )

        result = reference.copy()
        result["dataList"] = list(rows)
        font_converter.process(result, relative_path)
        _ = output_file.write_text(
            json.dumps(result, ensure_ascii=False, indent=2), encoding="utf-8-sig"
        )


@dataclass
class Arguments(argparse.Namespace):
    config: Path = Path("config.toml")
    output: Path = Path("dist/localize")
    reference: Path = Path(".reference")
    no_download_reference: bool = False
    no_include_font: bool = False


def parse_arguments(argv: Sequence[str] | None = None) -> Arguments:
    parser = argparse.ArgumentParser(
        description="Build the Russian localization files."
    )
    defaults = Arguments()
    _ = parser.add_argument("--config", type=Path, default=defaults.config)
    _ = parser.add_argument("--output", type=Path, default=defaults.output)
    _ = parser.add_argument("--reference", type=Path, default=defaults.reference)
    _ = parser.add_argument("--no-download-reference", action="store_true")
    _ = parser.add_argument("--no-include-font", action="store_true")
    return parser.parse_args(argv, namespace=defaults)


def main(argv: Sequence[str] | None = None) -> None:
    args = parse_arguments(argv)
    config = Config.from_file(args.config)
    reference_path = (
        args.reference
        if args.no_download_reference
        else prepare_reference(config.reference, args.reference)
    )
    build(config, reference_path, args.output, include_fonts=not args.no_include_font)


if __name__ == "__main__":
    main()
