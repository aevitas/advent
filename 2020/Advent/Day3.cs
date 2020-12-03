using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    internal static class Day3
    {
        public static async Task PartOneAsync()
        {
            Console.WriteLine(await TraverseSlope(3, 1));
        }

        public static async Task PartTwoAsync()
        {
            var one = await TraverseSlope(1, 1);
            var two = await TraverseSlope(3, 1);
            var three = await TraverseSlope(5, 1);
            var four = await TraverseSlope(7, 1);
            var five = await TraverseSlope(1, 2);

            Console.Write(one * two * three * four * five);
        }

        private static async Task<long> TraverseSlope(int stepsRight, int stepsDown)
        {
            using var sr = new StreamReader("Day3.txt");
            var input = (await sr.ReadToEndAsync()).Split(Environment.NewLine);

            int height = input.Length;
            var lines = new List<string>();

            foreach (var line in input)
            {
                var sb = new StringBuilder();
                sb.Append(line);

                while (sb.Length < height * stepsRight)
                    sb.Append(line);

                lines.Add(sb.ToString());
            }

            int numTrees = 0;
            int x = 0;
            for (int y = 0; y < lines.Count; y += stepsDown)
            {
                if (lines[y][x] == '#')
                    numTrees++;

                x += stepsRight;
            }

            return numTrees;
        }
    }
}
