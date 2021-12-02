namespace Advent;

internal static class Day1
{
    public static async Task Solve()
    {
        var input = (await File.ReadAllLinesAsync("Day1.txt")).Select(int.Parse).ToArray();

        var increments = new List<int>();
        for (int i = 0; i < input.Length - 1; i++)
        {
            if (input[i + 1] > input[i])
                increments.Add(i);
        }

        Console.WriteLine(increments.Count);
    }
}