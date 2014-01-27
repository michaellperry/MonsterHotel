using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Gameplay
{
    public class Dice
    {
        private Queue<int> _nextRoll = new Queue<int>();
        private Random _random = new Random();

        public void WhenRoll(int number)
        {
            _nextRoll.Enqueue(number);
        }

        public int Roll()
        {
            if (_nextRoll.Any())
                return _nextRoll.Dequeue();

            return _random.Next(6) + 1;
        }
    }
}
