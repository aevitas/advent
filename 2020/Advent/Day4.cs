using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    internal static class Day4
    {
        public static async Task PartOneAsync()
        {
            using var sr = new StreamReader("Day4.txt");
            var passports = (await sr.ReadToEndAsync()).Split($"{Environment.NewLine}{Environment.NewLine}");

            Console.WriteLine(passports.Count(IsValid));

            static bool IsValid(string p)
            {
                return p.Contains("byr:") && p.Contains("iyr:") && p.Contains("eyr:") && p.Contains("hgt:") &&
                       p.Contains("hcl:") && p.Contains("ecl:") && p.Contains("pid:");
            }
        }
    }
}
