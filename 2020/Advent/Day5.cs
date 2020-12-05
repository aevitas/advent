using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    internal static class Day5
    {
        public static void PartOne()
        {
            var sr = new StreamReader("Day5.txt");
            var boardingPasses = sr.ReadToEnd().Split(Environment.NewLine);

            var rows = new int[128];
            for (int i = 0; i < 128; i++)
                rows[i] = i;

            var columns = new int[8];
            for (int i = 0; i < 8; i++)
                columns[i] = i;

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
                Console.WriteLine($"Row {row[0]} - Seat {col[0]} -> {id}");

                seatIds.Add(id);
            }

            Console.WriteLine(seatIds.Max());
        }
    }
}
