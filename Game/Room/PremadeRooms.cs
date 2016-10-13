﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class PremadeRooms
    {
        static Random rand = new Random();
        public static Room SparseRoom(string name, string description, int x, int y, int forestChance, RoomType roomType)
        {
            Room r = new Room(name, description, x, y, roomType);
            for (int h = 0; h < y; h++)
            {
                for (int i = 0; i < x; i++)
                {
                    if (i == 0 || h == 0 || i == r.RoomMap.GetLength(0) - 1 || h == r.RoomMap.GetLength(1) - 1)
                    {
                        r.RoomMap[i, h] = 'X';
                    }
                    else
                    {
                        r.RoomMap[i, h] = ' ';
                    }
                }
            }
            MakeForest(x, y, r, forestChance);
            RoomItemHandler.RandomizeItems(r);
            int maxSpawnCount = 0;
            if (roomType == RoomType.Wasteland) maxSpawnCount = 3;
            if (roomType == RoomType.Forest) maxSpawnCount = 5;
            RoomMonsterHandler.SpawnMonsters(r,5,rand.Next(0, maxSpawnCount), false);
            return r;
        }

        public static Room Town(string name, string description)
        {
            Room r = new Room(name, description, 40, 20, RoomType.Town);
            char[,] map = { {'X', 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , 'O' , 'O' , 'O' , ' ' , 'O' , ' ' , ' ' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , 'O' , ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , 'O' , 'O' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , 'O' , ' ' , 'O' , 'O' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , 'O' , ' ' , 'O' , 'O' , 'O' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , 'O' , 'O' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', 'O' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , 'O' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' }
            };
            r.RoomMap = map;
            RoomItemHandler.RandomizeItems(r);
            return r;
        }

        public static Room Boss(string name, string description)
        {
            Room r = new Room(name, description, 40, 20,RoomType.BossRoom, true);
            char[,] map = { {'X', 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , 'O' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , 'O' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , ' ' , 'X' },
                            {'X', 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' , 'X' }
            };
            r.RoomMap = map;
            RoomItemHandler.RandomizeItems(r);
            RoomMonsterHandler.SpawnMonsters(r,5,rand.Next(0, 5), true);
            return r;
        }

        private static void MakeForest(int x, int y, Room r, int threshold)
        {
            for (int h = 0; h < y; h++)
            {
                for (int i = 0; i < x; i++)
                {
                    if (!(i < 5|| h < 5 || i > r.RoomMap.GetLength(0) - 5 || h > r.RoomMap.GetLength(1) - 5))
                    {
                        if (rand.Next(1, 101) >= threshold && r.RoomMap[i,h] == ' ')
                        {
                            r.RoomMap[i, h + 1] = 'T';
                            r.RoomMap[i - 1, h] = 'T';
                            r.RoomMap[i, h] = 'T';
                            r.RoomMap[i, h - 1] = 'T';
                            r.RoomMap[i + 1, h] = 'T';
                        }
                    }

                }
            }
        }
    }
}