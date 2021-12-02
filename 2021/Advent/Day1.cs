namespace Advent;

internal static class Day1
{
    private static readonly int[] Input = File.ReadAllLines("Day1.txt").Select(int.Parse).ToArray();

    public static void PartOne()
    {
        var input = Input;

        int count = 0;
        for (int i = 0; i < input.Length - 1; i++)
        {
            if (input[i + 1] > input[i])
                count++;
        }

        Console.WriteLine(count);
    }

    public static void PartTwo()
    {
        var input = Input;

        int count = 0;
        for (int i = 0; i < input.Length - 3; i++)
        {
            var cur = input.Skip(i).Take(3).Sum();
            var next = input.Skip(i + 1).Take(3).Sum();

            if (cur < next)
                count++;
        }

        Console.WriteLine(count);
    }
}
