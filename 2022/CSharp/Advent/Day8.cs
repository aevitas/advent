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
                for (int j = 0; v && j < bounds.X; j++)
                {
                    var cur = grid[(j, y)];
                    if (cur >= height)
                        visible[(j, y)] = v = false;
                }
                
                v = true;
                for (int j = 0; v && j < bounds.Y; j++)
                {
                    var cur = grid[(x, j)];
                    if (cur >= height)
                        visible[(x, j)] = v = false;
                }

                v = true;
                for (int j = (int) bounds.X - 1; v && j > 0; j--)
                {
                    var cur = grid[(j, y)];
                    if (cur >= height)
                        visible[(j, y)] = v = false;
                }

                v = true;
                for (int j = (int)bounds.Y - 1; v && j > 0; j--)
                {
                    var cur = grid[(x, j)];
                    if (cur >= height)
                        visible[(x, j)] = v = false;
                }
            }
        }
        
        Console.WriteLine(visible.Count);
    }

    public static void PartTwo()
    {

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