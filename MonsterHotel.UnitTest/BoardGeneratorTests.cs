using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterHotel.Generation;
using MonsterHotel.Gameplay;
using FluentAssertions;
using System.Collections.Generic;

namespace MonsterHotel.UnitTest
{
    [TestClass]
    public class BoardGeneratorTests
    {
        [TestMethod]
        public void CanGenerateBoard()
        {
            var randomNumberGenerator = new SweepingRandomNumberGenerator();
            var boardGenerator = new BoardGenerator(randomNumberGenerator);

            int passes = 0;
            while (randomNumberGenerator.Next())
            {
                var board = boardGenerator.GenerateBoard();

                OutputBoard(board);
                ++passes;
            }

            passes.Should().Be(5);
        }

        private int IndexOfSpace(List<Space> spaces, Space space)
        {
            int index = spaces.IndexOf(space);
            if (index == -1)
            {
                spaces.Add(space);
                return spaces.Count - 1;
            }
            return index;
        }

        private void OutputBoard(Board board)
        {
            List<Space> spaces = new List<Space>();
            List<Space> visited = new List<Space>();
            Queue<Space> toVisit = new Queue<Space>();
            toVisit.Enqueue(board.Start);

            System.Diagnostics.Debug.WriteLine("graph board {");
            while (toVisit.Count > 0)
            {
                var space = toVisit.Dequeue();
                int start = IndexOfSpace(spaces, space);
                visited.Add(space);
                foreach (var neighbor in space.Neighbors)
                {
                    int stop = IndexOfSpace(spaces, neighbor);
                    if (start < stop)
                        System.Diagnostics.Debug.WriteLine(String.Format("  {0}--{1}", start, stop));
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        toVisit.Enqueue(neighbor);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("}");
        }
    }
}
