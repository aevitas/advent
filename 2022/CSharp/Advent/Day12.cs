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
            builder.AddNode(node.Key.ToString());
            
            if (grid.TryGetValue((c.x - 1, c.y), out var l))
                if (Characters.IndexOf(l) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(node.Key.ToString(), (c.x - 1, c.y).ToString(),
                        Characters.IndexOf(l) - Characters.IndexOf(node.Value));

            if (grid.TryGetValue((c.x + 1, c.y), out var r))
                if (Characters.IndexOf(r) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(node.Key.ToString(), (c.x + 1, c.y).ToString(),
                        Characters.IndexOf(r) - Characters.IndexOf(node.Value));

            if (grid.TryGetValue((c.x, c.y - 1), out var u))
                if (Characters.IndexOf(u) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(node.Key.ToString(), (c.x, c.y - 1).ToString(),
                        Characters.IndexOf(u) - Characters.IndexOf(node.Value));

            if (grid.TryGetValue((c.x, c.y + 1), out var d))
                if (Characters.IndexOf(d) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(node.Key.ToString(), (c.x, c.y + 1).ToString(),
                        Characters.IndexOf(d) - Characters.IndexOf(node.Value));
        }

        var graph = builder.Build();

        var start = grid.FirstOrDefault(c => c.Value == 'S');
        var end = grid.FirstOrDefault(c => c.Value == 'E');

        var path = graph.Dijkstra(start.Key.ToString(), end.Key.ToString());

        Console.WriteLine(path.Segments.Count);
    }

    public static void PartTwo()
    {
        var grid = CreateGrid();
        var builder = new GraphBuilder();

        foreach (var node in grid)
        {
            var c = node.Key;
            builder.AddNode(node.Key.ToString());

            if (grid.TryGetValue((c.x - 1, c.y), out var l))
                if (Characters.IndexOf(l) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(node.Key.ToString(), (c.x - 1, c.y).ToString(),
                        Characters.IndexOf(l) - Characters.IndexOf(node.Value));

            if (grid.TryGetValue((c.x + 1, c.y), out var r))
                if (Characters.IndexOf(r) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(node.Key.ToString(), (c.x + 1, c.y).ToString(),
                        Characters.IndexOf(r) - Characters.IndexOf(node.Value));

            if (grid.TryGetValue((c.x, c.y - 1), out var u))
                if (Characters.IndexOf(u) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(node.Key.ToString(), (c.x, c.y - 1).ToString(),
                        Characters.IndexOf(u) - Characters.IndexOf(node.Value));

            if (grid.TryGetValue((c.x, c.y + 1), out var d))
                if (Characters.IndexOf(d) - Characters.IndexOf(node.Value) <= 1)
                    builder.AddLink(node.Key.ToString(), (c.x, c.y + 1).ToString(),
                        Characters.IndexOf(d) - Characters.IndexOf(node.Value));
        }

        var graph = builder.Build();

        var starts = grid.Where(c => c.Value == 'a');
        var end = grid.FirstOrDefault(c => c.Value == 'E');

        List<Path> paths = new List<Path>();
        foreach (var s in starts)
        {
            var path = graph.Dijkstra(s.Key.ToString(), end.Key.ToString());

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