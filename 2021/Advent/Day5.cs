using System.Numerics;
using System.Text;

namespace Advent;

internal class Day5
{
    private static readonly string[] Input = File.ReadAllLines("Day5.txt");

    public static void PartOne()
    {
        // This solution is probably the worst way to do it
        var input = Input.ToArray();
        var lines = new List<LineSegment2>();
        foreach (var line in input)
        {
            var s = line.Split(" -> ");
            var start = new Vector2(float.Parse(s[0].Split(',')[0]), float.Parse(s[0].Split(',')[1]));
            var end = new Vector2(float.Parse(s[1].Split(',')[0]), float.Parse(s[1].Split(',')[1]));

            lines.Add(new LineSegment2(start, end));
        }

        var vectors = new List<Vector2>();
        foreach (var line in lines)
        {
            var points = GetPoints(line);
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

        DrawGrid(vectors);

        Console.ReadLine();
        
        Vector2[] GetPoints(LineSegment2 line)
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

internal readonly record struct LineSegment2(Vector2 Start, Vector2 End);