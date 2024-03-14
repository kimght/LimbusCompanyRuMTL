using HarmonyLib;
using MainUI;
using UnityEngine.EventSystems;
using TMPro;
using static UI.Utility.InfoModels;
using UnityEngine;
using UnityEngine.UI;
using UI;
using MainUI.Gacha;

namespace LimbusLocalizeRUS
{
    public static class LCBR_EventUI
    {
        #region New Manager Banner
        [HarmonyPatch(typeof(BannerSlot<GachaBannerSlot>), nameof(BannerSlot<GachaBannerSlot>.SetData))]
        [HarmonyPostfix]
        private static void GachaBannerSlot_SetData(BannerSlot<GachaBannerSlot> __instance)
        {
            if (__instance._name == "gacha_3_illust")
            {
                __instance._base._bannerImage.sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_NewManagerGacha_Banner"];
            }
        }
        [HarmonyPatch(typeof(GachaUIPanel), nameof(GachaUIPanel.SetGachaInfoPanel))]
        [HarmonyPostfix]
        private static void GachaUIPanel_SetData(GachaUIPanel __instance)
        {
            Sprite safe = __instance.img_displayCharacterCG.sprite;
            if (__instance._lastSettingId == 3)
            {
                __instance.img_displayCharacterCG.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_NewManagerGacha"];
                __instance._currentGachaTitleImage.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_NewManagerGacha_Typo"];
            }
            else
            {
                __instance.img_displayCharacterCG.overrideSprite = safe;
            }
        }
        [HarmonyPatch(typeof(ChanceCounter), nameof(ChanceCounter.SetData))]
        [HarmonyPostfix]
        private static void ChanceCounter_SetData(ChanceCounter __instance)
        {
            __instance.tmp_number_of_times.text = getRaza(__instance.tmp_number.text);
        }
        public static string getRaza(string numStr)
        {
            if (!int.TryParse(numStr, out int num))
            {
                return "HAHAHA";
            }

            int lastDigit = num % 10;
            int secondLastDigit = (num / 10) % 10;

            if (lastDigit == 1 && secondLastDigit != 1)
            {
                return "Раз";
            }
            else if (lastDigit >= 2 && lastDigit <= 4 && secondLastDigit != 1)
            {
                return "Раза";
            }
            else
            {
                return "Раз";
            }
        }
        #endregion

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

        #region 1st Anniversary of LCB
        [HarmonyPatch(typeof(Limbus1stAnniveAttendanceUIPopup), nameof(Limbus1stAnniveAttendanceUIPopup.SetData))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryPeriod(Limbus1stAnniveAttendanceUIPopup __instance)
        {
            __instance.tmp_eventDate.text = "06:00 22.02.2024(ЧТ) - 04:00 21.03.2024(ЧТ) (МСК)";
            __instance.tmp_eventDate.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            __instance.tmp_eventDate.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
        }
        [HarmonyPatch(typeof(Limbus1stAnnivRewardSign), nameof(Limbus1stAnnivRewardSign.SetData))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryTexts(Limbus1stAnnivRewardSign __instance)
        {
            __instance.tmp_rewardName.text = __instance.tmp_rewardName.text.Replace("Анжела Комментатор&\n1st Birthday", "<size=52><cspace=-2px>Получите комментатора\nАнжелу и билет 1-ой Годовщины!</cspace></size>");
            Transform day = __instance.transform.Find("[Image]DailyPanel/[Text]DayText");
            if (day != null)
            {
                string daytext = day.GetComponentInChildren<TextMeshProUGUI>(true).text;
                if (daytext.Contains("Daily"))
                    daytext = "Ежедневно";
                else if (daytext.EndsWith("Days"))
                    daytext = daytext.Replace(" Days", "-й день");
            }
        }
        [HarmonyPatch(typeof(Limbus1stAnnivRewardButton), nameof(Limbus1stAnnivRewardButton.SetData))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryComplete(Limbus1stAnnivRewardButton __instance)
        {
            Transform complete = __instance.transform.Find("[Image]Complete");
            if (complete != null)
            {
                complete.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_1stLCBAnniversary_Complete"];
            }
        }
        [HarmonyPatch(typeof(Limbus1stAnnivEventUIPopup), nameof(Limbus1stAnnivEventUIPopup.InitDateText))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryPopUp(Limbus1stAnnivEventUIPopup __instance)
        {
            Color yellowish = new Color(1.0f, 0.506f, 0, 0.502f);
            Color yellow = new Color(0.97f, 0.76f, 0, 1.0f);
            __instance.tmp_eventTitle.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            __instance.tmp_eventTitle.color = yellow;
            __instance.tmp_eventTitle.fontMaterial.SetColor("_GlowColor", yellowish);
            __instance.tmp_eventTitle.fontMaterial.SetFloat("_GlowInner", 0.2f);
            __instance.tmp_eventTitle.fontMaterial.SetFloat("_GlowOuter", 0.4f);
            __instance.tmp_eventTitle.fontMaterial.SetFloat("_GlowPower", 3);
            __instance.tmp_eventDate.text = "18:00 26.02.2024(ЧТ) - 04:00 21.03.2024(ЧТ) (МСК)";
            __instance.tmp_eventDate.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            __instance.tmp_eventDate.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;

        }
        #endregion

        #region Yield My Flesh
        [HarmonyPatch(typeof(YCGDEventUIPanel), nameof(YCGDEventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void YCGD_MainEvent(YCGDEventUIPanel __instance)
        {
            Transform loading = __instance.transform.Find("[Rect]OpenProduceObj/[Image]EventLogo");
            if (loading != null)
                loading.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_YCGD_Logo"];
            Transform logo = __instance.transform.Find("[Rect]UIObjs/[Image]TitleLogo");
            if (logo != null)
                logo.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_YCGD_Logo"];
            Transform date = __instance.transform.Find("[Rect]UIObjs/[Image]TitleLogo/[Rect]EventPeriod/tmp_period");
            if (date != null)
            {
                date.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 22.02.2024(ЧТ) - 04:00 21.03.2024(ЧТ) (МСК)";
                date.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                date.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
        }
        [HarmonyPatch(typeof(YCGDRewardUIPopup), nameof(YCGDRewardUIPopup.Initialize))]
        [HarmonyPostfix]
        private static void YCGD_RewardUI(YCGDRewardUIPopup __instance)
        {
            __instance.img_logo.sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_YCGD_Logo"];
            __instance.tmp_eventDate.text = "06:00 22.02.2024(ЧТ) - 04:00 28.03.2024(ЧТ) (МСК)";
            __instance.tmp_eventDate.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            __instance.tmp_eventDate.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
        }
        [HarmonyPatch(typeof(YCGDMainEventBanner), nameof(YCGDMainEventBanner.Init))]
        [HarmonyPostfix]
        private static void YCGD_MainBanner(YCGDMainEventBanner __instance)
        {
            __instance._bannerImage.sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_YCGD_EventBanner"];
        }
        [HarmonyPatch(typeof(YCGDSubEventBanner), nameof(YCGDSubEventBanner.Init))]
        [HarmonyPostfix]
        private static void YCGD_SubBanner(YCGDSubEventBanner __instance)
        {
            __instance._bannerImage.sprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_YCGD_ExchangeBanner"];
        }
        #endregion
    }
}
