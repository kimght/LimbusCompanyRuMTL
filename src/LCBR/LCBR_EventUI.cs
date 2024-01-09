using HarmonyLib;
using MainUI;
using UnityEngine.EventSystems;
using TMPro;
using static UI.Utility.InfoModels;
using UnityEngine;
using UnityEngine.UI;
using UI;

namespace LimbusLocalizeRUS
{
    public static class LCBR_EventUI
    {
        #region 7th Anniversary
        [HarmonyPatch(typeof(SeventhAnniversaryEventPopup), nameof(SeventhAnniversaryEventPopup.InitEventStataicData))]
        [HarmonyPostfix]
        private static void ProjectMoon7AnnivCG_Init(SeventhAnniversaryEventPopup __instance)
        {
            Transform anniversaryBG = __instance.transform.Find("GameObject/[Image]Bg");
            if (anniversaryBG != null)
            {
                anniversaryBG.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_7thAnniversary"];
            }
        }
        [HarmonyPatch(typeof(SeventhAnniversaryEventPopup), nameof(SeventhAnniversaryEventPopup.InitLocalizeText))]
        [HarmonyPostfix]
        private static void ProjectMoon7AnnivText_Init(SeventhAnniversaryEventPopup __instance)
        {
            Transform anniversaryDate = __instance.transform.Find("GameObject/[Text]EventDate");
            if (anniversaryDate != null)
            {
                anniversaryDate.GetComponentInChildren<TextMeshProUGUI>(true).m_text = "18:00 17.11.2023(ПТ) - 17:59 24.11.2023(ПТ) (МСК)";
                anniversaryDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                anniversaryDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
        }
        #endregion



        #region Miracle in District 20
        [HarmonyPatch(typeof(MiracleEventUIPanel), nameof(MiracleEventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void MiracleEventUI_Init(MiracleEventUIPanel __instance)
        {
            Transform miracleLoading = __instance.transform.Find("[Rect]IntroObjs/[Image]EventTitle");
            Transform miracleLogo = __instance.transform.Find("[Rect]UIObjs/[Image]TitleLogo");
            Transform miracleDate = __instance.transform.Find("[Rect]UIObjs/[Image]TitleLogo/[Rect]EventPeriod/tmp_period");
            Transform miracleStory = __instance.transform.Find("[Rect]UIObjs/[Button]StoryEventUI");
            Transform miracleStoryMO = __instance.transform.Find("[Rect]UIObjs/[Button]StoryEventUI/[Image]StageButtonMouseover");
            Transform miracleStage = __instance.transform.Find("[Rect]UIObjs/[Button]StageEventUI");
            Transform miracleStageMO = __instance.transform.Find("[Rect]UIObjs/[Button]StageEventUI/[Image]StageButtonMouseover");
            if (miracleLoading != null)
                miracleLoading.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Logo"];
            if (miracleLogo != null)
                miracleLogo.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Logo"];
            if (miracleDate != null)
            {
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 28.12.2023(ЧТ) - 04:00 25.01.2024(ЧТ) (МСК)";
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
            if (miracleStory != null)
                miracleStory.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Story"];
            if (miracleStoryMO != null)
                miracleStoryMO.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Story_Mouseover"];
            if (miracleStage != null)
                miracleStage.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Stage"];
            if (miracleStageMO != null)
                miracleStageMO.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Stage_Mouseover"];
        }
        [HarmonyPatch(typeof(MiracleMainEventBanner), nameof(MiracleMainEventBanner.Init))]
        [HarmonyPostfix]
        private static void MiracleMainBanner_Init(MiracleMainEventBanner __instance)
        {
            Transform mainBanner = __instance.transform.Find("[Image]BannerImage (1)");
            if (mainBanner != null)
                mainBanner.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_EventBanner"];
        }
        [HarmonyPatch(typeof(MiracleSubEventBanner), nameof(MiracleSubEventBanner.Init))]
        [HarmonyPostfix]
        private static void MiracleSubBanner_Init(MiracleSubEventBanner __instance)
        {
            Transform subBanner = __instance.transform.Find("[Mask]BannerImage/[Image]BannerImage (1)");
            if (subBanner != null)
                subBanner.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_ExchangeBanner"];
        }
        [HarmonyPatch(typeof(MiracleEventRewardUIPanel), nameof(MiracleEventRewardUIPanel.InitEventStataicData))]
        [HarmonyPostfix]
        private static void MiracleRewardUI_Init(MiracleEventRewardUIPanel __instance)
        {
            Transform miracleBG = __instance.transform.Find("img_background");
            Transform miracleLogo = __instance.transform.Find("EventDescriptionPanel/EventLocalizeLogo");
            if (miracleBG != null)
                miracleBG.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_ExchangeBG"];
            if (miracleLogo != null)
                miracleLogo.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Logo"];
        }
        
        [HarmonyPatch(typeof(MiracleEventRewardUIPanel), nameof(MiracleEventRewardUIPanel.InitDateText))]
        [HarmonyPostfix]
        private static void MiracleRewardDate_Init (MiracleEventRewardUIPanel __instance)
        {
            Transform miracleDate = __instance.transform.Find("EventDescriptionPanel/tmp_eventPeriod");

            if (miracleDate != null)
            {
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 28.12.2023(ЧТ) - 04:00 01.02.2024(ЧТ) (МСК)";
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }

        }
        [HarmonyPatch(typeof(MiracleEventRewardButton), nameof(MiracleEventRewardButton.SetData))]
        [HarmonyPostfix]
        private static void MiracleLock_Init (MiracleEventRewardButton __instance)
        {
            __instance.img_lock.sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Lock"];
            __instance.img_complete.sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Get"];
        }
        #endregion
    }
}
