using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent._2018.Classes
{
    public class LicenseNode
    {
        public List<int> Metadata { get; private set; } = new List<int>();
        public List<LicenseNode> Children { get; private set; } = new List<LicenseNode>();
        
        public LicenseNode()
        { }

        public int Sum()
        {
            //< Recursive Sum() of this Node (and all child Node's) Metadata
            return Metadata.Sum() + Children.Sum(x => x.Sum());
        }

        public int Value()
        {
            //< If no children -> return the Metadata's Sum()
            if (!Children.Any())
            {
                return Metadata.Sum();
            }

            int value = 0;
            //< Have Child nodes -> must treat metadata as indices of child nodes
            foreach (var meta in Metadata)
            {
                //< If the index is zero -> skip
                if (meta == 0)
                    continue;

                //< Only consider valid child references (skip all others)
                if (meta <= Children.Count)
                {
                    value += Children[meta - 1].Value();
                }
            }

            return value;
        }
    }

    public class LicenseTree
    {
        public List<int> Inputs { get; } = null;

        public LicenseNode Root { get; } = null;

        public LicenseTree(string input)
        {
            this.Inputs = input.Split(' ').Select(int.Parse).ToList();

            int idx = 0;
            this.Root = ParseNode(Inputs, ref idx);
        }

        public static LicenseNode ParseNode(List<int> data, ref int idx)
        {
            var node = new LicenseNode();

            //< Parse the header and iterate the index (idx) forward
            var numChildren = data[idx++];
            var numMetadata = data[idx++];

            //< Parse each available child node (recursively, rip)
            for (int j = 0; j < numChildren; j++)
            {
                node.Children.Add(ParseNode(data, ref idx));
            }

            //< Parse each piece of MetaData (iterate index after each one)
            for (int j = 0; j < numMetadata; j++)
            {
                node.Metadata.Add(data[idx++]);
            }

            return node;
        }
    }
}
