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
    public class Day05 : IDailyTests
    {
        public int Number => 5;

        public string InputFile => TestHelper.GetInputFile(this);

        [TestMethod]
        public void Test_KnownPolymer()
        {
            var poly = new Polymer("dabAcCaCBAcCcaDA");

            Assert.IsTrue(poly.Reduced == "dabCBAcaDA");
            Assert.IsTrue(poly.Reduced.Length == 10);
        }

        [TestMethod]
        public void PartOne()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile);
            var poly = new Polymer(input.Single());

            Assert.IsTrue(poly.Reduced.Length == 10564);
        }

        [TestMethod]
        public void PartTwo()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile);
            var poly = new Polymer(input.Single());
            poly.GetSmallestReducedPolymer();

            int len = poly.MinimalReduced.Length;

            Assert.IsTrue(len == 6336);
        }
    }
}
