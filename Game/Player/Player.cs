using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game
{
    public class Player
    {
        public Room CurrentRoom;
        public string Name { get; set; }
        public int PlayerX;
        public int PlayerY;
        public int level = 1;
        public int currentXP = 0;
        public int maxHP = 100;
        public int currentHP;
        public int Energy = 100;
        public List<Item> inventory;
        public PlayerEquipment equipment;
        public List<Potions> potionBonus;
        public Random random = new Random();
        public Player(string name)
        {
            inventory = new List<Item>();
            equipment = new PlayerEquipment();
            potionBonus = new List<Potions>();
            currentXP = 0;
            currentHP = maxHP;
            Name = name;
        }

        public void Move(Direction direction)
        {
            int new_X = PlayerX;
            int new_Y = PlayerY;
            switch (direction)
            {
                case Direction.North:
                    new_Y++;
                    break;
                case Direction.South:
                    new_Y--;
                    break;
                case Direction.West:
                    new_X--;
                    break;
                case Direction.East:
                    new_X++;
                    break;
                default:
                    break;
            }
            DecrementPotionRound();
            CurrentRoom.PlayerMove(this, new_X, new_Y);

        }

        public void DecrementPotionRound()
        {
            for (int i = 0; i < potionBonus.Count; i++)
            {
                potionBonus[i].rounds--;
                if(potionBonus[i].rounds == 0)
                {
                    potionBonus.RemoveAt(i);
                }
            }
        }

        public bool IsDead()
        {
            return currentHP == 0;
        }

        public void UseEnergy(int modifier)
        {
            if(CheckEnergy(modifier))
            {
                Energy -= modifier;
            }
        }

        public bool CheckEnergy(int modifier)
        {
            return Energy - modifier >= 0;
        }

        public void Rest()
        {
            Energy = 100;
        }


        public Item HasItem(string itemName)
        {
            Item found = null;
            foreach (Item i in inventory)
            {
                if (i.Name.ToLower().Equals(itemName.ToLower()))
                {
                    found = i;
                }
            }
            return found;
        }


        public void EquipItem(Item i, bool rightHand = true)
        {
            Item oldItem;
            if(i.GetType() == typeof(Weapon))
            {
                oldItem = equipment.EquipWeapon((Weapon)i, rightHand);
            }
            else
            {
                oldItem = equipment.EquipArmor((Armor)i);
            }
            if(oldItem != null)
            {
                Console.WriteLine(oldItem.Name + " added to inventory");
                inventory.Add(oldItem);
            }
           
      
        }

        public int CalculateProtection()
        {
            int potionProtectionBonus = 0;
            foreach (Potions p in potionBonus)
            {
                potionProtectionBonus += p.protectionBonus;
            }
            return equipment.CalculateProtection();
        }

        public int TakeDamage(int damageDone)
        {
            damageDone -= CalculateProtection() / 10;
            if (damageDone < 0) damageDone = 0;
            this.currentHP -= damageDone;
            if (currentHP <= 0)
            {
                currentHP = 0;
            }
            return damageDone;
        }

        public int CalculateFlatDamage()
        {
            int damage = 0;
            foreach (Potions p in potionBonus)
            {
                damage += p.damageBonus;
            }
            damage += equipment.CalculateDamage();
            return damage;
        }

        public int CalculateCritBonus()
        {
            int critFactor = 0;
            foreach (Potions p in potionBonus)
            {
                critFactor += p.critBonus;
            }
            critFactor += equipment.CalculateCritFactor();
            return critFactor;
        }
        public int CalculateDamage()
        {
            int damage = CalculateFlatDamage();
            if (random.Next(0, 100) < CalculateCritBonus()) damage *= 2;
            return damage;
        }

        public string UseItem(Item i)
        {
            if(i.GetType() == typeof(Potions))
            {
                Potions p = (Potions)i;
                if(p.effectOverTime)
                {
                    potionBonus.Add(p);
                }
                else
                {
                    currentHP += p.healing;
                    if (currentHP > maxHP) currentHP = maxHP;

                }
                if(i.Stackable)
                {
                    if(i.GetStackCount() == 0)
                    {
                        inventory.Remove(i);
                    }
                    else
                    {
                        i.ReduceStack();
                    }
                }
                else
                {
                    inventory.Remove(i);
                }
               
                return "Item used";
            }
            return "Cannot use item";
        }

        public void AddXp(int xp)
        {
            currentXP += xp;
            if(currentXP > 1000*level)
            {
                level++;
                maxHP = (int)(Math.Floor(1.5d * maxHP));
                currentHP = maxHP;
            }
        }

        public override string ToString()
        {
            string s = "Name : " + Name + "\n";
            s += "Level : " + level + "\n";
            s += "XP : " + currentXP + "\n";
            s += "HP : " + currentHP + "/" + maxHP + "\n";
            s += "Energy : " + Energy + "/100" + "\n";
            s += "Armor: \n";
            s += "---------- \n";
            s += "Head : " + equipment.Head.ToString() + "\n";
            s += "Shoulders : " + equipment.Shoulders.ToString() + "\n";
            s += "Chest : " + equipment.Chest.ToString() + "\n";
            s += "Hands : " + equipment.Hands.ToString() + "\n";
            s += "Legs : " + equipment.Legs.ToString() + "\n";
            s += "Boots : " + equipment.Boots.ToString() + "\n";
            s += "---------- \n";
            s += "Weapons: \n";
            if (equipment.IsTwoHanded())
            {
                s += "Both hands : " + equipment.RightHand.ToString() + "\n";
            }
            else
            {
                if (equipment.RightHand != null) s += "Right Hand : " + equipment.RightHand.ToString() + "\n";
                if (equipment.LeftHand != null) s += "Left Hand : " + equipment.LeftHand.ToString() + "\n";
            }
            s += "Active potions: \n";
            s += "---------- \n";
            foreach (Potions p in potionBonus)
            {
                s += p.ToString();
            }
            return s;
        }
    }
}
