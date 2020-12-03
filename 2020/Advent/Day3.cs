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
            using var sr = new StreamReader("Day3.txt");
            var input = (await sr.ReadToEndAsync()).Split(Environment.NewLine);

            int height = input.Length;
            var lines = new List<string>();

            foreach (var line in input)
            {
                var sb = new StringBuilder();
                sb.Append(line);

                while (sb.Length < height * 3)
                    sb.Append(line);

                lines.Add(sb.ToString());
            }

            int numTrees = 0;
            int x = 0;
            foreach (var l in lines)
            {
                if (l[x] == '#')
                    numTrees++;

                x += 3;
            }

            Console.WriteLine(numTrees);
        }
    }
}
