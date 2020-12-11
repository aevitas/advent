using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    internal static class Day10
    {
        public static void PartOne()
        {
            using var sr = new StreamReader("Day10.txt");
            var adapters = sr.ReadToEnd().Split(Environment.NewLine).Select(int.Parse).OrderBy(i => i).ToArray();

            var diffs = new List<int>();
            for (var i = 0; i < adapters.Length; i++)
            {
                if (i == 0)
                    continue;

                diffs.Add(adapters[i] - adapters[i - 1]);
            }

            var grouped = diffs.GroupBy(i => i).ToList();
            Console.WriteLine(
                $"{(grouped.First(g => g.Key == 1).Count() + 1) * (grouped.First(g => g.Key == 3).Count() + 1)}");
        }

        public static void PartTwo()
        {
            using var sr = new StreamReader("Day10.txt");
            var adapters = sr.ReadToEnd().Split(Environment.NewLine).Select(long.Parse).OrderBy(i => i).ToList();

            adapters = adapters.Prepend(0).ToList();
            adapters.Add(adapters.Max() + 3);

            adapters.Reverse();

            var tree = new Dictionary<long, long>();

            foreach (var adapter in adapters)
            {
                var nexts = adapters.Where(j => j > adapter && j <= adapter + 3);
                tree[adapter] = nexts.Select(n => tree[n]).Sum();

                if (tree[adapter] == 0)
                    tree[adapter] = 1;
            }

            Console.WriteLine(tree[0]);
        }
    }
}