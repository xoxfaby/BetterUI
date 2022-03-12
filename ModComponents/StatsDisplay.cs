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
    public static class StatsDisplay
    {

        private static GameObject statsDisplayContainer;
        private static GameObject stupidBuffer;
        private static RoR2.UI.HGTextMeshProUGUI textMesh;
        private static int highestMultikill = 0;
        private static CharacterBody playerBody;
        private static Boolean statsDisplayToggle = false;

        internal static readonly Dictionary<string, DisplayCallback> regexmap;
        static Regex regexpattern;

        static string[] normalText;
        static string[] altText;

        public delegate string DisplayCallback(CharacterBody characterBody);

        static StatsDisplay()
        {
            regexmap = new Dictionary<String, DisplayCallback> {
                { "$armordmgreduction", (statBody) => ((statBody.armor >= 0 ? statBody.armor / (100 + statBody.armor) : (100 / (100 - statBody.armor) - 1)) * 100).ToString("0.##") },
                { "$exp", (statBody) => TeamManager.instance.GetTeamExperience(statBody.teamComponent.teamIndex).ToString("0.##") },
                { "$maxexp", (statBody) => TeamManager.instance.GetTeamNextLevelExperience(statBody.teamComponent.teamIndex).ToString("0.##") },
                { "$level", (statBody) => statBody.level.ToString() },
                { "$dmg", (statBody) => statBody.damage.ToString("0.##") },
                { "$crit", (statBody) => statBody.crit.ToString("0.##") },
                { "$critdamage", (statBody) => statBody.critMultiplier.ToString("0.##") },
                { "$luckcrit", (statBody) =>  ( 100 * ((int)statBody.crit / 100) + 100 * Utils.LuckCalc(statBody.crit % 100 * 0.01f,statBody.master.luck)).ToString("0.##") },
                { "$hp", (statBody) => Math.Floor(statBody.healthComponent.health).ToString("0.##") },
                { "$maxhp", (statBody) => statBody.maxHealth.ToString("0.##") },
                { "$shield", (statBody) => Math.Floor(statBody.healthComponent.shield).ToString("0.##") },
                { "$maxshield", (statBody) => statBody.maxShield.ToString("0.##") },
                { "$barrier", (statBody) => Math.Floor(statBody.healthComponent.barrier).ToString("0.##") },
                { "$maxbarrier", (statBody) => statBody.maxBarrier.ToString("0.##") },
                { "$armor", (statBody) => statBody.armor.ToString("0.##") },
                { "$regen", (statBody) => statBody.regen.ToString("0.##") },
                { "$movespeed", (statBody) => statBody.moveSpeed.ToString("0.##") },
                { "$velocity", (statBody) => statBody.characterMotor.velocity.magnitude.ToString("0.##") },
                { "$2dvelocity", (statBody) => {
                    var vector = statBody.characterMotor.velocity;
                    vector.y = 0;
                    return vector.magnitude.ToString("0.##");
                    }
                },
                { "$jumps", (statBody) => (statBody.maxJumpCount - statBody.characterMotor.jumpCount).ToString() },
                { "$maxjumps", (statBody) => statBody.maxJumpCount.ToString() },
                { "$atkspd", (statBody) => statBody.attackSpeed.ToString() },
                { "$luck", (statBody) => statBody.master.luck.ToString() },
                { "$multikill", (statBody) => statBody.multiKillCount.ToString() },
                { "$highestmultikill", (statBody) => highestMultikill.ToString() },
                { "$killcount", (statBody) => statBody.killCountServer.ToString() },
                //{ \"$deaths", (statBody) => statBody.master.dea },
                { "$dpscharacter", (statBody) => DPSMeter.CharacterDPS.ToString("N0") },
                { "$dpsminion", (statBody) => DPSMeter.MinionDPS.ToString("N0") },
                { "$dps", (statBody) => DPSMeter.DPS.ToString("N0") },
                { "$mountainshrines", (statBody) => TeleporterInteraction.instance ? TeleporterInteraction.instance.shrineBonusStacks.ToString() : "N/A" },
                { "$blueportal", (statBody) => TeleporterInteraction.instance ? TeleporterInteraction.instance.shouldAttemptToSpawnShopPortal.ToString() : "N/A" },
                { "$goldportal", (statBody) => TeleporterInteraction.instance ? TeleporterInteraction.instance.shouldAttemptToSpawnGoldshoresPortal.ToString() : "N/A" },
                { "$celestialportal", (statBody) => TeleporterInteraction.instance ? TeleporterInteraction.instance.shouldAttemptToSpawnMSPortal.ToString() : "N/A" },
                { "$difficulty", (statBody) => Run.instance.difficultyCoefficient.ToString("0.##") },
            };
            regexpattern = new Regex(@"(\" + String.Join(@"|\", regexmap.Keys) + ")");


            if (ConfigManager.StatsDisplayEnable.Value)
            {
                RoR2.Run.onRunStartGlobal += runStartGlobal;
            }
            BetterUIPlugin.onStart += onStart;
            BetterUIPlugin.onUpdate += onUpdate;
            BetterUIPlugin.onHUDAwake += onHUDAwake;
        }
        internal static void Hook() { }
        public static void AddStatsDisplay(string token, Func<CharacterBody, string> displayCallback)
        {
            AddStatsDisplay(token, new DisplayCallback(displayCallback));
        }
        public static void AddStatsDisplay(string token, DisplayCallback displayCallback)
        {
            regexmap[token] = displayCallback;
            regexpattern = new Regex(@"(\" + String.Join(@"|\", regexmap.Keys) + ")");
            normalText = regexpattern.Split(ConfigManager.StatsDisplayStatString.Value);
            altText = regexpattern.Split(ConfigManager.StatsDisplayStatStringCustomBind.Value);
        }


        static void onStart()
        {
            normalText = regexpattern.Split(ConfigManager.StatsDisplayStatString.Value);
            altText = regexpattern.Split(ConfigManager.StatsDisplayStatStringCustomBind.Value);
        }


        internal static void runStartGlobal(RoR2.Run self)
        {
            highestMultikill = 0;
        }
        static void onHUDAwake(RoR2.UI.HUD self)
        {
            if (ConfigManager.StatsDisplayEnable.Value)
            {
                statsDisplayContainer = new GameObject("StatsDisplayContainer");
                RectTransform rectTransform = statsDisplayContainer.AddComponent<RectTransform>();

                if (ConfigManager.StatsDisplayAttachToObjectivePanel.Value)
                {
                    stupidBuffer = new GameObject("StupidBuffer");
                    RectTransform rectTransform3 = stupidBuffer.AddComponent<RectTransform>();
                    LayoutElement layoutElement2 = stupidBuffer.AddComponent<LayoutElement>();

                    layoutElement2.minWidth = 0;
                    layoutElement2.minHeight = 2;
                    layoutElement2.flexibleHeight = 1;
                    layoutElement2.flexibleWidth = 1;

                    stupidBuffer.transform.SetParent(BetterUIPlugin.objectivePanelController.objectiveTrackerContainer.parent.parent.transform);
                    statsDisplayContainer.transform.SetParent(BetterUIPlugin.objectivePanelController.objectiveTrackerContainer.parent.parent.transform);

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
                    statsDisplayContainer.transform.SetParent(BetterUIPlugin.hud.mainContainer.transform);

                    rectTransform.localPosition = new Vector3(0, 0, 0);
                    rectTransform.anchorMin = ConfigManager.StatsDisplayWindowAnchorMin.Value;
                    rectTransform.anchorMax = ConfigManager.StatsDisplayWindowAnchorMax.Value;
                    rectTransform.localScale = new Vector3(1, -1, 1);
                    rectTransform.sizeDelta = ConfigManager.StatsDisplayWindowSize.Value;
                    rectTransform.anchoredPosition = ConfigManager.StatsDisplayWindowPosition.Value;
                    rectTransform.eulerAngles = ConfigManager.StatsDisplayWindowAngle.Value;
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

                if (ConfigManager.StatsDisplayPanelBackground.Value)
                {
                    Image image = statsDisplayContainer.AddComponent<UnityEngine.UI.Image>();
                    Image copyImage = BetterUIPlugin.objectivePanelController.objectiveTrackerContainer.parent.GetComponent<Image>();
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
        }
        static void onUpdate()
        {
            if (ConfigManager.StatsDisplayAttachToObjectivePanel.Value)
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
            if (BetterUIPlugin.hud && BetterUIPlugin.hud.targetBodyObject && textMesh)
            {
                playerBody = BetterUIPlugin.hud.targetBodyObject ? BetterUIPlugin.hud.targetBodyObject.GetComponent<CharacterBody>() : null;
                if (playerBody)
                {
                    bool customBindPressed = Input.GetKey(ConfigManager.StatsDisplayCustomBind.Value);
                    if (Input.GetKeyDown(ConfigManager.StatsDisplayCustomBind.Value)) statsDisplayToggle = !statsDisplayToggle;
                    bool showStatsDisplay = ConfigManager.StatsDisplayToggleOnBind.Value ? statsDisplayToggle : !(ConfigManager.StatsDisplayShowCustomBindOnly.Value && !customBindPressed);

                    highestMultikill = playerBody.multiKillCount > highestMultikill ? playerBody.multiKillCount : highestMultikill;

                    statsDisplayContainer.SetActive(showStatsDisplay);
                    if (showStatsDisplay)
                    {
                        BetterUIPlugin.sharedStringBuilder.Clear();
                        if (customBindPressed)
                        {
                            for (int i = 0; i < altText.Length; i++)
                            {
                                if(i % 2 == 0)
                                {
                                    BetterUIPlugin.sharedStringBuilder.Append(altText[i]);
                                }
                                else
                                {
                                    BetterUIPlugin.sharedStringBuilder.Append(regexmap[altText[i]](playerBody));
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < normalText.Length; i++)
                            {
                                if (i % 2 == 0)
                                {
                                    BetterUIPlugin.sharedStringBuilder.Append(normalText[i]);

                                }
                                else
                                {
                                    BetterUIPlugin.sharedStringBuilder.Append(regexmap[normalText[i]](playerBody));
                                }
                            }
                        }
                        textMesh.SetText(BetterUIPlugin.sharedStringBuilder);
                    }
                }
            }
        }
    }
}
