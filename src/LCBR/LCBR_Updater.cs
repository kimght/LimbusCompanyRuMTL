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
using System.Text.Json.Serialization;
using UnityEngine;
using UnityEngine.Networking;

namespace LimbusLocalizeRUS
{
    public static class LCBR_Updater
    {
        private static readonly string CyrillicFontRepo = "kimght/LimbusCyrillicFont";
        private static readonly string CyrillicFontName = "tmpcyrillicfonts";
        private static readonly string CyrillicFontPath = LCB_LCBRMod.ModPath + "/" + CyrillicFontName;

        private static readonly string LocalizationRepo = "kimght/LimbusLocalizeRU";
        private static readonly string LocalizationBranchName = "release";
        private static readonly string LocalizationDirectory = LCB_LCBRMod.ModPath + "/Localize";

        private static readonly string ModBinaryRepo = "kimght/LimbusCompanyRuMTL";
        private static readonly string ModBinaryName = "LimbusCompanyBusRUS_BIE.dll";
        private static readonly string ModBinaryPath = LCB_LCBRMod.ModPath + "/" + ModBinaryName;

        public static ConfigEntry<bool> AutomaticUpdatesCfg = LCB_LCBRMod.LCBR_Settings.Bind("LCBR Settings", "AutomaticUpdates", true, "Auto update mod (true/false)");

        public static void UpdateModSync()
        {
            if (AutomaticUpdatesCfg.Value) {
                UpdateLocalizationFiles();
                UpdateFontFiles();
                UpdateModBinary();
            }
        }

        private static void UpdateLocalizationFiles()
        {
            LCB_LCBRMod.LogWarning("Checking for localization updates...");
            string latestCommitSha = GetLatestCommitSha(LocalizationRepo, LocalizationBranchName);
            if (latestCommitSha == null)
            {
                LCB_LCBRMod.LogWarning("Failed to get the latest commit SHA.");
                return;
            }

            LCB_LCBRMod.LogInfo($"Loading metadata from {latestCommitSha}...");

            var metadata = GetLocalizationMetadata(latestCommitSha);
            if (metadata == null || metadata.Files == null)
            {
                LCB_LCBRMod.LogWarning("Failed to get localization metadata.");
                return;
            }

            LCB_LCBRMod.LogInfo($"Found {metadata.Files.Count} files in metadata.");

            int processed = 0;
            foreach (var file in metadata.Files)
            {
                processed += 1;

                string localFilePath = Path.Combine(LocalizationDirectory, file.Path);
                var localFileMetadata = GetLocalFileMetadata(localFilePath);

                if (localFileMetadata == null || localFileMetadata.Checksum != file.Checksum)
                {
                    LCB_LCBRMod.LogInfo($"[{processed}/{metadata.Files.Count}] Updating file: {file.Path}");
                    string fileUrl = $"https://raw.githubusercontent.com/{LocalizationRepo}/{latestCommitSha}/{file.Path}";
                    DownloadFile(fileUrl, localFilePath);
                }
            }

            LCB_LCBRMod.LogWarning("Localization is up-to-date.");
        }

        private static void UpdateFontFiles()
        {
            LCB_LCBRMod.LogWarning("Checking for font updates...");
            var latestRelease = GetLatestRelease(CyrillicFontRepo);
            if (latestRelease == null)
            {
                LCB_LCBRMod.LogWarning("Failed to get the latest font release.");
                return;
            }

            string releaseTag = latestRelease["tag_name"]?.ToString();
            if (string.IsNullOrEmpty(releaseTag))
            {
                LCB_LCBRMod.LogWarning("Failed to get the release tag.");
                return;
            }

            string checksumUrl = latestRelease["assets"]?.FirstOrDefault(a => a["name"]?.ToString() == "checksum.txt")?["browser_download_url"]?.ToString();
            if (string.IsNullOrEmpty(checksumUrl))
            {
                LCB_LCBRMod.LogWarning("Failed to get the checksum URL.");
                return;
            }

            var checksumRequest = GetUrl(checksumUrl);
            if (checksumRequest.result != UnityWebRequest.Result.Success)
            {
                LCB_LCBRMod.LogWarning($"Failed to download checksum: {checksumRequest.error}");
                return;
            }

            string latestChecksum = checksumRequest.downloadHandler.text.Trim();

            string localChecksum = GetFileChecksum(CyrillicFontPath);
            if (localChecksum == latestChecksum)
            {
                LCB_LCBRMod.LogInfo("Font is already up-to-date.");
                return;
            }

            string fontUrl = latestRelease["assets"]?.FirstOrDefault(a => a["name"]?.ToString().StartsWith("tmpcyrillicfonts_"))?["browser_download_url"]?.ToString();
            if (string.IsNullOrEmpty(fontUrl))
            {
                LCB_LCBRMod.LogWarning("Failed to get the font download URL.");
                return;
            }

            string tempZipPath = Path.Combine(Path.GetTempPath(), $"tmpcyrillicfonts_{releaseTag}.zip");
            DownloadFile(fontUrl, tempZipPath);

            ExtractAndReplaceFont(tempZipPath, CyrillicFontPath);
            LCB_LCBRMod.LogWarning("Font updated successfully.");
        }

