using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Day5
    {
        public static void Solve()
        {
            var input = File.ReadAllText("Day5.txt");
            var memory = input.Split(",").Select(int.Parse).ToArray();

            Run(memory);

            void Run(int[] mem)
            {
                int i = 0;
                while (true)
                {
                    // Hail JBN for this func, making life a lot easier.
                    ref int GetParameter(int index)
                    {
                        ref int start = ref mem[i + 1 + index];

                        int div = 1;
                        for (int y = 0; y < index; y++)
                            div *= 10;

                        int mode = (mem[i] / 100) / div % 10;

                        if (mode == 0)
                            return ref mem[start];

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
                    }
                }
            }
        }
    }
}