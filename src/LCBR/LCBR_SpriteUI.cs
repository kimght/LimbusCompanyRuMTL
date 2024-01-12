using System.Runtime.InteropServices;
using HarmonyLib;
using BattleUI;
using BattleUI.Typo;
using MainUI;
using MainUI.VendingMachine;
using Dungeon.Map.UI;
using Login;
using UI;
using static UI.Utility.InfoModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StorySystem;
using System.Collections;
using System.Collections.Generic;
using UtilityUI;
using TMPro;
using Dungeon;
using BattleUI.BattleUnit;

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
        [HarmonyPatch(typeof(FormationUIPanel), nameof(FormationUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void SinnerSlot_Init(FormationUIPanel __instance)
        {
            Transform slot_YiSang = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Yisang/[Image]ParticipateSlotUI");
            Transform slot_Faust = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Faust/[Image]ParticipateSlotUI");
            Transform slot_DonQuixote = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Donqui/[Image]ParticipateSlotUI");
            Transform slot_Ryoshu = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Ryoshu/[Image]ParticipateSlotUI");
            Transform slot_Meursault = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Meursault/[Image]ParticipateSlotUI");
            Transform slot_HongLu = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Honglu/[Image]ParticipateSlotUI");
            Transform slot_Heathcliff = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Heathcliff/[Image]ParticipateSlotUI");
            Transform slot_Ishmael = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Ishmael/[Image]ParticipateSlotUI");
            Transform slot_Rodya = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Rodion/[Image]ParticipateSlotUI");
            Transform slot_Sinclair = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Sinclair/[Image]ParticipateSlotUI");
            Transform slot_Outis = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Outis/[Image]ParticipateSlotUI");
            Transform slot_Gregor = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/[Layout]Personalities/PersonalityFormationSlot_Gregor/[Image]ParticipateSlotUI");
            if (slot_YiSang != null)
            {
                slot_YiSang.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_Faust != null)
            {
                slot_Faust.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_DonQuixote != null)
            {
                slot_DonQuixote.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_Ryoshu != null)
            {
                slot_Ryoshu.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_Meursault != null)
            {
                slot_Meursault.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_HongLu != null)
            {
                slot_HongLu.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_Heathcliff != null)
            {
                slot_Heathcliff.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_Ishmael != null)
            {
                slot_Ishmael.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_Rodya != null)
            {
                slot_Rodya.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_Sinclair != null)
            {
                slot_Sinclair.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_Outis != null)
            {
                slot_Outis.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
            if (slot_Gregor != null)
            {
                slot_Gregor.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
        }
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIPanel), nameof(FormationSwitchablePersonalityUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void ParticipateSlot_Init(FormationSwitchablePersonalityUIPanel __instance)
        {
            Transform slot = __instance.transform.Find("[Script]FormationSwitchablePersonalityScrollView/Viewport/Content/[Layout]Items/PersonalitySwitchableSlot/[Image]ParticipateSlotUI");
            Transform slot_1 = __instance.transform.Find("[Script]FormationSwitchablePersonalityScrollView/Viewport/Content/[Layout]Items/PersonalitySwitchableSlot (1)/[Image]ParticipateSlotUI");
            Transform slot_2 = __instance.transform.Find("[Script]FormationSwitchablePersonalityScrollView/Viewport/Content/[Layout]Items/PersonalitySwitchableSlot (2)/[Image]ParticipateSlotUI");
            Transform slot_3 = __instance.transform.Find("[Script]FormationSwitchablePersonalityScrollView/Viewport/Content/[Layout]Items/PersonalitySwitchableSlot (3)/[Image]ParticipateSlotUI");
            Transform slot_4 = __instance.transform.Find("[Script]FormationSwitchablePersonalityScrollView/Viewport/Content/[Layout]Items/PersonalitySwitchableSlot (4)/[Image]ParticipateSlotUI");
            Transform slot_5 = __instance.transform.Find("[Script]FormationSwitchablePersonalityScrollView/Viewport/Content/[Layout]Items/PersonalitySwitchableSlot (5)/[Image]ParticipateSlotUI");
            Transform slot_6 = __instance.transform.Find("[Script]FormationSwitchablePersonalityScrollView/Viewport/Content/[Layout]Items/PersonalitySwitchableSlot (6)/[Image]ParticipateSlotUI");
            Transform slot_7 = __instance.transform.Find("[Script]FormationSwitchablePersonalityScrollView/Viewport/Content/[Layout]Items/PersonalitySwitchableSlot (7)/[Image]ParticipateSlotUI");
            Transform slot_8 = __instance.transform.Find("[Script]FormationSwitchablePersonalityScrollView/Viewport/Content/[Layout]Items/PersonalitySwitchableSlot (8)/[Image]ParticipateSlotUI");
            if (slot != null)
            {
                slot.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
                slot_1.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
                slot_2.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
                slot_3.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
                slot_4.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
                slot_5.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
                slot_6.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
                slot_7.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
                slot_8.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            }
        }
        #endregion

        #region Dungeon Formation UI
        [HarmonyPatch(typeof(FormationPersonalityUI), nameof(FormationPersonalityUI.SetData))]
        [HarmonyPostfix]
        private static void FormationPersonalityUI_Init(FormationPersonalityUI __instance)
        {
            __instance.img_isParticipaged.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            __instance.img_support.sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_SupportTag"];
        }
        #endregion

        #region Support Formation UI
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIPanel), nameof(FormationSwitchablePersonalityUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void Support_Init (FormationSwitchablePersonalityUIPanel __instance)
        {
            // SELECTED
            Transform slot_1 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot/[Image]ParticipateSlotUI");
            if (slot_1 != null)
                slot_1.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_2 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (1)/[Image]ParticipateSlotUI");
            if (slot_2 != null)
                slot_2.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_3 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (2)/[Image]ParticipateSlotUI");
            if (slot_3 != null)
                slot_3.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_4 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (3)/[Image]ParticipateSlotUI");
            if (slot_4 != null)
                slot_4.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_5 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (4)/[Image]ParticipateSlotUI");
            if (slot_5 != null)
                slot_5.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_6 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (5)/[Image]ParticipateSlotUI");
            if (slot_6 != null)
                slot_6.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_7 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (6)/[Image]ParticipateSlotUI");
            if (slot_7 != null)
                slot_7.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_8 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (7)/[Image]ParticipateSlotUI");
            if (slot_8 != null)
                slot_8.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_9 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (8)/[Image]ParticipateSlotUI");
            if (slot_9 != null)
                slot_9.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_10 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (9)/[Image]ParticipateSlotUI");
            if (slot_10 != null)
                slot_10.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_11 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (10)/[Image]ParticipateSlotUI");
            if (slot_11 != null)
                slot_11.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];
            Transform slot_12 = __instance.transform.Find("[Script]SwitchableSupportList/Viewport/Content/[Layout]Items/PersonalitySwitchableSupporterSlot (11)/[Image]ParticipateSlotUI");
            if (slot_12 != null)
                slot_12.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_InParty"];

            // SUPPORT TAG
            Transform support_tag = __instance.transform.Find("[Script]RightPanel/[Script]FormationEgoList/[Image]SupportTag");
            if (support_tag != null)
                support_tag.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_SupportTag"];
        }
        #endregion

        #region Mirror Dungeon
        [HarmonyPatch(typeof(MirrorDungeonEntranceScrollUIPanel), nameof(MirrorDungeonEntranceScrollUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void Bonus_Init(MirrorDungeonEntranceScrollUIPanel __instance)
        {
            Transform bonus_1 = __instance.transform.Find("MirrorDungeonSelectEgoGiftCategoryPanel/[Rect]View/[Rect]Grid/EgoGiftCategorySetUI (1)/[Image]Bonus");
            Transform bonus_2 = __instance.transform.Find("MirrorDungeonSelectEgoGiftCategoryPanel/[Rect]View/[Rect]Grid/EgoGiftCategorySetUI (2)/[Image]Bonus");
            Transform bonus_3 = __instance.transform.Find("MirrorDungeonSelectEgoGiftCategoryPanel/[Rect]View/[Rect]Grid/EgoGiftCategorySetUI (3)/[Image]Bonus");
            Transform bonus_4 = __instance.transform.Find("MirrorDungeonSelectEgoGiftCategoryPanel/[Rect]View/[Rect]Grid/EgoGiftCategorySetUI (4)/[Image]Bonus");
            Transform bonus_5 = __instance.transform.Find("MirrorDungeonSelectEgoGiftCategoryPanel/[Rect]View/[Rect]Grid/EgoGiftCategorySetUI (5)/[Image]Bonus");
            Transform bonus_6 = __instance.transform.Find("MirrorDungeonSelectEgoGiftCategoryPanel/[Rect]View/[Rect]Grid/EgoGiftCategorySetUI (6)/[Image]Bonus");
            Transform bonus_7 = __instance.transform.Find("MirrorDungeonSelectEgoGiftCategoryPanel/[Rect]View/[Rect]Grid/EgoGiftCategorySetUI (7)/[Image]Bonus");
            Transform bonus_8 = __instance.transform.Find("MirrorDungeonSelectEgoGiftCategoryPanel/[Rect]View/[Rect]Grid/EgoGiftCategorySetUI (8)/[Image]Bonus");
            if (bonus_1 != null)
            {
                bonus_1.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StarlightBonus"];
                bonus_2.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StarlightBonus"];
                bonus_3.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StarlightBonus"];
                bonus_4.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StarlightBonus"];
                bonus_5.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StarlightBonus"];
                bonus_6.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StarlightBonus"];
                bonus_7.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StarlightBonus"];
                bonus_8.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_StarlightBonus"];
            }
        }
        [HarmonyPatch(typeof(AbnormalityStatUI), nameof(AbnormalityStatUI.SetAbnormalityGuideUIActive))]
        [HarmonyPostfix]
        private static void BottomStat_Init(AbnormalityStatUI __instance)
        {
            Transform new_info = __instance.transform.Find("[Rect]FixedScalePivot/[Text]UnitName/[Image]Icon");
            if (new_info != null)
                new_info.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_NewInfo"];
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
        private static void AutoButton_Init (StoryManager __instance)
        {
            GameObject autoButton = GameObject.Find("StoryManager/[Rect]NonPostProcessCanvas/[Rect]Buttons/[Rect]MenuObject/[Button]Auto");
            GameObject autoButton_between = GameObject.Find("[Rect]Story/[Canvas]Story/[Rect]NonPostProcessCanvas/[Rect]Buttons/[Rect]MenuObject/[Button]Auto");
            if (autoButton != null)
            {
                autoButton.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_AutoButton"];
                autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[0] = LCBR_ReadmeManager.ReadmeSprites["LCBR_AutoButton"];
                autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[1] = LCBR_ReadmeManager.ReadmeSprites["LCBR_AutoButton_Enabled"];
                autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[2] = LCBR_ReadmeManager.ReadmeSprites["LCBR_TextButton"];
            }
            if (autoButton_between != null)
            {
                autoButton_between.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeSprites["LCBR_AutoButton"];
                autoButton_between.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[0] = LCBR_ReadmeManager.ReadmeSprites["LCBR_AutoButton"];
                autoButton_between.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[1] = LCBR_ReadmeManager.ReadmeSprites["LCBR_AutoButton_Enabled"];
                autoButton_between.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[2] = LCBR_ReadmeManager.ReadmeSprites["LCBR_TextButton"];
            }
        }
        #endregion
    }
}
