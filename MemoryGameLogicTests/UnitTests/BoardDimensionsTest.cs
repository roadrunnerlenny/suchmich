using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryGameLogicLib.Model;
using MemoryGameLogicLib.Exceptions;
using MemoryGameLogicTests.TestBasics;

namespace MemoryGameLogicTests.UnitTests
{
    [TestClass]
    public class BoardDimensionsTest : TestBase
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidBoardDimensionsException))]
        public void TestVerification()
        {
            BoardDimensions dimensions = new BoardDimensions(TestDataCreator.IncorrectBoardWidth, TestDataCreator.IncorrectBoardHeight);
            dimensions.VerifyDimensions();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidBoardDimensionsException))]
        public void TestInvalidBoardWidth()
        {
            BoardDimensions dimensions = new BoardDimensions(TestDataCreator.TestBoardWidth, TestDataCreator.TestBoardHeight);
            dimensions.Width = null;
            dimensions.VerifyDimensions();
        }

        [TestMethod]        
        public void TestHasValidBoardDimension()
        {
            BoardDimensions dimensions = new BoardDimensions(TestDataCreator.TestBoardWidth, TestDataCreator.TestBoardHeight);
            dimensions.Width = TestDataCreator.IncorrectBoardWidth;
            dimensions.Height = TestDataCreator.IncorrectBoardHeight;
            Assert.IsFalse(dimensions.HasValidDimensions);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidBoardDimensionsException))]
        public void TestInvalidBoardHeight()
        {
            BoardDimensions dimensions = new BoardDimensions(TestDataCreator.TestBoardWidth, TestDataCreator.TestBoardHeight);
            dimensions.Height = null;
            dimensions.VerifyDimensions();
        }

        [TestMethod]
        public void TestSetDimensions()
        {
            BoardDimensions dimensions = new BoardDimensions(TestDataCreator.TestBoardWidth, TestDataCreator.TestBoardHeight);
            Assert.AreEqual(TestDataCreator.TestBoardWidth, dimensions.Width);
            Assert.AreEqual(TestDataCreator.TestBoardHeight, dimensions.Height);
            dimensions.Width = TestDataCreator.OtherTestBoardWidth;
            dimensions.Height = TestDataCreator.OtherTestBoardHeight;
            Assert.AreEqual(TestDataCreator.OtherTestBoardWidth, dimensions.Width);
            Assert.AreEqual(TestDataCreator.OtherTestBoardHeight, dimensions.Height);
            Assert.IsTrue(dimensions.HasValidDimensions);
        }

    }
}
