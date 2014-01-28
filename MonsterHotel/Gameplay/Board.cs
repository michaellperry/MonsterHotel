using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Gameplay
{
    public class Board
    {
        private readonly Space _start = new Space();

        public Space Start
        {
            get { return _start; }
        }
    }
}
