using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Generation
{
    public class Region
    {
        private readonly List<Edge> _edges;

        public Region(List<Edge> edges)
        {
            _edges = edges;            
        }
    }
}
