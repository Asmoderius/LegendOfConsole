using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class WorldGenerator
    {
        private static Random random = new Random();
        public static void GenerateWorld(World w)
        {
            Room start = PremadeRooms.SparseRoom("Clearing", "A clearing. You can see smoke in the distance.", 40, 20, 95, RoomType.Wasteland);
            Room forest = PremadeRooms.SparseRoom("Forest", "Forest", 40, 20, 90, RoomType.Forest);
            Room forest2 = PremadeRooms.SparseRoom("A sparse forest", "Forest", 40, 20, 94, RoomType.Forest);
            Room wasteLand = PremadeRooms.SparseRoom("Wasteland", "A very barren wasteland", 40, 20, 98, RoomType.Wasteland);
            Room wasteLand2 = PremadeRooms.SparseRoom("Wasteland", "A dry wasteland", 20, 20, 97, RoomType.Wasteland);
            Room wasteLand3 = PremadeRooms.SparseRoom("Wasteland", "A rocky wasteland", 40, 20, 97, RoomType.Wasteland);
            Room town = PremadeRooms.Town("Town", "A little town");
            Room r1 = new Room("Small cave", "A very dark cave", 20, 20, RoomType.Cavern);
            Room r2 = new Room("Cavern", "Murky nasty place", 40, 20, RoomType.Cavern);
            Room r3 = new Room("Cavern", "Huge!", 60, 20, RoomType.Cavern);
            Room r4 = new Room("Cavern", "Complete darkness", 20, 20, RoomType.Cavern);
            Room r5 = new Room("Cavern", "A dark cavern", 40, 20, RoomType.Cavern);
            Room r6 = new Room("Cavern", "Creepy", 40, 20, RoomType.Cavern);
            Room r7 = new Room("Passage", "Narrow passage", 40, 5, RoomType.Cavern);
            Room r8 = new Room("Large Cavern", "A large cavern", 60, 20, RoomType.Cavern);
            Room r9 = new Room("Cavern", "Have you been here before?", 60, 20, RoomType.Cavern);
            Room r10 = new Room("Cavern", "Feeling a bit lost?", 20, 20, RoomType.Cavern);
            Room r11 = new Room("Cavern", "You get the feeling that you are being watched!", 40, 20, RoomType.Cavern);
            Room r12 = new Room("Cavern", "A damp cavern", 40, 20, RoomType.Cavern);
            Room r13 = new Room("Cavern", "This is not the cavern you are looking for!", 40, 20, RoomType.Cavern);
            Room r14 = new Room("Cave", "A small cave", 20, 20, RoomType.Cavern);
            Room r15 = PremadeRooms.Boss("Altar of N'thegtha", "The altar is surrounded by swirling energies. Horrid visages can be seen twisting within the altar");


            w.rooms.Add(start);
            w.rooms.Add(forest);
            w.rooms.Add(wasteLand);
            w.rooms.Add(wasteLand2);
            w.rooms.Add(wasteLand3);
            w.rooms.Add(town);
            w.rooms.Add(r1);
            w.rooms.Add(r2);
            w.rooms.Add(r3);
            w.rooms.Add(r4);
            w.rooms.Add(r5);
            w.rooms.Add(r6);
            w.rooms.Add(r7);
            w.rooms.Add(r8);
            w.rooms.Add(r9);
            w.rooms.Add(r10);
            w.rooms.Add(r11);
            w.rooms.Add(r12);
            w.rooms.Add(r13);
            w.rooms.Add(r14);
            w.rooms.Add(r15);
            r1.GenerateRoom(80);
            r2.GenerateRoom(75);
            r3.GenerateRoom(70);
            r4.GenerateRoom(78);
            r5.GenerateRoom(70);
            r6.GenerateRoom(74);
            r7.GenerateRoom(97);
            r8.GenerateRoom(75);
            r9.GenerateRoom(70);
            r10.GenerateRoom(78);
            r11.GenerateRoom(70);
            r12.GenerateRoom(74);
            r13.GenerateRoom(70);
            r14.GenerateRoom(78);

           AddConnection(start, wasteLand, Direction.West, false, true);
           AddConnection(start, forest, Direction.East, false, true);
           AddConnection(wasteLand, wasteLand2, Direction.North, false, true);
           AddConnection(wasteLand2, town, Direction.West, false, false);
           AddConnection(wasteLand2, forest2, Direction.North, false, true);
           AddConnection(town, wasteLand3, Direction.North, false, true);
           AddConnection(wasteLand3, r1, Direction.North, false, true);
           AddConnection(r1, r2, Direction.North, false, true);
           AddConnection(r2, r3, Direction.West, false, true);
           AddConnection(r2, r4, Direction.East, false, true);
           AddConnection(r4, r7, Direction.East, false, true);
           AddConnection(r3, r5, Direction.North, false, true);
            AddConnection(r5, r6, Direction.North, false, true);
            AddConnection(r7, r8, Direction.East, false, true);
           AddConnection(r8, r9, Direction.South, false, true);
           AddConnection(r8, r10, Direction.East, false, true);
            AddConnection(r8, r11, Direction.North, false, true);
            AddConnection(r11, r12, Direction.North, false, true);
            AddConnection(r2, r14, Direction.North, false, true);
            AddConnection(r4, r13, Direction.North, false, true);
            AddConnection(r13, r14, Direction.West, false, true);
            AddConnection(r12, r15, Direction.North, false, true);

            GeneratePlayer(w, start);






        }

        public static void AddConnection(Room r1, Room r2, Direction dir, bool directed, bool digEntrance)
        {
            r1.AddNeighbour(r2, dir, digEntrance);
            if (!directed)
            {
                r2.AddNeighbour(r1, ReverseDirection.Reverse(dir), digEntrance);
            }
        }

        public static void GenerateRandomWorld(World w, int roomCount)
        {
            w.rooms.Clear();
            Queue<Room> unfinishedRooms = new Queue<Room>();
            Room start = PremadeRooms.SparseRoom("Clearing", "A green clearing.", 40, 20, 95, RoomType.Wasteland);
            GeneratePlayer(w, start);

            unfinishedRooms.Enqueue(start);
            while (unfinishedRooms.Count > 0)
            {
                Room r = unfinishedRooms.Dequeue();
                bool addingNeighbours = w.rooms.Count <= roomCount;
                while (addingNeighbours)
                {
                    List<Direction> freeNeighbours = r.GetFreeNeighbours();
                    int exitThreshold = 0;
                    if (r.roomType == RoomType.Cavern) exitThreshold = 50;
                    else exitThreshold = 66;
                    int neighbourChance = random.Next(0, exitThreshold);
                    if (neighbourChance <= freeNeighbours.Count * 25 && freeNeighbours.Count != 0)
                    {
                        Direction randomDirection = freeNeighbours[random.Next(0, freeNeighbours.Count)];
                        Room r2 = GenerateNeighbourRoom(r);
                        bool digEntrance = r.roomType != RoomType.Town && r2.roomType != RoomType.Town;
                        AddConnection(r, r2, randomDirection, false, digEntrance);
                        unfinishedRooms.Enqueue(r2);
                    }
                    else
                    {
                        addingNeighbours = false;
                    }
                }
                w.rooms.Add(r);
            }
        }

        private static void GeneratePlayer(World w, Room start)
        {
            Console.WriteLine("Please enter the name of your character...");
            string playerName = Console.ReadLine();
            w.player = new Player(playerName);
            w.player.CurrentRoom = start;

            bool spotFound = false;
            int p_x = 0;
            int p_y = 0;
            while (!spotFound)
            {
                p_x = random.Next(1, w.player.CurrentRoom.RoomMap.GetLength(0) - 1);
                p_y = random.Next(1, w.player.CurrentRoom.RoomMap.GetLength(1) - 1);
                if (w.player.CurrentRoom.RoomMap[p_x, p_y] == ' ')
                {
                    spotFound = true;
                }
            }
            w.player.PlayerX = p_x;
            w.player.PlayerY = p_y;
            w.player.CurrentRoom.RoomMap[w.player.PlayerX, w.player.PlayerY] = 'P';

            Armor head = new Armor("Old helmet", "Very old and rusty helmet", 10, 5, EquipmentSlot.Head);
            Armor shoulders = new Armor("Old shoulderguards", "Very old and rusty shoulderguards", 10, 5, EquipmentSlot.Shoulders);
            Armor chest = new Armor("Old chest armor", "Very old and rusty chest armor", 10, 5, EquipmentSlot.Chest);
            Armor hands = new Armor("Old gloves", "Very old and rusty gloves", 10, 5, EquipmentSlot.Hands);
            Armor legs = new Armor("Old legguards", "Very old and rusty armor", 10, 5, EquipmentSlot.Legs);
            Armor boots = new Armor("Old boots", "Very old and dirty boots", 10, 5, EquipmentSlot.Boots);
            Weapon club = new Weapon("Club", "A wooden club with rusty nails", 4, 5, 9, 0, false, 0, false);
            w.player.equipment.Head = head;
            w.player.equipment.Shoulders = shoulders;
            w.player.equipment.Chest = chest;
            w.player.equipment.Hands = hands;
            w.player.equipment.Legs = legs;
            w.player.equipment.Boots = boots;
            w.player.equipment.RightHand = club;
        }

        private static Room GenerateNeighbourRoom(Room r)
        {
            //Wasteland -> Wasteland(50%) Forest(25%) Cavern (20%) Town (5%)
            //Forest -> Wasteland(25%) Forest(50%) Cavern(20%) Town (5%)
            //Town -> Wasteland(50%) Forest(50%)
            //Cavern -> Cavern(89%) Wasteland(5%) Forest(5%) Boss(1%)
            //Boss -> Cavern (80%) Wasteland(20%)

            RoomType roomType = GetNewRoomType(r);

            int x = 0;
            int y = 0;
            switch (roomType)
            {
                case RoomType.Cavern:
                    x = random.Next(20, 41);
                    y = random.Next(5, 21);
                    break;
                default:
                    x = 40;
                    y = 20;
                    break;
            }
            string name = RoomNameGenerator.GenerateName(roomType,x,y);
            string description = RoomNameGenerator.GenerateDescription(roomType, x, y);
            Room r2 = null;
            switch (roomType)
            {
                case RoomType.Forest:
                    r2 = PremadeRooms.SparseRoom(name, description, 40, 20, random.Next(85,95), RoomType.Forest);
                    break;
                case RoomType.Wasteland:
                    r2 = PremadeRooms.SparseRoom(name, description, 40, 20, random.Next(93, 101), RoomType.Wasteland);
                    break;
                case RoomType.Town:
                    r2 = PremadeRooms.Town(name, description);
                    break;
                case RoomType.Cavern:
                    r2 = new Room(name, description, x, y, RoomType.Cavern);
                    r2.GenerateRoom(random.Next(70, 100));
                    break;
                case RoomType.BossRoom:
                    r2 = PremadeRooms.Boss(name, description);
                    break;
                default:
                    break;
            }
            return r2;

        }

        private static RoomType GetNewRoomType(Room r)
        {
            RoomType newRoomType = RoomType.Wasteland;
            int randomRoll = random.Next(0, 101);
            switch (r.roomType)
            {
                case RoomType.Forest:
                    if (randomRoll <= 50) newRoomType = RoomType.Forest;
                    else if (randomRoll > 50 && randomRoll <= 75) newRoomType = RoomType.Wasteland;
                    else if (randomRoll > 75 && randomRoll <= 95) newRoomType = RoomType.Cavern;
                    else if (randomRoll > 95) newRoomType = RoomType.Town;
                    break;
                case RoomType.Wasteland:
                    if (randomRoll <= 50) newRoomType = RoomType.Wasteland;
                    else if (randomRoll > 50 && randomRoll <= 75) newRoomType = RoomType.Forest;
                    else if (randomRoll > 75 && randomRoll <= 95) newRoomType = RoomType.Cavern;
                    else if (randomRoll > 95) newRoomType = RoomType.Town;
                    break;
                case RoomType.Town:
                    if (randomRoll <= 50) newRoomType = RoomType.Wasteland;
                    else if (randomRoll > 50) newRoomType = RoomType.Forest;
                    break;
                case RoomType.Cavern:
                    if (randomRoll <= 89) newRoomType = RoomType.Cavern;
                    else if (randomRoll > 89 && randomRoll <= 94) newRoomType = RoomType.Wasteland;
                    else if (randomRoll > 94 && randomRoll <= 99) newRoomType = RoomType.Forest;
                    else if (randomRoll > 99) newRoomType = RoomType.BossRoom;
                    break;
                case RoomType.BossRoom:
                    if (randomRoll <= 80) newRoomType = RoomType.Cavern;
                    else if (randomRoll > 80) newRoomType = RoomType.Wasteland;
                    break;
                default:
                    break;
            }
            return newRoomType;
        }
    }
}
