using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class RoomGenerator
    {
        static Random rand = new Random();

        public static void GenerateRoom(int randomFill, Room r)
        {
           
            for (int i = 0; i < r.RoomMap.GetLength(0); i++)
            {
                for (int h = 0; h < r.RoomMap.GetLength(1); h++)
                {
                    if (i == 0 || h == 0 || i == r.RoomMap.GetLength(0) - 1 || h == r.RoomMap.GetLength(1) - 1)
                    {
                        r.RoomMap[i, h] = 'X';
                    }
                    else
                    {
                        int r_i = rand.Next(0, 100);
                        if (r_i > randomFill)
                        {
                            r.RoomMap[i, h] = 'X';
                        }
                        else
                        {
                            r.RoomMap[i, h] = ' ';
                        }

                    }
                }
            }
            DoGeneration(r);
          

        }

        public static void DigEntrance(Room r,int x, int y)
        {
            for (int h = y - 2; h < y + 3; h++)
            {
                for (int i = x - 2; i < x + 3; i++)
                {
                    if (isValidCell(i, h,r)) r.RoomMap[i, h] = ' ';
                }
            }
        }

        private static void DoGeneration(Room r)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < r.RoomMap.GetLength(0); i++)
                {
                    for (int h = 0; h < r.RoomMap.GetLength(1); h++)
                    {
                        int wallCount = GetCellNeighbourCount(i, h,r);
                        if (r.RoomMap[i, h] == 'X')
                        {
                            if (wallCount >= 4)
                            {
                                r.RoomMap[i, h] = 'X';
                            }
                            else if (wallCount < 2)
                            {
                                r.RoomMap[i, h] = ' ';
                            }
                        }
                        else
                        {
                            if (wallCount >= 5)
                            {
                                r.RoomMap[i, h] = 'X';
                            }
                        }

                    }
                }
            }
        }


        private static int GetCellNeighbourCount(int x, int y, Room r)
        {
            int count = 0;
            if (IsWall(x - 1, y, r)) count++;
            if (IsWall(x - 1, y + 1, r)) count++;
            if (IsWall(x, y + 1, r)) count++;
            if (IsWall(x + 1, y + 1, r)) count++;
            if (IsWall(x + 1, y, r)) count++;
            if (IsWall(x + 1, y - 1, r)) count++;
            if (IsWall(x, y - 1, r)) count++;
            if (IsWall(x - 1, y - 1, r)) count++;
            return count;
        }

        private static bool IsWall(int x, int y, Room r)
        {
            if (x <= 0 || x >= r.RoomMap.GetLength(0)) return true;
            if (y <= 0 || y >= r.RoomMap.GetLength(1)) return true;
            return r.RoomMap[x, y] == 'X';
        }

        private static bool isValidCell(int x, int y, Room r)
        {
            if (x <= 0 || x >= r.RoomMap.GetLength(0) - 1) return false;
            if (y <= 0 || y >= r.RoomMap.GetLength(1) - 1) return false;
            return r.RoomMap[x, y] == 'X';
        }

    }
}
