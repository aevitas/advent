using System.Numerics;

namespace Advent;

public static class Day8
{
    private static readonly string[] Input = File.ReadAllLines("Day8.txt");

    public static void PartOne()
    {
        var grid = CreateHeightmap(out var bounds);
        var visible = new Dictionary<(int, int), bool>();

        for (int y = 0; y < bounds.Y; y++)
        {
            for (int x = 0; x < bounds.X; x++)
            {
                var height = grid[(x, y)];
                var v = true;
                for (int i = 0; v && i < bounds.X; i++)
                {
                    var cur = grid[(i, y)];
                    if (cur >= height)
                        visible[(i, y)] = v = false;
                }

                v = true;
                for (int i = 0; v && i < bounds.Y; i++)
                {
                    var cur = grid[(x, i)];
                    if (cur >= height)
                        visible[(x, i)] = v = false;
                }

                v = true;
                for (int i = (int)bounds.X - 1; v && i > 0; i--)
                {
                    var cur = grid[(i, y)];
                    if (cur >= height)
                        visible[(i, y)] = v = false;
                }

                v = true;
                for (int i = (int)bounds.Y - 1; v && i > 0; i--)
                {
                    var cur = grid[(x, i)];
                    if (cur >= height)
                        visible[(x, i)] = v = false;
                }
            }
        }

        Console.WriteLine(visible.Count);
    }

    public static void PartTwo()
    {
        var grid = CreateHeightmap(out var bounds);
        int max = 0;

        for (int y = 0; y < bounds.Y; y++)
        {
            for (int x = 0; x < bounds.X; x++)
            {
                var height = grid[(x, y)];
                var score = 1;

                var c = 0;
                for (int i = x + 1; i < bounds.X; i++)
                {
                    c++;
                    var cur = grid[(i, y)];
                    if (cur >= height)
                        break;
                }

                score *= c;
                c = 0;

                for (int i = y + 1; i < bounds.Y; i++)
                {
                    c++;
                    var cur = grid[(x, i)];
                    if (cur >= height)
                        break;
                }

                score *= c;
                c = 0;

                for (int i = x - 1; i >= 0; i--)
                {
                    c++;
                    var cur = grid[(i, y)];
                    if (cur >= height)
                        break;
                }

                score *= c;
                c = 0;

                for (int i = y - 1; i >= 0; i--)
                {
                    c++;
                    var cur = grid[(x, i)];
                    if (cur >= height)
                        break;
                }

                score *= c;

                if (score > max)
                    max = score;
            }
        }

        Console.WriteLine(max);
    }

    private static Dictionary<(int, int), int> CreateHeightmap(out Vector2 bounds)
    {
        var grid = new Dictionary<(int, int), int>();
        for (int y = 0; y < Input.Length; y++)
        {
            var line = Input[y];
            for (int x = 0; x < line.Length; x++)
            {
                grid[(x, y)] = int.Parse(line[x].ToString());
            }
        }

        bounds = new Vector2(Input[0].Length, Input.Length);

        return grid;
    }
}