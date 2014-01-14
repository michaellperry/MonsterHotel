using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel
{
    public static class Dice
    {
        private static Queue<int> _nextRoll = new Queue<int>();
        private static Random _random = new Random();

        public static void ShouldRoll(int number)
        {
            _nextRoll.Enqueue(number);
        }

        public static int Roll()
        {
            if (_nextRoll.Any())
                return _nextRoll.Dequeue();

            return _random.Next(6) + 1;
        }
    }
}
