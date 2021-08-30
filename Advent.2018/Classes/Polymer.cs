using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Advent._2018.Classes
{
    public class Polymer
    {
        public string Source { get; } = null;

        public string Reduced { get; private set; } = null;
        public string MinimalReduced { get; private set; } = null;

        public Polymer(string input)
        {
            //< Retain original, input Polymer
            this.Source = input;
            //< Solve them reactions
            this.Reduced = SolveReactions(Source);
        }

        public void GetSmallestReducedPolymer()
        {
            for (char c = 'a'; c < 'z'; c++)
            {
                var cleaned = Clean(Source, c);

                var res = SolveReactions(cleaned);
                if (MinimalReduced == null || res.Length < MinimalReduced.Length)
                {
                    MinimalReduced = res;
                }
            }
        }

        static string SolveReactions(string polymer)
        {
            var stacc = new Stack<char>();
            foreach (char c in polymer)
            {
                if (stacc.Count == 0)
                {
                    stacc.Push(c);
                }
                else
                {
                    char last = stacc.Peek();
                    if (Reacts(c, last))
                    {
                        //< If it reacts -> remove the last entry
                        stacc.Pop();
                    }
                    else
                    {
                        //< No reaction -> add to stack
                        stacc.Push(c);
                    }
                }
            }

            return string.Join("", stacc.Reverse());
        }

        static bool Reacts(char a, char b)
        {
            //< Check if the letter matches (case insensitive)
            bool bLetter = (char.ToLower(a) == char.ToLower(b));
            //< If correct letter but strings not equal -> polar match
            return (bLetter && a != b);
        }

        static string Clean(string str, char c)
        {
            return str.Replace(c.ToString(), "").Replace(char.ToUpper(c).ToString(), "");
        }
    }
}
