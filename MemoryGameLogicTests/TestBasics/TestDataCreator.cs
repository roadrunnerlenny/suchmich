using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryGameLogicLib.Model;

namespace MemoryGameLogicTests.TestBasics
{
    [TestClass]
    public class TestDataCreator
    {
        public const ushort TestBoardWidth = 2;
        public const ushort TestBoardHeight = 2;

		public static ushort NumberOfSets
		{
			get
			{
				return (ushort)Math.Abs(TestBoardHeight * TestBoardWidth / 2);
			}
		}

		public static ushort NumberOfCards
		{
			get
			{
				return (ushort)(TestBoardHeight * TestBoardWidth);
			}
		}

        public const ushort OtherTestBoardWidth = 4;
        public const ushort OtherTestBoardHeight = 2;

        public const ushort IncorrectBoardWidth = 3;
        public const ushort IncorrectBoardHeight = 3;

		public const string TestPlayer1Name = "Player1";
		public const string TestPlayer2Name = "Player2";

		public const string TestDeckName = "Tiere";

        public BoardDimensions Dimensions { get; set; }
        
        public TestDataCreator()
        {
            InitBoardDimensions();
        }

        private void InitBoardDimensions()
        {
            this.Dimensions = new BoardDimensions(TestBoardWidth, TestBoardHeight);
            Assert.AreEqual(Dimensions.Width, TestBoardWidth);
            Assert.AreEqual(Dimensions.Height, TestBoardHeight);
        }

        public List<CardSet> GetTestCardSets()
        {
			List<CardSet> sets = new List<CardSet>();
			for (int i = 0; i < TestBoardWidth*TestBoardHeight; i=i+2)
				sets.Add(new CardSet(new Card()
				{
					CardSymbol = new Symbol("Test" + i)
				},
				new Card()
				{
					CardSymbol = new Symbol("Test" + i)
				}
				));
			Assert.IsTrue(sets.Count == NumberOfSets);
			return sets;
        }

		public Card GetTestCard(Position p) {			
			Card testCard = new Card();
			testCard.InGamePosition = p;
			testCard.CardSymbol = new Symbol("Test");
			Assert.IsNotNull(testCard);
			Assert.AreEqual(testCard.InGamePosition, p);
			Assert.IsTrue(testCard.CardSymbol.Equals(new Symbol("Test")));			
			return testCard;
		}

		public List<Player> GetTestPlayers()
		{
			List<Player> result = new List<Player>();
			result.Add(new Player(TestPlayer1Name));
			Assert.AreEqual(result[0].Name, TestPlayer1Name);
			result.Add(new Player(TestPlayer2Name));
			Assert.AreEqual(result[1].Name, TestPlayer2Name);
			return result;
		}

		public List<string> GetTestPlayerNames()
		{
			List<string> result = new List<string>();
			result.Add(TestPlayer1Name);
			result.Add(TestPlayer2Name);
			return result;
		}
		
    }
}
