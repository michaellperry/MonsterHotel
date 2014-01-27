using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel.Gameplay
{
    public class Monster
    {
        private readonly Game _game;

        public Monster(Game game)
        {
            _game = game;
        }

        public string Name { get; set; }
        public Dictionary<Class, int> Defense { get; set; }
        public bool IsDestroyed { get; set; }

        public void Attack(Hero hero)
        {
            int roll = _game.Dice.Roll() + _game.Dice.Roll();

            if (roll == 12)
            {
                hero.Die();
            }
            else if (roll == 11)
            {
                hero.DropHalf();
                hero.GoHome();
            }
            else if (roll > 7)
            {
                hero.Drop();
                hero.LostTurns++;
                hero.KnockBack();
            }
            else if (roll > 5)
            {
                hero.Drop();
            }
        }
    }
}
