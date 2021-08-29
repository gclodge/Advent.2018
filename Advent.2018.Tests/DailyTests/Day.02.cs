using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Advent._2018.Classes;

namespace Advent._2018.Tests.DailyTests
{
    [TestClass]
    public class Day02 : IDailyTests
    {
        public int Number => 2;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [TestMethod]
        public void Test_KnownCounts()
        {
            var input = Helpers.FileHelper.ParseFile(TestFile, BoxID.Parse)
                                          .ToList();

            int num2 = input.Count(x => x.Twice);
            int num3 = input.Count(x => x.Thrice);

            Assert.IsTrue(num2 == 4);
            Assert.IsTrue(num3 == 3);
            Assert.IsTrue(num2 * num3 == 12);
        }

        [TestMethod]
        public void Test_KnownMatches()
        {
            var input = Helpers.FileHelper.ParseFile(TestHelper.GetTestFile(this, "Test2"), BoxID.Parse)
                                          .ToList();

            var res = BoxID.FindCommonLetters(input);
            Assert.IsTrue(res == "fgij");
        }

        [TestMethod]
        public void PartOne()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile, BoxID.Parse)
                                          .ToList();

            int num2 = input.Count(x => x.Twice);
            int num3 = input.Count(x => x.Thrice);
            int checksum = num2 * num3;

            Assert.IsTrue(checksum == 7533);
        }

        [TestMethod]
        public void PartTwo()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile, BoxID.Parse)
                                          .ToList();

            var res = BoxID.FindCommonLetters(input);
            Assert.IsTrue(res == "mphcuasvrnjzzkbgdtqeoylva");
        }
    }
}
