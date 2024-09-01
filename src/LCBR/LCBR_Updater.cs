using System;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using BepInEx.Configuration;
using Il2CppSystem.Threading;

namespace LimbusLocalizeRUS
{
    public static class LCBR_Updater
    {
        private const string CyrillicFontRepo = "kimght/LimbusCyrillicFont";
        private const string CyrillicFontName = "tmpcyrillicfonts";
        private static readonly string CyrillicFontPath = LCB_LCBRMod.ModPath + "/" + CyrillicFontName;

        private const string LocalizationRepo = "kimght/LimbusLocalizeRU";
        private const string LocalizationBranchName = "release";
        private static readonly string LocalizationDirectory = LCB_LCBRMod.ModPath + "/Localize";

        private const string ModBinaryRepo = "kimght/LimbusCompanyRuMTL";
        private const string ModBinaryName = "LimbusCompanyBusRUS_BIE";

        private static ConfigEntry<bool> _automaticUpdatesCfg = LCB_LCBRMod.LCBR_Settings.Bind("LCBR Settings", "AutomaticUpdates", true, "Auto update mod (true/false)");

        public static void UpdateModSync()
        {
            DisableOldVersions();

            if (!_automaticUpdatesCfg.Value)
            {
                return;
            }
            
            try {
                UpdateLocalizationFiles();
            } catch (Exception e) {
                LCB_LCBRMod.LogWarning($"Failed to update localization: {e.Message}");
            }
            
            try {
                UpdateFontFiles();
            } catch (Exception e) {
                LCB_LCBRMod.LogWarning($"Failed to update font: {e.Message}");
            }
            
            try {
                UpdateModBinary();
            } catch (Exception e) {
                LCB_LCBRMod.LogWarning($"Failed to update mod: {e.Message}");
            }
        }

        private static void DisableOldVersions()
        {
            var modDirectory = new DirectoryInfo(LCB_LCBRMod.ModPath);
            var backupDirectory = new DirectoryInfo(LCB_LCBRMod.ModPath + "/backups");
            var runningMod = new FileInfo(LCB_LCBRMod.RunningAssemblyPath);

            backupDirectory.Create();

            foreach (var fileInfo in modDirectory.GetFiles()
                        .Where(file => file.Name.StartsWith(ModBinaryName) && file.Extension == ".dll"))
            {
                if (fileInfo.Name == runningMod.Name) {
                    continue;
                }

                fileInfo.MoveTo(Path.Combine(backupDirectory.FullName, fileInfo.Name + ".bak"));
            }
        }

