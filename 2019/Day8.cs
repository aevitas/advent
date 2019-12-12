using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day8
    {
        public static async Task Solve()
        {
            var input = File.ReadAllText("Day8.txt");
            var layers = new List<int[]>();

            for (int i = 0; i < 6; i++)
                layers.Add(input.Skip(i * 25).Take(25).Select(c => (int) char.GetNumericValue(c)).ToArray());

            int fewestZeroes = int.MaxValue;
            int[] layer = layers.First();

            foreach (var l in layers)
            {
                var count = GetNumberCount(l, 0);

                if (count >= fewestZeroes) 
                    continue;

                layer = l;
                fewestZeroes = count;
            }

            Console.WriteLine(layer.Count(i => i == 1) * layer.Count(i => i == 2));

            int GetNumberCount(int[] arr, int num)
            {
                return arr.Count(i => i == num);
            }
        }
    }
}
