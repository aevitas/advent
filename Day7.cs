using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AdventOfCode
{
    public static class Day7
    {
        public static async Task Solve()
        {
            var code = File.ReadAllText("Day7.txt").Split(",").Select(int.Parse).ToImmutableArray();

            int max = -1;
            var permutations = new[] { 0, 1, 2, 3, 4 }.Permutations();
            var outputBlock = new BufferBlock<int>();
            foreach (var p in permutations)
            {
                var q = p.ToArray();
                var inputBlock = new BufferBlock<int>();

                var s = 0;
                for (int i = 0; i < q.Length; i++)
                {
                    inputBlock.Post(q[i]);
                    inputBlock.Post(s);
                    Run(code.ToArray(), inputBlock, outputBlock);

                    s = outputBlock.Receive();
                }
                
                max = Math.Max(max, s);
            }

            Console.WriteLine(max);

            void Run(int[] mem, BufferBlock<int> blockIn, BufferBlock<int> blockOut)
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
                            var input = blockIn.Receive();
                            GetParameter(0) = input;
                            i += 2;
                            break;
                        case 4:
                            var val = GetParameter(0);
                            blockOut.Post(val);
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
                        case 99:
                            return;
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<int>> Permutations(this IEnumerable<int> values)
        {
            if (values.Count() == 1)
                return new[] { values };
            return values.SelectMany(v => Permutations(values.Where(x => x != v)), (v, p) => p.Prepend(v));
        }
    }
}
