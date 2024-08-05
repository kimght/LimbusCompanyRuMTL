using SimpleJSON;
using BepInEx.Configuration;
using Il2CppSystem.Threading;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace LimbusLocalizeRUS
{
    public static class LCBR_UpdateChecker
    {
        public static ConfigEntry<bool> AutoUpdate = LCB_LCBRMod.LCBR_Settings.Bind("LCBR Settings", "AutoUpdate", false, "");
        public static void StartAutoUpdate()
        {
            if (AutoUpdate.Value)
            {
                LCB_LCBRMod.LogWarning("Check Mod Update");
                Action ModUpdate = CheckModUpdate;
                new Thread(ModUpdate).Start();
            }
        }
        static void CheckModUpdate()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://api.github.com/repos/kimght/LimbusCompanyRuMTL/releases/latest");
            www.timeout = 4;
            www.SendWebRequest();
            while (!www.isDone)
                Thread.Sleep(100);
            if (www.result != UnityWebRequest.Result.Success)
                LCB_LCBRMod.LogWarning("Ошибка со связью с GitHub!" + www.error);
            else
            {
                var latest = JSONNode.Parse(www.downloadHandler.text).AsObject;
                string latestReleaseTag = latest["tag_name"].Value;
//                string latest2ReleaseTag = releases.m_List.Count > 1 ? releases[1]["tag_name"].Value : string.Empty;
                if (Version.Parse(LCB_LCBRMod.VERSION) < Version.Parse(latestReleaseTag.Remove(0, 1)))
                {
                    string updatelog = "LimbusCompanyRuMTL_" + latestReleaseTag;
                    string download = $"https://github.com/kimght/LimbusCompanyRuMTL/releases/download/{latestReleaseTag}/{updatelog}.zip";
                    var dirs = download.Split('/');
                    string filename = LCB_LCBRMod.GamePath + "/" + dirs[^1];
                    if (!File.Exists(filename))
                        DownloadFileAsync(download, filename).GetAwaiter().GetResult();
                    UpdateCall = UpdateDel;
                }
                // TODO: Add cyrillic font update checker
                // LCB_LCBRMod.LogWarning("Check Cyrillic Font Asset Update");
                // Action FontAssetUpdate = CheckCyrillicFontAssetUpdate;
                // new Thread(FontAssetUpdate).Start();
            }
        }
        static void CheckCyrillicFontAssetUpdate()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://drive.google.com/file/d/12JMuwx-93WTN-uO0xl3mGRhBzemUKYhJ/view?usp=drive_link");
            string FilePath = LCB_LCBRMod.ModPath + "/tmpcyrillicfonts";
            var LastWriteTime = File.Exists(FilePath) ? int.Parse(TimeZoneInfo.ConvertTime(new FileInfo(FilePath).LastWriteTime, TimeZoneInfo.FindSystemTimeZoneById("Moscow Standard Time")).ToString("ddMMyy")) : 0;
            www.SendWebRequest();
            while (!www.isDone)
                Thread.Sleep(100);
            var latest = JSONNode.Parse(www.downloadHandler.text).AsObject;
            int latestReleaseTag = int.Parse(latest["tag_name"].Value);
            if (LastWriteTime < latestReleaseTag)
            {
                string download = "https://drive.google.com/uc?export=download&id=12JMuwx-93WTN-uO0xl3mGRhBzemUKYhJ";
                var dirs = download.Split('/');
                string filename = LCB_LCBRMod.GamePath + "/" + dirs[^1];
                if (!File.Exists(filename))
                    DownloadFileAsync(download, filename).GetAwaiter().GetResult();
                UpdateCall = UpdateDel;
            }
        }
        static void UpdateDel()
        {
            LCB_LCBRMod.OpenGamePath();
            Application.Quit();
        }
        // TODO: Fix
        static async Task DownloadFileAsync(string url, string filePath)
        {
            LCB_LCBRMod.LogWarning("Download " + url + " To " + filePath);
            using HttpClient client = new();
            using HttpResponseMessage response = await client.GetAsync(url);
            using HttpContent content = response.Content;
            using FileStream fileStream = new(filePath, FileMode.Create);
            await content.CopyToAsync(fileStream);
        }
        public static void CheckReadmeUpdate()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/kimght/LimbusCompanyRuMTL/blob/Localize/Readme/Readme.json");
            www.timeout = 1;
            www.SendWebRequest();
            string FilePath = LCB_LCBRMod.ModPath + "/Localize/Readme/Readme.json";
            var LastWriteTime = new FileInfo(FilePath).LastWriteTime;
            while (!www.isDone)
            {
                Thread.Sleep(100);
            }
            if (www.result == UnityWebRequest.Result.Success && LastWriteTime < DateTime.Parse(www.downloadHandler.text))
            {
                UnityWebRequest www2 = UnityWebRequest.Get("https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/kimght/LimbusCompanyRuMTL/blob/Localize/Readme/Readme.json");
                www2.SendWebRequest();
                while (!www2.isDone)
                {
                    Thread.Sleep(100);
                }
                File.WriteAllText(FilePath, www2.downloadHandler.text);
                LCBR_ReadmeManager.InitReadmeList();
            }
        }
        public static string Updatelog;
        public static Action UpdateCall;
    }
}
