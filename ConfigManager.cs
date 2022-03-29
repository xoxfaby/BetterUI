using BepInEx;
using BepInEx.Configuration;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RoR2;

namespace BetterUI
{
    internal static class ConfigManager
    {
        // Files 

        public static ConfigFile ConfigFileComponents;
        public static ConfigFile ConfigFileMisc;
        public static ConfigFile ConfigFileAdvancedIcons;
        public static ConfigFile ConfigFileBuffs;
        public static ConfigFile ConfigFileCommandImprovements;
        public static ConfigFile ConfigFileDPSMeter;
        public static ConfigFile ConfigFileItemCounters;
        public static ConfigFile ConfigFileStatsDisplay;
        public static ConfigFile ConfigFileSorting;

        // Components

        public static ConfigEntry<bool> ComponentsAdvancedIcons;
        public static ConfigEntry<bool> ComponentsBuffTimers;
        public static ConfigEntry<bool> ComponentsCommandImprovements;
        public static ConfigEntry<bool> ComponentsDPSMeter;
        public static ConfigEntry<bool> ComponentsItemCounters;
        public static ConfigEntry<bool> ComponentsItemSorting;
        public static ConfigEntry<bool> ComponentsMisc;
        public static ConfigEntry<bool> ComponentsStatsDisplay;

        // Misc

        public static ConfigEntry<bool> MiscShowHidden;
        public static ConfigEntry<bool> MiscAdvancedPickupNotificationsItems;
        public static ConfigEntry<bool> MiscAdvancedPickupNotificationsEquipements;
        public static ConfigEntry<bool> MiscHidePickupNotificiationsItems;
        public static ConfigEntry<bool> MiscHidePickupNotificiationsEquipements;
        public static ConfigEntry<bool> MiscHidePickupNotificiationsArtifacts;
        public static ConfigEntry<bool> MiscShowPickupDescription;
        public static ConfigEntry<bool> MiscPickupDescriptionAdvanced;

        // AdvancedIcons

        public static ConfigEntry<bool> AdvancedIconsItemAdvancedDescriptions;
        public static ConfigEntry<bool> AdvancedIconsItemItemStatsIntegration;
        public static ConfigEntry<bool> AdvancedIconsEquipementShowCooldownStacks;
        public static ConfigEntry<bool> AdvancedIconsEquipementAdvancedDescriptions;
        public static ConfigEntry<bool> AdvancedIconsEquipementShowBaseCooldown;
        public static ConfigEntry<bool> AdvancedIconsEquipementShowCalculatedCooldown;
        public static ConfigEntry<bool> AdvancedIconsSkillShowCooldownStacks;
        public static ConfigEntry<bool> AdvancedIconsSkillShowBaseCooldown;
        public static ConfigEntry<bool> AdvancedIconsSkillShowCalculatedCooldown;
        public static ConfigEntry<bool> AdvancedIconsSkillShowProcCoefficient;
        public static ConfigEntry<bool> AdvancedIconsSkillCalculateSkillProcEffects;

        // Buffs

        public static ConfigEntry<bool> BuffTimers;
        public static ConfigEntry<bool> BuffTimersDecimal;
        public static ConfigEntry<bool> BuffTooltips;
        public static ConfigEntry<String> BuffTimersPosition;
        public static TMPro.TextAlignmentOptions BuffTimersTextAlignmentOption;
        public static ConfigEntry<float> BuffTimersFontSize;

        // CommandImprovements

        public static ConfigEntry<bool> CommandResizeCommandWindow;
        public static ConfigEntry<bool> CommandRemoveBackgroundBlur;
        public static ConfigEntry<bool> CommandCloseOnEscape;
        public static ConfigEntry<bool> CommandCloseOnWASD;
        public static ConfigEntry<String> CommandCloseOnCustom;
        public static ConfigEntry<bool> CommandTooltipsShow;
        public static ConfigEntry<bool> CommandTooltipsItemStatsBeforeAfter;
        public static ConfigEntry<bool> CommandCountersShow;
        public static ConfigEntry<bool> CommandCountersHideOnZero;
        public static ConfigEntry<String> CommandCountersPosition;
        public static TMPro.TextAlignmentOptions CommandCountersTextAlignmentOption;
        public static ConfigEntry<float> CommandCountersFontSize;
        public static ConfigEntry<String> CommandCountersPrefix;

