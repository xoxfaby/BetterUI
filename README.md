# BetterUI

A simple mod that adds various UI improvements.  
Each can be disabled and configured in the config file.

## Support Me

If you like what I'm doing, consider supporting me through GitHub Sponsors so I can keep doing it:

https://github.com/sponsors/xoxfaby

## Currently implemented:
- ItemStats
- Item Counters
- DPS Meter
- StatsDisplay
- Ordered Inventory, Scrapper Menu, Command Menu
- Command/Scrapper Menu Item Counters and Tooltips
- Close the command menu with escape, WASD or a custom keybind
- Automatically resize the command window if there are more items
- Remove background blur from command window
- Advanced Item Descriptions
- Advanced Equipment Descriptions
- Hide Pickup Notifications
- Skill Proc Coefficients with calculated effects based on the items you are carrying
- Buff Timers and Tooltips
- Show Hidden Items

## Help & Feedback

If you need help or have suggestions, create an issue on github, join my discord or find me on the RoR2 Modding Discord

[My Discord](https://discord.gg/Zy2HSB4) XoXFaby#1337

Please do not add me on discord, join of the the two servers and ping me there.

https://github.com/xoxfaby/BetterUI

## Features

### ItemStats
Show calculcated stat bonuses for your items.

![ItemStats](https://cdn.faby.dev/itemstats.png)

### ItemCounters
Customizable ItemCounters. Choose which tiers you want counted, choose which tiers to show.  
Use ItemScore to not just see how many items you have but how good they are.
By default item score is based on tiers, but you can change how much each tier is worth or even set custom values for each item!

![ItemCounters](https://cdn.faby.dev/itemcounters.png)

### DPSMeter
Fully clientside DPS Meter that can be integrated into the StatsDisplay. Counts minion damage!

![DPSMeter](https://cdn.faby.dev/dpsmeter.png)

### StatsDisplay
Show all of your character's stats! Completely customizable!

![StatsDisplay](https://cdn.faby.dev/statsdisplay.png)

### Skill Proc Coefficient Information

![Skill Tooltip](https://cdn.faby.dev/skilltooltip.png)

### Command/Scrapper Improvements
See how many items you have when using the scrapper or picking an item using the command artifact!
Tooltips with ItemStats!
Close the command/scrapper window with Escape, WASD or a custom keybind!

![Command Counters](https://cdn.faby.dev/commandcounters.png)

### Improved Item Sorting
Sort items alphabetically, by tier, stacks or even tags like "Scrap" or "Damage". EVEN RANDOMLY?!

![Item Sorting](https://cdn.faby.dev/itembar.png)

![Sorted Scrapper](https://cdn.faby.dev/sortedscrapper.png)

### BuffTimers & Tooltips
Buff timers currently only work if you are the host because the timers are not networked in multiplayer.

![Buff Timers](https://cdn.faby.dev/buffs.png)

### Advanced item descriptions
Use the advanced item descriptions from the logbook that show the actual numbers for all the changes.
Integration with ItemStats in the command and scrapper windows.

![Item Description](https://cdn.faby.dev/itemdesc.png)

### Show Hidden Items
Show hidden items like the hidden monsoon/drizzle items

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

The sorting is completely customizable.
The default sorting value is **S134**

**S** means the items are first sorted by the "Scrap" tag and all the scrap is put at the end of the list.  
**1** then sorts it by tier, in descending order, putting higher tier items at the front.
**3** sorts it by the stack size in descending order, meaning if you have more of an item, it will come first.
**4** then sorts it by pickup order, meaning items you got first, come first.

You can customize this in any way you like.

Filters:

\# + a tag will apply the next sorting to only items that match the tag.  
You can use any tags from the tag based sorting.   
Examples:  
`#s0` to sort scrap by tier ascending.  
`#t14#t28` to sort Tier 1 items by pickup order and tier 2 items randomly

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

t1 = Tier 1 First
t2 = Tier 2 First
t3 = Tier 3 First
tL = Lunar Tier First
tB = Boss Tier First
tN = NoTier First

T1 = Tier 1 Last
T2 = Tier 2 Last
T3 = Tier 3 Last
TL = Lunar Tier Last
TB = Boss Tier Last
TN = NoTier Last