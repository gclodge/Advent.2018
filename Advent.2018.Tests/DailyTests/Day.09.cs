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
    public class Day09 : IDailyTests
    {
        public int Number => 9;

        public string InputFile => TestHelper.GetInputFile(this);

        [TestMethod]
        public void Test_KnownGames()
        {
            var knownGames = new List<Tuple<int, int, int>>()
            {
                Tuple.Create(10, 1618, 8317),
                Tuple.Create(13, 7999, 146373),
                Tuple.Create(17, 1104, 2720),
                Tuple.Create(21, 6111, 54718),
                Tuple.Create(30, 5807, 37305)
            };

            foreach (var game in knownGames)
            {
                var res = MarbleGame.Play(game.Item1, game.Item2);
                Assert.IsTrue(res == game.Item3);
            }
        }

        [TestMethod]
        public void PartOne()
        {
            int players = 435;
            int lastMarble = 71184;

            var res = MarbleGame.Play(players, lastMarble);
            Assert.IsTrue(res == 412959);
        }

        [TestMethod]
        public void PartTwo()
        {
            int players = 435;
            int lastMarble = 71184;

            var res = MarbleGame.Play(players, lastMarble, partTwo: true);
            Assert.IsTrue(res == 3333662986);
        }
    }
}
