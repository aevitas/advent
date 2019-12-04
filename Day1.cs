using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day1
    {
        public static async Task Solve()
        {
            using var sr = new StreamReader("Day1.txt");
            var input = await sr.ReadToEndAsync();

            var masses = input.Split(Environment.NewLine).Select(int.Parse);
            var totalMass = masses.Sum(m => (int) Math.Floor((double) m / 3) - 2);

            Console.WriteLine(totalMass);
        }
    }
}
