using System;
using System.Collections.Generic;
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

        public static async Task PartTwoAsync()
        {
            using var sr = new StreamReader("Day6.txt");
            var groups = (await sr.ReadToEndAsync()).Split($"{Environment.NewLine}{Environment.NewLine}");

            int count = 0;
            foreach (var group in groups)
            {
                var affirmatives = new List<char>();

                var groupAnswers = group.Split(Environment.NewLine);
                foreach (var individualAnswer in groupAnswers)
                {
                    foreach (var c in individualAnswer)
                    {
                        if (affirmatives.Contains(c))
                            continue;

                        if (groupAnswers.All(s => s.Contains(c)))
                        {
                            affirmatives.Add(c);
                            count++;
                        }
                    }
                }
            }

            Console.WriteLine(count);
        }
    }
}
