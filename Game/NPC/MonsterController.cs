using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class MonsterController
    {
        private static List<AI> monsters = new List<AI>();
        private static List<AI> bosses = new List<AI>();
        private static Random random = new Random();


        public static Monster SpawnRandomMonster()
        {
            Monster m = monsters[random.Next(0, monsters.Count)] as Monster;
            return new Monster(m.Name, m.Description, m.Level, m.Icon, m.HP, m.MinDamage, m.MaxDamage, m.CritFactor, m.ProtectionFactor, m.IsBoss);
          
        }

        public static Monster SpawnBoss()
        {
            Monster m;
            m = bosses[random.Next(0, bosses.Count)] as Monster;
            return new Monster(m.Name, m.Description, m.Level, m.Icon, m.HP, m.MinDamage, m.MaxDamage, m.CritFactor, m.ProtectionFactor, m.IsBoss);
        }

        public static void AddNewMonster(Monster monster)
        {
            if(monster.IsBoss)
            {
                bosses.Add(monster);
            }
            else
            {
                monsters.Add(monster);
            }
        }
    }
}
