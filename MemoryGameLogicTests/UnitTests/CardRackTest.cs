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
	public class CardRackTest : TestBase
	{
		[TestMethod]
		public void TestDiscardCardNotInGame()
		{
			Card card1 = TestData.GetTestCardSets().First().Card1;
			Card card2 = TestData.GetTestCardSets().First().Card2;
			TestDiscardCards(card1, card2);				
		}

		[TestMethod]
		public void TestDiscardInGameCard()
		{
			Card card1 = TestData.GetTestCardSets().First().Card1;
			Card card2 = TestData.GetTestCardSets().First().Card2;
			card1.InGamePosition = new Position(0, 1);
			card2.InGamePosition = new Position(0, 0);
			TestDiscardCards(card1, card2);
			Assert.IsNull(card1.InGamePosition);
			Assert.IsNull(card2.InGamePosition);
		}

		void TestDiscardCards(Card card1, Card card2)
		{
			Player testPlayer = TestData.GetTestPlayers().First();
			CardRack rack = new CardRack(testPlayer);
			Assert.AreEqual(testPlayer.Rack.Owner, testPlayer);
			Assert.IsNotNull(rack.DiscardedCards);
			rack.DiscardCards(card1, card2);
			Assert.AreEqual(1, rack.DiscardedCards.Count);
			Assert.AreEqual(1, rack.NumberOfSets);			
		}

	}
}
