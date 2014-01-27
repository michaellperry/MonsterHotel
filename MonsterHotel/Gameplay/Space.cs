using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Gameplay
{
    public class Space
    {
        public Space()
        {
            Neighbor = new Dictionary<Direction, Space>();
        }

        public Dictionary<Direction, Space> Neighbor { get; set; }
        public Monster Monster { get; set; }
    }
}
