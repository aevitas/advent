using System;

namespace AdventOfCode
{
    public static class Day4
    {
        public static void Solve()
        {
            int min = 125730;
            int max = 579381;
            int possibilities = 0;

            for (int i = min; i < max; i++)
            {
                var arr = i.ToString();

                if (IsAscending(arr) && HasAdjacents(arr))
                    possibilities += 1;
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

            Console.WriteLine(possibilities);
        }
    }
}
