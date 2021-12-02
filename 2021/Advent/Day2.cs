using System.Numerics;

namespace Advent;

internal static class Day2
{
    private static readonly string[] Input = File.ReadAllLines("Day2.txt");

    public static void PartOne()
    {
        Vector3 position = Vector3.Zero;

        var input = Input.ToArray();
        foreach (var line in input)
        {
            position += GetVelocity(line);
        }

        Console.WriteLine(position.X * -position.Z);

        Vector3 GetVelocity(string line)
        {
            var instr = line.Split(' ')[0];
            var units = int.Parse(line.Split(' ')[1]);

            var velocity = instr switch
            {
                "forward" => new Vector3(units, 0, 0),
                "down" => new Vector3(0, 0, -units),
                "up" => new Vector3(0, 0, units),
                _ => throw new ArgumentOutOfRangeException($"unk instr: {instr}")
            };

            return velocity;
        }
    }
}