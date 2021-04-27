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
        public static ConfigEntry<int> ItemCountersTierScoreTier1;
        public static ConfigEntry<int> ItemCountersTierScoreTier2;
        public static ConfigEntry<int> ItemCountersTierScoreTier3;
        public static ConfigEntry<int> ItemCountersTierScoreLunar;
        public static ConfigEntry<int> ItemCountersTierScoreBoss;
        public static ConfigEntry<int> ItemCountersTierScoreNoTier;
        public static int[] ItemCountersTierScores;
        public static Dictionary<ItemDef,int> ItemCountersItemScores;

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

            ComponentsAdvancedIcons = ConfigFileComponents.Bind("Components", "AdvanedIcons", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsBuffTimers = ConfigFileComponents.Bind("Components", "BuffTimers", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsCommandImprovements = ConfigFileComponents.Bind("Components", "CommandImprovements", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsDPSMeter = ConfigFileComponents.Bind("Components", "DPSMeter", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsItemCounters = ConfigFileComponents.Bind("Components", "ItemCounters", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsItemSorting = ConfigFileComponents.Bind("Components", "ItemSorting", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsMisc = ConfigFileComponents.Bind("Components", "Misc", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            ComponentsStatsDisplay = ConfigFileComponents.Bind("Components", "StatsDisplay", true, "Enable/Disable the component entirely, stopping it from hooking any game functions.");

            // Misc


            MiscShowHidden = ConfigFileMisc.Bind("Misc", "ShowHidden", false, "Show hidden items in the item inventory.");

            MiscAdvancedPickupNotificationsItems = ConfigFileMisc.Bind("Misc", "AdvancedPickupNotificationsItems", false, "Show advanced descriptions when picking up an item.");

            MiscAdvancedPickupNotificationsEquipements = ConfigFileMisc.Bind("Misc", "AdvancedPickupNotificationsEquipements", false, "Show advanced descriptions when picking up equipment.");

            MiscHidePickupNotificiationsItems = ConfigFileMisc.Bind("Misc", "HidePickupNotificiationsItems", false, "Hide pickup notifications for items.");

            MiscHidePickupNotificiationsEquipements = ConfigFileMisc.Bind("Misc", "HidePickupNotificiationsEquipements", false, "Hide pickup notifications for equipment.");

            MiscHidePickupNotificiationsArtifacts = ConfigFileMisc.Bind("Misc", "HidePickupNotificiationsArtifacts", false, "Hide pickup notifications for artifacts.");

            MiscShowPickupDescription = ConfigFileMisc.Bind("Misc", "ShowPickupDescription", true, "Show the item description on the interaction popup.");

            MiscPickupDescriptionAdvanced = ConfigFileMisc.Bind("Misc", "PickupDescriptionAdvanced", false, "Show advanced descriptions for the interaction popup.");

            // Advanced Icons

            AdvancedIconsItemAdvancedDescriptions = ConfigFileAdvancedIcons.Bind("Item Improvements", "AdvancedDescriptions", true, "Show advanced descriptions when hovering over an item.");

            AdvancedIconsItemItemStatsIntegration = ConfigFileAdvancedIcons.Bind("Item Improvements", "ItemStatsIntegration", true, "If installed, show item stats from ItemStatsMod where applicable.");

            AdvancedIconsEquipementShowCooldownStacks = ConfigFileAdvancedIcons.Bind("Equipement Improvements", "ShowCooldownStacks", true, "Show the cooldown for your equipement when charging multiple stacks.");

            AdvancedIconsEquipementAdvancedDescriptions = ConfigFileAdvancedIcons.Bind("Equipement Improvements", "AdvancedDescriptions", true, "Show advanced descriptions when hovering over equipment.");

            AdvancedIconsEquipementShowBaseCooldown = ConfigFileAdvancedIcons.Bind("Equipement Improvements", "BaseCooldown", true, "Show the base cooldown when hovering over equipment.");

            AdvancedIconsEquipementShowCalculatedCooldown = ConfigFileAdvancedIcons.Bind("Equipement Improvements", "CalculatedCooldown", true, "Show the calculated cooldown based on your items when hovering over equipment.");

            AdvancedIconsSkillShowCooldownStacks = ConfigFileAdvancedIcons.Bind("Skill Improvements", "ShowCooldownStacks", true, "Show the cooldown for skills when charging multiple stacks.");

            AdvancedIconsSkillShowBaseCooldown = ConfigFileAdvancedIcons.Bind("Skill Improvements", "BaseCooldown", true, "Show the base cooldown when hovering over a skill.");

            AdvancedIconsSkillShowCalculatedCooldown = ConfigFileAdvancedIcons.Bind("Skill Improvements", "CalculatedCooldown", true, "Show the calculated cooldown based on your items when hovering over a skill.");

            AdvancedIconsSkillShowProcCoefficient = ConfigFileAdvancedIcons.Bind("Skill Improvements", "ShowProcCoefficient", true, "Show the proc coefficient when hovering over a skill.");

            AdvancedIconsSkillCalculateSkillProcEffects = ConfigFileAdvancedIcons.Bind("Skill Improvements", "CalculateProcEffects", true, "Show the effects of proc coefficient of a skill related to the items you are carrying.");

            // Buffs

            BuffTimers = ConfigFileBuffs.Bind("Buffs", "BuffTimers", true, "Show buff timers (host only).");

            BuffTimersDecimal = ConfigFileBuffs.Bind("Buffs", "BuffTimersDecimal", true, "Show 1 decimal point when timer is below 10.");

            BuffTooltips = ConfigFileBuffs.Bind("Buffs", "BuffTooltips", true, "Show buff tooltips.");

            BuffTimersPosition = ConfigFileBuffs.Bind("Buffs", "CountersPosition", "TopRight",
               "Location of buff timer text.\n" +
               "Valid options:\n" +
               "TopLeft\n" +
               "TopRight\n" +
               "BottomLeft\n" +
               "BottomRight\n" +
               "Center\n");

            BuffTimersTextAlignmentOption = (TMPro.TextAlignmentOptions)Enum.Parse(typeof(TMPro.TextAlignmentOptions), BuffTimersPosition.Value, true);

            BuffTimersFontSize = ConfigFileBuffs.Bind("Buffs", "CountersFontSize", 23f, "Size of the buff timer text.");

            // Command / Scrapper Improvements

            CommandResizeCommandWindow = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "ResizeCommandWindow", true, "Resize the command window depending on the number of items.");

            CommandRemoveBackgroundBlur = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "RemoveBackgroundBlur", true, "Remove the blur behind the command window that hides the rest of the UI.");

            CommandCloseOnEscape = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "CloseOnEscape", true, "Close the command/scrapper window when you press escape.");

            CommandCloseOnWASD = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "CloseOnWASD", true, "Close the command/scrapper window when you press W, A, S, or D.");

            CommandCloseOnCustom = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "CloseOnCustom", "", "Close the command/scrapper window when you press the key selected here.\n" +
                "Example: space\n" +
                "Must be lowercase. Leave blank to disable.");

            CommandTooltipsShow = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "TooltipsShow", true, "Show tooltips in the command and scrapper windows.");

            CommandTooltipsItemStatsBeforeAfter = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "TooltipsItemStatsBeforeAfter", true, "If ItemStatsMod is installed, show the stats before and after picking up the item.");

            CommandCountersShow = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "CountersShow", true, "Show counters in the command and scrapper windows.");

            CommandCountersHideOnZero = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "CountersHideOnZero", false, "Hide counters when they are zero.");

            CommandCountersPosition = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "CountersPosition", "TopRight",
                "Location of the command item counter.\n" +
                "Valid options:\n" +
                "TopLeft\n" +
                "TopRight\n" +
                "BottomLeft\n" +
                "BottomRight\n" +
                "Center\n");

            CommandCountersTextAlignmentOption = (TMPro.TextAlignmentOptions)Enum.Parse(typeof(TMPro.TextAlignmentOptions), CommandCountersPosition.Value, true);

            CommandCountersFontSize = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "CountersFontSize", 20f, "Size of the command item counter text.");

            CommandCountersPrefix = ConfigFileCommandImprovements.Bind("Command / Scrapper Improvements", "CountersPrefix", "x", "Prefix for the command item counter. Example 'x' will show x0, x1, x2, etc.\nCan be empty.");

            // DPSMeter

            DPSMeterTimespan = ConfigFileDPSMeter.Bind("DPSMeter", "Timespan", 5f, "Calculate DPS across this many seconds.");

            DPSMeterWindowShow = ConfigFileDPSMeter.Bind("DPSMeter", "WindowShow", true, "Show a dedicated DPSMeter.");

            DPSMeterWindowIncludeMinions = ConfigFileDPSMeter.Bind("DPSMeter", "WindowIncludeMinions", true, "Include minions such as turrets and drones in the DPS meter.");

            DPSMeterWindowBackground = ConfigFileDPSMeter.Bind("DPSMeter", "WindowBackground", true, "Whether or not the DPS window should have a background.");

            DPSMeterWindowAnchorMin = ConfigFileDPSMeter.Bind("DPSMeter", "WindowAnchorMin", new Vector2(0, 0),
                "Screen position the lower left window corner is anchored to.\n" +
                "X & Y can be any number from 0.0 to 1.0 (inclusive).\n" +
                "Screen position starts at the bottom-left (0.0, 0.0) and increases toward the top-right (1.0, 1.0).");

            DPSMeterWindowAnchorMax = ConfigFileDPSMeter.Bind("DPSMeter", "WindowAnchorMax", new Vector2(0, 0f),
                "Screen position the upper right window corner is anchored to.\n" +
                "X & Y can be any number from 0.0 to 1.0 (inclusive).\n" +
                "Screen position starts at the bottom-left (0.0, 0.0) and increases toward the top-right (1.0, 1.0).");

            DPSMeterWindowPosition = ConfigFileDPSMeter.Bind("DPSMeter", "WindowPosition", new Vector2(120, 240), "Position of the DPSMeter window relative to the anchor.");

            DPSMeterWindowPivot = ConfigFileDPSMeter.Bind("DPSMeter", "WindowPivot", new Vector2(0, 1), "Pivot of the DPSMeter window.\n" +
                "Window Position is from the anchor to the pivot.");

            DPSMeterWindowSize = ConfigFileDPSMeter.Bind("DPSMeter", "WindowSize", new Vector2(350, 45), "Size of the DPSMeter window.");

            DPSMeterWindowAngle = ConfigFileDPSMeter.Bind("DPSMeter", "WindowAngle", new Vector3(0, -6, 0), "Angle of the DPSMeter window.");

            // ItemCounters

            ItemCountersShowItemCounters = ConfigFileItemCounters.Bind("ItemCounters", "ShowItemCounters", true, "Enable/Disable ItemCounters entirely.");

            ItemCountersShowItemScore = ConfigFileItemCounters.Bind("ItemCounters", "ShowItemScore", true, "Show your item score.");

            ItemCountersItemScoreFromTier = ConfigFileItemCounters.Bind("ItemCounters", "ItemScoreFromTier", true, "Whether or not the ItemScore should be based on tier. If disabled, the per-item settings will be used.");

            ItemCountersShowItemSum = ConfigFileItemCounters.Bind("ItemCounters", "ShowItemSum", true, "Show the how many items you have.");

            ItemCountersItemSumTiersString = ConfigFileItemCounters.Bind("ItemCounters", "ItemSumTiersString", "01234", "Which tiers to include in the ItemSum.\n0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier");

            ItemCountersItemSumTiers = ItemCountersItemSumTiersString.Value.ToCharArray().Select(c => (ItemTier)char.GetNumericValue(c)).ToList();


            ItemCountersShowItemsByTier = ConfigFileItemCounters.Bind("ItemCounters", "ShowItemsByTier", true, "Show how many items you have, by tier.");

            ItemCountersItemsByTierOrderString = ConfigFileItemCounters.Bind("ItemCounters", "ItemsByTierOrderString", "43210", "Which tiers to include in the ItemsByTier, in order.\n0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier");

            ItemCountersItemsByTierOrder = ItemCountersItemsByTierOrderString.Value.ToCharArray().Select(c => (ItemTier)char.GetNumericValue(c)).ToList();

            ItemCountersTierScoreTier1 = ConfigFileItemCounters.Bind("ItemCounters Tier Score", "Tier1", 1, "Score for Tier 1 items.");
            ItemCountersTierScoreTier2 = ConfigFileItemCounters.Bind("ItemCounters Tier Score", "Tier2", 3, "Score for Tier 2 items.");
            ItemCountersTierScoreTier3 = ConfigFileItemCounters.Bind("ItemCounters Tier Score", "Tier3", 12, "Score for Tier 3 items.");
            ItemCountersTierScoreLunar = ConfigFileItemCounters.Bind("ItemCounters Tier Score", "Lunar", 0, "Score for Lunar items.");
            ItemCountersTierScoreBoss = ConfigFileItemCounters.Bind("ItemCounters Tier Score", "Boss", 4, "Score for Boss items.");
            ItemCountersTierScoreNoTier = ConfigFileItemCounters.Bind("ItemCounters Tier Score", "NoTier", 0, "Score for items without a tier.");

            ItemCountersTierScores = new int[]
            {
                ItemCountersTierScoreTier1.Value,
                ItemCountersTierScoreTier2.Value,
                ItemCountersTierScoreTier3.Value,
                ItemCountersTierScoreLunar.Value,
                ItemCountersTierScoreBoss.Value,
                ItemCountersTierScoreNoTier.Value,
            };


            ItemCountersItemScores = new Dictionary<ItemDef, int>();


            // StatsDisplay

            StatsDisplayEnable = ConfigFileStatsDisplay.Bind("StatsDisplay", "Enable", true, "Enable/Disable the StatsDisplay entirely.");

            StatsDisplayStatString = ConfigFileStatsDisplay.Bind("StatsDisplay", "StatString",
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
                "$exp $maxexp $level $luck\n" +
                "$dmg $crit $luckcrit $atkspd\n" +
                "$hp $maxhp $shield $maxshield $barrier $maxbarrier\n" +
                "$armor $armordmgreduction $regen\n" +
                "$velocity $2dvelocity $movespeed $jumps $maxjumps\n" +
                "$killcount $multikill $highestmultikill\n" +
                "$dps $dpscharacter $dpsminions\n" +
                "$mountainshrines\n" +
                "$blueportal $goldportal $celestialportal");

            StatsDisplayStatStringCustomBind = ConfigFileStatsDisplay.Bind("StatsDisplay", "StatStringCustomBind",
                "<color=#FFFFFF>" +
                "<size=18><b>Stats</b></size>\n" +
                "<size=14>Luck: $luck\n" +
                "Experience: $exp/$maxexp\n" +
                "Base Damage: $dmg\n" +
                "Crit Chance: $crit%\n" +
                "Crit w/ Luck: $luckcrit%\n" +
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
                "Celestial Portal: $celestialportal\n",
                "StatDisplay string to show when the custom bind is being pressed. This can be the same or different from the normal StatString.");

            StatsDisplayCustomBind = ConfigFileStatsDisplay.Bind("StatsDisplay", "CustomBind", "tab", "Bind to show secondary StatsDisplay string.\n" +
                "Example: space\n" +
                "Must be lowercase. Leave blank to disable.");

            StatsDisplayShowCustomBindOnly = ConfigFileStatsDisplay.Bind("StatsDisplay", "ShowCustomBindOnly", false, "Only show the StatsDisplay when the scoreboard is open.");

            StatsDisplayToggleOnBind = ConfigFileStatsDisplay.Bind("StatsDisplay", "ToggleOnBind", false, "Toggle the StatsDisplay when the bind is pressed rather than showing it when it is held.");

            StatsDisplayPanelBackground = ConfigFileStatsDisplay.Bind("StatsDisplay", "PanelBackground", true, "Whether or not the StatsDisplay panel should have a background.");

            StatsDisplayAttachToObjectivePanel = ConfigFileStatsDisplay.Bind("StatsDisplay", "AttachToObjectivePanel", true, "Whether to attach the stats display to the objective panel.\n" +
                "If not, it will be a free-floating window that can be moved with the options below.");

            StatsDisplayWindowAnchorMin = ConfigFileStatsDisplay.Bind("StatsDisplay", "WindowAnchorMin", new Vector2(1, 0.5f),
                "Screen position the lower left window corner is anchored to.\n" +
                "X & Y can be any number from 0.0 to 1.0 (inclusive).\n" +
                "Screen position starts at the bottom-left (0.0, 0.0) and increases toward the top-right (1.0, 1.0).");

            StatsDisplayWindowAnchorMax = ConfigFileStatsDisplay.Bind("StatsDisplay", "WindowAnchorMax", new Vector2(1, 0.5f),
                "Screen position the upper right window corner is anchored to.\n" +
                "X & Y can be any number from 0.0 to 1.0 (inclusive).\n" +
                "Screen position starts at the bottom-left (0.0, 0.0) and increases toward the top-right (1.0, 1.0).");

            StatsDisplayWindowPosition = ConfigFileStatsDisplay.Bind("StatsDisplay", "WindowPosition", new Vector2(-210, 100), "Position of the StatsDisplay window relative to the anchor.");

            StatsDisplayWindowPivot = ConfigFileStatsDisplay.Bind("StatsDisplay", "WindowPivot", new Vector2(0, 0.5f), "Pivot of the StatsDisplay window.\n" +
                "Window Position is from the anchor to the pivot.");

            StatsDisplayWindowSize = ConfigFileStatsDisplay.Bind("StatsDisplay", "WindowSize", new Vector2(200, 600), "Size of the StatsDisplay window.");

            StatsDisplayWindowAngle = ConfigFileStatsDisplay.Bind("StatsDisplay", "WindowAngle", new Vector3(0, 6, 0), "Angle of the StatsDisplay window.");

            // Sorting

            SortingSortItemsInventory = ConfigFileSorting.Bind("Sorting", "SortItemsInventory", true, "Sort items in the inventory and scoreboard.");

            SortingSortItemsCommand = ConfigFileSorting.Bind("Sorting", "SortItemsCommand", true, "Sort items in the command window.");

            SortingSortItemsScrapper = ConfigFileSorting.Bind("Sorting", "SortItemsScrapper", true, "Sort items in the scrapper window.");

            SortingTierOrderString = ConfigFileSorting.Bind("Sorting", "TierOrder", "012345", "Tiers in ascending order, left to right.\n0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier");

            SortingTierOrder = new int[]
            {
                SortingTierOrderString.Value.IndexOf('0'),
                SortingTierOrderString.Value.IndexOf('1'),
                SortingTierOrderString.Value.IndexOf('2'),
                SortingTierOrderString.Value.IndexOf('3'),
                SortingTierOrderString.Value.IndexOf('4'),
                SortingTierOrderString.Value.IndexOf('5'),
            };

            SortingSortOrder = ConfigFileSorting.Bind("Sorting", "SortOrder", "S134",
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

            SortingSortOrderCommand = ConfigFileSorting.Bind("Sorting", "SortOrderCommand", "6",
            "Sort order for the command window.\n" +
            "The command window has a special sort option \"C\" which will place the last selected item in the middle.\n" +
            "Note: This option must be the last one in the SortOrderCommand option.");

            SortingSortOrderScrapper = ConfigFileSorting.Bind("Sorting", "SortOrderScrapper", "134",
            "Sort order for the scrapper window.");

        }
    }
}
