using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace AdventOfCode
{
    public static class Day5
    {
        public static void Solve()
        {
            var input = File.ReadAllText("Day5.txt").Split(",").Select(int.Parse).ToArray();

            Run(input);

            void Run(int[] mem)
            {

                for (int i = 0; i < mem.Length; i++)
                {
                    int GetParameter(int index, bool deref) => deref ? mem[mem[i + 1 + index]] : mem[i + 1 + index];

                    bool ShouldDeref(int index)
                    {
                        var divisor = 1;

                        switch (index)
                        {
                            case 0:
                                divisor *= 10;
                                break;
                            case 1:
                                divisor *= 100;
                                break;
                            case 2:
                                divisor *= 1000;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        return mem[i] / 100 / divisor % 10 == 0;
                    }

                    var opcode = mem[i] % 100;

                    switch (opcode)
                    {
                        case 1:
                            var result = GetParameter(0, ShouldDeref(0)) + GetParameter(1, ShouldDeref(1));

                            mem[i + 2] = result;
                            i += 4;
                            break;
                    }
                }
            }
        }
    }
}
