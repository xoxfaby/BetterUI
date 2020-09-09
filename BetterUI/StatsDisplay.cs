using System;
using System.Collections.Generic;
using System.Text;

using RoR2;
using BepInEx;
using UnityEngine;
using TMPro;



namespace BetterUI
{
    class StatsDisplay
    {
        ConfigManager config;

        private GameObject statsDisplayContainer;
        private RoR2.UI.HGTextMeshProUGUI textMesh;
        private CharacterBody playerBody;
        public StatsDisplay(ConfigManager c)
        {
            config = c;
        }
        public void hook_OnEnable(On.RoR2.UI.HUD.orig_OnEnable orig, RoR2.UI.HUD self)
        {
            orig(self);




            statsDisplayContainer = new GameObject("StatsDisplayContainer");
            statsDisplayContainer.transform.SetParent(self.mainContainer.transform);

            RectTransform rectTransform = statsDisplayContainer.AddComponent<RectTransform>();
            rectTransform.anchorMin = config.windowAnchorMin.Value;
            rectTransform.anchorMax = config.windowAnchorMax.Value;
            rectTransform.anchoredPosition = config.windowPosition.Value;
            rectTransform.sizeDelta = config.windowSize.Value;

            textMesh = statsDisplayContainer.AddComponent<RoR2.UI.HGTextMeshProUGUI>();
            textMesh.fontSize = config.statsFontSize.Value;
            textMesh.faceColor = config.statsFontColor.Value;
            textMesh.outlineColor = config.statsFontOutlineColor.Value;
            textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.2f);
            textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.4f);

            On.RoR2.UI.HUD.Update += hook_HUDUpdate;
        }

        public void hook_OnDisable(On.RoR2.UI.HUD.orig_OnDisable orig, RoR2.UI.HUD self)
        {
            orig(self);

            On.RoR2.UI.HUD.Update -= hook_HUDUpdate;
            playerBody = null;

        }

        public void hook_HUDUpdate(On.RoR2.UI.HUD.orig_Update orig, RoR2.UI.HUD self)
        {
            orig(self);


            if (self != null && self.targetMaster != null)
            {
                playerBody = self.targetMaster.GetBody();
            }

            

            if (playerBody != null && textMesh != null)
            {
                if (config.scoreboardOnly.Value)
                {
                    bool active = self.localUserViewer != null && self.localUserViewer.inputPlayer != null && self.localUserViewer.inputPlayer.GetButton("info");
                    statsDisplayContainer.SetActive(active);
                    if (!active) { return; }
                }
                string printString = config.statString.Value;
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
                printString = printString.Replace("$movespeed", playerBody.moveSpeed.ToString());
                printString = printString.Replace("$jumps", (playerBody.maxJumpCount - playerBody.characterMotor.jumpCount).ToString());
                printString = printString.Replace("$maxjumps", playerBody.maxJumpCount.ToString());
                printString = printString.Replace("$atkspd", playerBody.attackSpeed.ToString());
                printString = printString.Replace("$luck", self.targetMaster.luck.ToString());
                printString = printString.Replace("$multikill", playerBody.multiKillCount.ToString());
                printString = printString.Replace("$killcount", playerBody.killCountServer.ToString());


                textMesh.text = printString;
            }
        }
    }
}
