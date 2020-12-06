using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    internal static class Day6
    {
        public static async Task PartOneAsync()
        {
            using var sr = new StreamReader("Day6.txt");
            var groups = (await sr.ReadToEndAsync()).Split($"{Environment.NewLine}{Environment.NewLine}");

            long sum = 0;
            foreach (var answers in groups)
            {
                sum += answers.Replace(Environment.NewLine, string.Empty).Distinct().Count();
            }

            Console.WriteLine(sum);
        }
    }
}
