using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Weapon : Item
    {
        public int minDamage;
        public int maxDamage;
        public int critFactor;
        public int protectionBonus;
        public bool twoHanded = false;
        public bool isShield = false;
        public Weapon(string name, string description, int value, int minDamage, int maxDamage, int critFactor, bool twoHanded, int protectionBonus, bool isShield) : base(name, description, value)
        {
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
            this.critFactor = critFactor;
            this.twoHanded = twoHanded;
            this.protectionBonus = protectionBonus;
            this.isShield = isShield;
        }

        public override string ToString()
        {
            string s = base.ToString();
            s += " - Min: " + minDamage + " - Max: " + maxDamage + " - Crit: " + critFactor + " - Twohanded: " + twoHanded + " - Protection bonus: " + protectionBonus;
            return s;
        }
    }
}
