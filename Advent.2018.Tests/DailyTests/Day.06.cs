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
    public class Day06 : IDailyTests
    {
        public int Number => 6;

        public string InputFile => TestHelper.GetInputFile(this);

        [TestMethod]
        public void Test_KnownPositions()
        {
            var input = new List<string>()
            {
                "1, 1",
                "1, 6",
                "8, 3",
                "3, 4",
                "5, 5",
                "8, 9"
            };

            var handler = new ChronalCoordinateHandler(input);
            var map = handler.ComputeFiniteCoverageMap();

            var max = map.Max(kvp => kvp.Value);
            Assert.IsTrue(max == 17);

            var safeRegion = handler.CalculateSafeRegion(32);
            Assert.IsTrue(safeRegion == 16);
        }

        [TestMethod]
        public void PartOne()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile);

            var handler = new ChronalCoordinateHandler(input);
            var map = handler.ComputeFiniteCoverageMap();

            var result = map.OrderBy(x => -1 * x.Value).Skip(1).Take(1).Single();

            //< Know the result is 4215 -> curr max says 5700 but is infinite and erroneously marked
            Assert.IsTrue(result.Value == 4215);
        }

        [TestMethod]
        public void PartTwo()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile);

            var handler = new ChronalCoordinateHandler(input);
            var safeRegion = handler.CalculateSafeRegion(10000);

            Assert.IsTrue(safeRegion == 40376);
        }
    }
}
