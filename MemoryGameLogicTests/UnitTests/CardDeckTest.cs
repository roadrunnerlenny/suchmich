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
    public class CardDeckTest : TestBase
    {
        [TestMethod]
        public void GetStockTest()
        {
			CardDeck deck = new CardDeck(TestDataCreator.NumberOfSets,TestDataCreator.TestDeckName);
			Assert.AreEqual(deck.AllSets.Count, TestDataCreator.NumberOfSets);
            Assert.IsTrue(deck.HasCards);             
        }
    }
}
