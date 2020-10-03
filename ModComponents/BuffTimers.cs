using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using UnityEngine;
using RoR2;

namespace BetterUI
{
    class BuffTimers : BetterUI.ModComponent
    {
        IEnumerable<CharacterBody.TimedBuff> timedBuffs;
        CharacterBody.TimedBuff thisBuff;
        public BuffTimers(BetterUI mod) : base(mod){ }

        internal override void Hook()
        {
            if (mod.config.BuffTimers.Value || mod.config.BuffTooltips.Value)
            {
                On.RoR2.UI.BuffIcon.Awake += mod.buffTimers.hook_BuffIconAwake;
                On.RoR2.UI.BuffIcon.UpdateIcon += mod.buffTimers.hook_BuffIconUpdateIcon;
            }
        }
        internal void hook_BuffIconAwake(On.RoR2.UI.BuffIcon.orig_Awake orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            if (self.transform.parent.name == "BuffDisplayRoot")
            {
                if (mod.config.BuffTooltips.Value)
                {
                    UnityEngine.UI.GraphicRaycaster raycaster = self.transform.parent.GetComponent<UnityEngine.UI.GraphicRaycaster>();
                    if (raycaster == null)
                    {
                        self.transform.parent.gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                    }
                    self.gameObject.AddComponent<RoR2.UI.TooltipProvider>();
                }
                if (mod.config.BuffTimers.Value)
                {
                    GameObject TimerText = new GameObject("TimerText");
                    RectTransform timerRect = TimerText.AddComponent<RectTransform>();
                    RoR2.UI.HGTextMeshProUGUI timerTextMesh = TimerText.AddComponent<RoR2.UI.HGTextMeshProUGUI>();
                    TimerText.transform.SetParent(self.transform);

                    timerTextMesh.enableWordWrapping = false;
                    timerTextMesh.alignment = mod.config.BuffTimersTextAlignmentOption;
                    timerTextMesh.fontSize = mod.config.BuffTimersFontSize.Value;
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

        internal void hook_BuffIconUpdateIcon(On.RoR2.UI.BuffIcon.orig_UpdateIcon orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            BuffDef buffDef = BuffCatalog.GetBuffDef(self.buffIndex);
            if (buffDef != null && self.transform.parent.name == "BuffDisplayRoot")
            {
                if (mod.config.BuffTooltips.Value)
                {
                    RoR2.UI.TooltipProvider tooltipProvider = self.GetComponent<RoR2.UI.TooltipProvider>();
                    tooltipProvider.overrideTitleText = buffDef.name;
                    tooltipProvider.titleColor = buffDef.buffColor;
                }
                if (mod.config.BuffTimers.Value)
                {
                    Transform timerText = self.transform.Find("TimerText");
                    if (timerText != null)
                    {
                        if (mod.HUD != null)
                        {
                            CharacterBody characterBody = mod.HUD.targetBodyObject ? mod.HUD.targetBodyObject.GetComponent<CharacterBody>() : null;
                            if (characterBody != null && characterBody.timedBuffs.Count > 0)
                            {
                                timedBuffs = characterBody.timedBuffs.Where(b => b.buffIndex == self.buffIndex);
                                if(timedBuffs.Any())
                                {
                                    thisBuff = timedBuffs.OrderByDescending(b => b.timer).First();
                                    timerText.GetComponent<RoR2.UI.HGTextMeshProUGUI>().text = thisBuff.timer < 10 && mod.config.BuffTimersDecimal.Value ? thisBuff.timer.ToString("N1") : thisBuff.timer.ToString("N0");
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
