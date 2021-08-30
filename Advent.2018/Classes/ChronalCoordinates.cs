using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Advent._2018.Classes
{
    public class ChronalCoordinate
    {
        public int X { get; }
        public int Y { get; }

        public string ID { get; } = null;

        public ChronalCoordinate(string input)
        {
            var arr = input.Split(',').Select(int.Parse).ToArray();
            this.X = arr[0];
            this.Y = arr[1];
            this.ID = $"{X}_{Y}";
        }

        public int DistanceTo(int x, int y)
        {
            return Math.Abs(x - X) + Math.Abs(y - Y);
        }
    }

    public class ChronalCoordinateHandler
    {
        public List<ChronalCoordinate> Coordinates { get; } = null;
        public List<ChronalCoordinate> InteriorCoordinates { get; } = null;

        public int MaxX { get; private set; } = int.MinValue;
        public int MaxY { get; private set; } = int.MinValue;
        public int MinX { get; private set; } = int.MaxValue;
        public int MinY { get; private set; } = int.MaxValue;

        public ChronalCoordinateHandler(IEnumerable<string> inputs)
        {
            //< Parse the original Coordinates into a List
            this.Coordinates = inputs.Select(x => new ChronalCoordinate(x)).ToList();
            //< Check the bounding box (extrema) of available coordinates
            CheckExtrema();
        }

        bool IsBoundary(int X, int Y)
        {
            if (X == MinX || X == MaxX)
                return true;

            if (Y == MinY || Y == MaxY)
                return true;

            return false;
        }

        void CheckExtrema()
        {
            foreach (var coord in Coordinates)
            {
                MaxX = Math.Max(coord.X, MaxX);
                MaxY = Math.Max(coord.Y, MaxY);
                MinX = Math.Min(coord.X, MinX);
                MinY = Math.Min(coord.Y, MinY);
            }
        }

        public Dictionary<string, int> ComputeFiniteCoverageMap()
        {
            var map = new Dictionary<string, int>();

            var infiniteIDs = new HashSet<string>();

            //< Scan the 2D grid encompassed by our min/max 2D extents
            foreach (var x in Enumerable.Range(MinX - 1, MaxX - MinX + 1))
            {
                foreach (var y in Enumerable.Range(MinY - 1, MaxY - MinY + 1))
                {
                    //< Get the minimum distance to other points - check how many share that distance
                    var dTups = Coordinates.Select(c => Tuple.Create(c.DistanceTo(x, y), c))
                                           .OrderBy(t => t.Item1)
                                           .ToList();
                    var dMin = dTups.Min(t => t.Item1);
                    var match = dTups.Where(t => t.Item1 == dMin).ToList();
                    
                    //< If only one coordinate at the minimal distance -> iterate count
                    if (match.Count == 1)
                    {
                        var coord = match.Single().Item2;
                        //< If we're the best match for a boundary point -> this surf is infinite
                        if (IsBoundary(x, y))
                        {
                            infiniteIDs.Add(coord.ID);
                        }
                        //< Iterate the coverage map
                        if (!map.ContainsKey(coord.ID))
                        {
                            map.Add(coord.ID, 0);
                        }
                        map[coord.ID]++;
                    }
                }
            }

            //< Only return values for non-infinite segments
            return map.Where(kvp => !infiniteIDs.Contains(kvp.Key)).ToDictionary(x => x.Key, x => x.Value);
        }

        public int CalculateSafeRegion(int maxDist)
        {
            int count = 0;
            //< Scan the 2D grid encompassed by our min/max 2D extents
            foreach (var x in Enumerable.Range(MinX, MaxX - MinX))
            {
                foreach (var y in Enumerable.Range(MinY, MaxY - MinY))
                {
                    var dists = Coordinates.Select(c => c.DistanceTo(x, y));
                    var sum = dists.Sum();
                    
                    if (sum < maxDist)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
