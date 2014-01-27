using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Gameplay
{
    public class Hero
    {
        private static Random _random = new Random();

        private readonly Game _game;

        private Space _previousSpace;
        
        public Hero(Game game, Class @class)
        {
            _game = game;
            Space = _game.Board.GreatHall;
            Class = @class;
            Treasure = new List<Treasure>();
        }

        public bool IsDestroyed { get; private set; }
        public Space Space { get; set; }
        public Class Class { get; private set; }
        public IList<Treasure> Treasure { get; private set; }
        public int Gold
        {
            get
            {
                return Treasure.Sum(t => t.Value);
            }
        }
        public int LostTurns { get; set; }

        public void Move(Direction direction)
        {
            try
            {
                Space nextSpace = Space.Neighbor[direction];
                _previousSpace = Space;
                Space = nextSpace;
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("You can't go that way.");
            }
        }

        public void KnockBack()
        {
            Space = _previousSpace;
        }

        public void GoHome()
        {
            Space = _game.Board.GreatHall;
        }

        public void Die()
        {
            IsDestroyed = true;
            Treasure.Clear();
        }

        public void Attack(Monster monster)
        {
            int roll = _game.Dice.Roll() + _game.Dice.Roll();
            if (roll >= monster.Defense[Class])
            {
                monster.IsDestroyed = true;
            }
        }

        public void Drop()
        {
            int index = _random.Next(Treasure.Count);
            Treasure.RemoveAt(index);
        }

        public void DropHalf()
        {
            int dropCount = (Treasure.Count + 1) / 2;
            for (int i = 0; i < dropCount; ++i)
                Drop();
        }
    }
}
