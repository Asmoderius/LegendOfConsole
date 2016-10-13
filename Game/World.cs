using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class World
    {
        public List<Room> rooms = new List<Room>();
        public Player player;
        bool gameStarted = false;
        public static CommandHandler commands;
        public World()
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;
            commands = new CommandHandler(this);
            ItemHandler.ReadFile();
            MonsterCreator.CreateMonsters();
            RunGame();
           
      
        }

        private void StartGame()
        {

            //WorldGenerator.GenerateWorld(this);
            WorldGenerator.GenerateRandomWorld(this, 128);
            gameStarted = true;
            RefreshMap();
            Random random = new Random();
            for (int i = 37; i <= 2000; i += 100)
            {
                Console.Beep(i, 100);
            }
        }

        public void RunGame()
        {


            Console.WriteLine("Welcome to Game");
            Console.WriteLine("Use Help for ingame help menu and command list");
            StartGame();
            bool isRunning = true;
            while(isRunning && !player.IsDead())
            {
                
                string input = Console.ReadLine();
                string[] subString = input.Split(' ');
                switch (subString[0].ToLower())
                {
                    case "start":
                        StartGame();
                        break;
                    case "move":
                        if(gameStarted) commands.Move(player);
                        break;
                    case "rest":
                        if (gameStarted)
                        {
                            player.Rest();
                            RefreshMap();
                        }
                        break;
                    case "look":
                        if (gameStarted) commands.Look(player);
                        break;
                    case "stats":
                        if (gameStarted)
                        {
                            Console.Clear();
                            Console.WriteLine(player.ToString());
                        }
                        break;
                    case "take":
                        if (gameStarted) commands.Take(subString, player);
                        break;
                    case "use":
                        if (gameStarted) commands.Use(subString, player);
                        break;
                    case "equip":
                        if (gameStarted) commands.Equip(subString, player);
                        break;
                    case "inventory":
                        if (gameStarted) commands.ShowInventory(player);
                        break;
                    case "help":
                        Console.Clear();
                        commands.Help();
                        break;
                    case "map":
                        if (gameStarted) RefreshMap();
                        break;
                    case "legend":
                        commands.Legend();
                        break;
                    case "exit":
                        isRunning = false;
                        break;
                    case "die":
                        player.TakeDamage(101);
                        break;
                    default:
                        break;
                }
               
      
            }
            if(player.IsDead())
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Over");
                Console.WriteLine("You died...");
                Console.ResetColor();
            }
            Console.WriteLine("Thanks for playing...");
            Console.ReadLine();
        }



        

        public Direction GetDirection(ConsoleKeyInfo input)
        {
            switch (input.KeyChar)
            {
                case 'w':
                    return Direction.North;
                case 's':
                    return Direction.South;
                case 'a':
                    return Direction.West;
                case 'd':
                    return Direction.East;
                case 'q':
                    return Direction.Stop;
                default:
                    return Direction.Unknown;
            }
        }


        public void RefreshMap()
        {
            Console.Clear();
            Console.Write(player.CurrentRoom.GetName() + "\nPlayer energy : " + player.Energy + " /100" + "\n");
            switch (player.CurrentRoom.roomType)
            {
                case RoomType.Forest:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case RoomType.Wasteland:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case RoomType.Town:
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case RoomType.Cavern:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case RoomType.BossRoom:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }
           Console.WriteLine(player.CurrentRoom.GetRoomMap());
           Console.ResetColor();

        }


        
    }
}