        // DPSMeter

        public static ConfigEntry<float> DPSMeterTimespan;
        public static ConfigEntry<bool> DPSMeterWindowShow;
        public static ConfigEntry<bool> DPSMeterWindowIncludeMinions;
        public static ConfigEntry<bool> DPSMeterWindowBackground;
        public static ConfigEntry<bool> DPSMeterWindowHideWhenTyping;
        public static ConfigEntry<Vector2> DPSMeterWindowAnchorMin;
        public static ConfigEntry<Vector2> DPSMeterWindowAnchorMax;
        public static ConfigEntry<Vector2> DPSMeterWindowPosition;
        public static ConfigEntry<Vector2> DPSMeterWindowPivot;
        public static ConfigEntry<Vector2> DPSMeterWindowSize;
        public static ConfigEntry<Vector3> DPSMeterWindowAngle;

        // ItemCounters

        public static ConfigEntry<bool> ItemCountersShowItemCounters;
        public static ConfigEntry<bool> ItemCountersShowItemScore;
        public static ConfigEntry<bool> ItemCountersItemScoreFromTier;
        public static ConfigEntry<bool> ItemCountersShowItemSum;
        public static ConfigEntry<string> ItemCountersItemSumTiersString;
        public static List<ItemTier> ItemCountersItemSumTiers;
        public static ConfigEntry<bool> ItemCountersShowItemsByTier;
        public static ConfigEntry<string> ItemCountersItemsByTierOrderString;
        public static List<ItemTier> ItemCountersItemsByTierOrder;
        public static ConfigEntry<float> ItemCountersTierScoreTier1;
        public static ConfigEntry<float> ItemCountersTierScoreTier2;
        public static ConfigEntry<float> ItemCountersTierScoreTier3;
        public static ConfigEntry<float> ItemCountersTierScoreLunar;
        public static ConfigEntry<float> ItemCountersTierScoreBoss;
        public static ConfigEntry<float> ItemCountersTierScoreNoTier;
        public static Dictionary<ItemTier, float> ItemCountersTierScores;
        public static Dictionary<ItemDef, float> ItemCountersItemScores;

        // StatsDisplay

        public static ConfigEntry<bool> StatsDisplayEnable;
        public static ConfigEntry<String> StatsDisplayStatString;
        public static ConfigEntry<String> StatsDisplayStatStringCustomBind;
        public static ConfigEntry<String> StatsDisplayCustomBind;
        public static ConfigEntry<bool> StatsDisplayShowCustomBindOnly;
        public static ConfigEntry<bool> StatsDisplayToggleOnBind;
        public static ConfigEntry<bool> StatsDisplayPanelBackground;
        public static ConfigEntry<bool> StatsDisplayAttachToObjectivePanel;
        public static ConfigEntry<Vector2> StatsDisplayWindowAnchorMin;
        public static ConfigEntry<Vector2> StatsDisplayWindowAnchorMax;
        public static ConfigEntry<Vector2> StatsDisplayWindowPosition;
        public static ConfigEntry<Vector2> StatsDisplayWindowPivot;
        public static ConfigEntry<Vector2> StatsDisplayWindowSize;
        public static ConfigEntry<Vector3> StatsDisplayWindowAngle;

        // Sorting

        public static ConfigEntry<bool> SortingSortItemsInventory;
        public static ConfigEntry<bool> SortingSortItemsCommand;
        public static ConfigEntry<bool> SortingSortItemsScrapper;
        public static ConfigEntry<String> SortingTierOrderString;
        public static int[] SortingTierOrder;
        public static ConfigEntry<String> SortingSortOrder;
        public static ConfigEntry<String> SortingSortOrderCommand;
        public static ConfigEntry<String> SortingSortOrderScrapper;

        // Internal

