namespace Advent;

internal static class Day6
{
    private static readonly string[] Input = File.ReadAllLines("Day6.txt");

    public static void PartOne()
    {
        var input = Input.ToArray();
        var fishies = input[0].Split(',').Select(int.Parse).ToList();
        
        for (int day = 0; day < 80; day++)
        {
            int newFishiesCount = 0;
            for (int i = 0; i < fishies.Count; i++)
            {
                var fish = fishies[i];

                if (fish == 0)
                {
                    newFishiesCount++;
                    fishies[i] = 6;

                    continue;
                }

                fishies[i]--;
            }

            for (int i = 0; i < newFishiesCount; i++)
                fishies.Add(8);
        }

        Console.WriteLine(fishies.Count);
    }
}