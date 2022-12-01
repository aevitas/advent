namespace Advent;

public static class Day1
{
    private static readonly string[] Input = File.ReadAllLines("Day1.txt");

    public static void PartOne()
    {
        var elves = CalculateCaloriesCarried();

        Console.WriteLine(elves.Max());
    }

    public static void PartTwo()
    {
        var elves = CalculateCaloriesCarried();

        Console.WriteLine(elves.OrderByDescending(i => i).Take(3).Sum());
    }

    private static List<int> CalculateCaloriesCarried()
    {
        List<int> elves = new();

        int cur = 0;
        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                elves.Add(cur);
                cur = 0;

                continue;
            }

            cur += int.Parse(line);
        }

        return elves;
    }
}