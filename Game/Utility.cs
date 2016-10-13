using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Utility
    {
    }

    public enum Direction
    {
        North,
        South,
        West,
        East,
        Stop,
        Unknown
    }

    public static class ReverseDirection
    {
        public static Direction Reverse(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                case Direction.East:
                    return Direction.West;
                default:
                    return Direction.Unknown;
            }
        }
    }


    public struct EntranceCoordinate
    {
        public Room room;
        public Tuple<int, int> coordinate;
        public EntranceCoordinate(Room room, Tuple<int, int> coordinate)
        {
            this.room = room;
            this.coordinate = coordinate;
        }

    }

    public enum RoomType
    {
        Forest,
        Wasteland,
        Town,
        Cavern,
        BossRoom

    }


}
