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
        [HarmonyPatch(typeof(MiracleEventMultipleBanner), nameof(MiracleEventMultipleBanner.Init))]
        [HarmonyPostfix]
        private static void MiracleBanners_Init(MiracleEventMultipleBanner __instance)
        {
            Transform mainBanner = __instance.transform.Find("[Button]MainBanner");
            if (mainBanner != null)
                mainBanner.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_EventBanner"];
            Transform subBanner = __instance.transform.Find("[Button]RewardBanner");
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
        private static void MiracleLock_Init(MiracleEventRewardButton __instance)
        {
            __instance.img_lock.sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Lock"];
            __instance.img_complete.sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Miracle20_Get"];
        }
        #endregion

        #region Valpurgis Nacht 2
        [HarmonyPatch(typeof(DawnOfGreenEventRewardBanner), nameof(DawnOfGreenEventRewardBanner.Init))]
        [HarmonyPostfix]
        private static void RewardBanner_Init (DawnOfGreenEventRewardBanner __instance)
        {
            GameObject secondValpurgisMission = GameObject.Find("[Canvas]RatioMainUI/[Rect]PresenterRoot/[UIPresenter]StageUIPresenter(Clone)/[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Script]StageLeftBanners/[Script]StageEventBanner_Multiple_DawnOfGreen(Clone)/[Button]SecondBanner");
            if (secondValpurgisMission != null)
                secondValpurgisMission.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_VN2_Mission_Banner"];
        }
        [HarmonyPatch(typeof(DawnOfGreenEventUIPanel), nameof(DawnOfGreenEventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void DawnOfGreen_Init(DawnOfGreenEventUIPanel __instance)
        {
            GameObject secondValpurgisDate = GameObject.Find("[Canvas]RatioMainUI/[Rect]PanelRoot/[UIPanel]DawnOfGreen_MainEvent(Clone)/[Text]Date");
            if (secondValpurgisDate != null)
            {
                secondValpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 11.01.2024(ЧТ) - 04:00 25.01.2024(ЧТ) (МСК)";
                secondValpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[0];
                secondValpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[0].material;
            }
        }
        [HarmonyPatch(typeof(DawnOfGreenEventRewardUIPanel), nameof(DawnOfGreenEventRewardUIPanel.SetData))]
        [HarmonyPostfix]
        private static void RewardBackground_Init(DawnOfGreenEventRewardUIPanel __instance)
        {
            GameObject secondValpurgisBG = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]DawnOfGreen_Reward(Clone)/[Image]Background");
            if (secondValpurgisBG != null)
                secondValpurgisBG.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_VN2_BG"];
            GameObject secondValpurgisDesc = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]DawnOfGreen_Reward(Clone)/EventDescriptionPanel");
            if (secondValpurgisDesc != null)
                secondValpurgisDesc.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_VN2_Desc"];
            GameObject secondValpurgisMissionDate = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]DawnOfGreen_Reward(Clone)/EventDescriptionPanel/[Text]EventPeriod");
            if (secondValpurgisMissionDate != null)
            {
                secondValpurgisMissionDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 11.01.2024(ЧТ) - 04:00 1.02.2024(ЧТ) (МСК)";
                secondValpurgisMissionDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                secondValpurgisMissionDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
        }
        [HarmonyPatch(typeof(DawnOfGreenEventRewardButton), nameof(DawnOfGreenEventRewardButton.SetData))]
        [HarmonyPostfix]
        private static void RewardClear_Init(DawnOfGreenEventRewardButton __instance)
        {
            __instance._completeImage.sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_VN2_Clear"];
        }
        #endregion
    }
}
