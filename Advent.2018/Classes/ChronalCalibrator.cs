using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent._2018.Classes
{
    public class ChronalCalibrator
    {
        public const int DefaultValue = 0;

        public List<int> Steps { get; private set; } = null;

        public ChronalCalibrator(IEnumerable<string> input)
        {
            this.Steps = input.Select(x => int.Parse(x)).ToList();
        }

        public int GetCalibratedValue(int startValue = DefaultValue)
        {
            var total = Steps.Sum(x => x);
            return startValue + total;
        }

        const int MaxLoops = 1000;
        public int FindRepeatedFrequency(int startValue = DefaultValue)
        {
            var freqMap = new HashSet<int>();

            int val = startValue;
            int loopCount = 0;
            while (true)
            {
                if (loopCount > MaxLoops)
                    break;

                foreach (var step in Steps)
                {
                    val += step;
                    if (!freqMap.Contains(val))
                    {
                        freqMap.Add(val);
                    }
                    else
                    {
                        return val;
                    }
                }
                loopCount++;
            }

            throw new ArgumentException($"Unable to find repeated frequency w/in {MaxLoops} loops");
        }
    }
}
