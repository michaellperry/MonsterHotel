using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Generation
{
    public class Graph
    {
        private List<Node> _nodes = new List<Node>();
        private List<Edge> _edges = new List<Edge>();
        private List<Region> _regions = new List<Region>();

        public Node NewNode()
        {
            Node node = new Node();
            _nodes.Add(node);
            return node;
        }

        public Edge NewEdge(Node n1, Node n2)
        {
            Edge edge = new Edge(n1, n2);
            _edges.Add(edge);
            return edge;
        }

        public Region NewRegion(params Edge[] edges)
        {
            Region region = new Region(edges.ToList());
            _regions.Add(region);
            return region;
        }

        public Region RandomRegion(IRandomNumberGenerator randomNumberGenerator)
        {
            return _regions[randomNumberGenerator.GetNumber(_regions.Count)];
        }
    }
}
