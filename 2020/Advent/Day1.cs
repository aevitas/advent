using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Advent
{
    internal static class Day1
    {
        public static async Task PartOneAsync()
        {
            using var sr = new StreamReader("Day1.txt");
            var input = await sr.ReadToEndAsync();

            var expenses = input.Split(Environment.NewLine).Select(int.Parse).ToArray();

            for (int i = 0; i < expenses.Length; i++)
            {
                for (int y = 0;  y < expenses.Length; y++)
                {
                    if (y == i)
                        continue;

                    if (expenses[i] + expenses[y] == 2020)
                    {
                        Console.WriteLine(expenses[i] * expenses[y]);

                        return;
                    }
                }
            }
        }

        public static async Task PartTwoAsync()
        {
            using var sr = new StreamReader("Day1.txt");
            var input = await sr.ReadToEndAsync();

            var expenses = input.Split(Environment.NewLine).Select(int.Parse).ToArray();

            for (int i = 0; i < expenses.Length; i++)
            {
                for (int y = 0; y < expenses.Length; y++)
                {
                    if (y == i)
                        continue;

                    for (int j = 0; j < expenses.Length; j++)
                    {
                        if (j == y)
                            continue;

                        if (expenses[i] + expenses[y] + expenses[j] == 2020)
                        {
                            Console.WriteLine(expenses[i] * expenses[y] * expenses[j]);

                            return;
                        }
                    }
                }
            }
        }
    }
}
