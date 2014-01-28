using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterHotel.Gameplay;

namespace MonsterHotel.Test
{
    [TestClass]
    public class MonsterHotelTests
    {
        private Space _northCorridor;
        private Space _room1;

        [TestMethod]
        public void StartsInTheGreatHall()
        {
            Game game = GivenGame();
            Hero fighter = GivenFighter(game);

            Assert.AreSame(game.Board.Start, fighter.Space);
        }

        [TestMethod]
        public void CanMoveToNorthCorridor()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Move();

            wizard.Space.Should().Be(_northCorridor);
        }

        [TestMethod]
        public void CanMoveBackToGreatHall()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Move();
            wizard.Move(_room1);

            wizard.Space.Should().Be(game.Board.Start);
        }

        [TestMethod]
        public void CanMoveIntoRoom()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Move();
            wizard.Move(game.Board.Start);

            wizard.Space.Should().Be(_room1);
        }

        [TestMethod]
        public void ARoomHasAMonster()
        {
            Game game = GivenGame();

            Monster goblin = _room1.Monster;

            goblin.Name.Should().Be("Goblin");
        }

        [TestMethod]
        public void AFighterAttacksAGoblin()
        {
            Game game = GivenGame();
            Hero fighter = GivenFighter(game);

            game.Dice.WhenRoll(1);
            game.Dice.WhenRoll(1);

            Monster goblin = Monster.NewGoblin(game);
            fighter.Attack(goblin);

            goblin.IsDestroyed.Should().BeTrue("The fighter should have destroyed the goblin.");
        }

        [TestMethod]
        public void AWizardMissesAGoblin()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            game.Dice.WhenRoll(2);
            game.Dice.WhenRoll(2);

            Monster goblin = Monster.NewGoblin(game);
            wizard.Attack(goblin);

            goblin.IsDestroyed.Should().BeFalse("The wizard should not have destroyed the goblin.");
        }

        [TestMethod]
        public void AMonsterMissesAHero()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Treasure.Add(new Treasure());
            game.Dice.WhenRoll(1);
            game.Dice.WhenRoll(4);

            Monster goblin = Monster.NewGoblin(game);
            goblin.Attack(wizard);

            wizard.Treasure.Count.Should().Be(1);
        }

        [TestMethod]
        public void AMonsterStunsAHero()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Treasure.Add(new Treasure());
            game.Dice.WhenRoll(2);
            game.Dice.WhenRoll(4);

            Monster goblin = Monster.NewGoblin(game);
            goblin.Attack(wizard);

            wizard.Treasure.Count.Should().Be(0);
        }

        [TestMethod]
        public void AMonsterWoundsAHero()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Treasure.Add(new Treasure());
            wizard.Move();
            wizard.Move();
            game.Dice.WhenRoll(4);
            game.Dice.WhenRoll(4);

            Monster goblin = Monster.NewGoblin(game);
            goblin.Attack(wizard);

            wizard.Treasure.Count.Should().Be(0);
            wizard.LostTurns.Should().Be(1);
            wizard.Space.Should().Be(_northCorridor);
        }

        [TestMethod]
        public void AMonsterSerioulyWoundsAHero()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            for (int i = 0; i < 7; ++i )
                wizard.Treasure.Add(new Treasure());
            wizard.Move();
            wizard.Move();
            game.Dice.WhenRoll(5);
            game.Dice.WhenRoll(6);

            Monster goblin = Monster.NewGoblin(game);
            goblin.Attack(wizard);

            wizard.Treasure.Count.Should().Be(3);
            wizard.LostTurns.Should().Be(0);
            wizard.Space.Should().Be(game.Board.Start);
        }

        [TestMethod]
        public void AMonsterKillsAHero()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            for (int i = 0; i < 7; ++i)
                wizard.Treasure.Add(new Treasure());
            wizard.Move();
            wizard.Move();
            game.Dice.WhenRoll(6);
            game.Dice.WhenRoll(6);

            Monster goblin = Monster.NewGoblin(game);
            goblin.Attack(wizard);

            wizard.IsDestroyed.Should().BeTrue("The goblin should have killed the wizard.");
            wizard.Treasure.Count.Should().Be(0);
        }

        [TestMethod]
        public void AFighterHasGold()
        {
            Game game = GivenGame();
            Hero fighter = GivenFighter(game);

            fighter.Treasure.Add(new Treasure { Value = 1000 });
            fighter.Treasure.Add(new Treasure { Value = 250 });

            fighter.Gold.Should().Be(1250);
        }

        private Game GivenGame()
        {
            Game game = new Game();
            _northCorridor = new Space();
            _room1 = new Space();
            _northCorridor.Join(game.Board.Start);
            _northCorridor.Join(_room1);

            _room1.Monster = Monster.NewGoblin(game);
            return game;
        }

        private static Hero GivenFighter(Game game)
        {
            return new Hero(game, Class.Fighter);
        }

        private static Hero GivenWizard(Game game)
        {
            return new Hero(game, Class.Wizard);
        }
    }
}
