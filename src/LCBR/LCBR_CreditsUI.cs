using System.Runtime.InteropServices;
using HarmonyLib;
using MainUI;
using UI;
using static UI.Utility.InfoModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StorySystem;
using static StorySystem.CreditsUIManager;

namespace LimbusLocalizeRUS
{
    public static class LCBR_CreditsUI
    {
        [HarmonyPatch(typeof(CreditsUIManager), nameof(CreditsUIManager.SetCredit))]
        [HarmonyPostfix]
        private static void SkipStory_Init(CreditsUIManager __instance)
        {
            Transform skip_story = __instance.transform.Find("[Rect]CreditsAnimation/[Rect]SkipButton/[Button]Skip");
            if (skip_story != null)
            {
                skip_story.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Skip"];
            }
        }
    }
}
