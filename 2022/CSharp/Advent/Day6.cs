﻿namespace Advent;

public static class Day6
{
    private static readonly string[] Input = File.ReadAllLines("Day6.txt");

    public static void PartOne()
    {
        var input = Input[0];

        for (int i = 4; i < input.Length; i++)
        {
            var chunk = input.Skip(i - 4).Take(4);

            if (chunk.Distinct().Count() == 4)
            {
                Console.WriteLine(i);
                break;
            }
        }
    }
}