using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent._2018.Tests
{
    [TestClass]
    public class TestHelperTests
    {
        private static readonly List<string> Things = new List<string>()
        {
            "Graham",
            "Craig",
            "Marcus"
        };

        [TestMethod]
        public void AdHoc_ChooseOne()
        {
            var item = TestHelper.ChooseRandom(Things);
            System.Diagnostics.Debug.WriteLine($"{item}");
            //< Lol, how actually test?
            Assert.IsTrue(item != null);
        }

        const int TestAmount = 1000;
        [TestMethod]
        public void Test_ChooseRandom()
        {
            var hist = new Dictionary<string, int>();
            foreach (int i in Enumerable.Range(0, TestAmount))
            {
                var item = TestHelper.ChooseRandom(Things);
                if (!hist.ContainsKey(item))
                {
                    hist.Add(item, 0);
                }
                hist[item]++;
            }

            Assert.IsTrue(hist.Keys.Count > 1);
        }
    }
}
