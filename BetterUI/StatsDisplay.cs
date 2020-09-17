using System;
using System.Collections.Generic;
using System.Text;

using RoR2;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace BetterUI
{
    class StatsDisplay
    {
        private readonly BetterUI mod;

        private GameObject statsDisplayContainer;
        private GameObject stupidBuffer;
        private RoR2.UI.HGTextMeshProUGUI textMesh;
        private int highestMultikill = 0;
        public StatsDisplay(BetterUI m)
        {
            mod = m;
        }

        public void hook_runStartGlobal(RoR2.Run self)
        {
            highestMultikill = 0;
        }
        public void hook_Awake(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);

            statsDisplayContainer = new GameObject("StatsDisplayContainer");
            RectTransform rectTransform = statsDisplayContainer.AddComponent<RectTransform>();

            if (mod.config.StatsDisplayAttachToObjectivePanel.Value)
            {
                stupidBuffer = new GameObject("StupidBuffer");
                RectTransform rectTransform3 = stupidBuffer.AddComponent<RectTransform>();
                LayoutElement layoutElement2 = stupidBuffer.AddComponent<LayoutElement>();

                layoutElement2.minWidth = 0;
                layoutElement2.minHeight = 2;
                layoutElement2.flexibleHeight = 1;
                layoutElement2.flexibleWidth = 1;

                stupidBuffer.transform.SetParent(self.objectivePanelController.objectiveTrackerContainer.parent.parent.transform);
                statsDisplayContainer.transform.SetParent(self.objectivePanelController.objectiveTrackerContainer.parent.parent.transform);

                rectTransform.localPosition = new Vector3(0, -10, 0);
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.localScale = new Vector3(1, -1, 1);
                rectTransform.sizeDelta = Vector2.zero;
                rectTransform.anchoredPosition = new Vector2(0, 0);
                rectTransform.eulerAngles = new Vector3(0, 6, 0);
            }
            else
            {
                statsDisplayContainer.transform.SetParent(self.mainContainer.transform);

                rectTransform.localPosition = new Vector3(0, 0, 0);
                rectTransform.anchorMin = mod.config.StatsDisplayWindowAnchorMin.Value;
                rectTransform.anchorMax = mod.config.StatsDisplayWindowAnchorMax.Value;
                rectTransform.localScale = new Vector3(1, -1, 1);
                rectTransform.sizeDelta = mod.config.StatsDisplayWindowSize.Value;
                rectTransform.anchoredPosition = mod.config.StatsDisplayWindowPosition.Value;
                rectTransform.eulerAngles = mod.config.StatsDisplayWindowAngle.Value;
            }


            VerticalLayoutGroup verticalLayoutGroup = statsDisplayContainer.AddComponent<UnityEngine.UI.VerticalLayoutGroup>();
            verticalLayoutGroup.padding = new RectOffset(5, 5, 10, 5);

            GameObject statsDisplayText = new GameObject("StatsDisplayText");
            RectTransform rectTransform2 = statsDisplayText.AddComponent<RectTransform>();
            textMesh = statsDisplayText.AddComponent<RoR2.UI.HGTextMeshProUGUI>();
            LayoutElement layoutElement = statsDisplayText.AddComponent<LayoutElement>();

            statsDisplayText.transform.SetParent(statsDisplayContainer.transform);


            rectTransform2.localPosition = Vector3.zero;
            rectTransform2.anchorMin = Vector2.zero;
            rectTransform2.anchorMax = Vector2.one;
            rectTransform2.localScale = new Vector3(1, -1, 1);
            rectTransform2.sizeDelta = Vector2.zero;
            rectTransform2.anchoredPosition = Vector2.zero;

            if (mod.config.StatsDisplayPanelBackground.Value)
            {
                Image image = statsDisplayContainer.AddComponent<UnityEngine.UI.Image>();
                Image copyImage = self.objectivePanelController.objectiveTrackerContainer.parent.GetComponent<Image>();
                image.sprite = copyImage.sprite;
                image.color = copyImage.color;
                image.type = Image.Type.Sliced;
            }

            textMesh.fontSize = 12;
            textMesh.fontSizeMin = 6;
            textMesh.faceColor = Color.white; ;
            textMesh.outlineColor = Color.black;
            textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.2f);
            textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.4f);

            layoutElement.minWidth = 1;
            layoutElement.minHeight = 1;
            layoutElement.flexibleHeight = 1;
            layoutElement.flexibleWidth = 1;

        }
             
        public void Update()
        {
            if (mod.config.StatsDisplayAttachToObjectivePanel.Value)
            {
                if (stupidBuffer != null)
                {
                    stupidBuffer.transform.SetAsLastSibling();
                }
                if (statsDisplayContainer != null)
                {
                    statsDisplayContainer.transform.SetAsLastSibling();
                }
            }
            if (mod.HUD != null && textMesh != null)
            {
                    CharacterBody playerBody = mod.HUD.targetBodyObject ? mod.HUD.targetBodyObject.GetComponent<CharacterBody>() : null;
                if (playerBody != null)
                {
                    bool scoreBoardOpen = LocalUserManager.GetFirstLocalUser().inputPlayer != null && LocalUserManager.GetFirstLocalUser().inputPlayer.GetButton("info");
                    if (mod.config.StatsDisplayShowScoreboardOnly.Value)
                    {
                        statsDisplayContainer.SetActive(scoreBoardOpen);
                        if (!scoreBoardOpen) { return; }
                    }

                    highestMultikill = playerBody.multiKillCount > highestMultikill ? playerBody.multiKillCount : highestMultikill;
                    string printString = scoreBoardOpen ? mod.config.StatsDisplayStatStringScoreboard.Value : mod.config.StatsDisplayStatString.Value;
                    printString = printString.Replace("$armordmgreduction", ((playerBody.armor >= 0 ? playerBody.armor / (100 + playerBody.armor) : (100 / (100 - playerBody.armor) - 1)) * 100).ToString("N2"));
                    printString = printString.Replace("$exp", playerBody.experience.ToString());
                    printString = printString.Replace("$level", playerBody.level.ToString());
                    printString = printString.Replace("$dmg", playerBody.damage.ToString());
                    printString = printString.Replace("$crit", playerBody.crit.ToString());
                    printString = printString.Replace("$hp", Math.Floor(playerBody.healthComponent.health).ToString());
                    printString = printString.Replace("$maxhp", playerBody.maxHealth.ToString());
                    printString = printString.Replace("$shield", Math.Floor(playerBody.healthComponent.shield).ToString());
                    printString = printString.Replace("$maxshield", playerBody.maxShield.ToString());
                    printString = printString.Replace("$barrier", Math.Floor(playerBody.healthComponent.barrier).ToString());
                    printString = printString.Replace("$maxbarrier", playerBody.maxBarrier.ToString());
                    printString = printString.Replace("$armor", playerBody.armor.ToString());
                    printString = printString.Replace("$regen", playerBody.regen.ToString());
                    printString = printString.Replace("$movespeed", Math.Round(playerBody.moveSpeed, 1).ToString());
                    printString = printString.Replace("$jumps", (playerBody.maxJumpCount - playerBody.characterMotor.jumpCount).ToString());
                    printString = printString.Replace("$maxjumps", playerBody.maxJumpCount.ToString());
                    printString = printString.Replace("$atkspd", playerBody.attackSpeed.ToString());
                    printString = printString.Replace("$luck", LocalUserManager.GetFirstLocalUser().cachedMaster.luck.ToString());
                    printString = printString.Replace("$multikill", playerBody.multiKillCount.ToString());
                    printString = printString.Replace("$highestmultikill", highestMultikill.ToString());
                    printString = printString.Replace("$killcount", playerBody.killCountServer.ToString());
                    //printString = printString.Replace("$deaths", playerBody.master.dea);
                    printString = printString.Replace("$dpscharacter", mod.DPSMeter.CharacterDPS.ToString("N0")); ;
                    printString = printString.Replace("$dpsminion", mod.DPSMeter.MinionDPS.ToString("N0")); ;
                    printString = printString.Replace("$dps", mod.DPSMeter.DPS.ToString("N0")); ;
                    printString = printString.Replace("$mountainshrines", TeleporterInteraction.instance.shrineBonusStacks.ToString()); ;
                    printString = printString.Replace("$blueportal", TeleporterInteraction.instance.shouldAttemptToSpawnShopPortal.ToString());
                    printString = printString.Replace("$goldportal", TeleporterInteraction.instance.shouldAttemptToSpawnGoldshoresPortal.ToString());
                    printString = printString.Replace("$celestialportal", TeleporterInteraction.instance.shouldAttemptToSpawnMSPortal.ToString());

                    textMesh.text = printString;
                }
            }
        }
    }
}
