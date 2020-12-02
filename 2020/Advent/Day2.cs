using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    internal static class Day2
    {
        public static async Task PartOneAsync()
        {
            using var sr = new StreamReader("Day2.txt");
            var input = await sr.ReadToEndAsync();

            var passwords = input.Split(Environment.NewLine);
            int numValid = 0;

            foreach (var p in passwords)
            {
                var s = p.Split(' ');

                var min = int.Parse(s[0].Split('-')[0]);
                var max = int.Parse(s[0].Split('-')[1]);

                var matchChar = s[1][0];

                var password = s[2];

                var charMatches = password.Count(c => c == matchChar);

                if (charMatches >= min && charMatches <= max)
                    numValid++;
            }

            Console.WriteLine(numValid);
        }
    }
}
