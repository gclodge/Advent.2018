using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using Advent._2018.Classes;

namespace Advent._2018.Tests.DailyTests
{
    [TestClass]
    public class Day04 : IDailyTests
    {
        public int Number => 4;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [TestMethod]
        public void Test_KnownSleep()
        {
            var input = Helpers.FileHelper.ParseFile(TestFile);

            var tracker = new GuardSleepTracker(input);
            var tup = tracker.GetMostAsleepGuard();

            int res = tup.Item1 * tup.Item3;
            Assert.IsTrue(res == 240);

            var tup2 = tracker.GetGuardMostRegularlyAsleep();
            int res2 = tup2.Item1 * tup2.Item3;
            Assert.IsTrue(res2 == 4455);
        }

        [TestMethod]
        public void PartOne()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile);

            var tracker = new GuardSleepTracker(input);
            var tup = tracker.GetMostAsleepGuard();

            int res = tup.Item1 * tup.Item3;
            Assert.IsTrue(res == 19830);
        }

        [TestMethod]
        public void PartTwo()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile);

            var tracker = new GuardSleepTracker(input);
            var tup = tracker.GetGuardMostRegularlyAsleep();

            int res = tup.Item1 * tup.Item3;
            Assert.IsTrue(res == 43695);
        }
    }
}
