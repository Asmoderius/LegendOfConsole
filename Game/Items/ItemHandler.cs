using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game
{
    /*
     * Please! Don't worry.
     * Input parsing er ikke svært, men super grimt. 
     * Der findes smukke måder at indlæse og parse, men så bliver det svært.
     * */
    public static class ItemHandler
    {
        private static List<Item> common;
        private static List<Item> common_equipment;
        private static List<Item> rare_equipment;
        private static List<Item> legendary_equipment;
        static Random  random;



        public static void ReadFile()
        {
            common = new List<Item>();
            common_equipment = new List<Item>();
            rare_equipment = new List<Item>();
            legendary_equipment = new List<Item>();
            random = new Random();
            if (File.Exists("items.txt"))
            {
                StreamReader sReader = new StreamReader("items.txt");
                string line; 
                while((line = sReader.ReadLine()) != null)
                {
                    string[] arr = line.Split('@');
                    switch (arr[0])
                    {
                        case "C":
                            AddCommon(arr);
                            break;
                        case "P":
                            AddPotion(arr);
                            break;
                        case "CE":
                            AddCommonEquipment(arr);
                            break;
                        case "RE":
                            AddRareEquipment(arr);
                            break;
                        case "LE":
                            AddLegendaryEquipment(arr);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void AddCommon(string[] arr)
        {
            common.Add(new Item(arr[1], arr[2], int.Parse(arr[3])));
        }

        private static void AddPotion(string[] arr)
        {
            common.Add(new Potions(arr[1], arr[2], int.Parse(arr[3]), int.Parse(arr[4]), int.Parse(arr[5]), int.Parse(arr[6]), int.Parse(arr[7]), bool.Parse(arr[8]),int.Parse(arr[9])));
        }
        private static void AddCommonEquipment(string[] arr)
        {
            if(arr[1] == "A")
            {
                EquipmentSlot slot = GetSlot(arr);
                common_equipment.Add(new Armor(arr[2], arr[3], int.Parse(arr[4]), int.Parse(arr[6]), slot));
            }
            else
            {
                common_equipment.Add(new Weapon(arr[2], arr[3], int.Parse(arr[4]), int.Parse(arr[5]), int.Parse(arr[6]), int.Parse(arr[7]), bool.Parse(arr[8]), int.Parse(arr[9]), bool.Parse(arr[10])));
            }
        }


        private static void AddRareEquipment(string[] arr)
        {
            if (arr[1] == "A")
            {
                EquipmentSlot slot = GetSlot(arr);
                rare_equipment.Add(new Armor(arr[2], arr[3], int.Parse(arr[4]), int.Parse(arr[6]), slot));
            }
            else
            {
                rare_equipment.Add(new Weapon(arr[2], arr[3], int.Parse(arr[4]), int.Parse(arr[5]), int.Parse(arr[6]), int.Parse(arr[7]), bool.Parse(arr[8]), int.Parse(arr[9]), bool.Parse(arr[10])));
            }
        }

        private static void AddLegendaryEquipment(string[] arr)
        {
            if (arr[1] == "A")
            {
                EquipmentSlot slot = GetSlot(arr);
                legendary_equipment.Add(new Armor(arr[2], arr[3], int.Parse(arr[4]), int.Parse(arr[6]), slot));
            }
            else
            {
                legendary_equipment.Add(new Weapon(arr[2], arr[3], int.Parse(arr[4]), int.Parse(arr[5]), int.Parse(arr[6]), int.Parse(arr[7]), bool.Parse(arr[8]), int.Parse(arr[9]), bool.Parse(arr[10])));
            }
        }

        public static Item GetRandomCommonItem()
        {
            return common[random.Next(0, common.Count)];
        }

        public static Item GetRandomCommonEquipment()
        {
            return common_equipment[random.Next(0, common_equipment.Count)];
        }

        public static Item GetRandomRareEquipment()
        {
            return rare_equipment[random.Next(0, rare_equipment.Count)];
        }

        public static Item GetRandomLegendaryEquipment()
        {
            return legendary_equipment[random.Next(0, legendary_equipment.Count)];
        }


        private static EquipmentSlot GetSlot(string[] arr)
        {
            EquipmentSlot slot = EquipmentSlot.Boots;
            switch (arr[7])
            {
                case "Head":
                    slot = EquipmentSlot.Head;
                    break;
                case "Shoulders":
                    slot = EquipmentSlot.Shoulders;
                    break;
                case "Hands":
                    slot = EquipmentSlot.Hands;
                    break;
                case "Chest":
                    slot = EquipmentSlot.Chest;
                    break;
                case "Legs":
                    slot = EquipmentSlot.Legs;
                    break;
                case "Boots":
                    slot = EquipmentSlot.Boots;
                    break;
                default:
                    break;
            }

            return slot;
        }

    }
}
