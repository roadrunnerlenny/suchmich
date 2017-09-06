using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryGameLogicTests.TestBasics
{
    public class TestBase
    {
        public TestDataCreator TestData { get; set; }

        public TestBase()
        {
            this.TestData = new TestDataCreator();
        }
    }
}
