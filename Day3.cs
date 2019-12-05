using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Day3
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("Day3.txt");
            var visits = new Dictionary<(int, int), Dictionary<int, int>>();

            for (var wire = 0; wire < input.Length; wire++)
            {
                var path = input[wire].Split(',');
                var x = 0;
                var y = 0;

                foreach (var step in path)
                {
                    // Get the direction vector, then apply that vector X times to get to the next pos
                    var (vecX, vecY) = step[0] switch
                    {
                        'U' => (0, 1),
                        'D' => (0, -1),
                        'L' => (-1, 0),
                        'R' => (1, 0),
                        _ => throw new ArgumentException($"Direction {step[0]} is not legal.")
                    };

                    var stepCount = int.Parse(step.Substring(1));
                    var dist = 0;

                    for (var i = 0; i < stepCount; i++)
                    {
                        x += vecX;
                        y += vecY;
                        dist++;

                        if (!visits.TryGetValue((x, y), out var pos))
                            visits.Add((x, y), pos = new Dictionary<int, int>()); // We've not been here before

                        // If the position contains the wire, we'd intersect with ourselves. We explicitly don't want that,
                        // because itd overwrite our previous dist.
                        if (!pos.ContainsKey(wire))
                            pos[wire] = dist;
                    }
                }
            }

            // Visits with one or fewer visits can never be an intersection.
            var closest = visits.Where(v => v.Value.Count >= 2).Select(v => Math.Abs(v.Key.Item1) + Math.Abs(v.Key.Item2)).Min();

            Console.WriteLine(closest);
        }
    }
}
