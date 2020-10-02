using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

using RoR2;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace BetterUI
{
    class StatsDisplay : BetterUI.ModComponent
    {

        private GameObject statsDisplayContainer;
        private GameObject stupidBuffer;
        private RoR2.UI.HGTextMeshProUGUI textMesh;
        private int highestMultikill = 0;

        readonly Dictionary<string, Func<CharacterBody,string>> regexmap;
        readonly Regex regexpattern;

        string[] normalText;
        string[] altText;

        public StatsDisplay(BetterUI mod) : base(mod)
        {
            regexmap = new Dictionary<String, Func<CharacterBody, string>> {
                { "$armordmgreduction", (statBody) => ((statBody.armor >= 0 ? statBody.armor / (100 + statBody.armor) : (100 / (100 - statBody.armor) - 1)) * 100).ToString("0.##") },
                { "$exp", (statBody) => statBody.experience.ToString("0.##") },
                { "$level", (statBody) => statBody.level.ToString() },
                { "$dmg", (statBody) => statBody.damage.ToString("0.##") },
                { "$crit", (statBody) => statBody.crit.ToString("0.##") },
                { "$luckcrit", (statBody) =>  ( 100 * ((int)statBody.crit / 100) + 100 * Utils.LuckCalc(statBody.crit % 100 * 0.01f,statBody.master.luck)).ToString("0.##") },
                { "$hp", (statBody) => Math.Floor(statBody.healthComponent.health).ToString("0.##") },
                { "$maxhp", (statBody) => statBody.maxHealth.ToString("0.##") },
                { "$shield", (statBody) => Math.Floor(statBody.healthComponent.shield).ToString("0.##") },
                { "$maxshield", (statBody) => statBody.maxShield.ToString("0.##") },
                { "$barrier", (statBody) => Math.Floor(statBody.healthComponent.barrier).ToString("0.##") },
                { "$maxbarrier", (statBody) => statBody.maxBarrier.ToString("0.##") },
                { "$armor", (statBody) => statBody.armor.ToString("0.##") },
                { "$regen", (statBody) => statBody.regen.ToString("0.##") },
                { "$movespeed", (statBody) => Math.Round(statBody.moveSpeed, 1).ToString("0.##") },
                { "$jumps", (statBody) => (statBody.maxJumpCount - statBody.characterMotor.jumpCount).ToString() },
                { "$maxjumps", (statBody) => statBody.maxJumpCount.ToString() },
                { "$atkspd", (statBody) => statBody.attackSpeed.ToString() },
                { "$luck", (statBody) => statBody.master.luck.ToString() },
                { "$multikill", (statBody) => statBody.multiKillCount.ToString() },
                { "$highestmultikill", (statBody) => highestMultikill.ToString() },
                { "$killcount", (statBody) => statBody.killCountServer.ToString() },
                //{ \"$deaths", (statBody) => statBody.master.dea },
                { "$dpscharacter", (statBody) => mod.DPSMeter.CharacterDPS.ToString("N0") },
                { "$dpsminion", (statBody) => mod.DPSMeter.MinionDPS.ToString("N0") },
                { "$dps", (statBody) => mod.DPSMeter.DPS.ToString("N0") },
                { "$mountainshrines", (statBody) => TeleporterInteraction.instance ? TeleporterInteraction.instance.shrineBonusStacks.ToString() : "N/A" },
                { "$blueportal", (statBody) => TeleporterInteraction.instance ? TeleporterInteraction.instance.shouldAttemptToSpawnShopPortal.ToString() : "N/A" },
                { "$goldportal", (statBody) => TeleporterInteraction.instance ? TeleporterInteraction.instance.shouldAttemptToSpawnGoldshoresPortal.ToString() : "N/A" },
                { "$celestialportal", (statBody) => TeleporterInteraction.instance ? TeleporterInteraction.instance.shouldAttemptToSpawnMSPortal.ToString() : "N/A" },
                { "$difficulty", (statBody) => Run.instance.difficultyCoefficient.ToString("0.##") },
            };
            regexpattern = new Regex(@"(\" + String.Join(@"|\", regexmap.Keys) + ")");
        }

        internal override void Start()
        {
            normalText = regexpattern.Split(mod.config.StatsDisplayStatString.Value);
            altText = regexpattern.Split(mod.config.StatsDisplayStatStringCustomBind.Value);
            var pattern1 = new List<string>();
            foreach (Match match in regexpattern.Matches(mod.config.StatsDisplayStatString.Value))
            {
                pattern1.Add(match.Value);
            }
            var pattern2 = new List<string>();
            foreach (Match match in regexpattern.Matches(mod.config.StatsDisplayStatStringCustomBind.Value))
            {
                pattern2.Add(match.Value);
            }
        }
        internal override void Hook()
        {
            if (mod.config.StatsDisplayEnable.Value)
            {
                RoR2.Run.onRunStartGlobal += hook_runStartGlobal;
                On.RoR2.UI.HUD.Awake += hook_Awake;
            }
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
        internal override void Update()
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
                    bool customBindPressed = Input.GetKey(mod.config.StatsDisplayCustomBind.Value);
                    bool showStatsDisplay = !(mod.config.StatsDisplayShowCustomBindOnly.Value && !customBindPressed);

                    statsDisplayContainer.SetActive(showStatsDisplay);
                    if (showStatsDisplay)
                    {
                        highestMultikill = playerBody.multiKillCount > highestMultikill ? playerBody.multiKillCount : highestMultikill;

                        Util.sharedStringBuilder.Clear();
                        if (customBindPressed)
                        {
                            for (int i = 0; i < altText.Length; i++)
                            {
                                if(i % 2 == 0)
                                {
                                    Util.sharedStringBuilder.Append(altText[i]);

                                }
                                else
                                {
                                    Util.sharedStringBuilder.Append(regexmap[altText[i]](playerBody));
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < normalText.Length; i++)
                            {
                                if (i % 2 == 0)
                                {
                                    Util.sharedStringBuilder.Append(normalText[i]);

                                }
                                else
                                {
                                    Util.sharedStringBuilder.Append(regexmap[normalText[i]](playerBody));
                                }
                            }
                        }
                        textMesh.SetText(Util.sharedStringBuilder);
                    }
                }
            }
        }
    }
}
