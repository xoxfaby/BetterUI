# Changelog

### v2.8.2
 - Addition: Singular Item Sorting: Allows arbitrary item sorting in the sort string. 

### v2.8.1
 - Bugfix: Fixed build issue that prevented v2.8.0 from releasing properly.
 
## v2.8.0
 - Addition: Language Support: Automatic Updates  
Language files will now automatically download the latest version if it is available, or if the language file is missing. This means you no longer have to update BetterUI just because a single string was added to a language.  
It will also automatically fix installation issues that can occur when people accidentally delete or move the language files to the wrong folder (or not at all). 
 - Addition: Language Support: Translation Platform  
 I've added a new online platform for translators to contribute at https://translate.faby.dev  
 If you would like to help by imprvoving our existing languages or by adding new ones, join my discord at https://discord.faby.dev and connect with me to get permissions on the translation website. 
 - Small Change: Language Support: Language files renamed to prevent them from showing up in the r2modman config editor as this was causing confusion.

### v2.7.9
 - Bugfix: ItemStats: Fixed incorrect scaling on negative movementspeed from Stone Flux Pauldron
 - Bugfix: ItemStats: Fixed incorrect language on Repulsion Armor Plate 
 - Bugfix: Modcompat: Fixed ItemDefs without language tokens breaking the UI
 - Bugfix: Modcompat: (Hopefully) fixed RiskUI crashing BetterUI

### v2.7.8
 - Bugfix: Included CHANGELOG.md in export so that you can read this. 

### v2.7.7
 - Addition: Language Support: Added Japanese
 - Small Update: Added more Russian strings ( Thanks l Yoh l )
 - Bugfix: ItemStats: Fixed Harvester's Scythe not showing the correct stack value. 
 - Bugfix: ItemStats: Updated Ben's Raincoat to the newest version

### v2.7.6
 - Addition: Language Support: Added Chinese ( Thanks 南家老树皮 )
 - Addition: Language Support: Added Russian ( Thanks l Yoh l )
 - Small Addition/Bugfix: Changed zip folder structure so that r2modman doesn't destroy the folder structure.
 - Internal Change: Language Support: Added additional logic for the display of seconds as required by some languages.
 - Bugfix: Language Support: Fixed language support not being case insensitive in regards to the language name. This should fix issues with the system default language not getting BetterUI strings.
 - Bugfix: Language Support: Removed extra "second" in effective skill cooldown.
 - Bugfix: Advanced Icons: Fixed incorrect Proc Coefficient for MUL-T Ram. ( 0 -> 1)

### v2.7.5
 - Bugfix: Language Support: Removed extra "second" in lobby skill tooltip. 

### v2.7.4
 - Small Addition: Language Support: Added localization support for all other BetterUI strings I found, exception of StatsDisplay because it's a bit of a complex system and needs its own rework. '

### v2.7.3
 - Language Support: Added Ukranian (Thank you Damglador).
 - Bugfix: Language Support: Fixed English strings not getting loaded when another language is selected.
 - Bugfix: Language Support: Fixed broken English buff string. 
### v2.7.2
 - Bugfix: Language Support: Fixes crash when a malformed language file is found. 

### v2.7.1
 - Bugfix: Language Support: Searches the plugin folder for languages file if the Languages folder doesn't exist. 

## v2.7.0

 - Feature: Language Support: BetterUI now supports multiple languages. Currently, only the original English language is included, but users can submit pull requests on the github to add additional languages.

### v2.6.4

 - Bugfix: Buffs: Fixed an error where a missing BuffDef was causing the game to hang on load. 

