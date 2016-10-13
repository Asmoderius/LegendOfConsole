using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PlayerEquipment
    {
        public Armor Head { get; set; }
        public Armor Shoulders { get; set; }
        public Armor Chest { get; set; }
        public Armor Hands { get; set; }
        public Armor Legs { get; set; }
        public Armor Boots { get; set; }
        public Weapon RightHand { get; set; }
        public Weapon LeftHand { get; set; }
        public static Random random = new Random();

        public Item EquipWeapon(Weapon weapon, bool rightHand)
        {
            Item oldWeapon = null;
            if (weapon.twoHanded)
            {
                oldWeapon = RightHand;
                RightHand = null;
                LeftHand = null;
                RightHand = weapon;
                LeftHand = weapon;
            }
            else
            {
                if (rightHand)
                {
                    oldWeapon = RightHand;
                    RightHand = weapon;
                }
                else
                {
                    oldWeapon = LeftHand;
                    LeftHand = weapon;
                }
            }
            return oldWeapon;
        }

        public Item EquipArmor(Armor armor)
        {
            Item oldArmor = null;
            switch (armor.requiredSlot)
            {
                case EquipmentSlot.Head:
                    oldArmor = Head;
                    Head = armor;
                    break;
                case EquipmentSlot.Shoulders:
                    oldArmor = Shoulders;
                    Shoulders = armor;
                    break;
                case EquipmentSlot.Chest:
                    oldArmor = Chest;
                    Chest = armor;
                    break;
                case EquipmentSlot.Hands:
                    oldArmor = Hands;
                    Hands = armor;
                    break;
                case EquipmentSlot.Legs:
                    oldArmor = Legs;
                    Legs = armor;
                    break;
                case EquipmentSlot.Boots:
                    oldArmor = Boots;
                    Boots = armor;
                    break;
                default:
                    break;
            }
            return oldArmor;
        }

        public int CalculateProtection()
        {
            int protection = 0;
            protection += Head.protectionRating;
            protection += Shoulders.protectionRating;
            protection += Chest.protectionRating;
            protection += Hands.protectionRating;
            protection += Legs.protectionRating;
            protection += Boots.protectionRating;
            if(LeftHand != null)
            {
                if (LeftHand.isShield)
                {
                    protection += LeftHand.protectionBonus;
                }
            }

            return protection;
        }

        public int CalculateDamage()
        {
            int damage = 0;
            if (IsTwoHanded())
            {
                damage = random.Next(RightHand.minDamage, RightHand.maxDamage);
            }
            else
            {
                if(LeftHand != null)
                {
                    damage = random.Next(RightHand.minDamage, RightHand.maxDamage) + random.Next(LeftHand.minDamage, LeftHand.maxDamage);
                }
                else
                {
                    damage = random.Next(RightHand.minDamage, RightHand.maxDamage);
                }
                
            }
            return damage;
        }


        public bool IsTwoHanded()
        {
            return RightHand.twoHanded;
        }

        public int CalculateCritFactor()
        {
            int critFactor = 0;
            critFactor += Head.protectionRating;
            critFactor += Shoulders.protectionRating;
            critFactor += Chest.protectionRating;
            critFactor += Hands.protectionRating;
            critFactor += Legs.protectionRating;
            critFactor += Boots.protectionRating;
            if(IsTwoHanded())
            {
                critFactor += RightHand.critFactor;
            }
            else
            {
                if(LeftHand != null)
                {
                    critFactor += RightHand.critFactor + LeftHand.critFactor;
                }
                else
                {
                    critFactor += RightHand.critFactor;
                }
               
            }
            return critFactor;
        }
    }

    public enum EquipmentSlot
    {
        Head,
        Shoulders,
        Chest,
        Hands,
        Legs,
        Boots,
        RightHand,
        LeftHand
    }
}
