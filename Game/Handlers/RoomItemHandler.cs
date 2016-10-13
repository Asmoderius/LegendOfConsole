using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class RoomItemHandler
    {
        private static Random rand = new Random();
        public static void RandomizeItems(Room r)
        {
            switch (r.roomType)
            {
                case RoomType.Forest:
                    AddRandomItemToRoom(r, 5, 4, 55, 10, 0);
                    break;
                case RoomType.Wasteland:
                    AddRandomItemToRoom(r, 5, 2, 30, 5, 0);
                    break;
                case RoomType.Town:
                    AddRandomItemToRoom(r, 10, 4, 55, 25, 0);
                    break;
                case RoomType.Cavern:
                    AddRandomItemToRoom(r, 10, 3, 40, 10, 5);
                    break;
                case RoomType.BossRoom:
                    AddRandomItemToRoom(r, 20, 6, 40, 0, 70);
                    break;
                default:
                    break;
            }
        }

        private static void AddRandomItemToRoom(Room r, int randomAttempts, int maxCount, int chanceCommon, int chanceEquipment, int chanceRare)
        {
            for (int i = 0; i < randomAttempts; i++)
            {
                int commonChance = rand.Next(1, 101);
                int equipmentChance = rand.Next(1, 101);
                int rareChance = rand.Next(1, 101);
                if (commonChance <= chanceCommon)
                {
                    AddItem(5, r,ItemHandler.GetRandomCommonItem());
                }
                if (equipmentChance <= chanceEquipment)
                {
                    AddItem(5, r, ItemHandler.GetRandomCommonEquipment());
                }
                if (rareChance <= chanceRare)
                {
                    AddItem(5, r, ItemHandler.GetRandomRareEquipment());
                }
                maxCount--;
                if (maxCount == 0)
                {
                    break;
                }
            }
        }


        public static void AddItem(int attempts, Room r, Item i)
        {
            bool itemPlaced = false;
            int x = 0;
            int y = 0;
            while (!itemPlaced && attempts > 0)
            {
                x = rand.Next(1, r.RoomMap.GetLength(0) - 2);
                y = rand.Next(1, r.RoomMap.GetLength(1) - 2);
                if (r.RoomMap[x, y] == ' ')
                {
                    r.RoomMap[x, y] = 'I';
                    r.Items.Add(new Tuple<int, int>(x, y), i);
                    itemPlaced = true;
                }
                else
                {
                    attempts--;
                }
            }
        }

        public static List<Tuple<int, int, Item>> GetItemsAtPlayerPosition(Room r, Player p)
        {
            //All 8 fields, check against Dictionary. If exists, add to list. Add map position. 
            List<Tuple<int, int, Item>> itemsNear = new List<Tuple<int, int, Item>>();
            for (int i = p.PlayerX - 1; i <= p.PlayerX + 1; i++)
            {
                for (int h = p.PlayerY - 1; h <= p.PlayerY + 1; h++)
                {
                    if (!(i == p.PlayerX && h == p.PlayerY) && (i > 0 && i < r.RoomMap.GetLength(0) - 1 && h > 0 && h < r.RoomMap.GetLength(1) - 1))
                    {
                        Item found;
                        r.Items.TryGetValue(new Tuple<int, int>(i, h), out found);
                        if (found != null)
                        {
                            itemsNear.Add(new Tuple<int, int, Item>(i, h, found));
                        }
                    }
                }
            }
            return itemsNear;
        }

        public static void RemoveItem(Room r, int x, int y)
        {
            Tuple<int, int> key = new Tuple<int, int>(x, y);
            if (r.Items.ContainsKey(key))
            {
                r.Items.Remove(key);
                r.RoomMap[x, y] = ' ';
            }
        }
    }
}
