using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent._2018.Classes
{
    public class Instruction
    {
        public string Precursor { get; } = null;
        public string Dependent { get; } = null;

        public Instruction(string input)
        {
            //< Format of: 'Step C must be finished before step A can begin.'
            var arr = input.Split(' ').Where(s => s.Length == 1).ToArray();

            if (arr.Length != 2)
                throw new ArgumentException($"Malformed input found: {input}");

            //< Assign this Step's name & target Step name
            this.Precursor = arr[0];
            this.Dependent = arr[1];
        }
    }

    public class StepSolver
    {
        //< Shout out /u/andrewsredditstuff - this is madness

        public List<Instruction> Instructions { get; } = null;

        private Dictionary<string, List<string>> Steps { get; set; } = null;
        private Dictionary<string, int> InProgress { get; set; } = null;

        private SortedSet<string> Available { get; set; } = null;
        private Queue<int> AvailableWorkers { get; set; } = null;

        public StringBuilder Ordered { get; set; } = null;

        public int TimeTaken { get; set; } = 0;
        public int SecondsPerStep { get; set; } = 0;
        public int NumWorkers { get; set; } = 0;

        public StepSolver(IEnumerable<string> input)
        {
            this.Instructions = input.Select(x => new Instruction(x)).ToList();
        }

        public void Solve(int part, bool isTest)
        {
            //< Initialize the maps
            Inititalize(part, isTest);

            do
            {
                //< Iterate the 'Time' counter
                TimeTaken++;

                while (Available.Count > 0 && AvailableWorkers.Count > 0)
                {
                    //< Pop the 'next' step to be done (first in the SortedSet)
                    var next = Available.First();
                    //< Append the Step to the 'Ordered' string
                    Ordered.Append(next);
                    //< Remove from the 'Available' Steps
                    Available.Remove(next);
                    //< Wut
                    InProgress.Add(next, char.Parse(next) - 64 + SecondsPerStep - 1);
                    //< Moar wut
                    AvailableWorkers.Dequeue();
                }

                //< Make a copy of the current 'InProgress' map
                var progCopy = new Dictionary<string, int>(InProgress);
                foreach (var kvp in progCopy)
                {
                    var curr = kvp.Key;
                    if (kvp.Value == 0)
                    {
                        InProgress.Remove(curr);
                        AvailableWorkers.Enqueue(0);
                        var stepCopy = new Dictionary<string, List<string>>(Steps);
                        foreach (var stepKvp in stepCopy)
                        {
                            //< Get the current list of Precursors for this Step
                            var list = stepKvp.Value;
                            //< If the current item is in the list -> action
                            if (list.Contains(curr))
                            {
                                //< Remove Precursor from list
                                list.Remove(curr);
                                //< Replace the list in the 'Step' map
                                Steps[stepKvp.Key] = list;
                                //< If no Precursor remain, Step is now available
                                if (list.Count == 0)
                                {
                                    Available.Add(stepKvp.Key);
                                }
                            }
                        }
                    }
                    else
                    {
                        InProgress[curr] = kvp.Value - 1;
                    }
                }


            }
            while (Available.Count > 0 || InProgress.Count > 0);
        }

        void Inititalize(int part, bool isTest)
        {
            this.Steps = new Dictionary<string, List<string>>();
            this.InProgress = new Dictionary<string, int>();
            this.Available = new SortedSet<string>();
            this.Ordered = new StringBuilder();

            this.TimeTaken = 0;
            this.SecondsPerStep = isTest ? 0 : 60;
            this.NumWorkers = part == 1 ? 1 : (isTest ? 2 : 5);

            this.AvailableWorkers = new Queue<int>(Enumerable.Range(0, NumWorkers));

            foreach (var ins in Instructions)
            {
                //< Check if we've encountered this Dependent step before
                if (Steps.ContainsKey(ins.Dependent))
                {
                    //< Add Precursor to map for this Dependent
                    Steps[ins.Dependent].Add(ins.Precursor);
                    //< Remove the Dependent from the available Steps
                    Available.Remove(ins.Dependent);
                }
                else
                {
                    //< Not seen before -> add to Step map with Precursor
                    Steps.Add(ins.Dependent, new List<string>() { ins.Precursor });
                }

                //< Check if we've seen this Precursor before
                if (!Steps.ContainsKey(ins.Precursor))
                {
                    //< Add to Step map (with no Precursor)
                    Steps.Add(ins.Precursor, new List<string>());
                    //< Add to available Steps to be checked
                    Available.Add(ins.Precursor);
                }
            }
        }
    }
}
