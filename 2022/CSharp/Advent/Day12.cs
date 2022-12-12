using System.Numerics;
using Dijkstra.Algorithm.Graphing;
using Dijkstra.Algorithm.Pathing;
using Path = Dijkstra.Algorithm.Pathing.Path;

namespace Advent;

public static class Day12
{
    private static readonly string[] Input = File.ReadAllLines("Day12.txt");
    private static readonly List<char> Characters = "0abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();

    public static void PartOne()
    {
        var grid = CreateGrid();
        var builder = new GraphBuilder();

        foreach (var node in grid)
        {
            var c = node.Key;
            if (grid.TryGetValue((c.x - 1, c.y), out var l))
                if (Characters.IndexOf(l) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(new Vector2(node.Key.x, node.Key.y).ToString(),
                        new Vector2(c.x - 1, c.y).ToString(), 1);

            if (grid.TryGetValue((c.x + 1, c.y), out var r))
                if (Characters.IndexOf(r) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(new Vector2(node.Key.x, node.Key.y).ToString(),
                        new Vector2(c.x + 1, c.y).ToString(), 1);
            
            if (grid.TryGetValue((c.x, c.y - 1), out var u))
                if (Characters.IndexOf(u) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(new Vector2(node.Key.x, node.Key.y).ToString(),
                        new Vector2(c.x, c.y - 1).ToString(), 1);

            if (grid.TryGetValue((c.x, c.y + 1), out var d))
                if (Characters.IndexOf(d) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(new Vector2(node.Key.x, node.Key.y).ToString(),
                        new Vector2(c.x, c.y + 1).ToString(), 1);

            builder.AddNode(new Vector2(node.Key.x, node.Key.y).ToString());
        }

        var graph = builder.Build();

        var start = grid.FirstOrDefault(c => c.Value == 'S');
        var end = grid.FirstOrDefault(c => c.Value == 'E');

        var path = graph.Dijkstra(new Vector2(start.Key.x, start.Key.y).ToString(),
            new Vector2(end.Key.x, end.Key.y).ToString());

        Console.WriteLine(path.Segments.Count);
    }

    public static void PartTwo()
    {
        var grid = CreateGrid();
        var builder = new GraphBuilder();

        foreach (var node in grid)
        {
            var c = node.Key;
            if (grid.TryGetValue((c.x - 1, c.y), out var l))
                if (Characters.IndexOf(l) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(new Vector2(node.Key.x, node.Key.y).ToString(),
                        new Vector2(c.x - 1, c.y).ToString(), 1);

            if (grid.TryGetValue((c.x + 1, c.y), out var r))
                if (Characters.IndexOf(r) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(new Vector2(node.Key.x, node.Key.y).ToString(),
                        new Vector2(c.x + 1, c.y).ToString(), 1);

            if (grid.TryGetValue((c.x, c.y - 1), out var u))
                if (Characters.IndexOf(u) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(new Vector2(node.Key.x, node.Key.y).ToString(),
                        new Vector2(c.x, c.y - 1).ToString(), 1);

            if (grid.TryGetValue((c.x, c.y + 1), out var d))
                if (Characters.IndexOf(d) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(new Vector2(node.Key.x, node.Key.y).ToString(),
                        new Vector2(c.x, c.y + 1).ToString(), 1);

            builder.AddNode(new Vector2(node.Key.x, node.Key.y).ToString());
        }

        var graph = builder.Build();

        var starts = grid.Where(c => c.Value == 'a');
        var end = grid.FirstOrDefault(c => c.Value == 'E');

        List<Path> paths = new List<Path>();
        foreach (var s in starts)
        {
            var path = graph.Dijkstra(new Vector2(s.Key.x, s.Key.y).ToString(),
                new Vector2(end.Key.x, end.Key.y).ToString());

            paths.Add(path);
        }

        Console.WriteLine(paths.OrderBy(p => p.Segments).First().Segments.Count);
    }

    private static Dictionary<(int x, int y), char> CreateGrid()
    {
        Dictionary<(int x, int y), char> grid = new Dictionary<(int x, int y), char>();
        for (int y = 0; y < Input.Length; y++)
        {
            for (int x = 0; x < Input[y].Length; x++)
            {
                grid[(x, y)] = Input[y][x];
            }
        }

        return grid;
    }
}