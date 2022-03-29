using System;
using System.Collections;
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

    static class DPSMeter
    {

        private static readonly Queue<DamageLog> characterDamageLog = new Queue<DamageLog>();
        private static float characterDamageSum = 0;
        private static readonly Queue<DamageLog> minionDamageLog = new Queue<DamageLog>();
        private static float minionDamageSum = 0;

        private static GameObject DPSMeterPanel;
        private static RoR2.UI.ChatBox chatBox;

        private static HGTextMeshProUGUI textMesh;
        public static float DPS { get => MinionDPS + CharacterDPS; }
        public static float CharacterDPS { get => characterDamageLog.Count > 0 ? characterDamageSum / Clamp(Time.time - characterDamageLog.Peek().time) : 0; }
        public static float MinionDPS { get => minionDamageLog.Count > 0 ? minionDamageSum / Clamp(Time.time - minionDamageLog.Peek().time) : 0; }
        internal struct DamageLog
        {
            public float damage;
            public float time;
            public DamageLog(float dmg)
            {
                damage = dmg;
                time = Time.time;
            }
        }

        internal static void Initialize()
        {
            BetterUIPlugin.Hooks.Add<GlobalEventManager, DamageDealtMessage>("ClientDamageNotified", DamageDealtMessage_ClientDamageNotified);

            BetterUIPlugin.onEnable += () => BetterUIPlugin.onUpdate += onUpdate;
            BetterUIPlugin.onDisable += () => BetterUIPlugin.onUpdate -= onUpdate;

            if (ConfigManager.DPSMeterWindowShow.Value)
            {
                BetterUIPlugin.onEnable += () => BetterUIPlugin.onHUDAwake += onHUDAwake;
                BetterUIPlugin.onDisable += () => BetterUIPlugin.onHUDAwake -= onHUDAwake;

                if (ConfigManager.DPSMeterWindowHideWhenTyping.Value) BetterUIPlugin.Hooks.Add<RoR2.UI.ChatBox>(nameof(RoR2.UI.ChatBox.Awake), ChatBox_Awake);
            }
        }

        public static float Clamp(float value)
        {
            return Math.Min(Math.Max(1, value), ConfigManager.DPSMeterTimespan.Value);
        }

   
        private static void onUpdate()
        {
            while(characterDamageLog.Count > 0 && characterDamageLog.Peek().time < Time.time - ConfigManager.DPSMeterTimespan.Value)
            {
                characterDamageSum -= characterDamageLog.Dequeue().damage;
            }
            while (minionDamageLog.Count > 0 && minionDamageLog.Peek().time < Time.time - ConfigManager.DPSMeterTimespan.Value)
            {
                minionDamageSum -= minionDamageLog.Dequeue().damage;
            }
            if (textMesh != null)
            {
                BetterUIPlugin.sharedStringBuilder.Clear();
                BetterUIPlugin.sharedStringBuilder.Append("DPS: ");
                BetterUIPlugin.sharedStringBuilder.Append((ConfigManager.DPSMeterWindowIncludeMinions.Value ? DPS : CharacterDPS).ToString("N0"));

                textMesh.SetText(BetterUIPlugin.sharedStringBuilder);
            }
            if (chatBox != null && DPSMeterPanel != null) DPSMeterPanel.gameObject.SetActive(!chatBox.showInput);
        }

        public static void ChatBox_Awake(Action<RoR2.UI.ChatBox> orig, RoR2.UI.ChatBox self)
        {
            orig(self);
            chatBox = self;
        }
        public static void DamageDealtMessage_ClientDamageNotified(Action<DamageDealtMessage> orig, DamageDealtMessage dmgMsg)
        {
            orig(dmgMsg);

            CharacterMaster localMaster = LocalUserManager.GetFirstLocalUser().cachedMasterController.master;

            if (dmgMsg.attacker && dmgMsg.victim) 
            {
                var victimBody = dmgMsg.victim.gameObject.GetComponent<CharacterBody>();
                if (victimBody && victimBody.teamComponent.teamIndex != TeamIndex.Player)
                {
                    if (dmgMsg.attacker == localMaster.GetBodyObject())
                    {
                        characterDamageSum += dmgMsg.damage;
                        characterDamageLog.Enqueue(new DamageLog(dmgMsg.damage));
                    }
                    else
                    {
                        var attackerBody = dmgMsg.attacker.GetComponent<CharacterBody>();
                        if (attackerBody && attackerBody.master && attackerBody.master.minionOwnership && attackerBody.master.minionOwnership.ownerMasterId == localMaster.netId)
                        {
                            minionDamageSum += dmgMsg.damage;
                            minionDamageLog.Enqueue(new DamageLog(dmgMsg.damage));
                        }
                    }
                }
            }
        }



        private static void onHUDAwake(HUD self)
        {
            if (DPSMeterPanel == null)
            {

                DPSMeterPanel = new GameObject("DPSMeterPanel");
                RectTransform rectTransform = DPSMeterPanel.AddComponent<RectTransform>();

                DPSMeterPanel.transform.SetParent(BetterUIPlugin.hud.mainContainer.transform);
                DPSMeterPanel.transform.SetAsFirstSibling();



                GameObject DPSMeterText = new GameObject("DPSMeterText");
                RectTransform rectTransform2 = DPSMeterText.AddComponent<RectTransform>();
                textMesh = DPSMeterText.AddComponent<HGTextMeshProUGUI>();

                DPSMeterText.transform.SetParent(DPSMeterPanel.transform);


                rectTransform.localPosition = Vector3.zero;
                rectTransform.anchorMin = ConfigManager.DPSMeterWindowAnchorMin.Value;
                rectTransform.anchorMax = ConfigManager.DPSMeterWindowAnchorMax.Value;
                rectTransform.localScale = Vector3.one;
                rectTransform.pivot = ConfigManager.DPSMeterWindowPivot.Value;
                rectTransform.sizeDelta = ConfigManager.DPSMeterWindowSize.Value;
                rectTransform.anchoredPosition = ConfigManager.DPSMeterWindowPosition.Value;
                rectTransform.eulerAngles = ConfigManager.DPSMeterWindowAngle.Value;


                DPSMeterPanel.transform.SetParent(BetterUIPlugin.hud.mainUIPanel.transform);

                rectTransform2.localPosition = Vector3.zero;
                rectTransform2.anchorMin = Vector2.zero;
                rectTransform2.anchorMax = Vector2.one;
                rectTransform2.localScale = Vector3.one;
                rectTransform2.sizeDelta = new Vector2(-12, -12);
                rectTransform2.anchoredPosition = Vector2.zero;

                if (ConfigManager.DPSMeterWindowBackground.Value)
                {
                    Image image = DPSMeterPanel.AddComponent<Image>();
                    Image copyImage = BetterUIPlugin.hud.itemInventoryDisplay.gameObject.GetComponent<Image>();
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
}
