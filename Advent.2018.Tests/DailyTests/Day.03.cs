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
    public class Day03 : IDailyTests
    {
        public int Number => 3;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        List<string> KnownInputs = new List<string>()
        {
            "#1 @ 1,3: 4x4",
            "#2 @ 3,1: 4x4",
            "#3 @ 5,5: 2x2"
        };

        [TestMethod]
        public void Test_KnownClaims()
        {
            var fabric = new Fabric(KnownInputs);

            int overlap = fabric.CountOverlappingCoverage(1);

            //< Debug output -- as required
            //fabric.WriteMapToFile(@"D:\_dev_test\Advent.2018\Day.03.csv");
            
            Assert.IsTrue(overlap == 4);
        }

        [TestMethod]
        public void PartOne()
        {
            var inputs = Helpers.FileHelper.ParseFile(InputFile);
            var fabric = new Fabric(inputs);

            int overlap = fabric.CountOverlappingCoverage(1);
            Assert.IsTrue(overlap == 114946);
        }

        [TestMethod]
        public void PartTwo()
        {
            var inputs = Helpers.FileHelper.ParseFile(InputFile);
            var fabric = new Fabric(inputs);

            var id = fabric.FindUncontestedClaim();
            Assert.IsTrue(id == "877");
        }
    }
}
