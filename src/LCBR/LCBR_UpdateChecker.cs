using SimpleJSON;
using BepInEx.Configuration;
using Il2CppSystem.Threading;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace LimbusLocalizeRUS
{
    public static class LCBR_LocalizationUpdater
    {
        private static readonly string RepoOwner = "kimght";
        private static readonly string RepoName = "LimbusLocalizeRU";
        private static readonly string BranchName = "release";
        private static readonly string LocalDirectory = LCB_LCBRMod.ModPath + "/Localize";
        private static readonly HttpClient HttpClient = new HttpClient();
        
        public static void UpdateLocalizationSync() {
            UpdateLocalization().GetAwaiter().GetResult();
        }

        public static async Task UpdateLocalization()
        {
            LCB_LCBRMod.LogWarning("Checking for localization updates...");
            await UpdateLocalizationFiles();
        }

        public static async Task UpdateLocalizationFiles()
        {
            string latestCommitSha = await GetLatestCommitSha();
            if (latestCommitSha == null)
            {
                LCB_LCBRMod.LogWarning("Failed to get the latest commit SHA.");
                return;
            }

            var metadata = await GetMetadata(latestCommitSha);
            if (metadata == null)
            {
                LCB_LCBRMod.LogWarning("Failed to get localization metadata.");
                return;
            }

            foreach (var file in metadata.Files)
            {
                string localFilePath = Path.Combine(LocalDirectory, file.Path);
                var localFileMetadata = GetLocalFileMetadata(localFilePath);

                if (localFileMetadata == null || localFileMetadata.Checksum != file.Checksum)
                {
                    LCB_LCBRMod.LogInfo($"Updating file: {file.Path}");
                    string fileUrl = $"https://raw.githubusercontent.com/{RepoOwner}/{RepoName}/{latestCommitSha}/{file.Path}";
                    await DownloadFile(fileUrl, localFilePath);
                }
            }
        }

        private static async Task<string> GetLatestCommitSha()
        {
            string url = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/commits/{BranchName}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", "LocalizationFileUpdater");

            var response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to fetch latest commit SHA: {response.ReasonPhrase}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var commitInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
            return commitInfo["sha"]?.ToString();
        }

        private static async Task<Metadata> GetMetadata(string commitSha)
        {
            string url = $"https://raw.githubusercontent.com/{RepoOwner}/{RepoName}/{commitSha}/metadata.json";
            var response = await HttpClient.GetStringAsync(url);

            return JsonSerializer.Deserialize<Metadata>(response);
        }

        private static FileMetadata GetLocalFileMetadata(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            return new FileMetadata
            {
                Size = new FileInfo(filePath).Length,
                Checksum = GetFileChecksum(filePath)
            };
        }

        private static string GetFileChecksum(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private static async Task DownloadFile(string url, string destPath)
        {
            var response = await HttpClient.GetByteArrayAsync(url);

            string directoryPath = Path.GetDirectoryName(destPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            await File.WriteAllBytesAsync(destPath, response);
        }

        public class Metadata
        {
            public string Version { get; set; }
            public List<FileData> Files { get; set; }
        }

        public class FileData
        {
            public string Path { get; set; }
            public string Checksum { get; set; }
        }

        public class FileMetadata
        {
            public long Size { get; set; }
            public string Checksum { get; set; }
        }
    }

    public static class LCBR_UpdateChecker
    {
        public static ConfigEntry<bool> AutoUpdate = LCB_LCBRMod.LCBR_Settings.Bind("LCBR Settings", "AutoUpdate", true, "Auto update (true/false)");

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
                    LCB_LCBRMod.LogWarning($"New version {latestReleaseTag} is available!");
                    UpdateLog = "LimbusCompanyRuMTL_" + latestReleaseTag;
                    string download = $"https://github.com/kimght/LimbusCompanyRuMTL/releases/download/{latestReleaseTag}/{UpdateLog}.zip";
                    var dirs = download.Split('/');
                    CreateUpdatesDirectory();
                    string filename = LCB_LCBRMod.GamePath + $"/BepInEx/updates/{UpdateLog}.zip";
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
        static void CreateUpdatesDirectory() {
            if (!Directory.Exists(LCB_LCBRMod.GamePath + "/BepInEx/updates"))
                Directory.CreateDirectory(LCB_LCBRMod.GamePath + "/BepInEx/updates");
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
            LCB_LCBRMod.OpenModUpdatesPath();
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
        public static string UpdateLog;
        public static Action UpdateCall;
    }
}
