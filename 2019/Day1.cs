using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day1
    {
        public static async Task Solve()
        {
            using var sr = new StreamReader("Day1.txt");
            var input = await sr.ReadToEndAsync();

            var masses = input.Split(Environment.NewLine).Select(int.Parse).ToList();
            var one = masses.Sum(i => i / 3 - 2); // Can skip Math.Floor because of int

            Console.WriteLine(one);

            var two = masses.Sum(i =>
            {
                var totalFuel = 0;
                int y = i / 3 - 2; // Initial fuel requirement for module;

                while (y > 0) // Keep adding fuel to carry the fuel until we hit negative fuel req
                {
                    totalFuel += y;
                    y = y / 3 - 2; 
                }
                
                return totalFuel;
            });

            Console.WriteLine(two);
        }
    }
}
