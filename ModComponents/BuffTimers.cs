using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using UnityEngine;
using RoR2;

namespace BetterUI
{
    static class BuffTimers
    {
        static IEnumerable<CharacterBody.TimedBuff> timedBuffs;
        static CharacterBody.TimedBuff thisBuff;
        internal static void Hook()
        {
            if (ConfigManager.BuffTimers.Value || ConfigManager.BuffTooltips.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.BuffIcon>("Awake", BuffIcon_Awake);
                BetterUIPlugin.Hooks.Add<RoR2.UI.BuffIcon>("UpdateIcon", BuffIcon_UpdateIcon);
            }
        }
        internal static void BuffIcon_Awake(Action<RoR2.UI.BuffIcon> orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            if (self.transform.parent.name == "BuffDisplayRoot")
            {
                if (ConfigManager.BuffTooltips.Value)
                {
                    UnityEngine.UI.GraphicRaycaster raycaster = self.transform.parent.GetComponent<UnityEngine.UI.GraphicRaycaster>();
                    if (raycaster == null)
                    {
                        self.transform.parent.gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                    }
                    self.gameObject.AddComponent<RoR2.UI.TooltipProvider>();
                }
                if (ConfigManager.BuffTimers.Value)
                {
                    GameObject TimerText = new GameObject("TimerText");
                    RectTransform timerRect = TimerText.AddComponent<RectTransform>();
                    RoR2.UI.HGTextMeshProUGUI timerTextMesh = TimerText.AddComponent<RoR2.UI.HGTextMeshProUGUI>();
                    TimerText.transform.SetParent(self.transform);

                    timerTextMesh.enableWordWrapping = false;
                    timerTextMesh.alignment = ConfigManager.BuffTimersTextAlignmentOption;
                    timerTextMesh.fontSize = ConfigManager.BuffTimersFontSize.Value;
                    timerTextMesh.faceColor = Color.white;
                    timerTextMesh.text = "";

                    timerRect.localPosition = Vector3.zero;
                    timerRect.anchorMin = new Vector2(1, 0);
                    timerRect.anchorMax = new Vector2(1, 0);
                    timerRect.localScale = Vector3.one;
                    timerRect.sizeDelta = new Vector2(48, 48);
                    timerRect.anchoredPosition = new Vector2(-24, 24);
                }
            }
        }

        internal static void BuffIcon_UpdateIcon(Action<RoR2.UI.BuffIcon> orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            if (self.buffDef && self.transform.parent.name == "BuffDisplayRoot")
            {
                if (ConfigManager.BuffTooltips.Value)
                {
                    RoR2.UI.TooltipProvider tooltipProvider = self.GetComponent<RoR2.UI.TooltipProvider>();
                    tooltipProvider.overrideTitleText = self.buffDef.name;
                    tooltipProvider.titleColor = self.buffDef.buffColor;
                }
                if (ConfigManager.BuffTimers.Value)
                {
                    Transform timerText = self.transform.Find("TimerText");
                    if (timerText != null)
                    {
                        if (BetterUIPlugin.HUD != null)
                        {
                            CharacterBody characterBody = BetterUIPlugin.HUD.targetBodyObject ? BetterUIPlugin.HUD.targetBodyObject.GetComponent<CharacterBody>() : null;
                            if (characterBody != null && characterBody.timedBuffs.Count > 0)
                            {
                                timedBuffs = characterBody.timedBuffs.Where(b => b.buffIndex == self.buffDef.buffIndex);
                                if(timedBuffs.Any())
                                {
                                    thisBuff = timedBuffs.OrderByDescending(b => b.timer).First();
                                    timerText.GetComponent<RoR2.UI.HGTextMeshProUGUI>().text = thisBuff.timer < 10 && ConfigManager.BuffTimersDecimal.Value ? thisBuff.timer.ToString("N1") : thisBuff.timer.ToString("N0");
                                    return;
                                }
                            }
                        }
                        timerText.GetComponent<RoR2.UI.HGTextMeshProUGUI>().text = "";
                    }
                }
            }
        }

    }
}
