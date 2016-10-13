using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Game
{
    public class Room
    {
        static Random rand = new Random();
        internal Room North;
        internal Room South;
        internal Room West;
        internal Room East;
        private string name;
        private string description;
        public char[,] RoomMap;
        public Dictionary<Tuple<int, int>, Item> Items;
        public Dictionary<Direction, EntranceCoordinate> Entrances;
        public List<AI> npcs;
        private bool isBossRoom = false;
        public RoomType roomType;
        public Room(string name, string description, int x, int y,RoomType roomType, bool isBossRoom = false)
        {
            RoomMap = new char[x, y];
            Entrances = new Dictionary<Direction, EntranceCoordinate>();
            Items = new Dictionary<Tuple<int, int>, Item>();
            npcs = new List<AI>();
            this.name = name;
            this.description = description;
            this.isBossRoom = isBossRoom;
            this.roomType = roomType;
        }

       
        public void GenerateRoom(int randomFill)
        {
            RoomGenerator.GenerateRoom(randomFill, this);
            RoomItemHandler.RandomizeItems(this);
            RoomMonsterHandler.SpawnMonsters(this,5, rand.Next(0,6), false);
        }

       

        public string GetRoomMap()
        {
            string map = "";
            for (int h = RoomMap.GetLength(1)-1; h >= 0; h--)
            {
                for (int i = 0; i < RoomMap.GetLength(0); i++)
                {
                    map += RoomMap[i, h];

                }
                map += "\n";
            }
            return map;
        }

        public Direction IsPositionEntrance(int x, int y)
        {
            Direction direction = Direction.Unknown;
            if (x == 0) direction = Direction.West;
            if (x == RoomMap.GetLength(0)-1) direction = Direction.East;
            if (y == 0) direction = Direction.South;
            if (y == RoomMap.GetLength(1) - 1) direction = Direction.North;
            if (Entrances.ContainsKey(direction))
            {
                return direction;
            }
            return Direction.Unknown;
        }

        public void PlayerMove(Player player,int x, int y)
        {
            bool wasInCombat = false;
            if(x >= 0 && x < RoomMap.GetLength(0) && y>= 0 && y < RoomMap.GetLength(1))
            {
                if (RoomMap[x, y] == ' ')
                {
                    Direction direction = IsPositionEntrance(x, y);
                    if (direction != Direction.Unknown)
                    {
                        RoomMap[player.PlayerX, player.PlayerY] = ' ';
                        PlayerEnterRoom(player, direction);

                    }
                    else
                    {
                        PlacePlayer(player, x, y);
                    }
                }
                else if(RoomMap[x,y] == 'X')
                {
                    if (RoomMap[x, y] == 'X' && (x > 0 && x < RoomMap.GetLength(0) - 1 && y > 0 && y < RoomMap.GetLength(1) - 1))
                    {

                        if (player.CheckEnergy(25))
                        {
                            PlacePlayer(player, x, y);
                            player.UseEnergy(25);
                        }

                    }
                }
                else if(RoomMap[x,y] == 'M' || RoomMap[x,y] == 'B')
                {
                    Monster m = RoomMonsterHandler.GetMonsterAtPosition(this,x, y);
                    if(m != null)
                    {
                        CombatResult result = CombatHandler.Combat(player, m, true);
                        if (result == CombatResult.PlayerWin)
                        {
                            //player wins
                            PlacePlayer(player, x, y);
                            npcs.Remove(m);
                        }
                        else if(result == CombatResult.MonsterWin)
                        {
                            RoomMap[m.Pos_X, m.Pos_Y] = ' ';
                            RoomMap[x, y] = m.Icon;
                            m.SetPosition(x, y);
                        }
                    }
                    wasInCombat = true;
                }
            }
            if(!wasInCombat) RoomMonsterHandler.MoveMonsters(this,player);
        }

       

        public void PlacePlayer(Player player, int x, int y)
        {
            RoomMap[player.PlayerX, player.PlayerY] = ' ';
            RoomMap[x, y] = 'P';
            Console.Beep(75, 100); //Console.Beep er meget sløv!

            player.PlayerX = x;
            player.PlayerY = y;
            if(roomType == RoomType.Cavern) TriggerEvent(player);
        }

       

        private void TriggerEvent(Player player)
        {
            int randomChance = rand.Next(0, 101);
            if(randomChance <= 5)
            {
                if(randomChance == 0)
                {
                    CombatHandler.Combat(player, MonsterController.SpawnRandomMonster(), false);
                }
                else if(randomChance > 0 && randomChance <= 2)
                {
                    Console.WriteLine("You step on a deadly trap!!");
                    int damageDone = rand.Next(5, 21);
                    player.TakeDamage(damageDone);
                    Console.WriteLine("You take " + damageDone + " damage");
                    Console.WriteLine("Press any key to continue..");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("You find hidden treasure!");
                    Item i = ItemHandler.GetRandomCommonItem();
                    Console.WriteLine(i.ToString() + " added to inventory");
                    player.inventory.Add(ItemHandler.GetRandomCommonItem());
                    Console.WriteLine("Press any key to continue your adventure");
                    Console.ReadKey(true);

                }

            }
        }

        private void PlayerEnterRoom(Player player, Direction direction)
        {
            EntranceCoordinate coordinate = Entrances[direction];
            EntranceCoordinate newEntranceCoordinate = coordinate.room.Entrances[ReverseDirection.Reverse(direction)];
            int new_x = newEntranceCoordinate.coordinate.Item1;
            int new_y = newEntranceCoordinate.coordinate.Item2;
            if (new_x == 0) new_x++;
            if (new_x == coordinate.room.RoomMap.GetLength(0) - 1) new_x--;
            if (new_y == 0) new_y++;
            if (new_y == coordinate.room.RoomMap.GetLength(1) - 1) new_y--;
            player.PlayerX = new_x;
            player.PlayerY = new_y;
            coordinate.room.RoomMap[new_x, new_y] = 'P';
            player.CurrentRoom = coordinate.room;
        }

        public void AddNeighbour(Room r, Direction dir, bool digEntrance)
        {

            int x, y;
            switch (dir)
            {
                case Direction.North:
                    North = r;
                    x = rand.Next(1, RoomMap.GetLength(0) - 1);
                    y = RoomMap.GetLength(1) - 1;
                    RoomMap[x, y] = ' ';
                    if(digEntrance)RoomGenerator.DigEntrance(this, x, y);
                    Entrances.Add(Direction.North, new EntranceCoordinate(North, new Tuple<int, int>(x, y)));
                    break;
                case Direction.South:
                    South = r;
                    x = rand.Next(1, RoomMap.GetLength(0) - 1);
                    y = 0;
                    RoomMap[x, y] = ' ';
                    if (digEntrance) RoomGenerator.DigEntrance(this, x, y);
                    Entrances.Add(Direction.South, new EntranceCoordinate(South, new Tuple<int, int>(x, y)));
                    break;
                case Direction.West:
                    West = r;
                    x = 0;
                    y = rand.Next(1, RoomMap.GetLength(1) - 1);
                    RoomMap[x, y] = ' ';
                    if (digEntrance) RoomGenerator.DigEntrance(this, x, y);
                    Entrances.Add(Direction.West, new EntranceCoordinate(West, new Tuple<int, int>(x, y)));
                    break;
                case Direction.East:
                    East = r;
                    x = RoomMap.GetLength(0) - 1;
                    y = rand.Next(1, RoomMap.GetLength(1) - 1);
                    RoomMap[x, y] = ' ';
                    if (digEntrance) RoomGenerator.DigEntrance(this, x, y);
                    Entrances.Add(Direction.East, new EntranceCoordinate(East, new Tuple<int, int>(x, y)));
                    break;
                case Direction.Unknown:
                    break;
                default:
                    break;
            }
        }


        public List<Direction> GetFreeNeighbours()
        {
            List<Direction> freeNeighbours = new List<Direction>();
            if (North == null) freeNeighbours.Add(Direction.North);
            if (South == null) freeNeighbours.Add(Direction.South);
            if (West == null) freeNeighbours.Add(Direction.West);
            if (East == null) freeNeighbours.Add(Direction.East);
            return freeNeighbours;
        }

        public string GetName()
        {
            return name;
        }

        public override string ToString()
        {
            string s = "";
            s += "Name: " + this.name + "\n";
            s += "Description: " + this.description + "\n";
            return s;
        }
    }
}
