using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Advent._2018.Tests
{
    public class TestHelper
    {
        static Assembly Self = Assembly.GetExecutingAssembly();

        private const string _TestDir = "TestData";
        public static string TestDir => GetTestDirectoryRoot(_TestDir);

        public static string GetTestDirectoryRoot(string relativePath = null)
        {
            string[] hypotheticals = new[]
            {
                Path.Combine(Path.GetDirectoryName(Self.Location), @"..\..\.."),
                Path.Combine(Path.GetDirectoryName(Self.Location), @"..\..\..\..")
            };

            if (relativePath != null)
            {
                hypotheticals = hypotheticals.Select(x => Path.Combine(x, relativePath)).ToArray();
            }

            var exists = hypotheticals.Where(x => File.Exists(x) || Directory.Exists(x)).FirstOrDefault();
            return exists ?? null;
        }

        public static string GetInputFile(IDailyTests dt)
        {
            return GetFile(dt, "Input");
        }

        public static string GetTestFile(IDailyTests dt, string kernel = null)
        {
            return GetFile(dt, (kernel == null) ? "Test" : kernel);
        }

        private static string GetFile(IDailyTests dt, string kernel)
        {
            return Path.Combine(TestDir, $"Day.{dt.Number.ToString().PadLeft(2, '0')}.{kernel}.txt");
        }

        const int Seed = 1337;
        static readonly Random Rand = new Random(Seed);
        public static T ChooseRandom<T>(IEnumerable<T> things)
        {
            int count = things.Count();
            int idx = Rand.Next(Seed);

            return things.ElementAt(idx % count);
        }

    }
}
