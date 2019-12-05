using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day2
    {
        public static async Task Solve()
        {
            var mem = (await File.ReadAllTextAsync("Day2.txt")).Split(",").Select(int.Parse).ToArray();

            mem[1] = 12;
            mem[2] = 2;

            for (int i = 0; i < mem.Length;)
            {
                int opCode = mem[i];

                if (opCode == 99)
                    break;

                if (opCode != 1 && opCode != 2)
                    throw new NotSupportedException($"Unknown OpCode encountered: {opCode}");

                var lhv = mem[mem[i + 1]]; // left operand
                var rhv = mem[mem[i + 2]]; // right operand

                mem[mem[i + 3]] = opCode == 1 ? lhv + rhv : lhv * rhv; // ret

                i += 4;
            }

            Console.WriteLine(mem[0]);
        }
    }
}
