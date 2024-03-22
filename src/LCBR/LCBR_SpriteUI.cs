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
                combo.GetComponent<Image>().sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Combo"];
            }
            __instance.img_parryingTypo.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Combo"];
        }
        #endregion

        #region Login
        [HarmonyPatch(typeof(LoginSceneManager), nameof(LoginSceneManager.OnInitLoginInfoManagerEnd))]
        [HarmonyPostfix]
        private static void LoginSceneManager_Init(LoginSceneManager __instance)
        {
            __instance.img_touchToStart.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Start"];
            Transform motto = __instance.transform.Find("[Canvas]/[Image]RedLine/[Image]Phrase");
            if (motto != null)
            {
                motto.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Motto"];
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
                lunacyTag.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_LunacyTag"];
            }
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
                soldOut.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_SoldOut"];
            }
        }
        #endregion

        #region Formation UI
        [HarmonyPatch(typeof(FormationPersonalityUI), nameof(FormationPersonalityUI.Initialize))]
        [HarmonyPostfix]
        private static void FormationPersonalityUI_Init(FormationPersonalityUI __instance)
        {
            __instance.img_isParticipaged.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            __instance.img_support.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_SupportTag"];
            __instance._redDot.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_New"];
        }
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIScrollViewItem), nameof(FormationSwitchablePersonalityUIScrollViewItem.Initialize))]
        [HarmonyPostfix]
        private static void FormationSwitchablePersonalityUIScrollViewItem_Init(FormationSwitchablePersonalityUIScrollViewItem __instance)
        {
            Transform img_isParticipaged = __instance._participatedObject.transform.parent.parent.parent.Find("[Image]ParticipateSlotUI");
            img_isParticipaged.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            __instance._newAcquiredRedDot.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_New"];
        }
        [HarmonyPatch(typeof(FormationSwitchableSupporterPersonalityUIScrollViewItem), nameof(FormationSwitchableSupporterPersonalityUIScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void YobenBoben(FormationSwitchableSupporterPersonalityUIScrollViewItem __instance)
        {
            __instance._selectedFrame.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
        }
        [HarmonyPatch(typeof(FormationEgoSlot), nameof(FormationEgoSlot.SetData))]
        [HarmonyPostfix]
        private static void FormationEgoSlot_Init(FormationEgoSlot __instance)
        {
            __instance._redDot.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_New"];
        }
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIPanel), nameof(FormationSwitchablePersonalityUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void FormationSwitchablePersonalityUIPanel_Init(FormationSwitchablePersonalityUIPanel __instance)
        {
            Transform newPersonality = __instance.transform.Find("[Script]RightPanel/[Script]FormationEgoList/[Script]RedDot");
            __instance._egoRedDot.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_New"];
            __instance._personalityRedDot.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_New"];
            if (newPersonality != null)
                newPersonality.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_New"];
        }
        [HarmonyPatch(typeof(FormationSwitchableEgoUIScrollViewItem), nameof(FormationSwitchableEgoUIScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void RedDotAgain_Init(FormationSwitchableEgoUIScrollViewItem __instance)
        {
            __instance._newAcquiredRedDot.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_New"];
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
                support_tag.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_SupportTag"];
        }
        #endregion

        #region Mirror Dungeon
        [HarmonyPatch(typeof(EgoGiftCategorySetUIToggleButton), nameof(EgoGiftCategorySetUIToggleButton.SetData))]
        [HarmonyPostfix]
        private static void EgoGiftCategorySetUIToggleButton_Init(EgoGiftCategorySetUIToggleButton __instance)
        {
            __instance.img_bonus.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StarlightBonus"];
        }
        [HarmonyPatch(typeof(AbnormalityStatUI), nameof(AbnormalityStatUI.SetAbnormalityGuideUIActive))]
        [HarmonyPostfix]
        private static void BottomStat_Init(AbnormalityStatUI __instance)
        {
            Transform new_info = __instance.transform.Find("[Rect]FixedScalePivot/[Text]UnitName/[Image]Icon");
            if (new_info != null)
                new_info.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_NewInfo"];
        }
        [HarmonyPatch(typeof(MirrorDungeonShopItemSlot), nameof(MirrorDungeonShopItemSlot.SetData))]
        [HarmonyPostfix]
        private static void MirrorDungeonShopItemSlot_Init(MirrorDungeonShopItemSlot __instance)
        {
            __instance._soldOutTitleObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Mirror_SoldOut"];
        }
        [HarmonyPatch(typeof(MirrorDungeonFloorRewardItem), nameof(MirrorDungeonFloorRewardItem.SetFloorStatus))]
        [HarmonyPostfix]
        private static void DungeonClearLogo(MirrorDungeonFloorRewardItem __instance)
        {
            __instance.img_clearLogo.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_DungeonClearLogo"];
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
                waveUI.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_WaveUI"];
                waveUI.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_WaveUI"];
                waveUI.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_WaveUI"];
            }
            if (turnUI != null)
            {
                turnUI.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_TurnUI"];
                turnUI.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_TurnUI"];
                turnUI.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_TurnUI"];
            }
            if (start != null)
            {
                start.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StartBattle"];
                start.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StartBattle"];
                start.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StartBattle"];
            }
        }
        [HarmonyPatch(typeof(ActTypoController), nameof(ActTypoController.Init))]
        [HarmonyPostfix]
        private static void PreBattleUI_Init(ActTypoController __instance)
        {
            Transform turn = __instance.transform.Find("[Rect]Active/[Script]ActTypoTurnUI/[Image]Turn");
            if (turn != null)
            {
                turn.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Turn"];
                turn.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Turn"];
                turn.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Turn"];
            }
        }
        //[HarmonyPatch(typeof(BattleSkillViewUIOverClock), nameof(BattleSkillViewUIOverClock.SetActiveOverClock))]
        //[HarmonyPostfix]
        //private static void OverClock_Init(BattleSkillViewUIOverClock __instance)
        //{
        //    Transform overclock_stable = __instance.transform.Find("[Canvas]Canvas/[Script]SkillViewCanvas/OverClock/OverClockImg");
        //    if (overclock_stable != null)
        //    {
        //        overclock_stable.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Overclock"];
        //        overclock_stable.GetComponentInChildren<Image>(true).m_Sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Overclock"];
        //        overclock_stable.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Overclock"];
        //        __instance._image_OverClock.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Overclock"];
        //        __instance._image_OverClock.m_Sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Overclock"];
        //        __instance._image_OverClock.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Overclock"];
        //    }
        //}
        #endregion

        #region Skip Button
        [HarmonyPatch(typeof(GachaResultUI), nameof(GachaResultUI.Initialize))]
        [HarmonyPostfix]
        private static void SkipGacha_Init(GachaResultUI __instance)
        {
            Transform skip_gacha = __instance.transform.Find("[Rect]GetNewCardRoot/[Button]Skip");
            if (skip_gacha != null)
            {
                skip_gacha.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Skip"];
            }
        }
        #endregion

        #region Auto Button
        [HarmonyPatch(typeof(StoryManager), nameof(StoryManager.Init))]
        [HarmonyPostfix]
        private static void AutoButton_Init(StoryManager __instance)
        {
            Transform autoButton = __instance._nonPostProcessRectTransform.transform.Find("[Rect]Buttons/[Rect]MenuObject/[Button]Auto");
            autoButton.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_AutoButton"];
            autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[0] = LCBR_ReadmeManager.ReadmeSprites["LCBR_AutoButton"];
            autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[1] = LCBR_ReadmeManager.ReadmeSprites["LCBR_AutoButton_Enabled"];
            autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[2] = LCBR_ReadmeManager.ReadmeSprites["LCBR_TextButton"];
        }
        #endregion

        #region BattleEnding
        [HarmonyPatch(typeof(ActBattleEndTypoUI), nameof(ActBattleEndTypoUI.Open))]
        [HarmonyPostfix]
        private static void ActBattleEndTypoUI_Init(ActBattleEndTypoUI __instance)
        {
            Transform Def = __instance._defeatGroup.transform.Find("[Image]Typo_Defeat");
            Transform Win = __instance._victoryGroup.transform.Find("[Image]Typo_Victory");
            Def.GetComponentInChildren<Image>().overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Defeat_Text"];
            Win.GetComponentInChildren<Image>().overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Victory_Text"];
        }
        [HarmonyPatch(typeof(BattleResultUIPanel), nameof(BattleResultUIPanel.SetStatusUI))]
        [HarmonyPostfix]
        private static void BattleResultUIPanel_Init(BattleResultUIPanel __instance)
        {
            if (__instance.img_ResultMark.sprite.name == "MainUI_BattleResult_1_20")
            {
                __instance.img_ResultMark.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Defeat"];
            }
            else
            {
                __instance.img_ResultMark.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Victory"];
            }
            __instance.img_exclear.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_EX"];
        }
        #endregion

        #region GachaResult
        [HarmonyPatch(typeof(GachaCardUI), nameof(GachaCardUI.OnDisable))]
        [HarmonyPostfix]
        private static void GachaCardUI_SetData(GachaCardUI __instance)
        {
            __instance.img_newMark.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_NewGacha"];
        }
        [HarmonyPatch(typeof(ElementsSummary), nameof(ElementsSummary.SetDefault))]
        [HarmonyPostfix]
        private static void ElementsSummary_Init(ElementsSummary __instance)
        {
            if (__instance._redDot != null)
            {
                __instance._redDot.GetComponentInChildren<Image>().overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_New"];
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
            __instance._image_OverClock.m_OverrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_Overclock"];
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
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_CritAmber"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Azure"))
            {
                //Gloom
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_CritAzure"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Crimson"))
            {
                //Wrath
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_CritCrimson"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Indigo"))
            {
                //Pride
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_CritIndigo"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Scarlet"))
            {
                //Lust
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_CritScarlet"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Shamrock"))
            {
                //Gluttony
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_CritShamrock"];
            }
            else if (__instance.img_resist.sprite.name.Contains("Violet"))
            {
                //Envy
                __instance.img_Critical.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_CritViolet"];
            }
        }
        #endregion
    }
}
