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
	public class PlayBoardTest : TestBase
	{
		[TestMethod]
		public void TestPlayBoard()
		{		
			PlayBoard board1 = new PlayBoard(TestData.Dimensions);
			TestBoard(board1);
		}

		private void TestBoard(PlayBoard board1)
		{
			Assert.IsTrue(board1.HasDimensions);
			Assert.AreEqual(board1.Dimensions, TestData.Dimensions);
		}

		[TestMethod]
		public void TestFindExistingInGameCard()
		{
			PlayBoard board1 = new PlayBoard(TestData.Dimensions);
			TestBoard(board1);
			
			Position testPosition = new Position(0,0);			
			Card testCard = TestData.GetTestCard(testPosition);

			board1.InGameCards.Add(testCard);
			Card retrieved = board1.FindInGameCard(testPosition);
            Assert.IsTrue(board1.HasInGameCards);
			Assert.AreEqual(retrieved, testCard);
		}

		[TestMethod]
		public void TestFindNotExistingInGameCard()
		{
			PlayBoard board1 = new PlayBoard(TestData.Dimensions);
			TestBoard(board1);

			Position testPosition = new Position(0, 0);
			Card testCard = TestData.GetTestCard(testPosition);
						
			Card retrieved = board1.FindInGameCard(testPosition);

			Assert.AreEqual(retrieved, null);
		}
	}
}
