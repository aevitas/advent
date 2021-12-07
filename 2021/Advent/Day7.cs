namespace Advent;

internal static class Day7
{
    private static readonly string[] Input = File.ReadAllLines("Day7.txt");

    public static void PartOne()
    {
        var positions = Input.ToArray()[0].Split(',').Select(int.Parse).ToArray();
        
        var max = positions.Max();
        var costs = new List<int>();
        for (int i = 0; i < max; i++) // We align to this coord
        {
            var roundCost = new List<int>();
            foreach (var pos in positions)
            {
                if (pos > i)
                {
                    roundCost.Add(pos - i);
                    continue;
                }

                roundCost.Add(i - pos);
            }

            costs.Add(roundCost.Sum());
        }

        Console.WriteLine(costs.Min());
    }
}