from pathlib import Path
from typing import Self

import msgspec


class FontRule(msgspec.Struct):
    font: str
    path: str
    escape_short_keywords: bool = True
    escape_keywords: bool = True


class IncludedFont(msgspec.Struct):
    path: str
    filename: str


class Font(msgspec.Struct):
    replacement_map_path: str
    repo: str | None = None
    include: list[IncludedFont] = msgspec.field(default_factory=list)


class Reference(msgspec.Struct):
    path: str
    repo: str | None = None
    branch: str | None = None


class KeywordShorthands(msgspec.Struct):
    regex: str
    regex_tuple: str
    regex_small_hints: str
    apply_for: list[str]


class IllegalSquareBracketsTweak(msgspec.Struct):
    regex: str
    open_skilltag: str
    close_skilltag: str


class Priority(msgspec.Struct):
    order: list[str] = msgspec.field(default_factory=list)


class XmlEscape(msgspec.Struct):
    singular_keywords: list[str] = msgspec.field(default_factory=list)


class CloseHighlight(msgspec.Struct):
    path: str
    file_pattern: str


class Rpg(msgspec.Struct):
    dialogue_files: list[str] = msgspec.field(default_factory=list)


class Config(msgspec.Struct):
    font: Font
    reference: Reference
    keyword_shorthands: KeywordShorthands
    illegal_square_brackets_tweak: IllegalSquareBracketsTweak
    priority: Priority
    xml_escape: XmlEscape

    font_rules: dict[str, list[FontRule]] = msgspec.field(default_factory=dict)
    close_highlight: list[CloseHighlight] = msgspec.field(default_factory=list)
    rpg: Rpg = msgspec.field(default_factory=Rpg)

    @classmethod
    def from_file(cls, path: Path) -> Self:
        return msgspec.toml.decode(path.read_bytes(), type=cls)


class ReleaseAsset(msgspec.Struct):
    name: str
    browser_download_url: str


class Release(msgspec.Struct):
    assets: list[ReleaseAsset]


class StoryEntity(msgspec.Struct):
    original: str
    translation: str
    model: str | None = None


class StoryEntities(msgspec.Struct):
    teller: list[StoryEntity] = msgspec.field(default_factory=list)
    title: list[StoryEntity] = msgspec.field(default_factory=list)
    place: dict[str, str | int] = msgspec.field(default_factory=dict)
