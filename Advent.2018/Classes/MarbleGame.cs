using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent._2018.Classes
{
    public static class MarbleGame
    {
        public const int StartValue = 0;

        public static long Play(int players, int lastMarble, bool partTwo = false)
        {
            var loopCount = partTwo ? lastMarble * 100 : lastMarble;

            var scores = new long[players];
            var circle = new LinkedList<long>();

            //< Initialize the Circle with the starting Marblej
            var curr = circle.AddFirst(StartValue);

            //< Simulate the remaining marbles (until last marble)
            for (int marb = 1; marb < loopCount; marb++)
            {
                //< Handle multiple of 23 case
                if (marb % 23 == 0)
                {
                    int currPlayer = marb % players;
                    scores[currPlayer] += marb;
                    foreach (int i in Enumerable.Range(0, 7))
                    {
                        //< Rotate seven times CCW to get the new marble
                        curr = curr.Previous ?? circle.Last;
                    }
                    //< Add to score then remove from the circle
                    scores[currPlayer] += curr.Value;
                    //< Update the 'current' marble to the CW marble from target
                    var remove = curr;
                    curr = remove.Next;
                    //< Remove the 'target' marbel from the circle
                    circle.Remove(remove);
                }
                else
                {
                    curr = circle.AddAfter(curr.Next ?? circle.First, marb);
                }
            }

            //< Return the high score
            return scores.Max();
        }
    }
}
