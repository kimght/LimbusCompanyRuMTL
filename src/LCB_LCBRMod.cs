using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace LimbusLocalizeRUS
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class LCB_LCBRMod : BasePlugin
    {
        public static ConfigFile LCBR_Settings;
        public static string ModPath;
        public static string GamePath;
        public const string GUID = "Com.BrightNKnightey.LocalizeLimbusCompanyRUS";
        public const string NAME = "LimbusCompanyBusRUS";
        public const string VERSION = "0.1.7";
        public const string AUTHOR = "Base: Bright\nRUS version: Knightey, abcdcode";
        public const string LCBRLink = "https://github.com/Crescent-Corporation/LimbusCompanyBusRUS";
        public static Action<string, Action> LogFatalError { get; set; }
        public static Action<string> LogError { get; set; }
        public static Action<string> LogWarning { get; set; }
        public static void OpenLCBRURL() => Application.OpenURL(LCBRLink);
        public static void OpenGamePath() => Application.OpenURL(GamePath);
        public override void Load()
        {
            LCBR_Settings = Config;
            LogError = (string log) => { Log.LogError(log); Debug.LogError(log); };
            LogWarning = (string log) => { Log.LogWarning(log); Debug.LogWarning(log); };
            LogFatalError = (string log, Action action) => { LCBR_Manager.FatalErrorlog += log + "\n"; LogError(log); LCBR_Manager.FatalErrorAction = action; LCBR_Manager.CheckModActions(); };
            ModPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            GamePath = new DirectoryInfo(Application.dataPath).Parent.FullName;
            LCBR_UpdateChecker.StartAutoUpdate();
            try
            {
                HarmonyLib.Harmony harmony = new(NAME);
                if (LCBR_Russian_Settings.IsUseRussian.Value)
                {
                    LCBR_Manager.InitLocalizes(new DirectoryInfo(ModPath + "/Localize/RU"));
                    harmony.PatchAll(typeof(LCB_Cyrillic_Font));
                    harmony.PatchAll(typeof(LCBR_ReadmeManager));
                    harmony.PatchAll(typeof(LCBR_LoadingManager));
                    harmony.PatchAll(typeof(LCBR_SpriteUI));
                }
                harmony.PatchAll(typeof(LCBR_Manager));
                harmony.PatchAll(typeof(LCBR_Russian_Settings));
                if (!LCB_Cyrillic_Font.AddCyrillicFont(ModPath + "/tmpcyrillicfonts"))
                    LogFatalError("You have forgotten to install Font Update Mod. Please, reread README on Github.", OpenLCBRURL);
            }
            catch (Exception e)
            {
                LogFatalError("Mod has met an unknown fatal error! Contact us through urls in Github with the log, please.", () => { CopyLog(); OpenGamePath(); OpenLCBRURL(); });
                LogError(e.ToString());
            }
        }
        public static void CopyLog()
        {
            File.Copy(GamePath + "/BepInEx/LogOutput.log", GamePath + "/Latest.log", true);
            File.Copy(Application.consoleLogPath, GamePath + "/Player.log", true);
        }
    }
}
