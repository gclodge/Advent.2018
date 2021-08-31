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
    public class Day08 : IDailyTests
    {
        public int Number => 8;

        public string InputFile => TestHelper.GetInputFile(this);

        [TestMethod]
        public void Test_KnownTree()
        {
            var input = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
            var tree = new LicenseTree(input);

            int sum = tree.Root.Sum();
            Assert.IsTrue(sum == 138);

            int val = tree.Root.Value();
            Assert.IsTrue(val == 66);
        }

        [TestMethod]
        public void PartOne()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile).Single();

            var tree = new LicenseTree(input);
            int sum = tree.Root.Sum();

            Assert.IsTrue(sum == 49426);
        }

        [TestMethod]
        public void PartTwo()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile).Single();

            var tree = new LicenseTree(input);
            int val = tree.Root.Value();

            Assert.IsTrue(val == 40688);
        }
    }
}
