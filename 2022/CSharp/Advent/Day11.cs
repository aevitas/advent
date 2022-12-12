using System.Numerics;

namespace Advent;

public static class Day11
{
    public static string[] Input = File.ReadAllLines("Day11.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
    
    public static void PartOne()
    {
        var monkeys = GetMonkeys();
        var inspections = new Dictionary<int, int>();

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                var m = monkey.Value;
                while (m.Items.TryDequeue(out var j))
                {
                    var worry = m.Operation(j);
                    var bored = worry / 3;

                    if (bored % m.Divisor == 0)
                    {
                        monkeys[m.SuccessMonkey].Items.Enqueue(bored);
                    }
                    else
                    {
                        monkeys[m.FailMonkey].Items.Enqueue(bored);
                    }

                    if (!inspections.ContainsKey(monkey.Key))
                        inspections[monkey.Key] = 0;

                    inspections[monkey.Key]++;
                }
            }
        }
        
        var r = inspections.OrderByDescending(i => i.Value).Take(2).ToArray();

        Console.WriteLine(r[0].Value * r[1].Value);
    }

    public static void PartTwo()
    {
        var monkeys = GetMonkeys();
        var inspections = new Dictionary<int, int>();

        for (int i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                var m = monkey.Value;
                while (m.Items.TryDequeue(out var j))
                {
                    var worry = m.Operation(j);

                    if (worry % m.Divisor == 0)
                    {
                        monkeys[m.SuccessMonkey].Items.Enqueue(worry);
                    }
                    else
                    {
                        monkeys[m.FailMonkey].Items.Enqueue(worry);
                    }

                    if (!inspections.ContainsKey(monkey.Key))
                        inspections[monkey.Key] = 0;

                    inspections[monkey.Key]++;
                }
            }
        }
        
        var r = inspections.OrderByDescending(i => i.Value).Take(2).ToArray();
        
        Console.WriteLine(new BigInteger(r[0].Value) * new BigInteger(r[1].Value));

    }

    private static Dictionary<int, Monkey> GetMonkeys()
    {
        var monkeys = new Dictionary<int, Monkey>();

        for (int i = 0; i < Input.Length; i += 6)
        {
            var m = Input[i].Trim().Split(' ')[1].Trim(':');
            var items = Input[i + 1].Trim().Split(':')[1].Split(',').Select(o => long.Parse(o.Trim())).ToArray();
            var operand = Input[i + 2].Trim().Split(' ')[5];
            Func<long, long> operation = _ => throw new ArgumentException();
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
                Items = new Queue<long>(items),
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

    public required Queue<long> Items { get; init; }

    public required Func<long, long> Operation { get; init; }

    public required int Divisor { get; init; }

    public int SuccessMonkey { get; init; }

    public int FailMonkey { get; init; }
}
