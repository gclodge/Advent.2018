using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent._2018.Classes
{
    public class BoxID
    {
        public string ID { get; } = null;
        public Dictionary<char, int> Map { get; set; } = null;

        public bool Twice => CheckCount(2);
        public bool Thrice => CheckCount(3);

        public BoxID(string id)
        {
            this.ID = id;
            this.Map = ParseID(ID);
        }

        bool CheckCount(int count)
        {
            return Map.Any(x => x.Value == count);
        }

        public override string ToString()
        {
            return ID;
        }

        static Dictionary<char, int> ParseID(string id)
        {
            var map = new Dictionary<char, int>();
            foreach (char c in id)
            {
                if (!map.ContainsKey(c))
                {
                    map.Add(c, 0);
                }
                map[c]++;
            }
            return map;
        }

        public static BoxID Parse(string input)
        {
            return new BoxID(input);
        }

        public static string FindCommonLetters(IEnumerable<BoxID> ids)
        {
            var toCheck = new List<BoxID>();
            foreach (var id in ids)
            {
                var matches = FindMatchingIDs(id, ids);
                if (matches.Count > 0)
                {
                    toCheck.Add(id);
                }
            }

            if (toCheck.Count != 2)
                throw new ArgumentException($"U wot? Count must be 2, had: {toCheck.Count}");

            return GetCommonString(toCheck.First(), toCheck.Last());
        }

        public static List<BoxID> FindMatchingIDs(BoxID source, IEnumerable<BoxID> ids)
        {
            var match = new List<BoxID>();
            foreach (var other in ids)
            {
                //< Skip duplicates
                if (other.ID == source.ID)
                    continue;

                int numDiff = 0;
                for (int i = 0; i < source.ID.Length; i++)
                {
                    if (source.ID[i] != other.ID[i])
                    {
                        numDiff += 1;
                    }
                }

                if (numDiff == 1)
                {
                    match.Add(other);
                }
            }

            return match;
        }

        public static string GetCommonString(BoxID a, BoxID b)
        {
            string res = "";
            for (int i = 0; i < a.ID.Length; i++)
            {
                if (a.ID[i] == b.ID[i])
                {
                    res += a.ID[i];
                }
            }
            return res;
        }
    }
}
