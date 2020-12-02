using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    internal static class Day1
    {
        public static async Task SolveAsync()
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
    }
}
