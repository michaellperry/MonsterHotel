using MonsterHotel.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Generation
{
    public class BoardGenerator
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public BoardGenerator(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public Board GenerateBoard()
        {
            Graph graph = InitialGraph();
            var region = graph.RandomRegion(_randomNumberGenerator);

            Board board = GraphToBoard(graph);

            return board;
        }

        private static Graph InitialGraph()
        {
            var graph = new Graph();

            var c = graph.NewNode();
            var n = graph.NewNode();
            var e = graph.NewNode();
            var s = graph.NewNode();
            var w = graph.NewNode();

            var cn = graph.NewEdge(c, n);
            var ce = graph.NewEdge(c, e);
            var cs = graph.NewEdge(c, s);
            var cw = graph.NewEdge(c, w);
            var ne = graph.NewEdge(n, e);
            var es = graph.NewEdge(e, s);
            var sw = graph.NewEdge(s, w);
            var wn = graph.NewEdge(w, n);

            var r1 = graph.NewRegion(cn, ne, ce);
            var r2 = graph.NewRegion(ce, es, cs);
            var r3 = graph.NewRegion(cs, sw, cw);
            var r4 = graph.NewRegion(cw, wn, cn);
            var r5 = graph.NewRegion(wn, sw, es, ne);

            return graph;
        }

        private static Board GraphToBoard(Graph graph)
        {
            Board board = new Board();
            Dictionary<Node, Space> nodeBySpace = new Dictionary<Node, Space>();
            nodeBySpace[graph.Center] = board.Start;
            foreach (var edge in graph.Edges)
            {
                Space space1 = EdgeToSpace(nodeBySpace, edge.Node1);
                Space space2 = EdgeToSpace(nodeBySpace, edge.Node2);
                space1.Join(space2);
            }
            return board;
        }

        private static Space EdgeToSpace(Dictionary<Node, Space> nodeBySpace, Node node)
        {
            Space space;
            if (!nodeBySpace.TryGetValue(node, out space))
            {
                space = new Space();
                nodeBySpace.Add(node, space);
            }
            return space;
        }
    }
}
