using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Generation
{
    public class Edge
    {
        private readonly Node _node1;
        private readonly Node _node2;

        public Edge(Node node1, Node node2)
        {
            _node1 = node1;
            _node2 = node2;
        }

        public Node Node1
        {
            get { return _node1; }
        }

        public Node Node2
        {
            get { return _node2; }
        }
    }
}
