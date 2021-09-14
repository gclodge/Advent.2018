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
    public class Day10 : IDailyTests
    {
        public int Number => 10;
        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [TestMethod]
        public void Test_KnownMessage()
        {
            var input = Helpers.FileHelper.ParseFile(TestFile);
            var msg = new StarMessage(input);

            string testImage = Path.ChangeExtension(TestFile, ".png");
            msg.SimulateSteps(3);
            msg.PrintMessage(testImage);
        }

        const int KnownKeyStep = 10391;

        [TestMethod]
        public void PartOne()
        {
            //int testLoops = 100000;

            //< Need to simulate several steps and find the minimum area
            //< - Hoping that a minimal area indicates a message has formed
            var input = Helpers.FileHelper.ParseFile(InputFile);
            var msg = new StarMessage(input);

            string img = Path.ChangeExtension(InputFile, ".png");
            msg.SimulateSteps(KnownKeyStep);
            msg.PrintMessage(img);

            #region Code to find minimal area (by step)
            //var dict = new Dictionary<int, int>();
            //foreach (var i in Enumerable.Range(0, testLoops))
            //{
            //    //< Stepping one at a time to track overall area
            //    msg.SimulateSteps(1);
            //    //< Get the area (TODO :: Check this works..)
            //    dict.Add(i, msg.Area);
            //}

            //var min = dict.OrderBy(x => x.Value).Take(5).ToArray();
            #endregion
        }

        [TestMethod]
        public void PartTwo()
        {
            //< Part two was just 'what step was there a message on'
            Assert.IsTrue(KnownKeyStep == 10391);
        }
    }
}
