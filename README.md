# BetterUI

A simple mod that adds various UI improvements.  
Each can be disabled and configured in the config file.


## Currently implemented:
- DPS Meter
- StatsDisplay
- Ordered Inventory, Scrapper Menu, Command Menu
- Command/Scrapper Menu Item Counters and Tooltips
- Close the command menu with escape, WASD or a custom keybind
- Automatically resize the command window if there are more items
- Remove background blur from command window
- Advanced Item Description
- ItemsStatsMod integration for command window
- Buff Timers and Tooltips
- Show Hidden Items

## Support Me

If you like what I'm doing, consider supporting me through GitHub Sponsors so I can keep doing it:

https://github.com/sponsors/xoxfaby

## Features

### DPSMeter
Fully clientside DPS Meter that can be integrated into the StatsDisplay. Counts minion damage! 

![DPSMeter](https://fby.pw/dpsmeter.png)

### StatsDisplay
Show all of your character's stats! Completely customizable!

![StatsDisplay](https://fby.pw/statsdisplay.png)

### Command/Scrapper Improvements
See how many items you have when using the scrapper or picking an item using the command artifact!
Tooltips with ItemStatsMod integration!
Close the command/scrapper window with Escape, WASD or a custom keybind!

![Command Counters](https://fby.pw/commandcounters.png)

### Improved Item Sorting
Sort items alphabetically, by tier, stacks or even tags like "Scrap" or "Damage". EVEN RANDOMLY?!

![Item Sorting](https://fby.pw/itembar.png)

![Sorted Scrapper](https://fby.pw/sortedscrapper.png)

### BuffTimers & Tooltips
Buff timers currently only work if you are the host because the timers are not networked in multiplayer. 

![Buff Timers](https://fby.pw/buffs.png)

### Advanced item descriptions
Use the advanced item descriptions from the logbook that show the actual numbers for all the changes. 
Integration with ItemStats in the command and scrapper windows.

![Item Description](https://fby.pw/itemdesc.png)

### Show Hidden Items
Show hidden items like the hidden monsoon/drizzle items

## Help & Feedback

If you need help or have suggestions, create an issue on github or find me on the RoR2 Modding Discord 

[RoR2 Modding Discord](https://discord.com/invite/5MbXZvd) Username: @XoXFaby#1337

https://github.com/xoxfaby/BetterUI

## Configuration

#### StatsDisplay

The StatsDisplay parses the `StatString` in the config file and replaces all the parameters it finds.
The StatsDisplay can also be moved, resized and recolored and formatted ( See: http://digitalnativestudios.com/textmeshpro/docs/rich-text/ )  
If you want another parameter added, feel free to suggest it to me (See Help & Feedback)
Here is a list of all valid parameters right now

$exp $level $luck  
$dmg $crit $luckcrit $atkspd  
$hp $maxhp $shield $maxshield $barrier $maxbarrier  
$armor $armordmgreduction $regen  
$movespeed $jumps $maxjumps  
$killcount $multikill $highestmultikill  
$dps $dpscharacter $dpsminions  
$mountainshrines  
$blueportal $goldportal $celestialportal  

#### Sorting

You can enable/disable any part of the mod in the config file.  
The sorting is completely customizable as well. 
The default sorting value is **S134**

**S** means the items are first sorted by the "Scrap" tag and all the scrap is put at the end of the list.  
**1** then sorts it by tier, in descending order, putting higher tier items at the front. 
**3** sorts it by the stack size in descending order, meaning if you have more of an item, it will come first. 
**4** then sorts it by pickup order, meaning items you got first, come first. 

You can customize this in any way you like.

The full options:

0 = Tier Ascending  
1 = Tier Descending  
2 = Stack Size Ascending  
3 = Stack Size Descending  
4 = Pickup Order  
5 = Pickup Order Reversed  
6 = Alphabetical  
7 = Alphabetical Reversed  
8 = Random   
i = ItemIndex  
I = ItemIndex Descending  

Tag Based:  

s = Scrap First  
S = Scrap Last  
d = Damage First  
D = Damage Last  
h = Healing First  
H = Healing Last  
u = Utility First  
U = Utility Last  
o = On Kill Effect First  
O = On Kill Effect Last  
e = Equipment Related First  
E = Equipment Related Last  
p = Sprint Related First  
P = Sprint Related Last  

## Changelog

#### v1.4.3
 - undid the debug code i pushed on accident that locked all items in command. 

#### v1.4.2
 - Added $luckcrit for StatsDisplay to show the crit chance considering luck. 
 - Changed default command sortorder to "6" ( Alphabetical ). I believe that sorting that changes the order in the command window during a run leads to more confusion than it helps.
 - Fixed some small mistakes with config values and hooks
 - Moved command sorting optionmap application to OnCreateButton to remove an unnecesary hook and attempt to improve compatibility with mods that call SubmitChoice directly
 - Rewrote DPSMeter calculations for performance
 - Rewrote StatsDisplay StatsString parser for performance

#### v1.4.1
 - Fixed bug that would break StatsDisplay in levels without a teleporter and spam the console with errors. 

### v1.4.0
 - Added BuffTimers and Tooltips
 - Added seperate StatsDisplay StatString for when the scoreboard is open.
 - Added StatsDisplay parameters: $armordmgreduction $mountainshrines $blueportal $goldportal $celestialportal
 - Potential fix for (harmless) DPSMeter console spam

#### v1.3.2
 - Added $highestmultikill for StatsDisplay to keep track of your largest multikill of the run
 - This was added by request, if you want anything else added to the StatsDisplay, please just let me know. 

#### v1.3.1
 - Fixed CommandCountersHideOnZero not showing tooltips without counters

### v1.3.0
 - Clientside DPS Meter
 - StatsDisplay can now list DPS
 - StatsDisplay can now attach to the objective panel to automatically move out of the way
 - StatsDisplay can be fully formatted (See: http://digitalnativestudios.com/textmeshpro/docs/rich-text/)
 - Added ability remove background blur from the command menu
 - ItemStatsMod integration for the command and scrapper window
 - Many of these changes were directly requested so feel free to make suggestions to me on discord or on github. 
 - The config file has moved and split up which means your settings will be reset, but with all the new changes I encourage you to check options you might've disabled before. 

#### v1.2.3
 - Fixed a potential scrapper multiplayer bug

#### v1.2.2
 - Added ability to close command/scrapper menu with Escape, WASD or a custom key
 - Added ability to scale the command window to the number of items shown, this should help with mods that add new items.
 - removed debug spam ( sorry ) 

#### v1.2.1
 - Fixed bug with command and scrapper menu

### v1.2.0
 - Added command menu item counters and tooltips 

#### v1.1.6
 - Added AdvancedTooltips for equipement 

#### v1.1.5
 - Added ItemIndex (ID) Sorting

#### v1.1.4
 - Added special command menu sorting which keeps most recently picked item in the middle

#### v1.1.3
 - Fixed Equipments with sorted command window.

#### v1.1.2
 - Added Sorting to Scrapper and Command Menu

#### v1.1.1
 - Fixed $killcount not working

### v1.1.0
 - Added StatsDisplay

#### v1.0.1
 - Fixed up the README
 - Internal Changes

### v1.0.0
 - Inital Release