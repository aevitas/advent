using System;
using System.IO;
using System.Text;

namespace Advent
{
    internal static class Day11
    {
        public static void PartOne()
        {
            var seats = CreateGrid();
            string last = string.Empty;

            do
            {
                last = AsString(seats);

                for (int y = 0; y < seats.GetLength(1); y++)
                {
                    for (int x = 0; x < seats.GetLength(0); x++)
                    {
                        var state = seats[x, y];

                        if (state == 'L')
                        {
                            // If our adjacent seats are free, we'll take this one.
                            bool vacantAdjacents = true;
                            if (x > 0)
                                if (seats[x - 1, y] != 'L')
                                    vacantAdjacents = false;

                            if (x < seats.GetLength(0) - 1)
                                if (seats[x + 1, y] != 'L')
                                    vacantAdjacents = false;

                            if (vacantAdjacents)
                            {
                                if (x > 0)
                                    seats[x - 1, y] = '#';

                                if (x < seats.GetLength(0) - 1)
                                    seats[x + 1, y] = '#';
                            }
                        }

                        if (state == '#')
                        {
                            if (x > 4)
                            {
                                int leftAdjacentOccupied = 0;
                                for (int lx = x; lx > 0; lx--)
                                {
                                    if (seats[lx, y] == '#')
                                        leftAdjacentOccupied++;
                                }

                                if (leftAdjacentOccupied >= 4)
                                    seats[x, y] = 'L';
                            }

                            if (x < seats.GetLength(0) - 4)
                            {
                                int rightAdjacentOccupied = 0;
                                for (int lr = x; lr > 0; lr--)
                                {
                                    if (seats[lr, y] == '#')
                                        rightAdjacentOccupied++;
                                }

                                if (rightAdjacentOccupied >= 4)
                                    seats[x, y] = 'L';
                            }
                        }
                    }
                }

                RenderGrid(seats);
            } while (AsString(seats) != last);

            RenderGrid(seats);
        }

        private static char[,] CreateGrid()
        {
            using var sr = new StreamReader("Day11.txt");
            var input = sr.ReadToEnd().Split(Environment.NewLine);

            var grid = new char[input[0].Length, input.Length];
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                    grid[x, y] = input[y][x];
            }

            return grid;
        }

        private static void RenderGrid(char[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                    Console.Write(grid[x, y]);

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static string AsString(char[,] grid)
        {
            var sb = new StringBuilder();
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                    sb.Append(grid[x, y]);
            }

            return sb.ToString();
        }
    }
}
