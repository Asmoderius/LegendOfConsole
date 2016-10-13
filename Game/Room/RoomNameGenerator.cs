using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class RoomNameGenerator
    {
        private static Random random = new Random();
        private static List<string> caveDescription = new List<string>();
        private static List<string> cavernDescription = new List<string>();
        private static List<string> PassageDescription = new List<string>();
        private static List<string> LargeCavernDescription = new List<string>();
        private static List<string> bossRoomDescription = new List<string>();
        public static string GenerateName(RoomType type, int x, int y)
        {
            switch (type)
            {
                case RoomType.Forest:
                    return "Forest";
                case RoomType.Wasteland:
                    return "Wasteland";
                case RoomType.Town:
                    return "Town";
                case RoomType.Cavern:
                    if (x < 30 && y >= 10 && y < 15) return "Small cave";
                    if (y < 10) return "Passage";
                    if (x >= 35 && y >= 18) return "Large cavern";
                    return "Cavern";
                case RoomType.BossRoom:
                    return "Ancient room";
                default:
                    return "Unknown";
            }
        }

        public static string GenerateDescription(RoomType type, int x, int y)
        {
            caveDescription.Add("A small and dark cave");
            caveDescription.Add("A dark cave");
            caveDescription.Add("A damp cave");
            caveDescription.Add("A cave");
            caveDescription.Add("A forgotten cave, the light seems to flicker and shadows move on their own.");
            caveDescription.Add("Are you going insane? You think there are whispers in this cave");

            cavernDescription.Add("A dark cavern");
            cavernDescription.Add("A damp cavern, it also stinks of rotting .. something");
            cavernDescription.Add("A cavern, you can barely hear footsteps. Are you alone?");
            cavernDescription.Add("A forgotten cavern.");
            cavernDescription.Add("Are you going insane? You think there are whispers in this cavern?");

            LargeCavernDescription.Add("This is a large cavern");
            LargeCavernDescription.Add("a huge cavern, you can easily fit a small town in here!");
            LargeCavernDescription.Add("A large and forgotten cavern. You can hear footsteps and growling. You are not alone!");
            LargeCavernDescription.Add("Are you going insane? You think there are whispers in this cavern?");

            PassageDescription.Add("This passage connects two caves.");
            PassageDescription.Add("A narrow passage");
            PassageDescription.Add("You wonder where this passage leads to");
            PassageDescription.Add("You can hear alien whispers and screams at the far end of the passage");

            bossRoomDescription.Add("The altar is surrounded by swirling energies. Horrid visages can be seen twisting within the altar");

            switch (type)
            {
                case RoomType.Forest:
                    return "A green and lush forest";
                case RoomType.Wasteland:
                    return "A bleak and desolate wasteland";
                case RoomType.Town:
                    return "Townfolks are wandering about their daily lives. They do not seem to notice you.";
                case RoomType.Cavern:
                    if (x < 30 && y >= 10 && y < 15) return caveDescription[random.Next(0, caveDescription.Count)];
                    if (y < 10) return PassageDescription[random.Next(0, PassageDescription.Count)];
                    if (x >= 35 && y >= 18) return LargeCavernDescription[random.Next(0, LargeCavernDescription.Count)];
                    return cavernDescription[random.Next(0, cavernDescription.Count)];
                case RoomType.BossRoom:
                    return bossRoomDescription[random.Next(0, bossRoomDescription.Count)];
                default:
                    return "Unknown";
            }
        }
    }
}