        /*
         * NOTE:
         *  When renaming a config entry within the same config file, place both the new Section + Key
         *  and the old Section + Key in the dictionary below.
         *
         *  By creating a rename mapping, we're able to
         *      1) transfer the old entry's value to the new entry's value.
         *      2) remove the old entry from the config file.
         *
         *  This let's us update config files without users' configs suddenly changing.
         */
        static IReadOnlyDictionary<(string, string), (string, string)> previousEntryMap = new Dictionary<(string, string), (string, string)>()
        {
            // Current config section & key pairs.                  Previous config section & key pairs.
            { ("Components", "AdvancedIcons"),                      ("Components", "AdvanedIcons") },
            { ("Misc", "AdvancedPickupNotificationsEquipment"),     ("Misc", "AdvancedPickupNotificationsEquipements") },
            { ("Misc", "HidePickupNotificationsItems"),             ("Misc", "HidePickupNotificiationsItems") },
            { ("Misc", "HidePickupNotificationsEquipment"),         ("Misc", "HidePickupNotificiationsEquipements") },
            { ("Misc", "HidePickupNotificationsArtifacts"),         ("Misc", "HidePickupNotificiationsArtifacts") },
            { ("Equipment Improvements", "ShowCooldownStacks"),     ("Equipement Improvements", "ShowCooldownStacks") },
            { ("Equipment Improvements", "AdvancedDescriptions"),   ("Equipement Improvements", "AdvancedDescriptions") },
            { ("Equipment Improvements", "BaseCooldown"),           ("Equipement Improvements", "BaseCooldown") },
            { ("Equipment Improvements", "CalculatedCooldown"),     ("Equipement Improvements", "CalculatedCooldown") },
            { ("Components", "Buffs"),                              ("Components", "BuffTimers") },
        };

