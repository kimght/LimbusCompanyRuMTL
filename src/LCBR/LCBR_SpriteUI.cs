using HarmonyLib;
using BattleUI;
using BattleUI.Typo;
using MainUI;
using MainUI.VendingMachine;
using UnityEngine;
using UnityEngine.UI;
using StorySystem;
using BattleUI.BattleUnit;
using MainUI.Gacha;
using Dungeon.Shop;
using Dungeon.UI.Event;
using ChoiceEvent;
using BattleUI.Information;

namespace LimbusLocalizeRUS
{
    public static class LCBR_SpriteUI
    {
        #region Combo
        [HarmonyPatch(typeof(ParryingTypoUI), nameof(ParryingTypoUI.SetParryingTypoData))]
        [HarmonyPrefix]
        private static void ParryingTypoUI_Init(ParryingTypoUI __instance)
        {
            GameObject combo = GameObject.Find("[Prefab]ParryingTypo(Clone)/[Rect]Pivot/[Fixed,Image]ParryingText");
            if (combo != null)
            {
                combo.GetComponent<Image>().sprite = LCBR_ReadmeManager.ReadmeSprites["Combo"];
            }
            __instance.img_parryingTypo.sprite = LCBR_ReadmeManager.ReadmeSprites["Combo"];
        }
        #endregion

        #region Login
        [HarmonyPatch(typeof(LoginSceneManager), nameof(LoginSceneManager.OnInitLoginInfoManagerEnd))]
        [HarmonyPostfix]
        private static void LoginSceneManager_Init(LoginSceneManager __instance)
        {
            Transform catchphrase = __instance.transform.Find("[Canvas]/[Image]Catchphrase");
            if (catchphrase.GetComponentInChildren<Image>(true).sprite.name == "season_catchphrase")
            {
                catchphrase.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["Catchphrase"];
            }
            __instance.img_touchToStart.sprite = LCBR_ReadmeManager.ReadmeSprites["Start"];
            Transform motto = __instance.transform.Find("[Canvas]/[Image]RedLine/[Image]Phrase");
            if (motto != null)
            {
                motto.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["Motto"];
            }
        }
        #endregion

