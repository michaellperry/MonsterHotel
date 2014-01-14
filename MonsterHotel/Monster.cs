﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterHotel
{
    public class Monster
    {
        public string Name { get; set; }
        public Dictionary<Class, int> Defense { get; set; }
        public bool IsDestroyed { get; set; }

        public void Attack(Hero hero)
        {
            int roll = Dice.Roll() + Dice.Roll();

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
