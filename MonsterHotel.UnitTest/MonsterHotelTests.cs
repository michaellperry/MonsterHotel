using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterHotel.Gameplay;

namespace MonsterHotel.Test
{
    [TestClass]
    public class MonsterHotelTests
    {
        [TestMethod]
        public void StartsInTheGreatHall()
        {
            Game game = GivenGame();
            Hero fighter = GivenFighter(game);

            Assert.AreSame(game.Board.GreatHall, fighter.Space);
        }

        [TestMethod]
        public void CanMoveToNorthCorridor()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Move(Direction.North);

            wizard.Space.Should().Be(game.Board.NorthCorridor);
        }

        [TestMethod]
        public void CanMoveBackToGreatHall()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Move(Direction.North);
            wizard.Move(Direction.South);

            wizard.Space.Should().Be(game.Board.GreatHall);
        }

        [TestMethod]
        public void CanMoveIntoRoom()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Move(Direction.North);
            wizard.Move(Direction.West);

            wizard.Space.Should().Be(game.Board.Room1);
        }

        [TestMethod]
        public void ARoomHasAMonster()
        {
            Game game = GivenGame();

            Monster goblin = game.Board.Room1.Monster;

            goblin.Name.Should().Be("Goblin");
        }

        [TestMethod]
        public void AFighterAttacksAGoblin()
        {
            Game game = GivenGame();
            Hero fighter = GivenFighter(game);

            game.Dice.WhenRoll(1);
            game.Dice.WhenRoll(1);

            Monster goblin = game.Board.Room1.Monster;
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

            Monster goblin = game.Board.Room1.Monster;
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

            Monster goblin = game.Board.Room1.Monster;
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

            Monster goblin = game.Board.Room1.Monster;
            goblin.Attack(wizard);

            wizard.Treasure.Count.Should().Be(0);
        }

        [TestMethod]
        public void AMonsterWoundsAHero()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            wizard.Treasure.Add(new Treasure());
            wizard.Move(Direction.North);
            wizard.Move(Direction.West);
            game.Dice.WhenRoll(4);
            game.Dice.WhenRoll(4);

            Monster goblin = game.Board.Room1.Monster;
            goblin.Attack(wizard);

            wizard.Treasure.Count.Should().Be(0);
            wizard.LostTurns.Should().Be(1);
            wizard.Space.Should().Be(game.Board.NorthCorridor);
        }

        [TestMethod]
        public void AMonsterSerioulyWoundsAHero()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            for (int i = 0; i < 7; ++i )
                wizard.Treasure.Add(new Treasure());
            wizard.Move(Direction.North);
            wizard.Move(Direction.West);
            game.Dice.WhenRoll(5);
            game.Dice.WhenRoll(6);

            Monster goblin = game.Board.Room1.Monster;
            goblin.Attack(wizard);

            wizard.Treasure.Count.Should().Be(3);
            wizard.LostTurns.Should().Be(0);
            wizard.Space.Should().Be(game.Board.GreatHall);
        }

        [TestMethod]
        public void AMonsterKillsAHero()
        {
            Game game = GivenGame();
            Hero wizard = GivenWizard(game);

            for (int i = 0; i < 7; ++i)
                wizard.Treasure.Add(new Treasure());
            wizard.Move(Direction.North);
            wizard.Move(Direction.West);
            game.Dice.WhenRoll(6);
            game.Dice.WhenRoll(6);

            Monster goblin = game.Board.Room1.Monster;
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
            return new Game();
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