        private static void UpdateLocalizationFiles()
        {
            LCB_LCBRMod.LogWarning("Checking for localization updates...");
            var latestCommitSha = GetLatestCommitSha(LocalizationRepo, LocalizationBranchName);
            if (latestCommitSha == null)
            {
                LCB_LCBRMod.LogWarning("Failed to get the latest commit SHA.");
                return;
            }

            LCB_LCBRMod.LogInfo($"Loading metadata from {latestCommitSha}...");

            var metadata = GetLocalizationMetadata(latestCommitSha);
            if (metadata?.Files == null)
            {
                LCB_LCBRMod.LogWarning("Failed to get localization metadata.");
                return;
            }

            LCB_LCBRMod.LogInfo($"Found {metadata.Files.Count} files in metadata.");

            var processed = 0;
            foreach (var file in metadata.Files)
            {
                processed += 1;

                var localFilePath = Path.Combine(LocalizationDirectory, file.Path);
                var localFileMetadata = GetLocalFileMetadata(localFilePath);

                if (localFileMetadata?.Checksum == file.Checksum)
                {
                    continue;
                }
                
                LCB_LCBRMod.LogInfo($"[{processed}/{metadata.Files.Count}] Updating file: {file.Path}");
                var fileUrl = $"https://raw.githubusercontent.com/{LocalizationRepo}/{latestCommitSha}/{file.Path}";
                DownloadFile(fileUrl, localFilePath);
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

            var releaseTag = latestRelease.TagName;
            if (string.IsNullOrEmpty(releaseTag))
            {
                LCB_LCBRMod.LogWarning("Failed to get the release tag.");
                return;
            }

            var checksumUrl = latestRelease.Assets
                .FirstOrDefault(a => a.Name == "checksum.txt")?.DownloadUrl;
                

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

            var latestChecksum = checksumRequest.downloadHandler.text.Trim();

            var localChecksum = GetFileChecksum(CyrillicFontPath);
            if (localChecksum != null && localChecksum == latestChecksum)
            {
                LCB_LCBRMod.LogInfo("Font is already up-to-date.");
                return;
            }

            var fontUrl = latestRelease.Assets
                .FirstOrDefault(a => a.Name.StartsWith("tmpcyrillicfonts_"))?.DownloadUrl;
            
            if (string.IsNullOrEmpty(fontUrl))
            {
                LCB_LCBRMod.LogWarning("Failed to get the font download URL.");
                return;
            }

            LCB_LCBRMod.LogInfo("Downloading font update... This could take several minutes.");
            var tempZipPath = Path.Combine(Path.GetTempPath(), $"tmpcyrillicfonts_{releaseTag}.zip");
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

            var latestReleaseTag = latestRelease.TagName;
            if (string.IsNullOrEmpty(latestReleaseTag))
            {
                LCB_LCBRMod.LogWarning("Failed to get the release tag.");
                return;
            }

            var latestVersion = Version.Parse(latestReleaseTag.TrimStart('v'));
            var currentVersion = Version.Parse(LCB_LCBRMod.VERSION);

            if (currentVersion >= latestVersion)
            {
                LCB_LCBRMod.LogInfo("Mod is already up-to-date.");
                return;
            }

            var modUrl = latestRelease.Assets
                .FirstOrDefault(a => a.Name.StartsWith("LimbusCompanyRuMTL_"))?.DownloadUrl;

            if (string.IsNullOrEmpty(modUrl))
            {
                LCB_LCBRMod.LogWarning("Failed to get the mod download URL.");
                return;
            }

            LCB_LCBRMod.LogInfo("Downloading mod update... This could take several minutes.");
            var tempZipPath = Path.Combine(Path.GetTempPath(), $"LimbusCompanyRuMTL_{latestReleaseTag}.zip");
            DownloadFile(modUrl, tempZipPath);

            ExtractModBinary(tempZipPath, Path.Combine(LCB_LCBRMod.ModPath, $"{ModBinaryName}_{latestReleaseTag}.dll"));

            LCB_LCBRMod.LogWarning("Mod updated successfully.");
        }

        private static GithubRelease GetLatestRelease(string repo)
        {
            var request = GetUrl($"https://api.github.com/repos/{repo}/releases/latest");

            if (request.result != UnityWebRequest.Result.Success)
            {
                LCB_LCBRMod.LogWarning($"Failed to fetch latest release: {request.error}");
                return null;
            }

            var content = request.downloadHandler.text;
            return JsonSerializer.Deserialize<GithubRelease>(content);
        }

        private static void ExtractAndReplaceFont(string zipFilePath, string destinationPath)
        {
            var extractPath = Path.Combine(Path.GetTempPath(), "lcbr_tmpcyrillicfonts_extracted");
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, extractPath);

            var extractedFontPath = Path.Combine(extractPath, CyrillicFontName);
            if (File.Exists(extractedFontPath))
            {
                File.Copy(extractedFontPath, destinationPath, true);
            }

            Directory.Delete(extractPath, true);
            File.Delete(zipFilePath);
        }

        private static void ExtractModBinary(string zipFilePath, string destinationPath)
        {
            var extractPath = Path.Combine(Path.GetTempPath(), "lcbr_modbinary_extracted");
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, extractPath);

            var modDirectory = new DirectoryInfo(Path.Combine(extractPath, LCB_LCBRMod.NAME));
            var extractedModBinary = modDirectory.EnumerateFiles()
                .FirstOrDefault(file => file.Name.StartsWith(ModBinaryName) &&
                                        file.Extension == ".dll");

            extractedModBinary?.CopyTo(destinationPath, true);
            Directory.Delete(extractPath, true);
            File.Delete(zipFilePath);
        }

        private static UnityWebRequest GetUrl(string url) {
            var request = UnityWebRequest.Get(url);
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
            var url = $"https://raw.githubusercontent.com/{LocalizationRepo}/{commitSha}/metadata.json";
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
            var localChecksum = GetFileChecksum(filePath);

            if (localChecksum == null) {
                return null;
            }

            return new FileMetadata
            {
                Size = new FileInfo(filePath).Length,
                Checksum = localChecksum,
            };
        }

        private static string GetFileChecksum(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }


            using var stream = File.OpenRead(filePath);
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
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

            var directoryPath = Path.GetDirectoryName(destPath);
            if (directoryPath == null)
            {
                LCB_LCBRMod.LogWarning($"Failed to get directory path: {destPath}.");
                return;
            }
            
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
            
            public class FileData
            {
                [JsonPropertyName("path")]
                public string Path { get; set; }
                [JsonPropertyName("checksum")]
                public string Checksum { get; set; }
            }
        }

        
        public class FileMetadata
        {
            public long Size { get; set; }
            public string Checksum { get; set; }
        }

        public class GithubRelease
        {
            [JsonPropertyName("tag_name")]
            public string TagName { get; set; }
            [JsonPropertyName("assets")]
            public List<GithubReleaseAsset> Assets { get; set; }
        }

        public class GithubReleaseAsset
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("browser_download_url")]
            public string DownloadUrl { get; set; }
        }
    }
}
