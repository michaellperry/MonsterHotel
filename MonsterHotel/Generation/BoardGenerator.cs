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

            return new Board();
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
    }
}
