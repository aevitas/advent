namespace Advent;

internal static class Day3
{
    private static readonly string[] Input = File.ReadAllLines("Day3.txt");

    public static void PartOne()
    {
        var lines = Input.ToArray();

        string gamma = string.Empty;
        for (int i = 0; i < lines[0].Length; i++)
        {
            gamma += GetMostCommonBit(lines, i).ToString();
        }

        string epsilon = string.Empty;
        for (int i = 0; i < lines[0].Length; i++)
        {
            var e = GetMostCommonBit(lines, i) == 1 ? 0 : 1;
            epsilon += e.ToString();
        }

        var gammaRate = Convert.ToInt32(gamma, 2);
        var epsilonRate = Convert.ToInt32(epsilon, 2);

        Console.WriteLine(gammaRate * epsilonRate);
    }

    public static void PartTwo()
    {
        var lines = Input.ToArray();
        int len = lines[0].Length;

        for (int i = 0; i < len; i++)
        {
            if (lines.Length == 1)
                break;

            var c = GetMostCommonBit(lines, i);

            lines = lines.Where(l => l[i].ToString() == c.ToString()).ToArray();
        }

        var oxygen = lines.First();

        lines = Input.ToArray();

        for (int i = 0; i < len; i++)
        {
            if (lines.Length == 1)
                break;

            var c = GetLeastCommonBit(lines, i);

            lines = lines.Where(l => l[i].ToString() == c.ToString()).ToArray();
        }

        var scrubber = lines.First();

        var oxygenRating = Convert.ToInt32(oxygen, 2);
        var scrubberRating = Convert.ToInt32(scrubber, 2);

        Console.WriteLine(oxygenRating * scrubberRating);
    }

    private static int GetMostCommonBit(string[] lines, int pos)
    {
        int count = 0;
        for (int i = 0; i < lines.Length; i++)
            if (lines[i][pos] == '1')
                count++;

        if (lines.Length - count == count)
            return 1;

        return count > lines.Length - count ? 1 : 0;
    }

    private static int GetLeastCommonBit(string[] lines, int pos)
    {
        int count = 0;
        for (int i = 0; i < lines.Length; i++)
            if (lines[i][pos] == '1')
                count++;

        if (lines.Length - count == count)
            return 0;

        return count < lines.Length - count ? 1 : 0;
    }
}