using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Advent._2018.Classes
{
    public class FabricClaim
    {
        public string Source { get; } = null;

        public string ID { get; } = null;

        public int X { get; private set; } = 0;
        public int Y { get; private set; } = 0;

        public int Width { get; private set; } = 0;
        public int Height { get; private set; } = 0;

        public FabricClaim(string input)
        {
            //< Retain the source string
            this.Source = input;
            //< Get the ID from the leading segment
            var arr = Source.Substring(1).Split('@');
            this.ID = arr[0].Trim();
            //< Split the trailing part of the initial segment
            arr = arr[1].Split(':');
            //< Parse the position & size of the claim
            ParsePosition(arr[0]);
            ParseSize(arr[1]);
        }

        void ParsePosition(string pos)
        {
            var arr = pos.Split(',').Select(x => int.Parse(x)).ToList();
            this.X = arr[0];
            this.Y = arr[1];
        }

        void ParseSize(string size)
        {
            var arr = size.Split('x').Select(x => int.Parse(x)).ToList();
            this.Width = arr[0];
            this.Height = arr[1];
        }

        public override string ToString()
        {
            return Source;
        }
    }

    public class Fabric
    {
        public List<FabricClaim> Claims { get; } = null;

        public Dictionary<int, Dictionary<int, List<string>>> Map { get; private set; } = null;

        public int MinX { get; private set; } = int.MaxValue;
        public int MinY { get; private set; } = int.MaxValue;
        public int MaxX { get; private set; } = int.MinValue;
        public int MaxY { get; private set; } = int.MinValue;

        public Fabric(IEnumerable<string> input)
        {
            this.Claims = input.Select(x => new FabricClaim(x)).ToList();

            CalculateClaimCoverage();
        }

        void CalculateClaimCoverage()
        {
            this.Map = new Dictionary<int, Dictionary<int, List<string>>>();

            foreach (var claim in Claims)
            {
                foreach (var x in Enumerable.Range(claim.X, claim.Width))
                {
                    MinX = Math.Min(MinX, x);
                    MaxX = Math.Max(MaxX, x);

                    if (!Map.ContainsKey(x))
                    {
                        Map.Add(x, new Dictionary<int, List<string>>());
                    }

                    foreach (var y in Enumerable.Range(claim.Y, claim.Height))
                    {
                        MinY = Math.Min(MinY, y);
                        MaxY = Math.Max(MaxY, y);

                        if (!Map[x].ContainsKey(y))
                        {
                            Map[x].Add(y, new List<string>());
                        }

                        Map[x][y].Add(claim.ID);
                    }
                }
            }
        }

        public int CountOverlappingCoverage(int numOverlap)
        {
            int count = 0;
            foreach (var x in Map.Keys)
            {
                foreach (var y in Map[x].Keys)
                {
                    if (Map[x][y].Count > numOverlap)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public string FindUncontestedClaim()
        {
            var covered = new HashSet<string>();
            foreach (var x in Map.Keys)
            {
                foreach (var y in Map[x].Keys)
                {
                    if (Map[x][y].Count > 1)
                    {
                        foreach (var id in Map[x][y])
                        {
                            covered.Add(id);
                        }
                    }
                }
            }

            var uncontested = Claims.Where(x => !covered.Contains(x.ID))
                                    .ToList();

            if (uncontested.Count == 1)
            {
                return uncontested.Single().ID;
            }
            else
                throw new ArgumentException($"Found no uncovered IDs!");
        }

        public void WriteMapToFile(string file)
        {
            var sb = new StringBuilder();

            for (int y = MaxY; y >= MinY; y--)
            {
                string s = "";
                foreach (var x in Enumerable.Range(MinX, MaxX - MinX + 1))
                {
                    if (Map.ContainsKey(x))
                    {
                        if (Map[x].ContainsKey(y))
                        {
                            if (Map[x][y].Count > 1)
                            {
                                s += "X";
                            }
                            else
                                s += Map[x][y].Single();
                        }
                        else
                            s += ".";
                    }
                    else
                        s += ".";
                }
                sb.AppendLine(s);
            }

            File.WriteAllText(file, sb.ToString());
        }
    }
}
