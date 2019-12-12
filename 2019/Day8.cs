using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day8
    {
        public static async Task Solve()
        {
            const int width = 25;
            const int height = 6;
            var input = File.ReadAllText("Day8.txt").Select(c => (int) char.GetNumericValue(c)).ToArray();
            var layerCount = input.Length / width / height;
            var layers = Enumerable.Range(0, layerCount).Select(_ => new int[width][]).ToArray();
            int i = 0;

            for (int l = 0; l < layerCount; l++)
            {
                layers[l] = new int[height][];
                for (int y = 0; y < height; y++)
                {
                    layers[l][y] = new int[width];
                    for (int x = 0; x < width; x++)
                    {
                        layers[l][y][x] = input[i++];
                    }
                }
            }

            var leastZeroes = int.MaxValue;
            var layer = layers.First();
            foreach (var l in layers)
            {
                var c = l.Sum(o => o.Count(num => num == 0));
                if (c < leastZeroes)
                {
                    layer = l;
                    leastZeroes = c;
                }
            }

            var sum = layer.Sum(l => l.Count(num => num == 1)) * layer.Sum(l => l.Count(num => num == 2));

            Console.WriteLine(sum);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int v = Enumerable.Range(0, layerCount).Select(num => layers[num][y][x])
                        .FirstOrDefault(num => num != 2);

                    Console.Write(v == 0 ? " " : "X");
                }

                Console.Write(Environment.NewLine);
            }
        }
    }
}
