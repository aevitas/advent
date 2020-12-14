using System;
using System.IO;
using System.Linq;

namespace Advent
{
    internal static class Day13
    {
        public static void PartOne()
        {
            using var sr = new StreamReader("Day13.txt");
            var input = sr.ReadToEnd().Split(Environment.NewLine);

            var time = int.Parse(input[0]);
            var lines = input[1].Split(',').Where(s => s != "x").Select(int.Parse).ToArray();

            var dict = lines.ToDictionary(l => l, l => l - time % l);
            var earliest = dict.OrderBy(kvp => kvp.Value).FirstOrDefault();

            Console.WriteLine(earliest.Key * earliest.Value);
        }
    }
}
