using HarmonyLib;
using MainUI;
using MainUI.VendingMachine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UI;
using TMPro;
using static UI.Utility.InfoModels;
using static UI.Utility.TMProStringMatcher;

namespace LimbusLocalizeRUS
{
    public static class LCBR_SeasonUI
    {
        [HarmonyPatch(typeof(MainLobbyUIPanel), nameof(MainLobbyUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void MainLobbyUIPanel_Init(MainLobbyUIPanel __instance)
        {
            //MAIN MENU
            GameObject banner_s3 = GameObject.Find("[Canvas]RatioMainUI/[Rect]PresenterRoot/[UIPresenter]LobbyUIPresenter(Clone)/[Rect]Active/[UIPanel]MainLobbyUIPanel/[Rect]Banner/[Rect]RightBanners/[Script]FirstBanner/[Mask]BannerImageMask/[Image]BannerImage");
            banner_s3.GetComponentInChildren<UnityEngine.UI.Image>(true).m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Season3_Banner"];
        }
        [HarmonyPatch(typeof(VendingMachineUIPresenter), nameof(VendingMachineUIPresenter.Initialize))]
        [HarmonyPostfix]
        private static void VendingMachineUIPresenter_Init(VendingMachineUIPresenter __instance)
        {
            //VENDING MACHINE
            GameObject shop_s3 = GameObject.Find("[Canvas]RatioMainUI/[Rect]PresenterRoot/VendingMachineUIPresenter_CH5(Clone)/ActiveRect/VendingMachineUIPanel/VMBannerSystem/SubBannerScrollView/Viewport/Content/SubBannerButton (1)/BannerBase/Mask/Img_Banner");
            shop_s3.GetComponentInChildren<UnityEngine.UI.Image>(true).m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Season3_Shop"];
            GameObject shop_s2 = GameObject.Find("[Canvas]RatioMainUI/[Rect]PresenterRoot/VendingMachineUIPresenter_CH5(Clone)/ActiveRect/VendingMachineUIPanel/VMBannerSystem/SubBannerScrollView/Viewport/Content/SubBannerButton (2)/BannerBase/Mask/Img_Banner");
            shop_s2.GetComponentInChildren<UnityEngine.UI.Image>(true).m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Season2_Shop"];
            GameObject shop_s1 = GameObject.Find("[Canvas]RatioMainUI/[Rect]PresenterRoot/VendingMachineUIPresenter_CH5(Clone)/ActiveRect/VendingMachineUIPanel/VMBannerSystem/SubBannerScrollView/Viewport/Content/SubBannerButton (3)/BannerBase/Mask/Img_Banner");
            shop_s1.GetComponentInChildren<UnityEngine.UI.Image>(true).m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["LCBR_Season1_Shop"];
        }
        [HarmonyPatch(typeof(BattlePassUIPopup), nameof(BattlePassUIPopup.SetupBaseData))]
        [HarmonyPostfix]
        private static void SeasonPass_Init(BattlePassUIPopup __instance)
        {
            __instance.seasonPeriod.font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
            __instance.seasonPeriod.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
            __instance.seasonPeriod.text = "(МСК) 06:00 16.11.2023";

            //FLAGS
            __instance.seasonPeriod.m_isRebuildingLayout = false;
            __instance.seasonPeriod.ignoreVisibility = true;
            __instance.seasonPeriod.isOverlay = false;
            __instance.seasonPeriod.m_ignoreCulling = true;
            __instance.seasonPeriod.isOverlay = false;
            __instance.seasonPeriod.m_isOverlay = false;
            __instance.seasonPeriod.m_isParsingText = true;
            __instance.seasonPeriod.m_RaycastTarget = false;
            __instance.seasonPeriod.raycastTarget = false;
        }
    }
}
