using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Potions : Item
    {
        public int healing;
        public int damageBonus;
        public int protectionBonus;
        public int critBonus;
        public bool effectOverTime = false;
        public int rounds;

        public Potions(string name, string description, int value, int healing, int damageBonus, int protectionBonus, int critBonus, bool effectOverTime, int rounds) : base(name, description, value, true)
        {
            this.healing = healing;
            this.damageBonus = damageBonus;
            this.protectionBonus = protectionBonus;
            this.critBonus = critBonus;
            this.rounds = rounds;
            this.effectOverTime = effectOverTime;
        }


        public override string ToString()
        {
            string s = base.ToString();
            s += " - Healing: " + healing + " - Damage bonus: " + damageBonus + " - Protection bonus: " + protectionBonus + " - Crit bonus: " + critBonus + " Rounds left : " + rounds;
            return s;
        }
    }
}
