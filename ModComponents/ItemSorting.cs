using System;
using RoR2;
using BepInEx;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace BetterUI
{
    static class ItemSorting
    {
        internal static void Hook()
        {
            if (ConfigManager.SortingSortItemsInventory.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.ItemInventoryDisplay>("OnInventoryChanged", ItemInventoryDisplay_OnInventoryChanged);
            }
            if (ConfigManager.SortingSortItemsCommand.Value && ConfigManager.SortingSortOrderCommand.Value.Contains("C"))
            {
                BetterUIPlugin.Hooks.Add<RoR2.PickupPickerController, int>("SubmitChoice", CommandImprovements.PickupPickerController_SubmitChoice);
            }
        }

        public static List<EquipmentIndex> sortItems(List<EquipmentIndex> equipmentList, String sortOrder)
        {
            IOrderedEnumerable<EquipmentIndex> finalOrder = equipmentList.OrderBy(a => 1);
            foreach (char c in sortOrder.ToCharArray())
            {
                switch (c)
                {
                    case '6': // Alphabetical
                        finalOrder = finalOrder.ThenBy(equipment => Language.GetString(EquipmentCatalog.GetEquipmentDef(equipment).nameToken));
                        break;
                    case '7': // Alphabetical Reversed
                        finalOrder = finalOrder.ThenByDescending(equipment => Language.GetString(EquipmentCatalog.GetEquipmentDef(equipment).nameToken));
                        break;
                    case '8': // Random"
                        Random random = new Random();
                        finalOrder = finalOrder.ThenBy(equipment => random.Next());
                        break;
                }
            }
            return equipmentList;
        }

        private delegate IOrderedEnumerable<ItemIndex> DirectSorter(IOrderedEnumerable<ItemIndex> order);
        private delegate bool ItemFilter(ItemIndex item);
        private delegate T ItemSorter<T>(IOrderedEnumerable<ItemIndex> order, Inventory inventory, ItemIndex item);

        private static ItemFilter scrapFilter = item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Scrap);
        private static ItemFilter damageFilter = item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Damage);
        private static ItemFilter healingFilter = item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Healing);
        private static ItemFilter utilityFilter = item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Utility);
        private static ItemFilter onKillEffectFilter = item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.OnKillEffect);
        private static ItemFilter equipmentRelatedFilter = item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.EquipmentRelated);
        private static ItemFilter sprintRelatedFilter = item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.SprintRelated);
        private static Dictionary<Char, ItemFilter> tierFilters = new Dictionary<char, ItemFilter>()
        {
            { '1', item => ItemCatalog.GetItemDef(item).tier == ItemTier.Tier1 || ItemCatalog.GetItemDef(item).tier == ItemTier.VoidTier1 },
            { '2', item => ItemCatalog.GetItemDef(item).tier == ItemTier.Tier2 || ItemCatalog.GetItemDef(item).tier == ItemTier.VoidTier2 },
            { '3', item => ItemCatalog.GetItemDef(item).tier == ItemTier.Tier3 || ItemCatalog.GetItemDef(item).tier == ItemTier.VoidTier3 },
            { 'L', item => ItemCatalog.GetItemDef(item).tier == ItemTier.Lunar },
            { 'B', item => ItemCatalog.GetItemDef(item).tier == ItemTier.Boss || ItemCatalog.GetItemDef(item).tier == ItemTier.VoidBoss },
            { 'N', item => ItemCatalog.GetItemDef(item).tier == ItemTier.NoTier},
        };

        //0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier
        private static Dictionary<ItemTier, int> tierMap = new Dictionary<ItemTier, int>()
        {
            { ItemTier.Tier1, 0 },
            { ItemTier.VoidTier1, 0 },
            { ItemTier.Tier2, 1 },
            { ItemTier.VoidTier2, 1 },
            { ItemTier.Tier3, 2 },
            { ItemTier.VoidTier3, 2 },
            { ItemTier.Lunar, 3 },
            { ItemTier.Boss, 4 },
            { ItemTier.VoidBoss, 4 },
            { ItemTier.NoTier, 5 },
        };


        private static ItemSorter<bool> scrapSorter = (order, inventory, item) => scrapFilter(item);
        private static ItemSorter<bool> damageSorter = (order, inventory, item) => damageFilter(item);
        private static ItemSorter<bool> healingSorter = (order, inventory, item) => healingFilter(item);
        private static ItemSorter<bool> utilitySorter = (order, inventory, item) => utilityFilter(item);
        private static ItemSorter<bool> onKillEffectSorter = (order, inventory, item) => onKillEffectFilter(item);
        private static ItemSorter<bool> equipmentRelatedSorter = (order, inventory, item) => equipmentRelatedFilter(item);
        private static ItemSorter<bool> sprintRelatedSorter = (order, inventory, item) => sprintRelatedFilter(item);
        private static ItemSorter<int> tierSorter = (order, inventory, item) =>
        {
            if (tierMap.TryGetValue(ItemCatalog.GetItemDef(item).tier, out int value)){
                return ConfigManager.SortingTierOrder[value];
            }
            return ConfigManager.SortingTierOrder[5];
        };
        private static ItemSorter<int> stackSorter = (order, inventory, item) => inventory.itemStacks[(int)item];
        private static ItemSorter<int> pickupSorter = (order, inventory, item) => inventory.itemAcquisitionOrder.IndexOf(item);
        private static ItemSorter<String> alphabeticalSorter = (order, inventory, item) => Language.GetString(ItemCatalog.GetItemDef(item).nameToken);
        private static ItemSorter<int> indexSorter = (order, inventory, item) => (int)item;
        private static ItemSorter<int> randomSorter = (order, inventory, item) =>
        {
            Random random = new Random();
            return random.Next();
        };

        private static Dictionary<Char, ItemSorter<bool>> tierSorters = new Dictionary<char, ItemSorter<bool>>()
        {
            { '1', (list, inv, item) => ItemCatalog.GetItemDef(item).tier == ItemTier.Tier1 || ItemCatalog.GetItemDef(item).tier == ItemTier.VoidTier1 },
            { '2', (list, inv, item) => ItemCatalog.GetItemDef(item).tier == ItemTier.Tier2 || ItemCatalog.GetItemDef(item).tier == ItemTier.VoidTier2 },
            { '3', (list, inv, item) => ItemCatalog.GetItemDef(item).tier == ItemTier.Tier3 || ItemCatalog.GetItemDef(item).tier == ItemTier.VoidTier3 },
            { 'L', (list, inv, item) => ItemCatalog.GetItemDef(item).tier == ItemTier.Lunar },
            { 'B', (list, inv, item) => ItemCatalog.GetItemDef(item).tier == ItemTier.Boss || ItemCatalog.GetItemDef(item).tier == ItemTier.VoidBoss },
            { 'N', (list, inv, item) => ItemCatalog.GetItemDef(item).tier == ItemTier.NoTier },
        };

        private static DirectSorter commandSorter = (IOrderedEnumerable<ItemIndex> order) =>
        {
            ItemDef firstItemDef = ItemCatalog.GetItemDef(order.First());
            if (firstItemDef != null && CommandImprovements.lastItem[(int)firstItemDef.tier] != ItemIndex.None && order.Contains(CommandImprovements.lastItem[(int)firstItemDef.tier]))
            {
                int roundUp = (int)Math.Ceiling((double)order.Count() / 5) * 5;
                int offset;
                if (roundUp == 5)
                {
                    offset = order.Count() / 2;
                }
                else if (roundUp % 10 == 0)
                {
                    offset = (roundUp / 2) - 3;
                }
                else
                {
                    offset = roundUp / 2;
                }
                List<ItemIndex> finalOrderList = order.ToList();
                finalOrderList.Remove(CommandImprovements.lastItem[(int)firstItemDef.tier]);
                finalOrderList.Insert(offset, CommandImprovements.lastItem[(int)firstItemDef.tier]);
                order = finalOrderList.OrderBy(a => 1);
            }
            return order;
        };


        private static IOrderedEnumerable<ItemIndex> filteredSort<T>(IOrderedEnumerable<ItemIndex> list, Inventory inventory, ItemSorter<T> sorter, ItemFilter filter = null, bool reversed = false)
        {

            if (filter != null)
            {
                List<int> filteredIndexes = list.Select((x, i) => new { Value = x, Index = i }).Where(x => filter(x.Value)).Select(x => x.Index).ToList();
                List<ItemIndex> filteredItems = list.Select((x, i) => new { Value = x, Index = i }).Where(x => filter(x.Value)).Select(x => x.Value).ToList();
                List<ItemIndex> tempList = list.ToList();
                if (reversed)
                {
                    filteredItems = filteredItems.OrderByDescending(item => sorter(list, inventory, item)).ToList();
                }
                else
                {
                    filteredItems = filteredItems.OrderBy(item => sorter(list, inventory, item)).ToList();
                }
                foreach (var x in filteredIndexes.Select((x, i) => new { Value = x, Index = i }))
                {
                    tempList[x.Value] = filteredItems[x.Index];
                }
                return tempList.OrderBy(x => 1);
            }
            else
            {
                if (reversed)
                {
                    return list.OrderByDescending(item => sorter(list, inventory, item));
                }
                else
                {
                    return list.OrderBy(item => sorter(list, inventory, item));
                }
            }
        }
        struct SortStep
        {
            public ItemFilter filter;
            public ItemSorter<int> intSorter;
            public ItemSorter<bool> boolSorter;
            public ItemSorter<String> stringSorter;
            public DirectSorter directSorter;
            public bool reversed;
        }

        public static List<ItemIndex> sortItems(List<ItemIndex> itemList, Inventory inventory, String sortOrder)
        {
            List<SortStep> steps = new List<ItemSorting.SortStep>();
            bool filtering = false;
            bool tierSelect = false;
            bool tierReversed = false;
            ItemFilter nextFilter = null;
            IOrderedEnumerable<ItemIndex> finalOrder = itemList.OrderBy(a => 1);
            foreach (char c in sortOrder.ToCharArray())
            {
                if (tierSelect)
                {
                    if (filtering)
                    {
                        tierFilters.TryGetValue(Char.ToUpper(c), out nextFilter);
                        filtering = false;
                    }
                    else
                    {
                        if (tierSorters.TryGetValue(Char.ToUpper(c), out var tierSorter)){
                            steps.Add(new SortStep { filter = nextFilter, boolSorter = tierSorter, reversed = tierReversed });
                        }
                    }
                    tierSelect = false;
                    continue;
                }

                if (filtering)
                {
                    switch (c)
                    {
                        case 's':
                        case 'S': // Scrap Only 
                            nextFilter = scrapFilter;
                            break;
                        case 'd':
                        case 'D': // Damage Only
                            nextFilter = damageFilter;
                            break;
                        case 'h':
                        case 'H': // Healing Only
                            nextFilter = healingFilter;
                            break;
                        case 'u':
                        case 'U': // Utility Only
                            nextFilter = utilityFilter;
                            break;
                        case 'o':
                        case 'O': // OnKillEffect Only
                            nextFilter = onKillEffectFilter;
                            break;
                        case 'e':
                        case 'E': // EquipmentRelated Only
                            nextFilter = equipmentRelatedFilter;
                            break;
                        case 'p':
                        case 'P': // SprintRelated Only
                            nextFilter = sprintRelatedFilter;
                            break;
                    }
                    filtering = false;
                    continue;
                }

                switch (c)
                {
                    case '#':
                        filtering = true;
                        continue;
                    case 't':
                    case 'T':
                        tierSelect = true;
                        tierReversed = Char.IsLower(c);
                        continue;
                    case '0':
                    case '1':  // Tier
                        steps.Add(new SortStep { filter = nextFilter, intSorter = tierSorter, reversed = c == '1' });
                        break;
                    case '2':
                    case '3': // Stack Size
                        steps.Add(new SortStep { filter = nextFilter, intSorter = stackSorter, reversed = c == '3' });
                        break;
                    case '4':
                    case '5':// Pickup Order
                        steps.Add(new SortStep { filter = nextFilter, intSorter = pickupSorter, reversed = c == '5' });
                        break;
                    case '6':
                    case '7': // Alphabetical
                        steps.Add(new SortStep { filter = nextFilter, stringSorter = alphabeticalSorter, reversed = c == '7' });
                        break;
                    case '8': // Random"
                        steps.Add(new SortStep { filter = nextFilter, intSorter = randomSorter });
                        break;
                    case 'C': case 'c': // Special Command Centered
                        steps.Add(new SortStep { filter = nextFilter, directSorter = commandSorter });
                        break;
                    case 'i':
                    case 'I': // ItemIndex
                        steps.Add(new SortStep { filter = nextFilter, intSorter = indexSorter, reversed = Char.IsUpper(c) });
                        break;
                    case 's':
                    case 'S': // Scrap Only 
                        steps.Add(new SortStep { filter = nextFilter, boolSorter = scrapSorter, reversed = Char.IsLower(c) });
                        break;
                    case 'd':
                    case 'D': // Damage Only
                        steps.Add(new SortStep { filter = nextFilter, boolSorter = damageSorter, reversed = Char.IsLower(c) });
                        break;
                    case 'h':
                    case 'H': // Healing Only
                        steps.Add(new SortStep { filter = nextFilter, boolSorter = healingSorter, reversed = Char.IsLower(c) });
                        break;
                    case 'u':
                    case 'U': // Utility Only
                        steps.Add(new SortStep { filter = nextFilter, boolSorter = utilitySorter, reversed = Char.IsLower(c) });
                        break;
                    case 'o':
                    case 'O': // OnKillEffect Only
                        steps.Add(new SortStep { filter = nextFilter, boolSorter = onKillEffectSorter, reversed = Char.IsLower(c) });
                        break;
                    case 'e':
                    case 'E': // EquipmentRelated Only
                        steps.Add(new SortStep { filter = nextFilter, boolSorter = equipmentRelatedSorter, reversed = Char.IsLower(c) });
                        break;
                    case 'p':
                    case 'P': // SprintRelated Only
                        steps.Add(new SortStep { filter = nextFilter, boolSorter = sprintRelatedSorter, reversed = Char.IsLower(c) });
                        break;
                }
                nextFilter = null;
            }
            steps.Reverse();
            foreach (var step in steps)
            {
                if (step.boolSorter != null) finalOrder = filteredSort(finalOrder, inventory, step.boolSorter, step.filter, step.reversed);
                else if (step.intSorter != null) finalOrder = filteredSort(finalOrder, inventory, step.intSorter, step.filter, step.reversed);
                else if (step.stringSorter != null) finalOrder = filteredSort(finalOrder, inventory, step.stringSorter, step.filter, step.reversed);
                else if (step.directSorter != null) finalOrder = step.directSorter(finalOrder);
            }
            return finalOrder.ToList();
        }

        public static void ItemInventoryDisplay_OnInventoryChanged(Action<RoR2.UI.ItemInventoryDisplay> orig, RoR2.UI.ItemInventoryDisplay self)
        {
            orig(self);

            if (self.itemOrder != null && self.inventory && self.inventory.itemAcquisitionOrder.Any())
            {
                sortItems(self.inventory.itemAcquisitionOrder, self.inventory, ConfigManager.SortingSortOrder.Value).ToList().CopyTo(self.itemOrder);
            }
        }
    }
}
