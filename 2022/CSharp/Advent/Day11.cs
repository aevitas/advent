namespace Advent;

public static class Day11
{
    public static string[] Input = File.ReadAllLines("Day11.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

    private static readonly Action<string> Log = s => { };

    public static void PartOne()
    {
        var monkeys = GetMonkeys();
        var inspections = new Dictionary<int, int>();

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                Log($"Monkey {monkey.Key}");
                var m = monkey.Value;
                while (m.Items.TryDequeue(out var j))
                {
                    Log($"\tMonkey inspects an item with a worry level of {j}.");
                    var worry = m.Operation(j);
                    Log($"\t\tWorry level increased to {worry}");
                    var bored = worry / 3;
                    Log($"\t\tMonkey gets bored with item. Worry level is divided by 3 to {bored}.");

                    if (bored % m.Divisor == 0)
                    {
                        Log($"\t\tCurrent worry level is divisible by {m.Divisor}.");
                        monkeys[m.SuccessMonkey].Items.Enqueue(bored);
                        Log($"\t\tItem with worry level {bored} is thrown to monkey {m.SuccessMonkey}.");
                    }
                    else
                    {
                        Log($"\t\tCurrent worry level is not divisible by {m.Divisor}.");
                        monkeys[m.FailMonkey].Items.Enqueue(bored);
                        Log($"\t\tItem with worry level {bored} is thrown to monkey {m.FailMonkey}.");
                    }

                    if (!inspections.ContainsKey(monkey.Key))
                        inspections[monkey.Key] = 0;

                    inspections[monkey.Key]++;
                }
            }
        }

        foreach (var m in monkeys)
        {
            Log($"Monkey {m.Key}: ");
            foreach (var i in m.Value.Items)
                Log($"{i} ");

            Log("");
        }

        var r = inspections.OrderByDescending(i => i.Value).Take(2).ToArray();

        Console.WriteLine(r[0].Value * r[1].Value);
    }

    public static void PartTwo()
    {

    }

    private static Dictionary<int, Monkey> GetMonkeys()
    {
        var monkeys = new Dictionary<int, Monkey>();

        for (int i = 0; i < Input.Length; i += 6)
        {
            var m = Input[i].Trim().Split(' ')[1].Trim(':');
            var items = Input[i + 1].Trim().Split(':')[1].Split(',').Select(o => int.Parse(o.Trim())).ToArray();
            var operand = Input[i + 2].Trim().Split(' ')[5];
            Func<int, int> operation = _ => throw new ArgumentException();
            var op = Input[i + 2].Trim();

            if (op.Contains('*'))
                operation = j =>
                {
                    if (operand == "old")
                        return j * j;

                    return j * int.Parse(operand);
                };
            if (op.Contains('+'))
                operation = j =>
                {
                    if (operand == "old")
                        return j + j;


                    return j + int.Parse(operand);
                };
            if (op.Contains('-'))
                operation = j =>
                {
                    if (operand == "old")
                        return j - j;

                    return j - int.Parse(operand);
                };
            if (op.Contains('/'))
                operation = j =>
                {
                    if (operand == "old")
                        return j / j;

                    return j / int.Parse(operand);
                };

            var divisor = Input[i + 3].Trim().Split(' ')[3];

            var t = Input[i + 4].Trim().Split(' ')[5];
            var f = Input[i + 5].Trim().Split(' ')[5];

            var monkey = int.Parse(m);

            monkeys[monkey] = new Monkey
            {
                Divisor = int.Parse(divisor),
                FailMonkey = int.Parse(f),
                SuccessMonkey = int.Parse(t),
                Items = new Queue<int>(items),
                Number = monkey,
                Operation = operation
            };
        }

        return monkeys;
    }
}

public class Monkey
{
    public required int Number { get; init; }

    public required Queue<int> Items { get; init; }

    public required Func<int, int> Operation { get; init; }

    public required int Divisor { get; init; }

    public int SuccessMonkey { get; init; }

    public int FailMonkey { get; init; }
}
