using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public enum CombatResult
    {
        PlayerWin,
        Retreat,
        MonsterWin
    }
    public static class CombatHandler
    {
        private static Random random = new Random();
        private static string combatText = "";
        //Returns true if player wins.
        public static CombatResult Combat(Player p, Monster m, bool playerAttacked)
        {
            combatText = "";
            CombatResult result = CombatResult.PlayerWin;
            Console.Clear();
            bool inCombat = true;
            bool retreat = false;
            while (inCombat)
            {
                DisplayCombatField(p, m, playerAttacked);
                combatText = "";
                if (playerAttacked)
                {
                    retreat = ReadCombatInput(p, m);
                    if (retreat) inCombat = false;
                    MonsterAttack(p, m);
                }
                else
                {
                    MonsterAttack(p, m);
                    retreat = ReadCombatInput(p, m);
                    if (retreat) inCombat = false;
                }

                if (p.IsDead() || m.IsDead())
                {
                    inCombat = false;
                }
            }
            if(p.IsDead())
            {
                result = CombatResult.MonsterWin;
            }
            if (m.IsDead())
            {
                GenerateLoot(p, m);
                p.AddXp(m.Level * 100);
            }
            if(retreat)
            {
                result = CombatResult.Retreat;
            }

            return result;
        }

        private static void MonsterAttack(Player p, Monster m)
        {
            int m_DamageDone = p.TakeDamage(m.CalculateDamage()); 
            combatText += "The " + m.Name + " deals " + m_DamageDone + " damage to you \n";
        }

        private static bool ReadCombatInput(Player p, Monster m)
        {
            string s = Console.ReadLine();
            string[] arr = s.Split(' ');
            bool retreat = false;
            switch (arr[0])
            {
                case "attack":
                    int damageDone = m.TakeDamage(p.CalculateDamage());
                    combatText += "You deal " + damageDone + " damage to the " + m.Name + "\n";
                    p.DecrementPotionRound();
                    break;
                case "retreat":
                    Console.WriteLine("You attempt to retreat from combat. Press any key to roll your chance");
                    Console.ReadKey(true);
                    if (random.Next(0, 100) >= 25)
                    {
                        Console.WriteLine("You make a hasty retreat from combat. The monster seems confused by your cowardice. Press any key.");
                        Console.ReadKey(true);
                        retreat = true;
                    }
                    else
                    {
                        Console.WriteLine("The monster blocks your escape. The battle continues. Press any key.");
                        Console.ReadKey(true);
                    }
                    break;
                case "inventory":
                    World.commands.ShowInventory(p);
                    Console.WriteLine("Press any key to return to Combat screen");
                    Console.ReadKey(true);
                    break;
                case "use":
                    World.commands.Use(arr, p);
                    break;
                default:
                    break;
            }
            return retreat;
        }

        private static void GenerateLoot(Player p, Monster m)
        {
            List<Item> loot = new List<Item>();
            if (m.Level > 10)
            {
                int rareItemsToGet = random.Next(0, 2);
                for (int i = 0; i < rareItemsToGet; i++)
                {
                    loot.Add(ItemHandler.GetRandomRareEquipment());
                    loot.Add(ItemHandler.GetRandomCommonItem());
                }
            }
            else
            {
                int itemToGet = random.Next(0, 2);
                for (int i = 0; i < itemToGet; i++)
                {
                    loot.Add(ItemHandler.GetRandomCommonEquipment());
                    loot.Add(ItemHandler.GetRandomCommonItem());
                }
            }
            World.commands.Loot(p, loot);
        }

        private static void DisplayCombatField(Player p, Monster m, bool playerAttacked)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------- Combat ------------- \n");
            if (playerAttacked)
            {
                Console.WriteLine("You attack... \n  \n ");
            }
            else
            {
                Console.WriteLine("A monster ambushes you... \n  \n ");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(m.ToString());
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n \n---------------------------------------- \n \n \n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(p.ToString());
            if(combatText != string.Empty)
            {
                Console.WriteLine("\n \n------------- Combat result ------------- \n \n");
                Console.WriteLine(combatText);
            }
    
            Console.ResetColor();

        }
    }
}
