using System.Numerics;

namespace Advent;

public static class Day9
{
    private static readonly string[] Input = File.ReadAllLines("Day9.txt");

    public static void PartOne()
    {
        var head = new Vector2(10, 10);
        var tail = new Vector2(10, 10);
        var visited = new Dictionary<Vector2, bool>
        {
            {
                tail, true
            }
        };

        foreach (var line in Input)
        {
            var split = line.Split(' ');
            var dir = split[0];
            var num = int.Parse(split[1]);

            for (var i = 0; i < num; i++) Move(dir);
        }

        void Move(string mov)
        {
            head = mov switch
            {
                "L" => head with { X = head.X - 1 },
                "R" => head with { X = head.X + 1 },
                "U" => head with { Y = head.Y - 1 },
                "D" => head with { Y = head.Y + 1 },
                _ => head
            };

            var dX = Math.Abs(head.X - tail.X);
            var dY = Math.Abs(head.Y - tail.Y);

            if (dX < 2 && dY < 2)
                return;

            tail = new Vector2(tail.X + Math.Sign(head.X - tail.X), tail.Y + Math.Sign(head.Y - tail.Y));
            visited[tail] = true;
        }

        Console.WriteLine(visited.Count);

        for (var y = 0; y < 25; y++)
        {
            for (var x = 0; x < 25; x++)
            {
                var pos = new Vector2(x, y);

                if (head == pos)
                {
                    Console.Write("H");
                    continue;
                }

                if (tail == pos)
                {
                    Console.Write("T");
                    continue;
                }

                Console.Write(visited.ContainsKey(pos) ? "#" : "x");
            }

            Console.Write(Environment.NewLine);
        }
    }

    public static void PartTwo()
    {
        var rope = new Vector2[10];
        ref var tail = ref rope[^1];

        var visited = new Dictionary<Vector2, bool>
        {
            {
                tail, true
            }
        };

        foreach (var line in Input)
        {
            var split = line.Split(' ');
            var dir = split[0];
            var num = int.Parse(split[1]);

            for (var i = 0; i < num; i++) Move(dir, ref rope);
        }

        void Move(string mov, ref Vector2[] r)
        {
            ref var h = ref r[0];
            h = mov switch
            {
                "L" => h with { X = h.X - 1 },
                "R" => h with { X = h.X + 1 },
                "U" => h with { Y = h.Y - 1 },
                "D" => h with { Y = h.Y + 1 },
                _ => h
            };

            for (var i = 1; i < r.Length; i++)
            {
                ref var p = ref rope[i - 1];
                ref var c = ref rope[i];

                var dX = Math.Abs(p.X - c.X);
                var dY = Math.Abs(p.Y - c.Y);

                if (dX < 2 && dY < 2)
                    return;

                c = new Vector2(c.X + Math.Sign(p.X - c.X), c.Y + Math.Sign(p.Y - c.Y));
            }

            ref var t = ref r[^1];
            visited[t] = true;
        }

        Console.WriteLine(visited.Count);
    }
}