using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryGameLogicLib;
using MemoryGameLogicLib.Model;
using MemoryGameLogicTests.TestBasics;

namespace MemoryGameLogicTests
{
    [TestClass]
    public class GameControllerTest : TestBase
    {
		GameController Controller { get; set; }
		Random Rand { get; set; }

		[TestInitialize()]
		public void InitController()
		{
			Controller = new GameController(TestData.Dimensions);
			Assert.IsTrue(Controller.HasBoard);			
		}

        [TestMethod]
        public void TestDefaultConstructor() 
        {
            GameController def = new GameController();
            Assert.IsTrue(Controller.HasBoard);
        }

		[TestMethod]
		public void ThreeFixedTurnsTestRun()
		{
			TestLayCardsOnBoard();
			TestStartGameWithTestPlayers();

			//This test works only for a board with dimensions 2x2!
			Assert.IsTrue(Controller.Board.Dimensions.Height==2);
			Assert.IsTrue(Controller.Board.Dimensions.Width== 2);

			//Now just try all possible turns with this small set of cards and possibilities
			Position p1 = new Position(0, 0); Position p2 = new Position(0, 1);
			if (Controller.Board.CardStillInGame(p1) && Controller.Board.CardStillInGame(p2))
				TestRevealAndUnreveal(p1, p2);

			p1 = new Position(0, 0); p2 = new Position(1, 0);
			if (Controller.Board.CardStillInGame(p1) && Controller.Board.CardStillInGame(p2))
				TestRevealAndUnreveal(p1, p2);

			p1 = new Position(0, 0); p2 = new Position(1, 1);
			if (Controller.Board.CardStillInGame(p1) && Controller.Board.CardStillInGame(p2))
				TestRevealAndUnreveal(p1, p2);

			TestIfAnyPlayerHasDiscardedCards();
		}

		void TestLayCardsOnBoard()
		{
			Controller.LayCardsOnBoard();
			Assert.IsTrue(Controller.Board.InGameCards.Count > 0);
		}

		void TestStartGameWithTestPlayers()
		{
			Controller.StartGame((from name in TestData.GetTestPlayerNames() select new Player(name)).ToList());
			Assert.IsTrue(Controller.Board.CanRevealCard);
			Assert.IsTrue(Controller.PlayerHandler.AllPlayers.Count == TestData.GetTestPlayerNames().Count);
			Assert.IsNotNull(Controller.PlayerHandler.CurrentPlayer);
		}

		void TestRevealAndUnreveal(Position p1, Position p2)
		{
			TestRevealFirstCard(p1);
			TestRevealSecondCard(p2);

			if (Controller.Board.HasBothCardsUnrevealed)
			{
				TestCheckIfCardsMatch();
				Assert.IsFalse(Controller.Board.CanRevealCard);
				TestUnrevealCards();
				Assert.IsTrue(Controller.Board.CanRevealCard);
			}			
			
			Assert.IsNull(Controller.Board.UnrevealedCard1);
			Assert.IsNull(Controller.Board.UnrevealedCard2);
			Assert.IsFalse(Controller.Board.HasUnrevealedCard1);
			Assert.IsFalse(Controller.Board.HasUnrevealedCard2);
		}

		void TestRevealFirstCard(Position p1)
		{
			Controller.RevealCard(p1);
			Assert.AreEqual(Controller.Board.FindInGameCard(p1).IsRevealed, true);
			Assert.IsTrue(Controller.Board.CanRevealCard);
		}

		void TestRevealSecondCard(Position p2)
		{
			Card storeForTest = Controller.Board.FindInGameCard(p2);
			Controller.RevealCard(p2);
			if (SecondCardDidNotMatch(p2))
				Assert.AreEqual(Controller.Board.FindInGameCard(p2).IsRevealed, true);
			else
			{
				Assert.IsTrue(Controller.PlayerHandler.CurrentPlayer.Rack.DiscardedCards.Count > 0);
				Assert.IsTrue(Controller.PlayerHandler.CurrentPlayer.Rack.DiscardedCards.Any(set => set.Card2 == storeForTest || set.Card1 == storeForTest));
			}
		}

		bool SecondCardDidNotMatch(Position p2)
		{
			return Controller.Board.CardStillInGame(p2);
		}

		void TestCheckIfCardsMatch()
		{
			bool doMatch = Controller.CheckIfCardsMatchAndDiscard();
			if (doMatch)
				Assert.IsTrue(Controller.PlayerHandler.CurrentPlayer.Rack.NumberOfSets > 0);
			Controller.CheckIfGameIsOver();
		}

		void TestUnrevealCards()
		{
			Card card1 = Controller.Board.UnrevealedCard1;
			Card card2 = Controller.Board.UnrevealedCard2;
			Assert.IsTrue(card1.IsRevealed);
			Assert.IsTrue(card2.IsRevealed);
			Controller.Board.UnrevealCards();			
		}
				
		void TestIfAnyPlayerHasDiscardedCards()
		{
			var allCards = Controller.PlayerHandler.AllPlayers.SelectMany(player => player.Rack.DiscardedCards);
			Assert.IsNotNull(allCards);
			Assert.IsTrue(allCards.Count() > 0);
		}

		[TestMethod]
		public void CompleteTestRun()
		{
			TestLayCardsOnBoard();
			TestStartGameWithTestPlayers();
			
			Rand = new Random();
			int failCount = 0;
			do
			{
				failCount++;
				if (failCount > 100) Assert.Fail();
				Position p1 = GetRandomPosition();				
				Position p2 = GetRandomPosition();
				if (p1.Equals(p2) || !Controller.Board.CardStillInGame(p1) || !Controller.Board.CardStillInGame(p2)) continue;
				TestRevealAndUnreveal(p1, p2);
				TestNextTurn();				
			} while (Controller.IsGameRunning);
			TestIfAnyPlayerHasDiscardedCards();
            TestStopGame();
		}

		Position GetRandomPosition()
		{
			ushort x = (ushort)Rand.Next(0,(int)Controller.Board.Dimensions.Width);
			ushort y = (ushort)Rand.Next(0,(int)Controller.Board.Dimensions.Width);
			return new Position(x, y);
		}

		private void TestNextTurn()
		{
			Assert.IsTrue(Controller.PlayerHandler.HasCurrentPlayer);
			Player curPlayer = Controller.PlayerHandler.CurrentPlayer;
			Controller.PlayerHandler.NextTurn();
			Assert.AreNotEqual(curPlayer, Controller.PlayerHandler.CurrentPlayer); 
		}

        void TestStopGame()
        {
            Controller.StopGame();
            Assert.IsFalse(Controller.IsGameRunning);
            Assert.AreEqual(Controller.Board.InGameCards.Count, 0);
            Assert.IsNull(Controller.Board.UnrevealedCard1);
            Assert.IsNull(Controller.Board.UnrevealedCard2);
            Assert.IsFalse(Controller.Board.CanRevealCard);

        }

	}
}
