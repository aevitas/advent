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
    }
}