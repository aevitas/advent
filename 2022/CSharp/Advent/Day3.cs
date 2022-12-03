namespace Advent;

public static class Day3
{
    private static readonly string[] Input = File.ReadAllLines("Day3.txt");

    private static readonly List<char> Chars = "0abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();

    public static void PartOne()
    {
        List<int> priorities = new List<int>();
        foreach (var line in Input)
        {
            var a = line[..(line.Length / 2)].ToHashSet();
            var b = line[(line.Length / 2)..].ToHashSet();

            a.IntersectWith(b);

            priorities.Add(Chars.IndexOf(a.First()));
        }

        Console.WriteLine(priorities.Sum());
    }

    public static void PartTwo()
    {
        List<int> priorities = new List<int>();
        for (int i = 0; i < Input.Length; i += 3)
        {
            var group = Input.Skip(i).Take(3);

            HashSet<char> bag = null;
            foreach (var e in group)
            {
                bag ??= e.ToHashSet();

                bag.IntersectWith(e);
            }

            priorities.Add(Chars.IndexOf(bag.First()));
        }

        Console.WriteLine(priorities.Sum());
    }
}