using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryGameLogicLib.Model;
using MemoryGameLogicTests.TestBasics;

namespace MemoryGameLogicTests.UnitTests
{
    [TestClass]
    public class CardTest : TestBase
    {
        [TestMethod]
        public void TestRevealCard()
        {
            Card card1 = new Card();
            Assert.IsFalse(card1.IsRevealed);
            card1.Reveal();
            Assert.IsTrue(card1.IsRevealed);
			card1.Unreveal();
			Assert.IsFalse(card1.IsRevealed);			
        }

		[TestMethod]
		public void TestDiscardCard()
		{
			Card card1 = new Card();
			Player testPlayer = TestData.GetTestPlayers().First();
			Assert.IsNull(card1.OwningPlayer);
			card1.DiscardCard(testPlayer);
			Assert.AreEqual(card1.OwningPlayer, testPlayer);
		}

        [TestMethod]
        public void TestToString()
        {
            Position p = new Position(1, 1);
            Card card1 = TestData.GetTestCard(p);
            Assert.AreEqual(card1.ToString(), card1.CardSymbol.ToString() + "(" + p.ToString() + ")");           
        }
    }
}
