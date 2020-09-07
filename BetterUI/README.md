# BetterUI

A simple mod that adds various UI improvements. Currently implemented:

#### Improved Item Sorting
Sort items alphabetically, by tier, stacks or even tags like "Scrap" or "Damage". EVEN RANDOMLY?!

![Item Sorting](https://fby.pw/itembar.png)

#### Advanced item descriptions
Use the advanced item descriptions from the logbook that show the actual numbers for all the changes. 

![Item Description](https://fby.pw/itemdesc.png)

#### Show Hidden Items
Show hidden items like the hidden monsoon/drizzle items

### Configuration

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

### Help

If you need help or have suggestions, create an issue on github or find me on the RoR2 Modding Discord @XoXFaby#1337

https://github.com/xoxfaby/BetterUI

### Changelog

#### v1.0.1
 - Fixed up the README
 - Interal Changes
