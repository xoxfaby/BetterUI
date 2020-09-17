using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BepInEx;
using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.UI;


namespace BetterUI
{

    class DPSMeter
    {
        private readonly BetterUI mod;
        private List<DamageLog> characterDamageLog = new List<DamageLog>();
        private List<DamageLog> minionDamageLog = new List<DamageLog>();

        private HGTextMeshProUGUI textMesh;
        public float DPS { get => MinionDPS + CharacterDPS; }
        public float MinionDPS;
        public float CharacterDPS;
        internal struct DamageLog
        {
            public float damage;
            public DateTime time;
            public DamageLog(float dmg)
            {
                damage = dmg;
                time = DateTime.UtcNow;
            }
        }
        public DPSMeter(BetterUI m)
        {
            mod = m;
        }
        public float Clamp(float value)
        {
            return Math.Min(Math.Max(1, value), mod.config.DPSMeterTimespan.Value);
        }
        public void Update()
        {
            if (characterDamageLog.Count > 0)
            {
                float damageSum = 0;
                int i = characterDamageLog.Count - 1;
                var now = DateTime.UtcNow;
                while (i >= 0)
                {
                    if ((now - characterDamageLog[i].time).Seconds < 5)
                    {
                        damageSum += characterDamageLog[i].damage;
                        i--;
                    }
                    else
                    {
                        characterDamageLog.RemoveRange(0, i);
                        break;
                    }
                }
                CharacterDPS = damageSum / Clamp((characterDamageLog.First().time - characterDamageLog.Last().time).Seconds);
            }
            if (minionDamageLog.Count > 0)
            {
                float damageSum = 0;
                int i = minionDamageLog.Count - 1;
                var now = DateTime.UtcNow;
                while (i >= 0)
                {
                    if ((now - minionDamageLog[i].time).Seconds < 5)
                    {
                        damageSum += minionDamageLog[i].damage;
                        i--;
                    }
                    else
                    {
                        minionDamageLog.RemoveRange(0, i);
                        break;
                    }
                }
                MinionDPS = damageSum / Clamp((minionDamageLog.First().time - minionDamageLog.Last().time).Seconds);
            }
            if (textMesh != null)
            {
                textMesh.text = "DPS: " + (mod.config.DPSMeterWindowIncludeMinions.Value ? DPS : CharacterDPS).ToString("N0") ;
            }
        }

        public void hook_ClientDamageNotified(On.RoR2.GlobalEventManager.orig_ClientDamageNotified orig, DamageDealtMessage dmgMsg)
        {
            orig(dmgMsg);

            CharacterMaster localMaster = LocalUserManager.GetFirstLocalUser().cachedMasterController.master;



            if (dmgMsg != null && dmgMsg.attacker != null)
            {
                if (dmgMsg.attacker == localMaster.GetBodyObject())
                {
                    characterDamageLog.Add(new DamageLog(dmgMsg.damage));
                }
                else if (dmgMsg.attacker.GetComponent<CharacterBody>() != null &&
                    dmgMsg.attacker.GetComponent<CharacterBody>().master != null &&
                    dmgMsg.attacker.GetComponent<CharacterBody>().master.minionOwnership != null &&
                    dmgMsg.attacker.GetComponent<CharacterBody>().master.minionOwnership.ownerMasterId == localMaster.netId)
                { 
                    minionDamageLog.Add(new DamageLog(dmgMsg.damage));
                }
            }
        }

        public void hook_Awake(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);

            GameObject DPSMeterPanel = new GameObject("DPSMeterPanel");
            RectTransform rectTransform = DPSMeterPanel.AddComponent<RectTransform>();

            DPSMeterPanel.transform.SetParent(self.mainContainer.transform);
            DPSMeterPanel.transform.SetAsFirstSibling();



            GameObject DPSMeterText = new GameObject("DPSMeterText");
            RectTransform rectTransform2 = DPSMeterText.AddComponent<RectTransform>();
            textMesh = DPSMeterText.AddComponent<HGTextMeshProUGUI>();

            DPSMeterText.transform.SetParent(DPSMeterPanel.transform);


            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.anchorMin = mod.config.DPSMeterWindowAnchorMin.Value;
            rectTransform.anchorMax = mod.config.DPSMeterWindowAnchorMax.Value;
            rectTransform.localScale = Vector3.one;
            rectTransform.pivot = mod.config.DPSMeterWindowPivot.Value;
            rectTransform.sizeDelta = mod.config.DPSMeterWindowSize.Value;
            rectTransform.anchoredPosition = mod.config.DPSMeterWindowPosition.Value;
            rectTransform.eulerAngles = mod.config.DPSMeterWindowAngle.Value;
             
            rectTransform2.localPosition = Vector3.zero;
            rectTransform2.anchorMin = Vector2.zero;
            rectTransform2.anchorMax = Vector2.one;
            rectTransform2.localScale = Vector3.one;
            rectTransform2.sizeDelta = new Vector2(-12, -12);
            rectTransform2.anchoredPosition = Vector2.zero;

            if (mod.config.DPSMeterWindowBackground.Value)
            {
                Image image = DPSMeterPanel.AddComponent<Image>();
                Image copyImage = self.itemInventoryDisplay.gameObject.GetComponent<Image>();
                image.sprite = copyImage.sprite;
                image.color = copyImage.color;
                image.type = Image.Type.Sliced;
            }

            textMesh.enableAutoSizing = true;
            textMesh.fontSizeMax = 256;
            textMesh.faceColor = Color.white;
            textMesh.alignment = TMPro.TextAlignmentOptions.MidlineLeft;
        }
    }
}
