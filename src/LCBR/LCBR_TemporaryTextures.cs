using BattleUI;
using BattleUI.BattleUnit.SkillInfoUI;
using BattleUI.Information;
using UnitInformation.Tab;
using HarmonyLib;
using MainUI;
using MainUI.VendingMachine;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using MainUI.Gacha;
using BattleUI.UIRoot;
using Login;
using static UI.Utility.InfoModels;
using static UI.Utility.TMProStringMatcher;
using BattleUI.EvilStock;
using UnityEngine.Playables;
using BattleUI.Typo;
using Dungeon.Map.UI;
using LimbusLocalizeRUS;

namespace LimbusCompanyBusRUS
{
    internal class LCBR_TemporaryTextures
    {
        #region MainMenu
        [HarmonyPatch(typeof(StageUIPresenter), nameof(StageUIPresenter.Initialize))]
        [HarmonyPostfix]
        private static void Drive_Init(StageUIPresenter __instance)
        {
            Transform rd_glow1 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Rect]MirrorDungeonBanner/[Button]EnterRewardDungeon/[Rect]Items/[Text]Entrance");
            Transform rd_glow2 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Rect]MirrorDungeonBanner/[Button]EnterRewardDungeon/[Rect]Items/[Text]Entrance/[Text]Entrance (1)");
            if (rd_glow1 != null)
                rd_glow1.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[4];
            if (rd_glow2 != null)
                rd_glow2.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[3];
            Transform md_glow1 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Rect]MirrorDungeonBanner/[Button]EnterMirrorDungeon/[Rect]Items/[Text]Entrance");
            Transform md_glow2 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Rect]MirrorDungeonBanner/[Button]EnterMirrorDungeon/[Rect]Items/[Text]Entrance/[Text]Entrance (1)");
            if (md_glow1 != null)
                md_glow1.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[4];
            if (md_glow2 != null)
                md_glow2.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[3];
            Transform rr_glow1 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Rect]MirrorDungeonBanner/[Button]EnterRailwayDungeon/[Rect]Items/[Text]Entrance");
            Transform rr_glow2 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Rect]MirrorDungeonBanner/[Button]EnterRailwayDungeon/[Rect]Items/[Text]Entrance/[Text]Entrance (1)");
            if (rr_glow1 != null)
                rr_glow1.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[4];
            if (rr_glow2 != null)
                rr_glow2.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[3];
            Transform event_glow = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Script]StageLeftBanners/[Script]StageEventBanner_Main_MiracleOfDistrict20(Clone)/[Mask]BannerImage/[Text]EventOpening");
            if (event_glow != null)
                event_glow.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[4];
        }
        [HarmonyPatch(typeof(GachaUIPanel), nameof(GachaUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void Gacha_Init(GachaUIPanel __instance)
        {
            __instance.tmp_alreadyUsedGacha.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[7];
        }
        #endregion

        #region Dialogues
        [HarmonyPatch(typeof(LobbyUIPresenter), nameof(LobbyUIPresenter.Initialize))]
        [HarmonyPostfix]
        private static void Lobby_Init(LobbyUIPresenter __instance)
        {
            Transform lobby_glow = __instance.transform.Find("[Rect]Active/[UIPanel]MainLobbyUIPanel/[Rect]Banner/[Rect]LeftMainBanner/[Image]PersonalityIllust/DialogHolder/img_DialogPanel/tmp_Dialog");
            if (lobby_glow != null)
                lobby_glow.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[6];
        }
        [HarmonyPatch(typeof(StoryUIPanel), nameof(StoryUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void Story_Init (StoryUIPanel __instance)
        {
            Transform dialogue_glow = __instance.transform.Find("[Rect]PersonalityStoryUI/[Rect]PersonalityUI/[Rect]Banner/[Image]Panel/[Rect]Dialog_BG/img_shadow/[Text]VoiceLocal");
            if (dialogue_glow != null)
                dialogue_glow.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[6];
        }
        [HarmonyPatch(typeof(BattleSkillViewUIController), nameof(BattleSkillViewUIController.SetSkillViewUI))]
        [HarmonyPostfix]
        private static void EGO_Init(BattleSkillViewUIController __instance)
        {
            Transform ego_glow = __instance.transform.Find("[Canvas]Canvas/[Script]SkillViewCanvas/DialogHolder/[Image]Shadow/SkillViewText");
            if (ego_glow != null)
                ego_glow.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[6];
        }
        [HarmonyPatch(typeof(BattleResultUIPanel), nameof(BattleResultUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void BattleResult_Init (BattleResultUIPanel __instance)
        {
            __instance.tmp_dialog.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicmats[6];
        }
        #endregion
    }
}
