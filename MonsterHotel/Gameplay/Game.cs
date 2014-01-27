
namespace MonsterHotel.Gameplay
{
    public class Game
    {
        private readonly Board _board;
        private readonly Dice _dice;

        public Game()
        {
            _board = new Board(this);
            _dice = new Dice();
        }

        public Board Board
        {
            get { return _board; }
        }

        public Dice Dice
        {
            get { return _dice; }
        }
    }
}