        static ConfigManager()
        {
            ConfigFileComponents = new ConfigFile(Paths.ConfigPath + "\\BetterUI-Components.cfg", true);
            ConfigFileMisc = new ConfigFile(Paths.ConfigPath + "\\BetterUI-Misc.cfg", true);
            ConfigFileAdvancedIcons = new ConfigFile(Paths.ConfigPath + "\\BetterUI-AdvancedIcons.cfg", true);
            ConfigFileBuffs = new ConfigFile(Paths.ConfigPath + "\\BetterUI-Buffs.cfg", true);
            ConfigFileCommandImprovements = new ConfigFile(Paths.ConfigPath + "\\BetterUI-CommandImprovements.cfg", true);
            ConfigFileDPSMeter = new ConfigFile(Paths.ConfigPath + "\\BetterUI-DPSMeter.cfg", true);
            ConfigFileItemCounters = new ConfigFile(Paths.ConfigPath + "\\BetterUI-ItemCounters.cfg", true);
            ConfigFileStatsDisplay = new ConfigFile(Paths.ConfigPath + "\\BetterUI-StatsDisplay.cfg", true);
            ConfigFileSorting = new ConfigFile(Paths.ConfigPath + "\\BetterUI-Sorting.cfg", true);

            // Components

            ComponentsAdvancedIcons = Bind(ConfigFileComponents, "Components", "AdvancedIcons", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsBuffTimers = Bind(ConfigFileComponents, "Components", "Buffs", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsCommandImprovements = Bind(ConfigFileComponents, "Components", "CommandImprovements", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsDPSMeter = Bind(ConfigFileComponents, "Components", "DPSMeter", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsItemCounters = Bind(ConfigFileComponents, "Components", "ItemCounters", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsItemSorting = Bind(ConfigFileComponents, "Components", "ItemSorting", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsMisc = Bind(ConfigFileComponents, "Components", "Misc", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsStatsDisplay = Bind(ConfigFileComponents, "Components", "StatsDisplay", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            // Misc

            MiscShowHidden = Bind(ConfigFileMisc, "Misc", "ShowHidden", false, "Show hidden items in the item inventory.");

            MiscAdvancedPickupNotificationsItems = Bind(ConfigFileMisc, "Misc", "AdvancedPickupNotificationsItems", false, "Show advanced descriptions when picking up an item.");

            MiscAdvancedPickupNotificationsEquipements = Bind(ConfigFileMisc, "Misc", "AdvancedPickupNotificationsEquipment", false, "Show advanced descriptions when picking up equipment.");

            MiscHidePickupNotificiationsItems = Bind(ConfigFileMisc, "Misc", "HidePickupNotificationsItems", false, "Hide pickup notifications for items.");

            MiscHidePickupNotificiationsEquipements = Bind(ConfigFileMisc, "Misc", "HidePickupNotificationsEquipment", false, "Hide pickup notifications for equipment.");

            MiscHidePickupNotificiationsArtifacts = Bind(ConfigFileMisc, "Misc", "HidePickupNotificationsArtifacts", false, "Hide pickup notifications for artifacts.");

            MiscShowPickupDescription = Bind(ConfigFileMisc, "Misc", "ShowPickupDescription", true, "Show the item description on the interaction pop-up.");

            MiscPickupDescriptionAdvanced = Bind(ConfigFileMisc, "Misc", "PickupDescriptionAdvanced", false, "Show advanced descriptions for the interaction pop-up.");

            // Advanced Icons

            AdvancedIconsItemAdvancedDescriptions = Bind(ConfigFileAdvancedIcons, "Item Improvements", "AdvancedDescriptions", true, "Show advanced descriptions when hovering over an item.");

            AdvancedIconsItemItemStatsIntegration = Bind(ConfigFileAdvancedIcons, "Item Improvements", "ItemStatsIntegration", true, "Show ItemStats where applicable.");

            AdvancedIconsEquipementShowCooldownStacks = Bind(ConfigFileAdvancedIcons, "Equipment Improvements", "ShowCooldownStacks", true, "Show the cooldown for your equipment when charging multiple stacks.");

            AdvancedIconsEquipementAdvancedDescriptions = Bind(ConfigFileAdvancedIcons, "Equipment Improvements", "AdvancedDescriptions", true, "Show advanced descriptions when hovering over equipment.");

            AdvancedIconsEquipementShowBaseCooldown = Bind(ConfigFileAdvancedIcons, "Equipment Improvements", "BaseCooldown", true, "Show the base cooldown when hovering over equipment.");

            AdvancedIconsEquipementShowCalculatedCooldown = Bind(ConfigFileAdvancedIcons, "Equipment Improvements", "CalculatedCooldown", true, "Show the calculated cooldown based on your items when hovering over equipment.");

            AdvancedIconsSkillShowCooldownStacks = Bind(ConfigFileAdvancedIcons, "Skill Improvements", "ShowCooldownStacks", true, "Show the cooldown for skills when charging multiple stacks.");

            AdvancedIconsSkillShowBaseCooldown = Bind(ConfigFileAdvancedIcons, "Skill Improvements", "BaseCooldown", true, "Show the base cooldown when hovering over a skill.");

            AdvancedIconsSkillShowCalculatedCooldown = Bind(ConfigFileAdvancedIcons, "Skill Improvements", "CalculatedCooldown", true, "Show the calculated cooldown based on your items when hovering over a skill.");

            AdvancedIconsSkillShowProcCoefficient = Bind(ConfigFileAdvancedIcons, "Skill Improvements", "ShowProcCoefficient", true, "Show the proc coefficient when hovering over a skill.");

            AdvancedIconsSkillCalculateSkillProcEffects = Bind(ConfigFileAdvancedIcons, "Skill Improvements", "CalculateProcEffects", true, "Show the effects of carried items, adjusted for each skill's proc coefficient.");

            // Buffs

            BuffTimers = Bind(ConfigFileBuffs, "Buffs", "BuffTimers", true, "Show buff timers (host only).");

            BuffTimersDecimal = Bind(ConfigFileBuffs, "Buffs", "BuffTimersDecimal", true, "Show 1 decimal point when timer is below 10.");

            BuffTooltips = Bind(ConfigFileBuffs, "Buffs", "BuffTooltips", true, "Show buff tooltips.");

            BuffTimersPosition = Bind(ConfigFileBuffs, "Buffs", "CountersPosition", "TopRight",
               "Location of buff timer text.\n" +
               "Valid options:\n" +
               "TopLeft\n" +
               "TopRight\n" +
               "BottomLeft\n" +
               "BottomRight\n" +
               "Center\n");

            BuffTimersTextAlignmentOption = (TMPro.TextAlignmentOptions)Enum.Parse(typeof(TMPro.TextAlignmentOptions), BuffTimersPosition.Value, true);

            BuffTimersFontSize = Bind(ConfigFileBuffs, "Buffs", "CountersFontSize", 23f, "Size of the buff timer text.");

            // Command / Scrapper Improvements

            CommandResizeCommandWindow = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "ResizeCommandWindow", true, "Resize the command window depending on the number of items.");

            CommandRemoveBackgroundBlur = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "RemoveBackgroundBlur", true, "Remove the blur behind the command window that hides the rest of the UI.");

            CommandCloseOnEscape = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "CloseOnEscape", true, "Close the command/scrapper window when you press escape.");

            CommandCloseOnWASD = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "CloseOnWASD", true, "Close the command/scrapper window when you press W, A, S, or D.");

            CommandCloseOnCustom = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "CloseOnCustom", "", "Close the command/scrapper window when you press the key selected here.\n" +
                "Example: space\n" +
                "Must be lowercase. Leave blank to disable.");

            CommandTooltipsShow = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "TooltipsShow", true, "Show tooltips in the command and scrapper windows.");

            CommandTooltipsItemStatsBeforeAfter = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "TooltipsItemStatsBeforeAfter", true, "Show the ItemStats before and after picking up the item.");

            CommandCountersShow = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "CountersShow", true, "Show counters in the command and scrapper windows.");

            CommandCountersHideOnZero = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "CountersHideOnZero", false, "Hide counters when they are zero.");

