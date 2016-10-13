using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class CommandHandler
    {
        World world;

        public CommandHandler(World world)
        {
            this.world = world;
        }

        public void Help()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string s = "Commands are : \n";
            s += "Start for new game or restart \n";
            s += "Move for interactive mode \n";
            s += "Rest to regain energy \n";
            s += "Stats for player stats and effects \n";
            s += "Look to look around and spot items \n";
            s += "Inventory to show your inventory \n";
            s += "Take <item name> to pick up \n";
            s += "Use <item name> to use an item in your inventory";
            s += "Equip <item name> to equip an item";
            s += "Attack <monster name> to attack (Combat only)\n";
            s += "Retreat to attempt retreating from combat (Combat only)\n";
            s += "Legend to explain different icons\n";
            s += "Help \n";
            s += "Map \n";
            s += "Exit \n";
            Console.WriteLine(s);
            Console.ResetColor();
        }

        public void Move(Player player)
        {
            bool isMoving = true;
            while (isMoving && !player.IsDead())
            {
                world.RefreshMap();
                Console.WriteLine("Press q to quit interactive mode");
                ConsoleKeyInfo input = Console.ReadKey(true);
                Direction direction = world.GetDirection(input);
                if (direction == Direction.Stop)
                {
                    isMoving = false;
                }
                else
                {
                    player.Move(direction);
                }
            }
            if(!player.IsDead()) Console.WriteLine("Leaving interactive mode");

        }

        public void Look(Player player)
        {
            string s = player.CurrentRoom.ToString();
            List<Tuple<int, int, Item>> itemsFound = RoomItemHandler.GetItemsAtPlayerPosition(player.CurrentRoom,player);
            if (itemsFound.Count > 0)
            {
                s += "\nYou see the following items: \n";
                foreach (Tuple<int, int, Item> t in itemsFound)
                {
                    s += t.Item3.ToString() + "\n";
                }
            }
            else
            {
                s += "\nYou find no items!";
            }
            Console.WriteLine(s);
        }

        public void Take(string[] arr, Player player)
        {
            string itemName = CombineItemName(arr);
            List<Tuple<int, int, Item>> itemsFound = RoomItemHandler.GetItemsAtPlayerPosition(player.CurrentRoom, player);
            bool pickedUp = false;
            if (itemsFound.Count > 0)
            {
                foreach (Tuple<int, int, Item> t in itemsFound)
                {
                    if (t.Item3.Name.ToLower().Equals(itemName.ToLower()))
                    {
                        Item hasItem = player.HasItem(t.Item3.Name);
                        if (hasItem == null)
                        {
                            player.inventory.Add(t.Item3);
                        }
                        else
                        {
                            hasItem.AddToStack();
                        }
                     
                        RoomItemHandler.RemoveItem(player.CurrentRoom,t.Item1, t.Item2);
                        world.RefreshMap();
                        Console.WriteLine("You pick up " + t.Item3.Name);
                        pickedUp = true;
                    }
                }
                if(!pickedUp)
                {
                    Console.WriteLine("Nothing to pick up with that name!");
                }
            }
            else
            {
                Console.WriteLine("Nothing to pick up!");
            }
        }


        public void Use(string[] arr, Player p)
        {
            string itemName = CombineItemName(arr);
            Item i = p.HasItem(itemName);
            Console.WriteLine(p.UseItem(i));
        }

        public void Equip(string[] arr , Player p, bool rightHand = true)
        {
            string itemName = CombineItemName(arr);
            Item i = p.HasItem(itemName);
            if(i != null)
            {
                Console.WriteLine(i.Name + " equipped");
                p.EquipItem(i, rightHand);
                p.inventory.Remove(i);
            }
            else
            {
                Console.WriteLine("No item found..");
            }
         
        }

        private string CombineItemName(string[] arr)
        {
            string itemName = "";
            for (int i = 1; i < arr.Length; i++)
            {
                itemName += arr[i];
                if (i < arr.Length - 1) itemName += " ";
            }
            return itemName;
        }

        public void ShowInventory(Player p)
        {
            string s = "You have the following items: \n \n";
            foreach (Item i in p.inventory)
            {
                s += i.ToString() + "\n";
            }
            Console.WriteLine(s);
        }

        public void Loot(Player p, List<Item> items)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You are victorious!!");
            if(items.Count > 0)
            {
                Console.WriteLine("You loot the following items from the corpse");
                foreach (Item i in items)
                {
                    Console.WriteLine(i.ToString());
                    Item hasItem = p.HasItem(i.Name);
                    if (hasItem == null)
                    {
                        p.inventory.Add(i);
                    }
                    else
                    {
                        hasItem.AddToStack();
                    }
                }
            }
            else
            {
                Console.WriteLine("You do not find any items on the corpse");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        public void Legend()
        {
            string s = "P -> Player \n";
            s += "M -> Monster \n";
            s += "B -> Boss \n";
            s += "T -> Tree \n";
            s += "X -> Wall \n";
            s += "I -> Item \n";
            s += "O -> House \n";
            Console.WriteLine(s);
        }
    }
}
