using HarmonyLib;
using StorySystem.InterEffect;
using StorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MainUI;
using UtilityUI;
using BattleUI;

namespace LimbusLocalizeRUS
{
    internal class LCBR_StoryUI
    {
        [HarmonyPatch(typeof(KillCountUI), nameof(KillCountUI.Init))]
        [HarmonyPostfix]
        private static void KillCount(KillCountUI __instance)
        {
            __instance._testTitleText.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance._testTitleText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);

            __instance._testkillCountNameText.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(4);
            __instance._testkillCountNameText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(17);
        }

        [HarmonyPatch(typeof(StoryInterEffect_Type1), nameof(StoryInterEffect_Type1.Initialize))]
        [HarmonyPostfix]
        private static void StoryInterEffect_Type1_Init(StoryInterEffect_Type1 __instance)
        {
            //FAKE_TITLE
            Transform title = __instance._title.transform;
            Image facethe = title.Find("[Canvas]/[Image]RedLine/[Image]Phrase").transform.GetComponentInChildren<Image>();
            facethe.m_OverrideSprite = LCBR_ReadmeManager.ReadmeSprites["Motto"];
            Image donttouch = title.Find("[Canvas]/[Image]TouchToStart").GetComponentInChildren<Image>();
            donttouch.m_OverrideSprite = LCBR_ReadmeManager.ReadmeStorySprites["Don't_Start"];
            Transform goldenbough = title.Find("[Canvas]/[Text]GoldenBoughSynchronized");
            Transform goldenbough_glow = title.Find("[Canvas]/[Text]GoldenBoughSynchronized/[Text]Glow");
            List<Transform> goldens = new List<Transform> { goldenbough, goldenbough_glow };
            List<TextMeshProUGUI> goldens_text = new List<TextMeshProUGUI> { goldenbough.GetComponentInChildren<TextMeshProUGUI>(), goldenbough_glow.GetComponentInChildren<TextMeshProUGUI>() };
            foreach (TextMeshProUGUI t in goldens_text)
            {
                t.text = "РЕЗОНАНС С ЗОЛОТОЙ ВЕТВЬЮ";
                t.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                t.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);
            }
            goldenbough_glow.GetComponentInChildren<TextMeshProUGUI>(true).alpha = 0.25f;
            LCBR_TemporaryTextures.getBurnTS(goldens);
            //FAKE_LOADING
            Transform loading = __instance._loading.transform;
            TextMeshProUGUI now_l = loading.Find("[Rect]LoadingUI/Text_NowLoading").transform.GetComponentInChildren<TextMeshProUGUI>();
            now_l.text = "ЗАГРУЗКА...";
            now_l.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            now_l.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
            TextMeshProUGUI clearing = loading.Find("[Rect]LoadingUI/[Text]ProgressCategory").transform.GetComponentInChildren<TextMeshProUGUI>();
            clearing.text = "ОЧИЩАЕМ МИРЫ ОТ КЭТИ";
            clearing.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            clearing.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
            TextMeshProUGUI clearing_glow = loading.Find("[Rect]LoadingUI/[Text]ProgressCategory/[Text]ProgressCategoryGlow").transform.GetComponentInChildren<TextMeshProUGUI>();
            clearing_glow.text = "ОЧИЩАЕМ МИРЫ ОТ КЭТИ";
            clearing_glow.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            clearing_glow.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
        }
        [HarmonyPatch(typeof(Util), nameof(Util.GetDlgAfterClearingAllCathy))]
        [HarmonyPrefix]
        private static bool GetDlgAfterClearingAllCathy(string dlgId, string originString, ref string __result)
        {
            if (LCBR_Russian_Settings.IsUseRussian.Value)
            {
                __result = originString;
                UserDataManager instance = Singleton<UserDataManager>.Instance;
                if (instance == null || instance._unlockCodeData == null || !instance._unlockCodeData.CheckUnlockStatus(106))
                    return false;
                if ("battle_defeat_10707_1".Equals(dlgId))
                    __result = __result.Replace("Кэти", "■■■■");
                else if ("battle_dead_10704_1".Equals(dlgId))
                    __result = __result.Replace("Кэтрин", "■■■■■■");
                return false;
            }
            return true;
        }
        [HarmonyPatch(typeof(StoryPlayData), nameof(StoryPlayData.GetDialogAfterClearingAllCathy))]
        [HarmonyPrefix]
        private static bool GetDialogAfterClearingAllCathy(Scenario curStory, Dialog dialog, ref string __result)
        {
            if (LCBR_Russian_Settings.IsUseRussian.Value)
            {
                __result = dialog.Content;
                UserDataManager instance = Singleton<UserDataManager>.Instance;
                if ("P10704".Equals(curStory.ID) && instance != null && instance._unlockCodeData != null && instance._unlockCodeData.CheckUnlockStatus(106) && dialog.Id == 3)
                {
                    __result = __result.Replace("Кэти", "■■■■");
                }
                return false;
            }
            return true;
        }



        [HarmonyPatch(typeof(StoryTheaterUIPopup), nameof(StoryTheaterUIPopup.OpenStoryEnterPopup))]
        [HarmonyPostfix]
        private static void DescriptionChange(StoryTheaterUIPopup __instance)
        {
            __instance._storyEnterPopup._descText.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            __instance._storyEnterPopup._descText.text = __instance._storyEnterPopup._descText.text.Replace("войти в\n", "прочесть историю, ");
            String story = __instance._storyEnterPopup._descText.text;
            if (story.EndsWith("История?"))
            {
                story = story.Replace("  ", " ");
                string[] parts = story.Split(',');
                string faction = parts[1];
                string sinner = parts[2];
                if (faction.StartsWith(" Та"))
                {
                    faction = " Та, кто держит";
                    sinner = " Фауст";
                }
                else if (faction.StartsWith(" Тот"))
                {
                    faction = " Тот, кому суждено держать";
                    sinner = " Синклер";
                }

                __instance._storyEnterPopup._descText.text = $"Желаете ли прочесть историю из жизни {LCBR_TextUI.SinnerStory(sinner)} как{LCBR_Personality_MegaList.Personality_MegaList_Gendered(LCBR_Personality_MegaList.Personality_MegaList(faction), LCBR_TextUI.SinnerStory(sinner))}?";
            }
        }
    }
}