            CommandCountersPosition = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "CountersPosition", "TopRight",
                "Location of the command item counter.\n" +
                "Valid options:\n" +
                "TopLeft\n" +
                "TopRight\n" +
                "BottomLeft\n" +
                "BottomRight\n" +
                "Center\n");

            CommandCountersTextAlignmentOption = (TMPro.TextAlignmentOptions)Enum.Parse(typeof(TMPro.TextAlignmentOptions), CommandCountersPosition.Value, true);

            CommandCountersFontSize = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "CountersFontSize", 20f, "Size of the command item counter text.");

            CommandCountersPrefix = Bind(ConfigFileCommandImprovements, "Command / Scrapper Improvements", "CountersPrefix", "x", "Prefix for the command item counter. Example 'x' will show x0, x1, x2, etc.\nCan be empty.");

            // DPSMeter

            DPSMeterTimespan = Bind(ConfigFileDPSMeter, "DPSMeter", "Timespan", 5f, "Calculate DPS across this many seconds.");

            DPSMeterWindowShow = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowShow", true, "Show a dedicated DPSMeter.");

            DPSMeterWindowIncludeMinions = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowIncludeMinions", true, "Include minions such as turrets and drones in the DPS meter.");

            DPSMeterWindowBackground = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowBackground", true, "Whether or not the DPS window should have a background.");

            DPSMeterWindowHideWhenTyping = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowHideWhenTyping", true, "Whether or not the DPS window should be hidden when typing into the chat box.");

            DPSMeterWindowAnchorMin = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowAnchorMin", new Vector2(0, 0),
                "Screen position the lower left window corner is anchored to.\n" +
                "X & Y can be any number from 0.0 to 1.0 (inclusive).\n" +
                "Screen position starts at the bottom-left (0.0, 0.0) and increases toward the top-right (1.0, 1.0).");

            DPSMeterWindowAnchorMax = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowAnchorMax", new Vector2(0, 0f),
                "Screen position the upper right window corner is anchored to.\n" +
                "X & Y can be any number from 0.0 to 1.0 (inclusive).\n" +
                "Screen position starts at the bottom-left (0.0, 0.0) and increases toward the top-right (1.0, 1.0).");

            DPSMeterWindowPosition = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowPosition", new Vector2(120, 240), "Position of the DPSMeter window relative to the anchor.");

            DPSMeterWindowPivot = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowPivot", new Vector2(0, 1), "Pivot of the DPSMeter window.\n" +
                "Window Position is from the anchor to the pivot.");

            DPSMeterWindowSize = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowSize", new Vector2(350, 45), "Size of the DPSMeter window.");

            DPSMeterWindowAngle = Bind(ConfigFileDPSMeter, "DPSMeter", "WindowAngle", new Vector3(0, -6, 0), "Angle of the DPSMeter window.");

            // ItemCounters

            ItemCountersShowItemCounters = Bind(ConfigFileItemCounters, "ItemCounters", "ShowItemCounters", true, "Enable/Disable ItemCounters entirely.");

            ItemCountersShowItemScore = Bind(ConfigFileItemCounters, "ItemCounters", "ShowItemScore", true, "Show your item score.");

            ItemCountersItemScoreFromTier = Bind(ConfigFileItemCounters, "ItemCounters", "ItemScoreFromTier", true, "Whether or not the ItemScore should be based on tier. If disabled, the per-item settings will be used.");

            ItemCountersShowItemSum = Bind(ConfigFileItemCounters, "ItemCounters", "ShowItemSum", true, "Show the how many items you have.");

            ItemCountersItemSumTiersString = Bind(ConfigFileItemCounters, "ItemCounters", "ItemSumTiersString", "01234", "Which tiers to include in the ItemSum.\n0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier");

            ItemCountersItemSumTiers = ItemCountersItemSumTiersString.Value.ToCharArray().Select(c => (ItemTier)char.GetNumericValue(c)).ToList();


            ItemCountersShowItemsByTier = Bind(ConfigFileItemCounters, "ItemCounters", "ShowItemsByTier", true, "Show how many items you have, by tier.");

            ItemCountersItemsByTierOrderString = Bind(ConfigFileItemCounters, "ItemCounters", "ItemsByTierOrderString", "43210", "Which tiers to include in the ItemsByTier, in order.\n0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier");

            ItemCountersItemsByTierOrder = ItemCountersItemsByTierOrderString.Value.ToCharArray().Select(c => (ItemTier)char.GetNumericValue(c)).ToList();

            ItemCountersTierScoreTier1 = Bind(ConfigFileItemCounters, "ItemCounters Tier Score", "Tier1", 1f, "Score for Tier 1 items.");
            ItemCountersTierScoreTier2 = Bind(ConfigFileItemCounters, "ItemCounters Tier Score", "Tier2", 3f, "Score for Tier 2 items.");
            ItemCountersTierScoreTier3 = Bind(ConfigFileItemCounters, "ItemCounters Tier Score", "Tier3", 12f, "Score for Tier 3 items.");
            ItemCountersTierScoreLunar = Bind(ConfigFileItemCounters, "ItemCounters Tier Score", "Lunar", 0f, "Score for Lunar items.");
            ItemCountersTierScoreBoss = Bind(ConfigFileItemCounters, "ItemCounters Tier Score", "Boss", 4f, "Score for Boss items.");
            ItemCountersTierScoreNoTier = Bind(ConfigFileItemCounters, "ItemCounters Tier Score", "NoTier", 0f, "Score for items without a tier.");

            ItemCountersTierScores = new Dictionary<ItemTier, float>()
            {
                { ItemTier.Tier1, ItemCountersTierScoreTier1.Value },
                { ItemTier.VoidTier1, ItemCountersTierScoreTier1.Value },
                { ItemTier.Tier2, ItemCountersTierScoreTier2.Value},
                { ItemTier.VoidTier2, ItemCountersTierScoreTier2.Value},
                { ItemTier.Tier3, ItemCountersTierScoreTier3.Value},
                { ItemTier.VoidTier3, ItemCountersTierScoreTier3.Value},
                { ItemTier.Lunar, ItemCountersTierScoreLunar.Value},
                { ItemTier.Boss, ItemCountersTierScoreBoss.Value},
                { ItemTier.VoidBoss, ItemCountersTierScoreBoss.Value},
                { ItemTier.NoTier, ItemCountersTierScoreNoTier.Value},
            };


            ItemCountersItemScores = new Dictionary<ItemDef, float>();


            // StatsDisplay

            StatsDisplayEnable = Bind(ConfigFileStatsDisplay, "StatsDisplay", "Enable", true, "Enable/Disable the StatsDisplay entirely.");

            StatsDisplayStatString = Bind(ConfigFileStatsDisplay, "StatsDisplay", "StatString",
                "<color=#FFFFFF>" +
                "<size=18><b>Stats</b></size>\n" +
                "<size=14>Luck: $luck\n" +
                "Base Damage: $dmg\n" +
                "Crit Chance: $luckcrit%\n" +
                "Attack Speed: $atkspd\n" +
                "Armor: $armor | $armordmgreduction%\n" +
                "Regen: $regen\n" +
                "Speed: $velocity\n" +
                "Jumps: $jumps/$maxjumps\n" +
                "Kills: $killcount\n" +
                "Mountain Shrines: $mountainshrines\n",
                "You may format the StatString using formatting tags such as color, size, bold, underline, italics. See Readme for more.\n" +
                "Valid Parameters:\n" +
                string.Join(" ", StatsDisplay.regexmap.Keys));

            StatsDisplayStatStringCustomBind = Bind(ConfigFileStatsDisplay, "StatsDisplay", "StatStringCustomBind",
                "<color=#FFFFFF>" +
                "<size=18><b>Stats</b></size>\n" +
                "<size=14>Luck: $luck\n" +
                "Experience: $exp/$maxexp\n" +
                "Base Damage: $dmg\n" +
                "Crit Chance: $crit%\n" +
                "Crit w/ Luck: $luckcrit%\n" +
                "Crit Damage: $critdamage\n" +
                "Attack Speed: $atkspd\n" +
                "Armor: $armor | $armordmgreduction%\n" +
                "Regen: $regen\n" +
                "Speed: $velocity\n" +
                "Jumps: $jumps/$maxjumps\n" +
                "Kills: $killcount\n" +
                "Mountain Shrines: $mountainshrines\n" +
                "Difficulty: $difficulty\n" +
                "Blue Portal: $blueportal\n" +
                "Gold Portal: $goldportal\n" +
                "Celestial Portal: $celestialportal\n" +
                "Void Portal: $voidportal\n",
                "StatDisplay string to show when the custom bind is being pressed. This can be the same or different from the normal StatString.");

            StatsDisplayCustomBind = Bind(ConfigFileStatsDisplay, "StatsDisplay", "CustomBind", "tab", "Bind to show secondary StatsDisplay string.\n" +
                "Example: space\n" +
                "Must be lowercase. Leave blank to disable.");

            StatsDisplayShowCustomBindOnly = Bind(ConfigFileStatsDisplay, "StatsDisplay", "ShowCustomBindOnly", false, "Only show the StatsDisplay when the scoreboard is open.");

            StatsDisplayToggleOnBind = Bind(ConfigFileStatsDisplay, "StatsDisplay", "ToggleOnBind", false, "Toggle the StatsDisplay when the bind is pressed rather than showing it when it is held.");

            StatsDisplayPanelBackground = Bind(ConfigFileStatsDisplay, "StatsDisplay", "PanelBackground", true, "Whether or not the StatsDisplay panel should have a background.");

            StatsDisplayAttachToObjectivePanel = Bind(ConfigFileStatsDisplay, "StatsDisplay", "AttachToObjectivePanel", true, "Whether to attach the stats display to the objective panel.\n" +
                "If not, it will be a free-floating window that can be moved with the options below.");

            StatsDisplayWindowAnchorMin = Bind(ConfigFileStatsDisplay, "StatsDisplay", "WindowAnchorMin", new Vector2(1, 0.5f),
                "Screen position the lower left window corner is anchored to.\n" +
                "X & Y can be any number from 0.0 to 1.0 (inclusive).\n" +
                "Screen position starts at the bottom-left (0.0, 0.0) and increases toward the top-right (1.0, 1.0).");

            StatsDisplayWindowAnchorMax = Bind(ConfigFileStatsDisplay, "StatsDisplay", "WindowAnchorMax", new Vector2(1, 0.5f),
                "Screen position the upper right window corner is anchored to.\n" +
                "X & Y can be any number from 0.0 to 1.0 (inclusive).\n" +
                "Screen position starts at the bottom-left (0.0, 0.0) and increases toward the top-right (1.0, 1.0).");

            StatsDisplayWindowPosition = Bind(ConfigFileStatsDisplay, "StatsDisplay", "WindowPosition", new Vector2(-210, 100), "Position of the StatsDisplay window relative to the anchor.");

            StatsDisplayWindowPivot = Bind(ConfigFileStatsDisplay, "StatsDisplay", "WindowPivot", new Vector2(0, 0.5f), "Pivot of the StatsDisplay window.\n" +
                "Window Position is from the anchor to the pivot.");

            StatsDisplayWindowSize = Bind(ConfigFileStatsDisplay, "StatsDisplay", "WindowSize", new Vector2(200, 600), "Size of the StatsDisplay window.");

            StatsDisplayWindowAngle = Bind(ConfigFileStatsDisplay, "StatsDisplay", "WindowAngle", new Vector3(0, 6, 0), "Angle of the StatsDisplay window.");

            // Sorting

            SortingSortItemsInventory = Bind(ConfigFileSorting, "Sorting", "SortItemsInventory", true, "Sort items in the inventory and scoreboard.");

            SortingSortItemsCommand = Bind(ConfigFileSorting, "Sorting", "SortItemsCommand", true, "Sort items in the command window.");

            SortingSortItemsScrapper = Bind(ConfigFileSorting, "Sorting", "SortItemsScrapper", true, "Sort items in the scrapper window.");

            SortingTierOrderString = Bind(ConfigFileSorting, "Sorting", "TierOrder", "012345", "Tiers in ascending order, left to right.\n0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier");

            SortingTierOrder = new int[]
            {
                SortingTierOrderString.Value.IndexOf('0'),
                SortingTierOrderString.Value.IndexOf('1'),
                SortingTierOrderString.Value.IndexOf('2'),
                SortingTierOrderString.Value.IndexOf('3'),
                SortingTierOrderString.Value.IndexOf('4'),
                SortingTierOrderString.Value.IndexOf('5'),
            };

            SortingSortOrder = Bind(ConfigFileSorting, "Sorting", "SortOrder", "S134",
                    "What to sort the items by, most important to least important.\n" +
                    "Find the full details and an example in the Readme on Thunderstore/Github.\n" +
                    "0 = Tier Ascending\n" +
                    "1 = Tier Descending\n" +
                    "2 = Stack Size Ascending\n" +
                    "3 = Stack Size Descending\n" +
                    "4 = Pickup Order\n" +
                    "5 = Pickup Order Reversed\n" +
                    "6 = Alphabetical\n" +
                    "7 = Alphabetical Reversed\n" +
                    "8 = Random\n" +
                    "i = ItemIndex\n" +
                    "I = ItemIndex Descending\n" +
                    "\n" +
                    "Tag Based:\n" +
                    "s = Scrap First\n" +
                    "S = Scrap Last\n" +
                    "d = Damage First\n" +
                    "D = Damage Last\n" +
                    "h = Healing First\n" +
                    "H = Healing Last\n" +
                    "u = Utility First\n" +
                    "U = Utility Last\n" +
                    "o = On Kill Effect First\n" +
                    "O = On Kill Effect Last\n" +
                    "e = Equipment Related First\n" +
                    "E = Equipment Related Last\n" +
                    "p = Sprint Related First\n" +
                    "P = Sprint Related Last");

            SortingSortOrderCommand = Bind(ConfigFileSorting, "Sorting", "SortOrderCommand", "6",
            "Sort order for the command window.\n" +
            "The command window has a special sort option \"C\" which will place the last selected item in the middle.\n" +
            "Note: This option must be the last one in the SortOrderCommand option.");

            SortingSortOrderScrapper = Bind(ConfigFileSorting, "Sorting", "SortOrderScrapper", "134",
            "Sort order for the scrapper window.");

        }

        /// <summary>
        /// Automatically transfers the value of old config entries to new config entries based on
        /// <see cref="previousEntryMap"/>. Old config entries are deleted after their value has
        /// been transferred.<br/>
        /// Wraps <see cref="ConfigFile.Bind{T}(string, string, T, string)"/>.
        /// </summary>
        /// <param name="config">File containing the config entry..</param>
        /// <param name="section">Section/category/group of the setting. Settings are grouped by this.</param>
        /// <param name="key">Name of the setting.</param>
        /// <param name="defaultValue">Value of the setting if the setting was not created yet.</param>
        /// <param name="description">Simple description of the setting shown to the user.</param>
        /// <typeparam name="T">Type of the value contained in this setting.</typeparam>
        static ConfigEntry<T> Bind<T>(ConfigFile config, string section, string key, T defaultValue, string description)
        {
            var currentEntry = config.Bind(section, key, defaultValue, description);

            if (previousEntryMap.ContainsKey((section, key)))
            {
                var previousTuple = previousEntryMap[(section, key)];
                var previousDefinition = new ConfigDefinition(previousTuple.Item1, previousTuple.Item2);
                // ConfigFile has to Bind to a Section + Key before it knows that it's in the file.
                var previousEntry = config.Bind(previousDefinition, "If you ever set your config option to this, it's your own fault", new ConfigDescription(description));
                if(previousEntry.Value != "If you ever set your config option to this, it's your own fault")
                {
                    // Let ConfigBaseEntry deal with type parsing.
                    currentEntry.SetSerializedValue(previousEntry.GetSerializedValue());
                }
                config.Remove(previousDefinition);
            }

            return currentEntry;
        }
    }
}
