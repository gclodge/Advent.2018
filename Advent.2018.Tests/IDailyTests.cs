using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent._2018.Tests
{
    public enum Outcome
    {
        Ok,
        Fucked, 
        Default
    }

    public interface IDailyTests
    {
        int Number { get; }

        [TestMethod]
        void PartOne();

        [TestMethod]
        void PartTwo();
    }
}
