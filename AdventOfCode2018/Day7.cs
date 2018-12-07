using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    public class Day7
    {
        public class Node
        {
            public Node(char name, List<Node> edges)
            {
                Name = name;
                Edges = edges;
            }

            public char Name { get; }
            public List<Node> Edges { get; }
        }

        // Step Y must be finished before step M can begin.
        private static Regex pattern = new Regex(@"Step (?<req>[A-Z]) must be finished before step (?<node>[A-Z]) can begin\.");
        public static IEnumerable<ValueTuple<char, char>> Input => File.ReadAllLines("input7.txt")
            .Select(x => pattern.Match(x).Groups)
            .Select(x => ValueTuple.Create(x["req"].Value.First(), x["node"].Value.First()));

        public static string SolveFirst()
        {
            var adj = Input.GroupBy(x => x.Item1)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Item2).OrderBy(y => y).ToList());
            
            var sorted = new List<char>();
            var set = new HashSet<char>();
            foreach (var n in adj.Where(x => !adj.Any(y => y.Value.Contains(x.Key))))
            {
                set.Add(n.Key);
            }

            while (set.Count > 0)
            {
                var node = set.OrderBy(x => x).First();
                set.Remove(node);

                sorted.Add(node);

                if (adj.ContainsKey(node))
                {
                    var edges = adj[node];
                    while (edges.Count > 0)
                    {
                        var e = edges.First();
                        edges.Remove(e);
                        if (!adj.Any(y => y.Value.Contains(e)))
                        {
                            set.Add(e);
                        }
                    }
                }
            }

            var result = new string(sorted.ToArray());

            return result;
        }
    }
}
