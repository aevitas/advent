using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Day9
    {
        public static void Solve()
        {
            var code = File.ReadAllText("Day9.txt").Split(",").Select(long.Parse).ToArray();
            var extendedLength = code.Length + 1024 * 1024;
            var memory = new long[extendedLength];

            for (int i = 0; i < code.Length; i++)
            {
                ref long mem = ref memory[i];

                mem = code[i];
            }

            Run(memory.ToArray());

            void Run(long[] mem)
            {
                long sp = 0;
                long i = 0;
                while (true)
                {
                    ref long GetParameter(long index)
                    {
                        ref long start = ref mem[i + 1 + index];

                        int div = 1;
                        for (int y = 0; y < index; y++)
                            div *= 10;

                        long mode = (mem[i] / 100) / div % 10;

                        // deref
                        if (mode == 0)
                            return ref mem[start];

                        // relative
                        if (mode == 2)
                            return ref mem[sp + start];

                        // absolute
                        return ref start;
                    }

                    var opcode = mem[i] % 100;

                    switch (opcode)
                    {
                        case 1:
                            GetParameter(2) = GetParameter(0) + GetParameter(1);
                            i += 4;
                            break;
                        case 2:
                            GetParameter(2) = GetParameter(0) * GetParameter(1);
                            i += 4;
                            break;
                        case 3:
                            Console.WriteLine("Diagnostic input:");
                            var input = int.Parse(Console.ReadLine());
                            GetParameter(0) = input;
                            i += 2;
                            break;
                        case 4:
                            Console.WriteLine(GetParameter(0));
                            i += 2;
                            break;
                        case 5:
                            if (GetParameter(0) != 0)
                                i = GetParameter(1);
                            else
                            {
                                i += 3;
                            }
                            break;
                        case 6:
                            if (GetParameter(0) == 0)
                                i = GetParameter(1);
                            else
                            {
                                i += 3;
                            }
                            break;
                        case 7:
                            GetParameter(2) = GetParameter(0) < GetParameter(1) ? 1 : 0;
                            i += 4;
                            break;
                        case 8:
                            GetParameter(2) = GetParameter(0) == GetParameter(1) ? 1 : 0;
                            i += 4;
                            break;
                        case 9:
                            sp += GetParameter(0);
                            i += 2;
                            break;
                        case 99:
                            return;
                        default:
                            Trace.Fail("Oh shit " + mem[i]);
                            break;
                    }
                }
            }
        }
    }
}