### v2.6.3
 - Bugfix: AdvancedIcons: Fixed Void's special and corrupted special having reverse descriptions. 
 - Small Addition: Buffs: Added some missing descriptions for vanilla buffs.(Thanks [DestroyedClone](https://thunderstore.io/package/DestroyedClone/))

### v2.6.2
 - Bugfix: ItemSorting: Fixed a bug that caused BetterUI to break when custom Item Tiers are added, for now BetterUI will simply treat these items as NoTier tier. In the future, there will be config options for custom item tiers. Some developers have already "hacked" their tiers into BetterUI to fix this, this method will continue to work.   
 - Bugfix: Oops: Removed some debug text that was spammed into the console when the Strides of Heresy skill was being used. 
 - Mod Compatibility: ItemStats: Added helper methods for developers to access the ItemStats for an ItemDef, the ItemTags for an ItemStat and the ItemModifiers for an ItemTag. This allows easier access to change or remove ItemStats that have already been added, such as the ItemStats BetterUI adds for vanilla Items. This will allow developers of mods that change the way vanilla items work to also change the corresponding ItemStats. In the future, there will likely be a more user-friendly way to change existing ItemStats but this, somewhat raw, access to the ItemStats allows for max compatibility without large changes.  

### v2.6.1
 - Removed blacklist. Yes, this means I plan to continue working on BetterUI. 

## v2.6.0
 - Addition: BetterUI Window: For now, this window just shows some helpful links, but it lays the groundwork to let you change the BetterUI settings in-game in the future.

### v2.5.11 & v2.5.12
 - Bugfix: Update for the latest patch

### v2.5.10
 - Bugfix: ItemScores: Actually really finalized the fix for nameTokens being empty

### v2.5.9
 - Small Change: ItemScores: Total itemscore now rounds to 1 digit
 - Bugfix: ItemScores: Finalized fix for nameTokens being empty

### v2.5.8
 - Bugfix: ItemScores: Fixed 2.5.7 fix

### v2.5.7
 - Bugfix: ItemScores: Fixed check that ensures item have a properly set name. 

### v2.5.6
 - Small Addition: DPSMeter: Added option to hide the DPSMeter when the chatbox is open (enabled by default)
 - Small Change: ItemStats: Rounded non-chance percentages to 2 digits as well
 - Bugfix: DPSMeter: Fixed DPSMeter window being active but nonfunctional if the whole module is disabled

### v2.5.5
 - Bugfix: ItemStats: Fixed scaling on EquipemntCooldown Items
 - Bugfix: ItemCounters: Fixed void items not counting towards item sums for each tiers and the total. In the future, there will be options for each void tier seperately, but for now they are counted together with the non-void counterparts.

### v2.5.4
 - Bugfix: ItemStats: Fixed typo & scaling on Alien Head
 - Bugfix: ItemStats: Reused the old ItemStatsIntegration setting to toggle ItemStats for now. 
 - Bugfix: ItemStats: Fixed seer's rounding error
 - API: Added public ItemScore getter method. 

### v2.5.3
 - Bugfix: ItemStats: Added luck modifer to some items that were missing it

### v2.5.2
 - Bugfix: ItemStats: Corrected 2 incorrect ItemStats
 - Small Change: ItemStats: Changed default formatting to round numbers to 2 decimal places
 - Small Addition: ProcCoefiicients: Added proc effects from void items

### v2.5.1
 - Bugfix: ItemStats: Fixed issue that froze the game at the end screen

## v2.5.0
 - Addition: ItemStats:
   - This adds calculated stat bonuses to the descriptions of items.  
     This is a huge update, if you spot any errors or you feel anything is missing, please submit an issue on the Github: https://github.com/xoxfaby/BetterUI/issues
 - Small Addition: Added ability for Proc Coefficient skill names to use language tokens.

### v2.4.8
 - Note: In the changelog for the latest versions, $critdamage was incorrectly listed as $critdmg, this has been retroactively fixed.
 - Small Addition: Added different descriptions for survivor that has skills that work differently sometimes.
 - Small Addition: Void Portal has been added to the StatsDisplay ($voidportal)
 - Small Addition: Proc Coefficients for new survivors have been added. 
 - Bugfix: StatsDisplay: Added null-check to jumps to fully fix StatsDisplay for `CharacterBody`s without a CharacterMotor
 - Bugfix: ItemScores: ItemScores work again. Void tiers are counted as their normal counterparts (for now.)
 - Bugfix: DPSMeter: DPSMeter window now correctly hides when the rest of the HUD is hidden.

### v2.4.7
 - Bugfix: Actually fix $critdamage

### v2.4.6
 - Bugfixes:
  - Fixed $critdamage matching with $crit and showing the crit chance + the letters "damage" instead of showing the critical damage
  - Fixed the StatsDisplay breaking if the current CharacterBody doesn't have a CharacterMotor
 - Change:
  - Valid Parameter description for StatsDisplay is now automatically generated from all of the valid options. 

### v2.4.5
 - Bugfix: Moved HUD object aquisition back to Awake to fix bug where multiple StatsDisplays were being created when someone died and respawned. 

### v2.4.4
 - Addition: Added crit damage to StatsDisplay

### v2.4.3
 - Bugfix: Sort Void items the same as their normal items to quickly fix the game breaking when picking up a void item. Proper system to come later 

### v2.4.2
 - Bugfix: Get HUD and ObjectivePanelController from HudObjectiveTargetSetter because HUD.objectivePanelController was removed

### v2.4.1
 - README update only

## v2.4.0
 - Addition: Item Sorting: New sortString options:
	- Tier Tag: use t (lowercase) + the tier to sort items of that tier first.  
	Use T (uppercase) to sort them last:  
	t1 = Tier 1   
	t2 = Tier 2  
	t3 = Tier 3  
	tL = Lunar Tier  
	tB = Boss Tier  
	tN = NoTier  
	Examples:  
	`tL4` to put Lunar items first, and the rest of the items by pickup order.  
	`Tn6` to put NoTier items last, and the rest of the items alphabetically.   
	- Filters:  + a tag will apply the next sorting to only items that match the tag.  
	You can use any tags from the tag based sorting.   
	Examples:  
	`s0` to sort scrap by tier ascending.  
	`t14t28` to sort Tier 1 items by pickup order and tier 2 items randomly


### v2.3.3
 - Change: CommandImprovements: Command window will now resize both horizontally and vertically right away.
 - Bugfix: (Temporary) Disabled BetterUI command window sorting for command windows that contain both items and equipment. In the future, BetterUI will be able to sort these windows and provide configuration options for their sorting.
 - Bugfix: Configuration: Fixed certain config options always resetting to their default value due to a bug in the logic for porting over renamed config options.
### v2.3.2
 - Addition: CommandImprovements: Command window will now resize if it was going to overflow the screen

### v2.3.1
 - Bugfix: Fixed error when user attemps to change language

## v2.3.0
 - Addition: Added API for StatsDisplay to let other mods add their own parameters
 - Bugfix: Fixed item score config options not loading item names
 - Localization: Fixed some vanilla buff tooltips
 - Localization: Fixed some config option verbiage ( Thank you ethall )
 - Internal: Added ability to change config parameters while keeping user set values ( Thank you ethall )


### v2.2.0
 - Addition: Buffs: Buff tooltips now have better names and descriptions.
   Comes with a public API for other mods to register the names and descriptions of their buffs.
   Uses the Language system if you want to provide translated names/descriptions.
   Buffs registered with BetterAPI have their names imported if registered there.
 - Bugfix: AdvancedIcons: Fixed issue where skill tooltips didn't update if the skill was overridden.'

### v2.1.1
 - Bugfix: ItemScores: Fixed issue if mods added items with duplicate nametokens, uses ItemDef.name instead.

## v2.1.0
 - Bugfix: Fixed ItemScores:
 - Bugfix: StatsDisplay: Fixed $exp.
 - Addition: StatsDisplay: Added $maxexp which shows the exp needed to level up
 - Addition: StatsDisplay: Added $velocity and $2dvelocity to show how fast you are moving, $2dvelocity ignores the y axis.
 - Internal: Removed MMHook Dependency
 - Internal: Implemented ManivestV2 alongside normal manifest to hopefully allow local installation into mod managers.

### v2.0.4
 - Bugfix: Fixed command/scrapper not working after new update.

### v2.0.3
 - Bugfix: AdvanvedIcons: No longer breaks when bad items are added.
 - ModCompat: AdvancedIcons: Now accepts ItemDefs as well.

### v2.0.2
 - Depend on MMHOOK Standalone instead of shipping own MMHOOK.

### v2.0.1
 - Bugfix: Stopped R2API from adding BetterUI to the networkModList

# v2.0.0
 - No longer requires R2API
 - Anniversary Update: Added Proc coefficient values for new Character, Items & Item changes.
 - New Config Option: Added option to toggle StatsDisplay instead of holding to show.
 - Bugfix: Fixed HighestMultiKill in Statsdispaly only updating when the statsdisplay is open.
 - Bugfix: Fixed BuffIcon issues cause by new update.
 - Bugfix: Fixed tooltips showing seconds for cooldowns that are only 1 second
 - Bugfix: Fixed tooltips in lobby showing incorrect cooldowns
 - Internal: Removed deprecated ProcItemCatalog methods, if you were somehow still using these to add your custom proc effects please contact me for help to use the new ones.

### v1.6.17
 - Update simply to update the tags on the Thunderstore listing

### v1.6.16
 - Bugfix: Fixed bug when setting up config files for ItemCounters if a modded item had an invalid character in its name token.

### v1.6.15
 - Bugfix: Fixed bug in AdvancedIcons that could cause various issues when playing with an unmodded host.
 - Bugfix: Fixed small logic issue in CommandImprovements ( Thank you ethall )
 - Bugfix: Fixed bug in CommandImprovements if a specific set of config options was selected ( Thank you ethall )
 - Config change: Updated configuration tooltips for grammar, consistency, and clarity. ( Thank you ethall )

### v1.6.14
 - Actual upload of version 1.6.13

### v1.6.13
 - Bugfix: Fixed error that equipment cooldown tooltip

### v1.6.12
 - Bugfix: Fixed error that broke dpsmeter and statsdisplay

### v1.6.11
 - Bugfix: Fixed issues with AdvnacedIcons portion of BetterUI  

### v1.6.10
 - Bugfix: Fixed bug that would mess up skill tooltips.

### v1.6.9
 - Bugfix: Removed debug code that would massively slow down the game when opening scoreboard. oops.

### v1.6.8
 - Bugfix: Fixed issue that prevented luck from being updated.
 - Internal Change: More stringbuilder stuff

### v1.6.7
 - Addition: Added config file to entirely disable modcomponents ( these are the big internal parts the mod is split into )
 - Internal Change: Stopped using the ror2 built in sharedStringBuilder because some mods seem to be misuing it and bug out.  

### v1.6.6
 - Bugfix: Fixed scrapper/command breaking.
 - Bugfix: Fixed more BuffIcon related console spam.

### v1.6.5
 - Bugfix: Fixed BuffIcon related console spam.

### v1.6.4
 - Internal Change: Many internal changes to improve performance and memory usage.

### v1.6.3
 - New Feature: Added ability to show item description on the pickup interaction text
 - Bugfix: Readded all functionality from the Misc config
 - Bugfix: change nameLabel line space, overflow mode and word wrapping to work for more names

### v1.6.2
 - Internal Change: ItemCounters custom item score now uses the name token to increase compatibility with modded items.

### v1.6.1
 - Bugfix: Corrected Huntress Arrow Rain Proc Coefficient
 - Bugfix: Scrapper Tooltips w/ ItemStatsMod no longer shows you have one extra item.
 - Internal Change: Lots of things moved around, but none of this should affect anything.

## v1.6.0
 - New Feature: Item Counters: Fully customizable. Simple counting of items, groups by tier or item score by tier or even individual items
 - New Feature: Skill cooldowns: Base cooldown & calculated cooldown in the tooltip. Base cooldown in the loadout screen.

### v1.5.7
 - Addition: Added $difficulty parameter to display the Difficulty Coefficient in the StatsDisplay
 - Addition: Added the ability to change the custom bind for showing/changing the StatsDisplay, by default it replaces the old Scoreboard bind.
 - Addition: Added the Proc Coefficient information to the loadout screen.
 - Change: Replaced $crit with $luckcrit in the StatString, added both and $difficulty to StatStringCustomBind
 - Bugfix: Fixed $luckcrit not working.
 - Notes: The custom bind changes will reset your StatStringScoreboard because it was moved to a different config variable. It felt wrong to keep the same name when it is no longer tied to the scoreboard directly.

### v1.5.6
 - Bugfix: Added formatting for more numbers in StatsDisplay
 - Internal Change: more changes to allow modders to more easily register their items for display in skill tooltips

### v1.5.5
 - Oops: Removed debug console spam from FINALLY FIXING THIS BUG YAY anyways sorry

### v1.5.4
 - Bugfix: ACTUAL fix that fixes issues with scrapper/command sorting in multiplayer

### v1.5.3
 - Bugfix: Made a change that could *potentially* fix issues with scrapper/command sorting in multiplayer

### v1.5.2
 - Internal Change: Changed the way skills are registered to allow makers of custom skills to more easily register their skills.

### v1.5.1
 - Bugfix: Fixed a few incorrect proc coefficient values.

## v1.5.0
 - New Feature: Advanced Skill Tooltips: Proc Coefficients have been added to skill tooltips. Calculated effects based on the items you are carrying. Includes public methods so other mod makers can integrate their information into BetterUI.
 - New Feature: Equipment cooldown: Show the base cooldown of your equipment and the effective cooldown if it has been reduced by your items
 - New Feature: Skills and equipment show the cooldown remaining even if you can have multiple stacks.
 - New Feature: Ability to disable pickup notifications for items/equipemnts/artifacts
 - Bugfix Scrapper window no longer shows before/after ItemStatsIntegration text, only the normal one.
 - Bugfix $luckcrit now works correctly
 - Bugfix Fixed a bug with command windows if sorting was enabled on the scrapper window but not the command window
 - Note: Some default options have been changed, such as the default command sorting not changing the item order anymore because it was found to be disruptive. I encourage everyone to look back at features they might've previously disabled because it's possible they have been improved since then.
 - Note: If you want something added to the mod feel free to create an issue on GitHub, many features and options for the StatsDisplay were added because they were requested.

### v1.4.4
 - Fixed command bug when picking equipment due to leftover code and hook typo

### v1.4.3
 - undid the debug code i pushed on accident that locked all items in command.

### v1.4.2
 - Added $luckcrit for StatsDisplay to show the crit chance considering luck.
 - Changed default command sortorder to "6" ( Alphabetical ). I believe that sorting that changes the order in the command window during a run leads to more confusion than it helps.
 - Fixed some small mistakes with config values and hooks
 - Moved command sorting optionmap application to OnCreateButton to remove an unnecesary hook and attempt to improve compatibility with mods that call SubmitChoice directly
 - Rewrote DPSMeter calculations for performance
 - Rewrote StatsDisplay StatsString parser for performance

### v1.4.1
 - Fixed bug that would break StatsDisplay in levels without a teleporter and spam the console with errors.

## v1.4.0
 - Added BuffTimers and Tooltips
 - Added seperate StatsDisplay StatString for when the scoreboard is open.
 - Added StatsDisplay parameters: $armordmgreduction $mountainshrines $blueportal $goldportal $celestialportal
 - Potential fix for (harmless) DPSMeter console spam

### v1.3.2
 - Added $highestmultikill for StatsDisplay to keep track of your largest multikill of the run
 - This was added by request, if you want anything else added to the StatsDisplay, please just let me know.

### v1.3.1
 - Fixed CommandCountersHideOnZero not showing tooltips without counters

## v1.3.0
 - Clientside DPS Meter
 - StatsDisplay can now list DPS
 - StatsDisplay can now attach to the objective panel to automatically move out of the way
 - StatsDisplay can be fully formatted (See: http://digitalnativestudios.com/textmeshpro/docs/rich-text/)
 - Added ability remove background blur from the command menu
 - ItemStatsMod integration for the command and scrapper window
 - Many of these changes were directly requested so feel free to make suggestions to me on discord or on github.
 - The config file has moved and split up which means your settings will be reset, but with all the new changes I encourage you to check options you might've disabled before.

### v1.2.3
 - Fixed a potential scrapper multiplayer bug

### v1.2.2
 - Added ability to close command/scrapper menu with Escape, WASD or a custom key
 - Added ability to scale the command window to the number of items shown, this should help with mods that add new items.
 - removed debug spam ( sorry )

### v1.2.1
 - Fixed bug with command and scrapper menu

## v1.2.0
 - Added command menu item counters and tooltips

### v1.1.6
 - Added AdvancedTooltips for equipement

### v1.1.5
 - Added ItemIndex (ID) Sorting

### v1.1.4
 - Added special command menu sorting which keeps most recently picked item in the middle

### v1.1.3
 - Fixed Equipments with sorted command window.

### v1.1.2
 - Added Sorting to Scrapper and Command Menu

### v1.1.1
 - Fixed $killcount not working

## v1.1.0
 - Added StatsDisplay

### v1.0.1
 - Fixed up the README
 - Internal Changes

# v1.0.0
 - Inital Release
