using System.Numerics;

namespace Advent;

public static class Day9
{
    private static readonly string[] Input = File.ReadAllLines("Day9.txt");

    public static void PartOne()
    {
        Vector2 head = new Vector2(10,10);
        Vector2 tail = new Vector2(10, 10);
        Dictionary<Vector2, bool> visited = new Dictionary<Vector2, bool>
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

            for (int i = 0; i < num; i++)
            {
                Move(dir);
            }
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

        for (int y = 0; y < 25; y++)
        {
            for (int x = 0; x < 25; x++)
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

    }
}