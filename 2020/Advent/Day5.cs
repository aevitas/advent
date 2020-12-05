using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    internal static class Day5
    {
    public static void Solve()
    {
        var sr = new StreamReader("Day5.txt");
        var boardingPasses = sr.ReadToEnd().Split(Environment.NewLine);

        var rows = Enumerable.Range(0, 128).ToArray();
        var columns = Enumerable.Range(0, 8).ToArray();

        var seatIds = new List<int>();
        foreach (var p in boardingPasses)
        {
            var row = rows.ToArray();
            var col = columns.ToArray();
            foreach (var c in p)
            {
                switch (c)
                {
                    case 'F':
                        row = row.Take(row.Length / 2).ToArray();
                        break;
                    case 'B':
                        row = row.Skip(row.Length / 2).Take(row.Length / 2).ToArray();
                        break;
                }

                switch (c)
                {
                    case 'L':
                        col = col.Take(col.Length / 2).ToArray();
                        break;
                    case 'R':
                        col = col.Skip(col.Length / 2).Take(col.Length / 2).ToArray();
                        break;
                }
            }

            var id = row[0] * 8 + col[0];
            seatIds.Add(id);
        }

        PartOne();
        PartTwo();

        void PartOne()
        {
            Console.WriteLine(seatIds.Max());
        }

        void PartTwo()
        {
            int? prev = null;
            foreach (var s in seatIds.OrderBy(i => i))
            {
                prev ??= s;

                if (s - prev > 1)
                {
                    Console.WriteLine(s - 1);
                }

                prev = s;
            }
        }
    }
    }
}
