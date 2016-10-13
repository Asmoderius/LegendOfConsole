using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class RoomMonsterHandler
    {
        private static Random rand = new Random();
        public static void MoveMonsters(Room r, Player p)
        {
            for (int i = 0; i < r.npcs.Count; i++)
            {
                if (r.npcs[i].GetType() == typeof(Monster))
                {
                    Monster m = r.npcs[i] as Monster;
                    if (!m.IsBoss)
                    {
                        int randomDirection = rand.Next(0, 4);
                        int new_x = 0;
                        int new_y = 0; ;
                        switch (randomDirection)
                        {
                            case 0:
                                //North
                                new_x = m.Pos_X;
                                new_y = m.Pos_Y + 1;
                                break;
                            case 1:
                                //South
                                new_x = m.Pos_X;
                                new_y = m.Pos_Y - 1;
                                break;
                            case 2:
                                //West
                                new_x = m.Pos_X - 1;
                                new_y = m.Pos_Y;
                                break;
                            case 3:
                                //East
                                new_x = m.Pos_X + 1;
                                new_y = m.Pos_Y;
                                break;
                            default:
                                break;
                        }
                        PlaceMonster(r,p, m, new_x, new_y);
                    }

                }
            }
        }

        public static void PlaceMonster(Room r, Player p, Monster m, int x, int y)
        {
            if (x > 0 && x < r.RoomMap.GetLength(0) - 1 && y > 0 && y < r.RoomMap.GetLength(1) - 1)
            {
                if (r.RoomMap[x, y] == ' ')
                {
                    r.RoomMap[m.Pos_X, m.Pos_Y] = ' ';
                    r.RoomMap[x, y] = m.Icon;
                    m.SetPosition(x, y);
                }
                else if (r.RoomMap[x, y] == 'P')
                {
                    CombatResult result = CombatHandler.Combat(p, m, false);
                    if (result == CombatResult.PlayerWin)
                    {
                        //player wins
                        r.RoomMap[m.Pos_X, m.Pos_Y] = ' ';
                        r.npcs.Remove(m);
                    }
                    else if (result == CombatResult.MonsterWin)
                    {
                        r.RoomMap[m.Pos_X, m.Pos_Y] = ' ';
                        r.RoomMap[x, y] = m.Icon;
                        m.SetPosition(x, y);
                    }
                    else
                    {
                        r.PlacePlayer(p, m.Pos_X, m.Pos_Y);
                        r.RoomMap[x, y] = m.Icon;
                        m.SetPosition(x, y);
                    }
                }
            }
        }

        public static Monster GetMonsterAtPosition(Room r, int x, int y)
        {
            foreach (Monster monster in r.npcs)
            {
                if (monster.Pos_X == x && monster.Pos_Y == y)
                {
                    return monster;
                }
            }
            return null;
        }

        public static void SpawnMonsters(Room r, int attempts, int spawnCount, bool spawnBoss)
        {
            Queue<Monster> waitingToBePlaced = new Queue<Monster>();
            if (spawnBoss)
            {
                Monster boss = MonsterController.SpawnBoss();
                waitingToBePlaced.Enqueue(boss);
                spawnCount--;
            }
            for (int i = 0; i < spawnCount; i++)
            {
                Monster monster = MonsterController.SpawnRandomMonster();
                waitingToBePlaced.Enqueue(monster);
            }
            while (waitingToBePlaced.Count > 0)
            {
                bool isPlaced = false;
                int x = 0;
                int y = 0;
                Monster m = waitingToBePlaced.Dequeue();
                while (!isPlaced && attempts > 0)
                {
                    x = rand.Next(1, r.RoomMap.GetLength(0) - 2);
                    y = rand.Next(1, r.RoomMap.GetLength(1) - 2);
                    if (r.RoomMap[x, y] == ' ')
                    {
                        r.RoomMap[x, y] = m.Icon;
                        m.SetPosition(x, y);
                        isPlaced = true;
                        r.npcs.Add(m);
                    }
                    else
                    {
                        attempts--;
                    }
                }
            }
        }

    }
}
