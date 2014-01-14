using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel
{
    public class Board
    {
        public Space GreatHall = new Space();
        public Space NorthCorridor = new Space();
        public Space Room1 = new Space();

        public Board(Game game)
        {
            NorthSouth(NorthCorridor, GreatHall);
            EastWest(NorthCorridor, Room1);

            Room1.Monster = new Monster(game)
            {
                Name = "Goblin",
                Defense = new Dictionary<Class,int>
                {
                    { Class.Rogue, 3 },
                    { Class.Cleric, 4 },
                    { Class.Fighter, 2 },
                    { Class.Wizard, 5 }
                }
            };
        }

        private static void NorthSouth(Space north, Space south)
        {
            south.Neighbor[Direction.North] = north;
            north.Neighbor[Direction.South] = south;
        }

        private static void EastWest(Space east, Space west)
        {
            east.Neighbor[Direction.West] = west;
            west.Neighbor[Direction.East] = east;
        }
    }
}
