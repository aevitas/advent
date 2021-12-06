using System.Numerics;
using System.Text;

namespace Advent;

internal readonly record struct LineSegment2(Vector2 Start, Vector2 End);

internal static class Day5
{
    private static readonly string[] Input = File.ReadAllLines("Day5.txt");

    private static List<LineSegment2> GetLineSegments()
    {
        var input = Input.ToArray();
        var lines = new List<LineSegment2>();
        foreach (var line in input)
        {
            var s = line.Split(" -> ");
            var start = new Vector2(float.Parse(s[0].Split(',')[0]), float.Parse(s[0].Split(',')[1]));
            var end = new Vector2(float.Parse(s[1].Split(',')[0]), float.Parse(s[1].Split(',')[1]));

            lines.Add(new LineSegment2(start, end));
        }

        return lines;
    }

    public static void PartOne()
    {
        // This solution is probably the worst way to do it
        var lines = GetLineSegments();

        var vectors = new List<Vector2>();
        foreach (var line in lines)
        {
            var points = GetPointsInLine(line);
            vectors.AddRange(points);
        }

        int intersectCount = 0;
        int maxX = (int)vectors.Max(v => v.X) + 1;
        int maxY = (int)vectors.Max(v => v.Y) + 1;
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                var count = vectors.Count(v => Math.Abs(v.X - x) < .1 && Math.Abs(v.Y - y) < .1);

                if (count >= 2)
                    intersectCount++;
            }
        }

        Console.WriteLine($"Intersections: {intersectCount}");
        Console.WriteLine($"Points: {vectors.Count}");

        //DrawGrid(vectors);

        Console.ReadLine();
    }
    
    private static Vector2[] GetPointsInLine(LineSegment2 line)
    {
        (Vector2 start, Vector2 end) = line;
        bool runsAlongY = Math.Abs(start.X - end.X) < 0.1;
        bool runsAlongX = Math.Abs(start.Y - end.Y) < 0.1;

        if (!runsAlongX && !runsAlongY)
            return Array.Empty<Vector2>();

        var points = new HashSet<Vector2>();

        points.Add(start);
        points.Add(end);

        for (int x = (int)Math.Min(start.X, end.X); x < (int)Math.Max(start.X, end.X); x++)
            points.Add(new Vector2(x, start.Y));

        for (int y = (int)Math.Min(start.Y, end.Y); y < (int)Math.Max(start.Y, end.Y); y++)
            points.Add(new Vector2(start.X, y));

        return points.OrderBy(v => v.X).ToArray();
    }
    
    private static void DrawGrid(IReadOnlyList<Vector2> vectors)
    {
        int maxX = (int)vectors.Max(v => v.X) + 1;
        int maxY = (int)vectors.Max(v => v.Y) + 1;

        for (int x = 0; x < maxX; x++)
        {
            StringBuilder line = new StringBuilder();
            for (int y = 0; y < maxY; y++)
            {
                var count = vectors.Count(v => Math.Abs(v.X - x) < .1 && Math.Abs(v.Y - y) < .1);

                line.Append(count > 0 ? count.ToString() : ".");
            }

            Console.WriteLine(line.ToString());
        }
    }
}

//internal static class Day5
//{
//    private static readonly string[] Input = File.ReadAllLines("Day5.txt");

//    public static void PartOne()
//    {
//        var lines = GetLineSegments();
//        var points = lines.Select(GetPointsInLine);

//        int count = 0;
//        foreach (var sec in points)
//            count += sec.Length;

//        Console.WriteLine(count);

//        var grid = new Dictionary<Point, int>();
//        foreach (var set in points)
//        {
//            foreach (var p in set)
//            {
//                if (grid.ContainsKey(p))
//                {
//                    grid[p]++;
//                    continue;
//                }

//                grid.Add(p, 0);
//            }
//        }

//        Console.WriteLine(grid.Count(p => p.Value >= 1));
//    }

//    private static Point[] GetPointsInLine(LineSegment2 line)
//    {
//        var deltaX = line.Start.X == line.End.X ? 0 : line.End.X > line.Start.X ? 1 : -1;
//        var deltaY = line.Start.Y == line.End.Y ? 0 : line.End.Y > line.Start.Y ? 1 : -1;
//        var dist = Math.Max(Math.Abs(line.Start.X - line.End.X), Math.Abs(line.Start.Y - line.End.Y)) + 1;

//        return Enumerable.Range(0, dist).Select(i => new Point(line.Start.X + i * deltaX, line.End.Y + i * deltaY))
//            .ToArray();
//    }

//    private static List<LineSegment2> GetLineSegments()
//    {
//        var input = Input.ToArray();

//        // lol
//        return (from line in input
//                select line.Split(" -> ")
//            into s
//                let start = new Point(int.Parse(s[0].Split(',')[0]), int.Parse(s[0].Split(',')[1]))
//                let end = new Point(int.Parse(s[1].Split(',')[0]), int.Parse(s[1].Split(',')[1]))
//                select new LineSegment2(start, end)).ToList();
//    }
//}

//internal readonly record struct LineSegment2(Point Start, Point End);

// internal readonly record struct Point(int X, int Y);