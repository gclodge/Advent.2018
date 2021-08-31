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
    public class Day07 : IDailyTests
    {
        public int Number => 7;

        public string InputFile => TestHelper.GetInputFile(this);

        [TestMethod]
        public void Test_KnownSteps()
        {
            var inputs = new List<string>()
            {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
                "Step A must be finished before step D can begin.",
                "Step B must be finished before step E can begin.",
                "Step D must be finished before step E can begin.",
                "Step F must be finished before step E can begin."
            };

            var solver = new StepSolver(inputs);
            solver.Solve(1, isTest: true);

            var order = solver.Ordered.ToString();
            Assert.IsTrue(order == "CABDFE");
        }

        [TestMethod]
        public void PartOne()
        {
            var inputs = Helpers.FileHelper.ParseFile(InputFile);

            var solver = new StepSolver(inputs);
            solver.Solve(1, false);

            var order = solver.Ordered.ToString();
            Assert.IsTrue(order == "LAPFCRGHVZOTKWENBXIMSUDJQY");
        }

        [TestMethod]
        public void PartTwo()
        {
            var inputs = Helpers.FileHelper.ParseFile(InputFile);

            var solver = new StepSolver(inputs);
            solver.Solve(2, false);

            var time = solver.TimeTaken;
            Assert.IsTrue(time == 936);
        }
    }
}
