namespace Advent;

public static class Day6
{
    private static readonly string[] Input = File.ReadAllLines("Day6.txt");

    public static void PartOne()
    {
        var input = Input[0];

        for (int i = 4; i < input.Length; i++)
        {
            if (input.Skip(i - 4).Take(4).Distinct().Count() == 4)
            {
                Console.WriteLine(i);
                break;
            }
        }
    }

    public static void PartTwo()
    {
        var input = Input[0];

        for (int i = 14; i < input.Length; i++)
        {
            if (input.Skip(i - 14).Take(14).Distinct().Count() == 14)
            {
                Console.WriteLine(i);
                break;
            }
        }
    }
}