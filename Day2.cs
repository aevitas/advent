using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Day2
    {
        public static async Task Solve()
        {
            var input = (await File.ReadAllTextAsync("Day2.txt")).Split(",").Select(int.Parse).ToArray();
            var mem = input.ToArray();

            mem[1] = 12;
            mem[2] = 2;

            Console.WriteLine(Compute(mem));

            for (int i = 0; i < 99; i++)
            {
                for (int y = 0; y < 99; y++)
                {

                    mem = input.ToArray();

                    mem[1] = i;
                    mem[2] = y;

                    if (Compute(mem) == 19690720)
                    {
                        Console.WriteLine(100 * i + y);

                        break;
                    }
                }
            }

            int Compute(int[] memory)
            {
                for (int i = 0; i < memory.Length;)
                {
                    int opCode = memory[i];

                    if (opCode == 99)
                        break;

                    if (opCode != 1 && opCode != 2)
                        Debug.WriteLine($"Unknown OpCode: {opCode}");

                    var lhv = memory[memory[i + 1]]; // left operand
                    var rhv = memory[memory[i + 2]]; // right operand

                    memory[memory[i + 3]] = opCode == 1 ? lhv + rhv : lhv * rhv; // ret

                    i += 4;
                }

                return memory[0];
            }
        }
    }
}
