using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    internal static class Day9
    {
        public static void PartOne()
        {
            using var sr = new StreamReader("Day9.txt");
            long[] xmas = sr.ReadToEnd().Split(Environment.NewLine).Select(long.Parse).ToArray();
            int preambleSize = 25;
            long[] preamble = xmas[..preambleSize];

            List<long> found = new List<long>();
            for (int n = preambleSize; n < xmas.Length; n++)
            {
                var eligible = xmas[(n - preambleSize)..];
                for (int i = 0; i < eligible.Length; i++)
                {
                    for (int y = 0; y < eligible.Length; y++)
                    {
                        var sum = eligible[i] + eligible[y] == xmas[n];

                        if (sum)
                            found.Add(xmas[n]);
                    }
                }
            }

            var remaining = xmas.Except(preamble).Except(found);

            foreach (var num in remaining)
                Console.WriteLine(num);
        }
    }
}
