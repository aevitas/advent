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
            var adapters = sr.ReadToEnd().Split(Environment.NewLine).Select(int.Parse).OrderBy(i => i).ToArray();

            var clusters = GetContiguousAdapters(adapters);
            long configs = 1;

            foreach (var c in clusters)
            {
                var num = c.Length;
                num -= 1;
                configs *= 1 + num * (num + 1) / 2; //1 + n*(n+1)/2; - Lazy Caterer's Formula
            }

            Console.WriteLine(configs);

            static IEnumerable<int[]> GetContiguousAdapters(int[] adapters)
            {
                var result = new List<int[]>();
                var cur = new HashSet<int>();
                for (int i = 0; i < adapters.Length - 1; i++)
                {
                    if (adapters[i + 1] - adapters[i] == 1)
                    {
                        cur.Add(adapters[i]);
                        cur.Add(adapters[i + 1]);
                        continue;
                    }

                    if (cur.Any())
                    {
                        result.Add(cur.ToArray());
                        cur.Clear();
                    }
                }

                return result;
            }
        }
    }
}