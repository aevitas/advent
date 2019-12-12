using System;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode
{
    public static class Day4
    {
        public static void Solve()
        {
            int min = 125730;
            int max = 579381;
            int partOne = 0;
            int partTwo = 0;

            for (int i = min; i < max; i++)
            {
                var arr = i.ToString();

                if (IsAscending(arr) && HasAdjacents(arr))
                    partOne += 1;

                if (IsAscending(arr) && HasPairAdjacents(arr))
                    partTwo += 1;
            }

            bool IsAscending(string input)
            {
                for (int i = 0; i < input.Length - 1; i++)
                {
                    if (input[i] > input[i + 1])
                        return false;
                }

                return true;
            }

            bool HasAdjacents(string input)
            {
                for (int i = 0; i < input.Length - 1; i++)
                {
                    if (input[i] == input[i + 1])
                        return true;
                }

                return false;
            }

            bool HasGroupAdjacents(string input)
            {
                for (int i = 0; i < input.Length - 2; i++)
                {
                    if (input[i] == input[i + 1] && input[i + 1] == input[i + 2])
                        return true;
                }

                return false;
            }

            bool HasPairAdjacents(string input)
            {
                for (int i = 0; i < input.Length - 1; i++)
                {
                    if (input[i] != input[i + 1])
                        continue;

                    if (i + 2 < input.Length)
                    {
                        // Three or more of the same chars, skip them.
                        if (input[i] == input[i + 1] && input[i + 1] == input[i + 2])
                            i = i + 2;
                    }

                    // Last char
                    if (i == input.Length - 1)
                        continue;

                    if (input[i] == input[i + 1])
                        return true;
                }

                return false;
            }

            Console.WriteLine(partOne);
            Console.WriteLine(partTwo);
        }
    }
}
