using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Advent._2018.Classes;

namespace Advent._2018.Tests.DailyTests
{
    [TestClass]
    public class Day01 : IDailyTests
    {
        public int Number => 1;

        public string InputFile => TestHelper.GetInputFile(this);

        [TestMethod]
        public void PartOne()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile);
            var calib = new ChronalCalibrator(input);

            int val = calib.GetCalibratedValue(0);
            Assert.IsTrue(val == 574);
        }

        [TestMethod]
        public void PartTwo()
        {
            var input = Helpers.FileHelper.ParseFile(InputFile);
            var calib = new ChronalCalibrator(input);

            int val = calib.FindRepeatedFrequency();
            Assert.IsTrue(val == 452);
        }
    }
}