        #region Main Menu
        [HarmonyPatch(typeof(LowerControlUIPanel), nameof(LowerControlUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void LunacyTag_Init(LowerControlUIPanel __instance)
        {
            Transform lunacyTag = __instance.transform.Find("[Rect]Pivot/[Rect]UserInfoUI/[Rect]Info/[Button]CurrencyInfo/[Image]CashTag");
            if (lunacyTag != null)
            {
                lunacyTag.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LunacyTag"];
            }
        }
        [HarmonyPatch(typeof(MirrorDungeonBanner), nameof(MirrorDungeonBanner.Initialize))]
        [HarmonyPostfix]
        private static void MirrorDungeon_Banner_Init(MirrorDungeonBanner __instance)
        {
            Transform banner = __instance._hsv.transform.Find("[Rect]Items/[Image]ImageBackground/[Image]Image");
            if (banner.GetComponentInChildren<Image>(true).sprite.name.StartsWith("MirrorDungeon4"))
            {
                banner.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["MirrorDungeon_Banner"];
            }
        }
        #endregion

        #region Friends
        [HarmonyPatch(typeof(UserInfoTicketItem), nameof(UserInfoTicketItem.SetData))]
        [HarmonyPostfix]
        private static void TicketInfoPopup_EffectSprite(UserInfoTicketItem __instance)
        {
            __instance._useEffectTagImage.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["UserInfo_Effect"];
            __instance._subTicketUseEffectTagImage.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["UserInfo_Effect"];
        }
        #endregion

        #region Vending Machine
        [HarmonyPatch(typeof(VendingMachineUIPanel), nameof(VendingMachineUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void VendingMachineUIPanel_Init(VendingMachineUIPanel __instance)
        {
            Transform soldOut = __instance.transform.Find("GoodsStoreAreaMaster/GoodsStorePanelGroup/BackPanel/Main/SoldOut");
            if (soldOut != null)
            {
                soldOut.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["SoldOut"];
            }
        }
        #endregion

        #region Formation UI
        [HarmonyPatch(typeof(FormationPersonalityUI), nameof(FormationPersonalityUI.Initialize))]
        [HarmonyPostfix]
        private static void FormationPersonalityUI_Init(FormationPersonalityUI __instance)
        {
           // __instance.img_isParticipaged.sprite = LCBR_ReadmeManager.ReadmeSprites["InParty"];
            __instance.img_support.sprite = LCBR_ReadmeManager.ReadmeSprites["SupportTag"];
            __instance._redDot.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["New"];
        }
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIScrollViewItem), nameof(FormationSwitchablePersonalityUIScrollViewItem.Initialize))]
        [HarmonyPostfix]
        private static void FormationSwitchablePersonalityUIScrollViewItem_Init(FormationSwitchablePersonalityUIScrollViewItem __instance)
        {
            Transform img_isParticipaged = __instance._participatedObject.transform.parent.parent.parent.Find("[Image]ParticipateSlotUI");
            img_isParticipaged.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["InParty"];
            __instance._newAcquiredRedDot.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["New"];
        }
        [HarmonyPatch(typeof(FormationSwitchableSupporterPersonalityUIScrollViewItem), nameof(FormationSwitchableSupporterPersonalityUIScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void YobenBoben(FormationSwitchableSupporterPersonalityUIScrollViewItem __instance)
        {
            __instance._selectedFrame.sprite = LCBR_ReadmeManager.ReadmeSprites["InParty"];
        }
        [HarmonyPatch(typeof(FormationPersonalityUISettings_Label), nameof(FormationPersonalityUISettings_Label.Convert))]
        [HarmonyPostfix]
        private static void FormationPersonalityUISettings_Label_Init(FormationPersonalityUISettings_Label __instance)
        {
            __instance._participatedLabelSprite = LCBR_ReadmeManager.ReadmeSprites["InParty"];
            __instance._batonSprite = LCBR_ReadmeManager.ReadmeEventSprites["Backup_Label"];
        }
        [HarmonyPatch(typeof(FormationEgoSlot), nameof(FormationEgoSlot.SetData))]
        [HarmonyPostfix]
        private static void FormationEgoSlot_Init(FormationEgoSlot __instance)
        {
            __instance._redDot.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["New"];
        }
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIPanel), nameof(FormationSwitchablePersonalityUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void FormationSwitchablePersonalityUIPanel_Init(FormationSwitchablePersonalityUIPanel __instance)
        {
            Transform newPersonality = __instance.transform.Find("[Script]RightPanel/[Script]FormationEgoList/[Script]RedDot");
            __instance._egoRedDot.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["New"];
            __instance._personalityRedDot.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["New"];
            if (newPersonality != null)
                newPersonality.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["New"];
        }
        [HarmonyPatch(typeof(FormationSwitchableEgoUIScrollViewItem), nameof(FormationSwitchableEgoUIScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void RedDotAgain_Init(FormationSwitchableEgoUIScrollViewItem __instance)
        {
            __instance._newAcquiredRedDot.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["New"];
        }
        #endregion

        #region Support Formation UI
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIPanel), nameof(FormationSwitchablePersonalityUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void Support_Init (FormationSwitchablePersonalityUIPanel __instance)
        {
            // SUPPORT TAG
            Transform support_tag = __instance.transform.Find("[Script]RightPanel/[Script]FormationEgoList/[Image]SupportTag");
            if (support_tag != null)
                support_tag.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["SupportTag"];
        }
        #endregion

        #region Mirror Dungeon
        [HarmonyPatch(typeof(EgoGiftCategorySetUIToggleButton), nameof(EgoGiftCategorySetUIToggleButton.SetData))]
        [HarmonyPostfix]
        private static void EgoGiftCategorySetUIToggleButton_Init(EgoGiftCategorySetUIToggleButton __instance)
        {
            __instance.img_bonus.sprite = LCBR_ReadmeManager.ReadmeSprites["StarlightBonus"];
        }
        [HarmonyPatch(typeof(AbnormalityStatUI), nameof(AbnormalityStatUI.SetAbnormalityGuideUIActive))]
        [HarmonyPostfix]
        private static void BottomStat_Init(AbnormalityStatUI __instance)
        {
            Transform new_info = __instance.transform.Find("[Rect]FixedScalePivot/[Text]UnitName/[Image]Icon");
            if (new_info != null)
                new_info.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["NewInfo"];
        }
        [HarmonyPatch(typeof(MirrorDungeonShopItemSlot), nameof(MirrorDungeonShopItemSlot.SetData))]
        [HarmonyPostfix]
        private static void MirrorDungeonShopItemSlot_Init(MirrorDungeonShopItemSlot __instance)
        {
            __instance._soldOutTitleObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = LCBR_ReadmeManager.ReadmeSprites["Mirror_SoldOut"];
        }
        [HarmonyPatch(typeof(ChoiceEventController), nameof(ChoiceEventController.InitCallbacks))]
        [HarmonyPostfix]
        private static void ChoiceEventSkip(ChoiceEventController __instance)
        {
            __instance._backgroundUI.img_battleBG.transform.Find("[Image]ScenarioPanel/[Script]ScenarioField/[Rect]StorySkipButton/[Button]StorySkipButton/[Image]SkipIcon (1)").GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["Skip_Choice"];
            __instance._choiceSectionUI.btn_skip.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["Skip_Choice"];
        }
        [HarmonyPatch(typeof(MirrorDungeonShopUIController), nameof(MirrorDungeonShopUIController.Initialize))]
        [HarmonyPostfix]
        private static void ShopSkipButton(MirrorDungeonShopUIController __instance)
        {
            __instance._backgroundUI.img_battleBG.transform.Find("[Image]ScenarioPanel/[Script]ScenarioField/[Rect]StorySkipButton/[Button]StorySkipButton/[Image]SkipIcon").GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["Skip_Choice"];
        }
        [HarmonyPatch(typeof(UIButtonWithOverlayImg), nameof(UIButtonWithOverlayImg.SetDefault))]
        [HarmonyPostfix]
        private static void ChoiceEvent_EgoGiftButton(UIButtonWithOverlayImg __instance)
        {
            if (__instance.gameObject.name == "[Button]ViewEgoGift")
                __instance.gameObject.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["EgoGift_MirrorButton"];
        }
        [HarmonyPatch(typeof(MirrorDungeonFloorRewardItem), nameof(MirrorDungeonFloorRewardItem.SetFloorStatus))]
        [HarmonyPostfix]
        private static void DungeonClearLogo(MirrorDungeonFloorRewardItem __instance)
        {
            __instance.img_clearLogo.sprite = LCBR_ReadmeManager.ReadmeSprites["DungeonClearLogo"];
        }
        [HarmonyPatch(typeof(MirrorDungeonIconManagerInBattleScene), nameof(MirrorDungeonIconManagerInBattleScene.SetActiveMirrorDungeonEgoGiftButton))]
        [HarmonyPostfix]
        private static void MirrorGiftBox(MirrorDungeonIconManagerInBattleScene __instance)
        {
            __instance.btn_egoGiftPopup.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["EgoGift_MirrorButton"];
        }
        [HarmonyPatch(typeof(MirrorDungeonFinishedPanel), nameof(MirrorDungeonFinishedPanel.Initialize))]
        [HarmonyPostfix]
        private static void MirrorClear(MirrorDungeonFinishedPanel __instance)
        {
            __instance. _progressPanel._iconList.transform.Find("[Image]Logo").GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["DungeonClearLogo"];
        }
        [HarmonyPatch(typeof(MirrorDungeonRewardPopup_Season4), nameof(MirrorDungeonRewardPopup_Season4.SetDataOpenEvent))]
        [HarmonyPostfix]
        private static void LastClearPlease(MirrorDungeonRewardPopup_Season4 __instance)
        {
            __instance._progressUI.img_clearLogo.sprite = LCBR_ReadmeManager.ReadmeSprites["DungeonClearLogo"];
        }
        [HarmonyPatch(typeof(MirrorDungeonSelectThemeUIPanel), nameof(MirrorDungeonSelectThemeUIPanel.Reload))]
        [HarmonyPostfix]
        private static void ThemeUIPanel_ClearBonus(MirrorDungeonSelectThemeUIPanel __instance)
        {
            Image clear_bonus = __instance._clearBonus.GetComponentInChildren<Image>(true);
            if (clear_bonus.sprite.name.Contains("MirrorDungeon4"))
            {
                clear_bonus.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["MirrorDungeon_FloorBonus"];
            }
        }
        #endregion

        #region Battle UI
        [HarmonyPatch(typeof(BattleUIRoot), nameof(BattleUIRoot.Init))]
        [HarmonyPostfix]
        private static void BattleUI_Init(BattleUIRoot __instance)
        {
            Transform waveUI = __instance.transform.Find("[Canvas]PerspectiveUI/SafeArea/[Script]WaveUI/[Rect]Pivot/[Image]WavePanel");
            Transform turnUI = __instance.transform.Find("[Canvas]PerspectiveUI/SafeArea/[Script]WaveUI/[Rect]Pivot/[Image]TurnPanel");
            Transform start = __instance.transform.Find("[Canvas,Script]BattleUIController/SafeArea/[Script]NewOperationController/[Rect]ActiveControl/[Rect]Pivot/[Rect]ActionableSlotList/[Layout]SinActionSlotsGrid/[EventTrigger]EndButton/[Image]RightLeg/[Rect]StartUI/[Rect]Pivot/[Image]Start");
            if (waveUI != null)
            {
                waveUI.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["WaveUI"];
                waveUI.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["WaveUI"];
                waveUI.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["WaveUI"];
            }
            if (turnUI != null)
            {
                turnUI.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["TurnUI"];
                turnUI.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["TurnUI"];
                turnUI.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["TurnUI"];
            }
            if (start != null)
            {
                start.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["StartBattle"];
                start.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["StartBattle"];
                start.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["StartBattle"];
            }
        }
        [HarmonyPatch(typeof(ActTypoController), nameof(ActTypoController.Init))]
        [HarmonyPostfix]
        private static void PreBattleUI_Init(ActTypoController __instance)
        {
            Transform turn = __instance.transform.Find("[Rect]Active/[Script]ActTypoTurnUI/[Image]Turn");
            if (turn != null)
            {
                turn.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["Turn"];
                turn.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["Turn"];
                turn.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["Turn"];
            }
        }
        [HarmonyPatch(typeof(UnitInformationAbnormalityNameTag), nameof(UnitInformationAbnormalityNameTag.UpdateLayout))]
        [HarmonyPostfix]
        private static void RisksNameTag(UnitInformationAbnormalityNameTag __instance)
        {
            if (__instance.img_abClassType.sprite.name.EndsWith("27") || __instance.img_abClassType.sprite.name.EndsWith("28") || __instance.img_abClassType.sprite.name.EndsWith("31"))
                __instance.img_abClassType.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["Risk_Unindentified"];
        }
        [HarmonyPatch(typeof(AbnormalityStatUI), nameof(AbnormalityStatUI.CheckAbUnlockInformation))]
        [HarmonyPostfix]
        private static void Risks(AbnormalityStatUI __instance)
        {
            if (__instance.img_level.sprite.name.EndsWith("27") || __instance.img_level.sprite.name.EndsWith("28") || __instance.img_level.sprite.name.EndsWith("31"))
                __instance.img_level.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["Risk_Unindentified"];
        }
        [HarmonyPatch(typeof(DanteAbilityButton), nameof(DanteAbilityButton.DanteAbilityButtonPropertyUpdate))]
        [HarmonyPostfix]
        private static void DanteAbility(DanteAbilityButton __instance)
        {
            if (__instance._dnateAbilityBtnImage.sprite.name.EndsWith('0'))
                __instance._dnateAbilityBtnImage.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["DanteAbility_Active"];
            else
                __instance._dnateAbilityBtnImage.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["DanteAbility_Inactive"];
        }
        //[HarmonyPatch(typeof(BattleSkillViewUIOverClock), nameof(BattleSkillViewUIOverClock.SetActiveOverClock))]
        //[HarmonyPostfix]
        //private static void OverClock_Init(BattleSkillViewUIOverClock __instance)
        //{
        //    Transform overclock_stable = __instance.transform.Find("[Canvas]Canvas/[Script]SkillViewCanvas/OverClock/OverClockImg");
        //    if (overclock_stable != null)
        //    {
        //        overclock_stable.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["Overclock"];
        //        overclock_stable.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["Overclock"];
        //        overclock_stable.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["Overclock"];
        //        __instance._image_OverClock.sprite = LCBR_ReadmeManager.ReadmeSprites["Overclock"];
        //        __instance._image_OverClock.m_Sprite = LCBR_ReadmeManager.ReadmeSprites["Overclock"];
        //        __instance._image_OverClock.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["Overclock"];
        //    }
        //}
        #endregion

        #region EGO
        [HarmonyPatch(typeof(BattleSkillViewUIInfo), nameof(BattleSkillViewUIInfo.Init))]
        [HarmonyPostfix]
        private static void EGO_Perspective_Warnings(BattleSkillViewUIInfo __instance)
        {
            __instance.textFrame_Awaken = LCBR_ReadmeManager.ReadmeSprites["EGO_Awaken"];
            __instance.textFrame_Erode = LCBR_ReadmeManager.ReadmeSprites["EGO_Erode"];
        }
        #endregion

        #region Skip Button
        [HarmonyPatch(typeof(GachaResultUI), nameof(GachaResultUI.Initialize))]
        [HarmonyPostfix]
        private static void SkipGacha_Init(GachaResultUI __instance)
        {
            Transform skip_gacha = __instance.transform.Find("[Rect]GetNewCardRoot/[Button]Skip");
            if (skip_gacha != null)
            {
                skip_gacha.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["Skip"];
            }
        }
        #endregion

        #region Auto Button
        [HarmonyPatch(typeof(StoryManager), nameof(StoryManager.Init))]
        [HarmonyPostfix]
        private static void AutoButton_Init(StoryManager __instance)
        {
            Transform autoButton = __instance._nonPostProcessRectTransform.transform.Find("[Rect]Buttons/[Rect]MenuObject/[Button]Auto");
            autoButton.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["AutoButton"];
            autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[0] = LCBR_ReadmeManager.ReadmeSprites["AutoButton"];
            autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[1] = LCBR_ReadmeManager.ReadmeSprites["AutoButton_Enabled"];
            autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[2] = LCBR_ReadmeManager.ReadmeSprites["TextButton"];
        }
        #endregion

        #region BattleEnding
        [HarmonyPatch(typeof(ActBattleEndTypoUI), nameof(ActBattleEndTypoUI.Open))]
        [HarmonyPostfix]
        private static void ActBattleEndTypoUI_Init(ActBattleEndTypoUI __instance)
        {
            Transform Def = __instance._defeatGroup.transform.Find("[Image]Typo_Defeat");
            Transform Win = __instance._victoryGroup.transform.Find("[Image]Typo_Victory");
            Def.GetComponentInChildren<Image>().overrideSprite = LCBR_ReadmeManager.ReadmeSprites["Defeat_Text"];
            Win.GetComponentInChildren<Image>().overrideSprite = LCBR_ReadmeManager.ReadmeSprites["Victory_Text"];
        }
        [HarmonyPatch(typeof(BattleResultUIPanel), nameof(BattleResultUIPanel.SetStatusUI))]
        [HarmonyPostfix]
        private static void BattleResultUIPanel_Init(BattleResultUIPanel __instance)
        {
            if (__instance.img_ResultMark.sprite.name == "MainUI_BattleResult_1_20")
            {
                __instance.img_ResultMark.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["Defeat"];
            }
            else
            {
                __instance.img_ResultMark.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["Victory"];
            }
            __instance.img_exclear.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["EX"];
        }
        #endregion

        #region GachaResult
        [HarmonyPatch(typeof(GachaCardUI), nameof(GachaCardUI.OnDisable))]
        [HarmonyPostfix]
        private static void GachaCardUI_SetData(GachaCardUI __instance)
        {
            __instance.img_newMark.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["NewGacha"];
        }
        [HarmonyPatch(typeof(ElementsSummary), nameof(ElementsSummary.SetDefault))]
        [HarmonyPostfix]
        private static void ElementsSummary_Init(ElementsSummary __instance)
        {
            if (__instance._redDot != null)
            {
                __instance._redDot.GetComponentInChildren<Image>().overrideSprite = LCBR_ReadmeManager.ReadmeSprites["New"];
            }
        }
        #endregion

        #region Overclock
        [HarmonyPatch(typeof(ActTypoOverClockUI), nameof(ActTypoOverClockUI.SetDefaultForAnim))]
        [HarmonyPostfix]
        private static void ActTypoOverClockUI_Init(ActTypoOverClockUI __instance)
        {
            __instance.tmp_content.text = "РАЗГОН";
            __instance.tmp_content.font = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp_content.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
        }
        [HarmonyPatch(typeof(BattleSkillViewUIOverClock), nameof(BattleSkillViewUIOverClock.SetActiveOverClock))]
        [HarmonyPostfix]
        private static void BattleSkillViewUIOverClock_SetData(BattleSkillViewUIOverClock __instance)
        {
            __instance._image_OverClock.m_OverrideSprite = LCBR_ReadmeManager.ReadmeSprites["Overclock"];
        }
        #endregion

        #region Critical
        [HarmonyPatch(typeof(BattleUnitDamageTypoUISlot), nameof(BattleUnitDamageTypoUISlot.Update))]
        [HarmonyPostfix]
        private static void BattleUnitDamageTypoUISlot_Init(BattleUnitDamageTypoUISlot __instance)
        {
            if (__instance.img_resist.sprite.name.Contains("Amber"))
            {
                //Sloth
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["CritAmber"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Azure"))
            {
                //Gloom
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["CritAzure"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Crimson"))
            {
                //Wrath
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["CritCrimson"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Indigo"))
            {
                //Pride
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["CritIndigo"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Scarlet"))
            {
                //Lust
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["CritScarlet"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Shamrock"))
            {
                //Gluttony
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["CritShamrock"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Violet"))
            {
                //Envy
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["CritViolet"];
            }
        }
        #endregion
    }
}
