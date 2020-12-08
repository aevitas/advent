using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Advent
{
    internal static class Day8
    {
        public static void PartOne()
        {
            using var sr = new StreamReader("Day8.txt");
            var instructions = sr.ReadToEnd().Split(Environment.NewLine).Select(s =>
            {
                var instr = s.Split(' ');

                return new Instruction
                {
                    Operator = instr[0],
                    Operand = instr[1]
                };
            }).ToArray();

            var acc = Run(instructions);

            Console.WriteLine(acc);

            static int Run(Instruction[] program)
            {
                var accumulator = 0;
                var ptr = 0;
                var executed = new bool[program.Length];

                while (true)
                {
                    if (executed[ptr])
                        break;

                    var instr = program[ptr];
                    bool addition = instr.Operand.StartsWith('+');
                    var val = int.Parse(instr.Operand[1..]);

                    var oldPtr = ptr;
                    switch (instr.Operator)
                    {
                        case "nop":
                            ptr++;
                            break;
                        case "acc":
                            accumulator = addition ? accumulator + val : accumulator - val;
                            ptr++;
                            break;
                        case "jmp":
                            ptr = addition ? ptr + val : ptr - val;
                            break;
                    }

                    executed[oldPtr] = true;
                }

                return accumulator;
            }
        }

        [DebuggerDisplay("{Operator} {Operand}")]
        private class Instruction
        {
            public string Operator { get; set; }

            public string Operand { get; set; }
        }
    }
}
