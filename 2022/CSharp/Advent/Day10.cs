namespace Advent;

public static class Day10
{
    private static readonly string[] Input = File.ReadAllLines("Day10.txt");
    
    public static void PartOne()
    {
        int numTicks = 0;
        int x = 1;
        int count = 0;

        for (int i = 0; i < Input.Length; i++)
        {
            var split = Input[i].Split(' ');
            var op = split[0];
            var arg = split.Length > 1 ? split[1] : string.Empty;

            int cycles = op == "noop" ? 1 : 2;
            for (int y = 0; y < cycles; y++)
            {
                numTicks++;

                if (numTicks % 40 == 20)
                    count += x * numTicks;
            }

            if (op == "addx")
                x += int.Parse(arg);
        }
        
        Console.WriteLine(count);
    }

    public static void PartOneAlt()
    {
        int numTicks = 0;
        List<int> reads = new();
        Dictionary<int, int> ops = new();

        for (int i = 0; i < Input.Length; i++)
        {
            var split = Input[i].Split(' ');
            var op = split[0];
            var arg = split.Length > 1 ? split[1] : string.Empty;

            if (op == "noop")
            {
                Run(1);
                continue;
            }

            // Effective end of 2 cycles after this, or start of third cycle
            ops[numTicks + 2] = int.Parse(arg);

            Run(2);
        }

        Run(10);

        var t = new[] { 20, 60, 100, 140, 180, 220 };

        foreach (var c in t)
        {
            var x = ops.Where(k => k.Key <= c - 1).Select(k => k.Value).Sum() + 1;

            reads.Add(x * c);
        }

        Console.WriteLine(reads.Sum());

        void Run(int ticks)
        {
            for (int i = 0; i < ticks; i++)
            {
                numTicks++;
            }
        }
    }

    public static void PartTwo()
    {
        int numTicks = 0;
        int x = 1;

        for (int i = 0; i < Input.Length; i++)
        {
            var split = Input[i].Split(' ');
            var op = split[0];
            var arg = split.Length > 1 ? split[1] : string.Empty;

            int cycles = op == "noop" ? 1 : 2;
            for (int y = 0; y < cycles; y++)
            {
                var p = numTicks % 40;
                Console.Write(p == x || p == x - 1 || p == x + 1 ? '\u2588' : " ");
                if (p % 40 == 39)
                    Console.WriteLine();

                numTicks++;
            }

            if (op == "addx")
                x += int.Parse(arg);
        }
    }
}