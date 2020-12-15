using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Advent
{
    internal static class Day11
    {
        private record Seat
        {
            public Vector2 Position { get; init; }

            public char State { get; init; }
        }

        public static void PartOne()
        {
            var seats = GetSeats().ToList();

            RenderGrid(seats);

            while (true)
            {
                var updated = new List<Seat>();
                foreach (var s in seats)
                {
                    if (s.State == '.')
                    {
                        updated.Add(s);
                        continue;
                    }

                    var adjacents = GetNeighbours(s, seats).ToArray();

                    if (s.State == 'L')
                    {
                        if (adjacents.All(IsEmpty))
                            updated.Add(s with { State = '#' });
                        else
                        {
                            updated.Add(s);
                        }
                    }


                    if (s.State == '#')
                    {
                        if (adjacents.Count(a => !IsEmpty(a)) >= 4)
                            updated.Add(s with { State = 'L' });
                        else
                        {
                            updated.Add(s);
                        }
                    }
                }

                RenderGrid(updated);

                if (seats.SequenceEqual(updated))
                    break;
                
                seats = updated;
            }

            Seat GetSeat(int x, int y) => seats.FirstOrDefault(s => s.Position == new Vector2(x, y));

            bool IsEmpty(Seat s) => s.State == '.' || s.State == 'L';
        }

        private static IEnumerable<Seat> GetSeats()
        {
            using var sr = new StreamReader("Day11.txt");
            var input = sr.ReadToEnd().Split(Environment.NewLine);

            var grid = new char[input[0].Length, input.Length];
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                    yield return new Seat { Position = new Vector2(x, y), State = input[y][x] };
            }
        }

        private static IEnumerable<Seat> GetNeighbours(Seat seat, IEnumerable<Seat> nodes)
        {
            var positions = new Vector2[]
            {
                new(seat.Position.X - 1, seat.Position.Y), // Left
                new(seat.Position.X + 1, seat.Position.Y), // Right
                new(seat.Position.X - 1, seat.Position.Y - 1), // Top left
                new(seat.Position.X + 1, seat.Position.Y - 1), // Top right
                new(seat.Position.X, seat.Position.Y - 1), // Above
                new(seat.Position.X, seat.Position.Y + 1), // Below
                new(seat.Position.X - 1, seat.Position.Y + 1), // Bottom left
                new(seat.Position.X + 1, seat.Position.Y + 1) // Bottom right
            };

            foreach (var p in positions)
            {
                var s = GetSeatOrDefault(p);

                if (s != null)
                    yield return s;
            }

            Seat GetSeatOrDefault(Vector2 position) => nodes.FirstOrDefault(s => s.Position == position);
        }

        private static void RenderGrid(IEnumerable<Seat> seats)
        {
            foreach (var row in seats.GroupBy(s => s.Position.Y))
            {
                foreach (var s in row)
                    Console.Write(s.State);

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
