using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class MonsterCreator
    {
        public static void CreateMonsters()
        {
            Monster bat = new Monster("Bat", "Winged beast", 1, 'M', 20, 3, 5, 1, 2, false);
            Monster wolf = new Monster("Wolf", "It snarls at you", 2, 'M', 25, 4, 6, 1, 2, false);
            Monster bear = new Monster("Bear", "It is not your average teddybear!", 3, 'M', 30, 5, 7, 3, 5, false);
            Monster goblin = new Monster("Goblin", "Small, green and ugly!", 4, 'M', 32, 4, 8, 2, 3, false);
            Monster hobgoblin = new Monster("Hobgoblin", "Larger and tougher cousin of the Goblin", 6, 'M', 50, 7, 10, 3, 5, false);
            Monster skeleton = new Monster("Skeleton", "Reanimated bones of pure evil", 3, 'M', 32, 5, 8, 1, 8, false);
            Monster zombie = new Monster("Zombie", "Brains.... BRAINS...", 2, 'M', 25, 3, 5, 1, 2, false);
            Monster ghoul = new Monster("Ghoul", "Tougher and more intelligent version of the Zombie.", 7, 'M', 55, 12, 14, 5, 5, false);
            Monster lich = new Monster("Undead Lich", "Wizard who has lost all sanity and turned to Lichdom. Pretty tough fight!", 12, 'M', 100, 14, 18, 3, 2, false);
            Monster vampire = new Monster("Vampire", "It whispers, Give me a hug!", 10, 'M', 80, 11, 14, 2, 5, false);
            Monster demon = new Monster("Demon", "Fresh out of Hell!", 10, 'M', 120, 13, 19, 5, 10, false);
            Monster boss = new Monster("The Unspoken", "Rumored to be a banished Old One. It is an intangible mass of writhing mouths, the Old One seems to be swirling with twisted faces of agony. ", 30, 'B', 250, 20, 30, 6, 15, true);

            MonsterController.AddNewMonster(bat);
            MonsterController.AddNewMonster(wolf);
            MonsterController.AddNewMonster(bear);
            MonsterController.AddNewMonster(goblin);
            MonsterController.AddNewMonster(hobgoblin);
            MonsterController.AddNewMonster(zombie);
            MonsterController.AddNewMonster(ghoul);
            MonsterController.AddNewMonster(lich);
            MonsterController.AddNewMonster(vampire);
            MonsterController.AddNewMonster(demon);
            MonsterController.AddNewMonster(boss);
        }
    }
}
