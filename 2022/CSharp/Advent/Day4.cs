namespace Advent;

public static class Day4
{
    private static readonly string[] Input = File.ReadAllLines("Day4.txt");

    public static void PartOne()
    {
        int count = 0;
        foreach (var line in Input)
        {
            var sets = line.Split(',');
            var left = sets[0].Split('-');
            var right = sets[1].Split('-');
            var a = Enumerable.Range(int.Parse(left[0]), int.Parse(left[1]) - int.Parse(left[0]) + 1).ToArray();
            var b = Enumerable.Range(int.Parse(right[0]), int.Parse(right[1]) - int.Parse(right[0]) + 1).ToArray();

            if (a.Intersect(b).Count() == a.Length)
            {
                count++;
                continue;
            }

            if (b.Intersect(a).Count() == b.Length)
                count++;
        }

        Console.WriteLine(count);
    }
}