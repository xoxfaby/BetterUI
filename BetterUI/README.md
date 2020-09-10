# BetterUI

A simple mod that adds various UI improvements.  
Each be disabled and configured in the config file.

##### Currently implemented:
- StatsDisplay
- Ordered Inventory, Scrapper Menu, Command Menu
- Advanced Item Description
- Show Hidden Items

#### StatsDisplay
Show all of your character's stats! Completely customizable!

![StatsDisplay](https://fby.pw/statsdisplay.png)

#### Improved Item Sorting
Sort items alphabetically, by tier, stacks or even tags like "Scrap" or "Damage". EVEN RANDOMLY?!

![Item Sorting](https://fby.pw/itembar.png)

![Sorted Scrapper](https://fby.pw/sortedscrapper.png)

#### Advanced item descriptions
Use the advanced item descriptions from the logbook that show the actual numbers for all the changes. 
No calculations are implemented at this point, but might be in the future!

![Item Description](https://fby.pw/itemdesc.png)

#### Show Hidden Items
Show hidden items like the hidden monsoon/drizzle items

### Help & Feedback

If you need help or have suggestions, create an issue on github or find me on the RoR2 Modding Discord 

[RoR2 Modding Discord](https://discord.com/invite/5MbXZvd) Username: @XoXFaby#1337

https://github.com/xoxfaby/BetterUI

### Configuration

#### StatsDisplay

The StatsDisplay parses the `StatString` in the config file and replaces all the parameters it finds.
The StatsDisplay can also be moved, resized and recolored.
Here is a list of all valid parameters right now: 

$exp $level $luck  
$dmg $crit $atkspd  
$hp $maxhp $shield $maxshield $barrier $maxbarrier  
$armor $regen  
$movespeed $jumps $maxjumps  
$killcount $multikill  


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

### Changelog

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

#### v1.1.0
 - Added StatsDisplay

#### v1.0.1
 - Fixed up the README
 - Internal Changes
