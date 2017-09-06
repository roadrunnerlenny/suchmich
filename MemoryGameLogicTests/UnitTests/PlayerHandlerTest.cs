using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryGameLogicTests.TestBasics;
using MemoryGameLogicLib.Model;

namespace MemoryGameLogicTests.UnitTests
{
	[TestClass]
	public class PlayerHandlerTest : TestBase
	{

		PlayerHandler Handler { get; set; }
		[TestMethod]
		public void TestDiscardCards()
		{
			Card card1 = TestData.GetTestCard(new Position(0,0));
			Card card2 = TestData.GetTestCard(new Position(0,1));
			Handler.DiscardCards(card1, card2);
			Assert.IsTrue(Handler.CurrentPlayer.Rack.NumberOfSets == 1);
			Assert.AreEqual(Handler.CurrentPlayer.Rack.DiscardedCards.First().Card1, card1);
			Assert.AreEqual(Handler.CurrentPlayer.Rack.DiscardedCards.First().Card2, card2);

		}

		[TestInitialize()]
		public void TestInitPlayers()
		{
			Handler = new PlayerHandler();
			Handler.InitPlayers((from name in TestData.GetTestPlayerNames() select new Player(name)).ToList());
            Assert.IsTrue(Handler.HasAllPlayers);
            Assert.IsTrue(Handler.AllPlayers.Count == TestData.GetTestPlayerNames().Count);
			Assert.IsTrue(Handler.CurrentPlayer.Name.Equals( TestData.GetTestPlayerNames().First()));
            Assert.AreEqual(Handler.CurrentPlayer.ToString(),TestData.GetTestPlayerNames().First());
		}

		[TestMethod] 
		public void TestNextTurn() {
			Assert.IsTrue(TestData.GetTestPlayerNames().Count == 2);
			Player CurPlayer = Handler.CurrentPlayer;
			Assert.IsNotNull(CurPlayer);
			Assert.IsTrue(TestData.GetTestPlayerNames().First().Equals(Handler.CurrentPlayer.Name));			
			
			Handler.NextTurn();
			
			Assert.AreNotEqual(Handler.CurrentPlayer,CurPlayer);
			Assert.IsTrue(TestData.GetTestPlayerNames().Last().Equals(Handler.CurrentPlayer.Name));			
			
			Handler.NextTurn();
			
			Assert.AreEqual(Handler.CurrentPlayer, CurPlayer);
			Assert.IsTrue(TestData.GetTestPlayerNames().First().Equals(Handler.CurrentPlayer.Name));			
		}
	}
}
