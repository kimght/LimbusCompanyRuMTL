<div align="center">
<a href="https://github.com/kimght/LimbusCompanyRuMTL">
   <img src="https://github.com/kimght/LimbusCompanyRuMTL/blob/main/Localize/Readme/Readme_Crescent_Button.png"
      width="200"
      height="200"/>
</a>
   
# LimbusCompanyRuMTL
Мод на (правленный?) машинный русский перевод Limbus Company
   
<b>Наши друзья</b><br/>

[한국어](https://limbuscompany.kr) | [简体中文](https://github.com/LocalizeLimbusCompany/LocalizeLimbusCompany) | [Ручной русский перевод](https://github.com/Crescent-Corporation/LimbusCompanyBusRUS)

[Español](https://github.com/Dreams-Office/LimbusCompanySpanishTranslationTeam) | [日本語](https://limbuscompany.kr) | [Français](https://github.com/Eden-Office/LimbusCompanyBusFR) 
[繁體中文](https://github.com/SmallYuanSY/LocalizeLimbusCompany) | [ภาษาไทย](https://github.com/JoshSnappas/LocalizeLimbusCompanyTH) | [Indonesia](https://github.com/ArtefactX1/LocalizeLimbusID)
   
[![Загрузки](https://img.shields.io/github/downloads/kimght/LimbusCompanyRuMTL/total?style=flat-square&label=%D0%92%D1%81%D0%B5%D0%B3%D0%BE%20%D0%B7%D0%B0%D0%B3%D1%80%D1%83%D0%B7%D0%BE%D0%BA&color=%23707489)](../../releases)
[![Тредик](https://img.shields.io/badge/%2Fpmg%2F-thread?style=flat-square&label=%D0%9B%D0%B8%D0%B3%D0%BC%D1%83%D1%81%20%D0%A2%D1%80%D0%B5%D0%B4%D0%B8%D0%BA&color=%23f99b06)](https://2ch.hk/gacha/catalog.html)
[![Последний релиз](https://img.shields.io/github/v/release/kimght/LimbusCompanyRuMTL?style=flat-square&label=%D0%9F%D0%BE%D1%81%D0%BB%D0%B5%D0%B4%D0%BD%D1%8F%D1%8F%20%D0%B2%D0%B5%D1%80%D1%81%D0%B8%D1%8F&labelColor=%23707489&color=%23484f58)](../../releases/latest)
</div>

# Установка
## Windows
### Установи .NET SDK (6.0)
1. Скачай и запусти [установщик](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.413-windows-x64-installer)

### Установи BepInEx
1. Скачай арахив с [BepInEx](https://builds.bepinex.dev/projects/bepinex_be/692/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.692%2B851521c.zip)
2. Чтобы найти папку игры, зайди в библиотеку Steam и нажми ПКМ по Limbus Company в списке игр. В контекстом меню выбери `Управление -> Посмотреть локальные файлы`
3. Перенеси все файлы из архива с 1 шага в эту папку
4. Запусти игру. Если при запуске открывается консоль, значит все ок. После того, как BepInEx установится, закрой игру

### Установи мод
1. Скачай [последнюю версию мода](../../releases/latest)
2. Перемести папку `LimbusCompanyRuMTL` из архива в `<Папка Игры>\BepInEx\plugins`

### Установи шрифт
1. Скачай [файл с русскими шрифтами](https://mega.nz/folder/jfpXCITY#lIR8cGWquj53lsC-73r7gQ/file/CS5GnaTB)
2. Перенеси файл `tmpcyrillicfont` в `<Папка Игры>\BepInEx\plugins\LimbusCompanyRuMTL`

## Proton (Linux, Steam Deck)
### Установи [protontricks](https://github.com/Matoking/protontricks)
`pipx install protontricks`

### Установи .NET SDK (6.0)
1. Скачай [установщик](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.413-windows-x64-installer)
2. Установи его в вайнпрефиксе Limbus Company:
   ```bash
   protontricks-launch --appid 1973530 dotnet-sdk.exe
   ```

### Установи BepInEx
1. Скачай арахив с [BepInEx](https://builds.bepinex.dev/projects/bepinex_be/692/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.692%2B851521c.zip)
2. Чтобы найти папку игры, зайди в библиотеку Steam и нажми ПКМ по Limbus Company в списке игр. В контекстом меню выбери `Управление -> Посмотреть локальные файлы`
3. Перенеси все файлы из архива с 1 шага в эту папку
4. Запусти protontricks:
   ```bash
   protontricks --gui
   ```
5. Запусти winecfg: `Limbus Company -> Select the default wineprefix -> Run winecfg`
6. Перейди во вкладку `Libraries`. В выпадающем списке `New override for library` выбери `winhttp` и нажми `Add`
   
   ![winecfg](https://docs.bepinex.dev/articles/advanced/images/winecfg_add_lib.png)
   
8. После этого нажми `Apply` и закрой winecfg
9. Запусти игру. Если при запуске открывается консоль, значит все ок. Подожди, пока BepInEx установится, и закрой игру

### Установи мод
1. Скачай [последнюю версию мода](../../releases/latest)
2. Перемести папку `LimbusCompanyRuMTL` из архива в `<Папка Игры>/BepInEx/plugins`

### Установи шрифт
1. Скачай [файл с русскими шрифтами](https://mega.nz/folder/jfpXCITY#lIR8cGWquj53lsC-73r7gQ/file/CS5GnaTB)
2. Перенеси файл `tmpcyrillicfont` в `<Папка Игры>/BepInEx/plugins/LimbusCompanyRuMTL`

# [Помочь с переводом](https://github.com/kimght/LimbusStory)

# Благодарности
- Команде [LLC](https://github.com/LocalizeLimbusCompany) за создание и поддержку проекта.
- Анонам из [/pmg/ треда](https://2ch.hk/gacha/catalog.html) за поддержку.
- [Авторам ручного русского перевода](https://github.com/Crescent-Corporation/LimbusCompanyBusRUS).
- <b>abcdcode</b> за помощь с кодом для улучшения отображения шрифтов.
