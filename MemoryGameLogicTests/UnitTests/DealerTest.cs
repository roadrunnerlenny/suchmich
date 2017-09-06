using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryGameLogicLib;
using System.Diagnostics;
using MemoryGameLogicLib.Model;
using MemoryGameLogicTests.TestBasics;

namespace MemoryGameLogicTests
{
    [TestClass]
    public class DealerTest : TestBase
    {
        [TestMethod]
        public void TestShuffleCards()
        {
			CardDeck deck = new CardDeck(TestDataCreator.NumberOfSets, TestDataCreator.TestDeckName);
			Assert.AreEqual(deck.AllSets.Count, TestDataCreator.NumberOfSets);

			Dealer dealer = new Dealer(deck.AllCards, TestData.Dimensions);
			Assert.AreEqual(dealer.Deck.Count, TestDataCreator.NumberOfCards);

			PlayBoard board = new PlayBoard(TestData.Dimensions);

			dealer.GiveNewCards(board);

			Assert.AreEqual(board.InGameCards.Count, TestDataCreator.NumberOfCards);
        }
    }
}
