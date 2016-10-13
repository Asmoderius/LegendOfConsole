using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Monster : AI
    {
        public int HP { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int CritFactor { get; set; }
        public int ProtectionFactor { get; set; }
        public bool IsBoss { get; set; }
        public int Pos_X { get; set; }
        public int Pos_Y { get; set; }
        public static Random random = new Random();
        public Monster(string name, string description, int level,char icon, int hp, int minDamage, int maxDamage, int critFactor, int protectionFactor, bool isBoss) : base(name, description, level, false, icon)
        {
            HP = hp;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            CritFactor = critFactor;
            ProtectionFactor = protectionFactor;
            IsBoss = isBoss;
        }

        public int CalculateDamage()
        {
            int damage = random.Next(MinDamage, MaxDamage);
            if (random.Next(0, 100) <= CritFactor) damage *= 2;
            return damage;
        }

        public int TakeDamage(int damageDone)
        {
            damageDone -= ProtectionFactor / 10;
            if (damageDone < 0) damageDone = 0;
            HP -= damageDone;
            if(HP < 0)
            {
                HP = 0;
            }
            return damageDone;
        }

        public bool IsDead()
        {
            return HP == 0;
        }

        public void SetPosition(int x, int y)
        {
            Pos_X = x;
            Pos_Y = y;
        }


        public override string ToString()
        {
            string s = "Name: " + Name + "\n";
            s += "Description: " + Description + "\n";
            s += "Level: " + Level + "\n";
            s += "HP: " + HP + "\n";
            return s;
        }


    }
}
