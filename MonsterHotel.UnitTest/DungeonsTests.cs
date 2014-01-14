using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterHotel;

namespace MonsterHotel.Test
{
    [TestClass]
    public class DungeonsTests
    {
        [TestMethod]
        public void StartsInTheGreatHall()
        {
            Board board = new Board();
            Hero fighter = new Hero(board, Class.Fighter);
            Assert.AreSame(board.GreatHall, fighter.Space);
        }

        [TestMethod]
        public void CanMoveToNorthCorridor()
        {
            Board board = new Board();
            Hero wizard = new Hero(board, Class.Wizard);
            wizard.Move(Direction.North);

            Assert.AreSame(board.NorthCorridor, wizard.Space);
        }

        [TestMethod]
        public void CanMoveBackToGreatHall()
        {
            Board board = new Board();
            Hero wizard = new Hero(board, Class.Wizard);
            wizard.Move(Direction.North);
            wizard.Move(Direction.South);

            Assert.AreSame(board.GreatHall, wizard.Space);
        }

        [TestMethod]
        public void CanMoveIntoRoom()
        {
            Board board = new Board();
            Hero wizard = new Hero(board, Class.Wizard);
            wizard.Move(Direction.North);
            wizard.Move(Direction.West);

            Assert.AreSame(board.Room1, wizard.Space);
        }

        [TestMethod]
        public void ARoomHasAMonster()
        {
            Board board = new Board();
            Monster goblin = board.Room1.Monster;

            Assert.AreEqual("Goblin", goblin.Name);
        }

        [TestMethod]
        public void AFighterAttacksAGoblin()
        {
            Board board = new Board();
            Hero fighter = new Hero(board, Class.Fighter);
            Monster goblin = board.Room1.Monster;

            Dice.ShouldRoll(1);
            Dice.ShouldRoll(1);

            fighter.Attack(goblin);
            Assert.IsTrue(goblin.IsDestroyed, "The fighter should have destroyed the goblin.");
        }

        [TestMethod]
        public void AWizardMissesAGoblin()
        {
            Board board = new Board();
            Hero wizard = new Hero(board, Class.Wizard);
            Monster goblin = board.Room1.Monster;

            Dice.ShouldRoll(2);
            Dice.ShouldRoll(2);

            wizard.Attack(goblin);
            Assert.IsFalse(goblin.IsDestroyed, "The wizard should not have destroyed the goblin.");
        }

        [TestMethod]
        public void AMonsterMissesAHero()
        {
            Board board = new Board();
            Hero wizard = new Hero(board, Class.Wizard);
            wizard.Treasure.Add(new Treasure());
            Monster goblin = board.Room1.Monster;

            Dice.ShouldRoll(1);
            Dice.ShouldRoll(4);

            goblin.Attack(wizard);
            Assert.AreEqual(1, wizard.Treasure.Count);
        }

        [TestMethod]
        public void AMonsterStunsAHero()
        {
            Board board = new Board();
            Hero wizard = new Hero(board, Class.Wizard);
            wizard.Treasure.Add(new Treasure());
            Monster goblin = board.Room1.Monster;

            Dice.ShouldRoll(2);
            Dice.ShouldRoll(4);

            goblin.Attack(wizard);
            Assert.AreEqual(0, wizard.Treasure.Count);
        }

        [TestMethod]
        public void AMonsterWoundsAHero()
        {
            Board board = new Board();
            Hero wizard = new Hero(board, Class.Wizard);
            wizard.Treasure.Add(new Treasure());
            wizard.Move(Direction.North);
            wizard.Move(Direction.West);
            Monster goblin = board.Room1.Monster;

            Dice.ShouldRoll(4);
            Dice.ShouldRoll(4);

            goblin.Attack(wizard);
            Assert.AreEqual(0, wizard.Treasure.Count);
            Assert.AreEqual(1, wizard.LostTurns);
            Assert.AreSame(board.NorthCorridor, wizard.Space);
        }

        [TestMethod]
        public void AMonsterSerioulyWoundsAHero()
        {
            Board board = new Board();
            Hero wizard = new Hero(board, Class.Wizard);
            for (int i = 0; i < 7; ++i )
                wizard.Treasure.Add(new Treasure());
            wizard.Move(Direction.North);
            wizard.Move(Direction.West);
            Monster goblin = board.Room1.Monster;

            Dice.ShouldRoll(5);
            Dice.ShouldRoll(6);

            goblin.Attack(wizard);
            Assert.AreEqual(3, wizard.Treasure.Count);
            Assert.AreEqual(0, wizard.LostTurns);
            Assert.AreSame(board.GreatHall, wizard.Space);
        }

        [TestMethod]
        public void AMonsterKillsAHero()
        {
            Board board = new Board();
            Hero wizard = new Hero(board, Class.Wizard);
            for (int i = 0; i < 7; ++i)
                wizard.Treasure.Add(new Treasure());
            wizard.Move(Direction.North);
            wizard.Move(Direction.West);
            Monster goblin = board.Room1.Monster;

            Dice.ShouldRoll(6);
            Dice.ShouldRoll(6);

            goblin.Attack(wizard);
            Assert.IsTrue(wizard.IsDestroyed, "The goblin should have killed the wizard.");
            Assert.AreEqual(0, wizard.Treasure.Count);
        }

        [TestMethod]
        public void AFighterHasGold()
        {
            Board board = new Board();
            Hero fighter = new Hero(board, Class.Fighter);
            fighter.Treasure.Add(new Treasure { Value = 1000 });
            fighter.Treasure.Add(new Treasure { Value = 250 });

            Assert.AreEqual(1250, fighter.Gold);
        }
    }
}
