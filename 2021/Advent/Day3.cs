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
            gamma += GetMostCommonBit(i).ToString();
        }

        string epsilon = string.Empty;
        for (int i = 0; i < lines[0].Length; i++)
        {
            var e = GetMostCommonBit(i) == 1 ? 0 : 1;
            epsilon += e.ToString();
        }

        var gammaRate = Convert.ToInt32(gamma, 2);
        var epsilonRate = Convert.ToInt32(epsilon, 2);

        Console.WriteLine(gammaRate * epsilonRate);

        int GetMostCommonBit(int pos)
        {
            int count = 0;
            for (int i = 0; i < lines.Length; i++)
                if (lines[i][pos] == '1')
                    count++;

            return count > Input.Length / 2 ? 1 : 0;
        }
    }
}