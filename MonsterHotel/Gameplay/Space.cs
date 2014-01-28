using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Gameplay
{
    public class Space
    {
        private List<Space> _neighbors = new List<Space>();

        public IEnumerable<Space> Neighbors
        {
            get { return _neighbors; }
        }

        public Monster Monster { get; set; }

        public void Join(Space neighbor)
        {
            this._neighbors.Add(neighbor);
            neighbor._neighbors.Add(this);
        }
    }
}