        private static void UpdateModBinary()
        {
            LCB_LCBRMod.LogWarning("Checking for mod updates...");

            var latestRelease = GetLatestRelease(ModBinaryRepo);
            if (latestRelease == null)
            {
                LCB_LCBRMod.LogWarning("Failed to get the latest mod release.");
                return;
            }

            string latestReleaseTag = latestRelease["tag_name"]?.ToString();
            if (string.IsNullOrEmpty(latestReleaseTag))
            {
                LCB_LCBRMod.LogWarning("Failed to get the release tag.");
                return;
            }

            Version latestVersion = Version.Parse(latestReleaseTag.TrimStart('v'));
            Version currentVersion = Version.Parse(LCB_LCBRMod.VERSION);

            if (currentVersion >= latestVersion)
            {
                LCB_LCBRMod.LogInfo("Mod is already up-to-date.");
                return;
            }

            string modUrl = latestRelease["assets"]?.FirstOrDefault(a => a["name"]?.ToString().StartsWith("LimbusCompanyRuMTL_"))?["browser_download_url"]?.ToString();
            if (string.IsNullOrEmpty(modUrl))
            {
                LCB_LCBRMod.LogWarning("Failed to get the mod download URL.");
                return;
            }

            string tempZipPath = Path.Combine(Path.GetTempPath(), $"LimbusCompanyRuMTL_{latestReleaseTag}.zip");
            DownloadFile(modUrl, tempZipPath);

            ExtractAndReplaceModBinary(tempZipPath, ModBinaryPath);

            LCB_LCBRMod.LogWarning("Mod updated successfully.");
        }

        private static JSONNode GetLatestRelease(string repo)
        {
            var request = GetUrl($"https://api.github.com/repos/{repo}/releases/latest");

            if (request.result != UnityWebRequest.Result.Success)
            {
                LCB_LCBRMod.LogWarning($"Failed to fetch latest release: {request.error}");
                return null;
            }

            var content = request.downloadHandler.text;
            return JSON.Parse(content);
        }

        private static void ExtractAndReplaceFont(string zipFilePath, string destinationPath)
        {
            string extractPath = Path.Combine(Path.GetTempPath(), "lcbr_tmpcyrillicfonts_extracted");
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, extractPath);

            string extractedFontPath = Path.Combine(extractPath, CyrillicFontName);
            if (File.Exists(extractedFontPath))
            {
                File.Copy(extractedFontPath, destinationPath, true);
            }

            Directory.Delete(extractPath, true);
            File.Delete(zipFilePath);
        }

        private static void ExtractAndReplaceModBinary(string zipFilePath, string destinationPath)
        {
            string extractPath = Path.Combine(Path.GetTempPath(), "lcbr_modbinary_extracted");
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, extractPath);

            string extractedModBinaryPath = Path.Combine(extractPath, ModBinaryName);
            if (File.Exists(extractedModBinaryPath))
            {
                File.Copy(extractedModBinaryPath, destinationPath, true);
            }

            Directory.Delete(extractPath, true);
            File.Delete(zipFilePath);
        }

        private static UnityWebRequest GetUrl(string url) {
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader("User-Agent", "ModUpdater");
            request.SendWebRequest();

            while (!request.isDone)
            {
                Thread.Sleep(50);
            }

            return request;
        }

        private static string GetLatestCommitSha(string repo, string branch)
        {
            var request = GetUrl($"https://api.github.com/repos/{repo}/commits/{branch}");

            if (request.result != UnityWebRequest.Result.Success)
            {
                LCB_LCBRMod.LogWarning($"Failed to fetch latest commit SHA: {request.error}");
                return null;
            }

            var content = request.downloadHandler.text;
            var commitInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

            return commitInfo["sha"]?.ToString();
        }

        private static Metadata GetLocalizationMetadata(string commitSha)
        {
            string url = $"https://raw.githubusercontent.com/{LocalizationRepo}/{commitSha}/metadata.json";
            LCB_LCBRMod.LogInfo($"Downloading metadata from {url}");
            
            var request = GetUrl(url);

            if (request.result != UnityWebRequest.Result.Success)
            {
                LCB_LCBRMod.LogWarning($"Failed to fetch metadata: {request.error}");
                return null;
            }

            var content = request.downloadHandler.text;
            return JsonSerializer.Deserialize<Metadata>(content);
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

        private static void DownloadFile(string url, string destPath)
        {
            var request = GetUrl(url);

            if (request.result != UnityWebRequest.Result.Success)
            {
                LCB_LCBRMod.LogWarning($"Failed to download file: {request.error}");
                return;
            }

            var response = request.downloadHandler.data;

            string directoryPath = Path.GetDirectoryName(destPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(destPath, response);
        }

        public class Metadata
        {
            [JsonPropertyName("version")]
            public string Version { get; set; }
            [JsonPropertyName("files")]
            public List<FileData> Files { get; set; }
        }

        public class FileData
        {
            [JsonPropertyName("path")]
            public string Path { get; set; }
            [JsonPropertyName("checksum")]
            public string Checksum { get; set; }
        }

        public class FileMetadata
        {
            public long Size { get; set; }
            public string Checksum { get; set; }
        }
    }
}
