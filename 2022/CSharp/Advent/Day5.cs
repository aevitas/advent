using System.Collections;

namespace Advent;

public static class Day5
{
    private static readonly string[] Input = File.ReadAllLines("Day5.txt");

    /**
            [D]    
        [N] [C]    
        [Z] [M] [P]
        1   2   3 
        
     */

    public static void PartOne()
    {
        var stacks = new Dictionary<int, Stack<string>>();

        for (int i = 1; i < 10; i++)
            stacks[i] = new Stack<string>();

        var lines = Input.ToArray();
        for (int l = lines.Length - 1; l >= 0; l--)
        {
            var line = lines[l];

            if (line.Trim().StartsWith('1'))
                continue;

            if (line.Trim().StartsWith("move"))
                continue;

            int spaces = 0;
            for (int i = 0; i < line.Length; i += 4)
            {
                var col = (i - spaces) / 3 + 1;
                var box = line[i..(i + 3)].Replace("[", "").Replace("]", "").Trim();

                spaces++;

                if (string.IsNullOrWhiteSpace(box))
                    continue;

                stacks[col].Push(box);
            }
        }

        foreach (var l in lines)
        {
            if (!l.StartsWith("move"))
                continue;

            var instr = l.Split(" ");

            var num = int.Parse(instr[1]);
            var from = int.Parse(instr[3]);
            var to = int.Parse(instr[5]);

            for (int i = 0; i < num; i++)
            {
                if (stacks[from].TryPop(out var e))
                    stacks[to].Push(e);
            }
        }

        foreach (var s in stacks)
        {
            if (s.Value.TryPop(out var c))
                Console.Write(c);
        }

        Console.WriteLine();
    }

    public static void PartTwo()
    {
        var stacks = new Dictionary<int, Stack<string>>();

        for (int i = 1; i < 10; i++)
            stacks[i] = new Stack<string>();

        var lines = Input.ToArray();
        for (int l = lines.Length - 1; l >= 0; l--)
        {
            var line = lines[l];

            if (line.Trim().StartsWith('1'))
                continue;

            if (line.Trim().StartsWith("move"))
                continue;

            int spaces = 0;
            for (int i = 0; i < line.Length; i += 4)
            {
                var col = (i - spaces) / 3 + 1;
                var box = line[i..(i + 3)].Replace("[", "").Replace("]", "").Trim();

                spaces++;

                if (string.IsNullOrWhiteSpace(box))
                    continue;

                stacks[col].Push(box);
            }
        }

        foreach (var l in lines)
        {
            if (!l.StartsWith("move"))
                continue;

            var instr = l.Split(" ");

            var num = int.Parse(instr[1]);
            var from = int.Parse(instr[3]);
            var to = int.Parse(instr[5]);

            if (num == 1)
            {
                if (stacks[from].TryPop(out var e))
                {
                    stacks[to].Push(e);
                }

                continue;
            }

            var q = new Stack<string>();
            for (int i = 0; i < num; i++)
            {
                if (stacks[from].TryPop(out var e))
                    q.Push(e);
            }

            while (q.TryPop(out var e))
                stacks[to].Push(e);
        }

        foreach (var s in stacks)
        {
            if (s.Value.TryPop(out var c))
                Console.Write(c);
        }

        Console.WriteLine();
    }
}