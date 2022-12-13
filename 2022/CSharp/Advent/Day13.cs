using System.Text.Json.Nodes;

namespace Advent;

public static class Day13
{
    public static string[] Input = File.ReadAllLines("Day13.txt");

    public static void PartOne()
    {
        var nodes = Input.Where(l => !string.IsNullOrWhiteSpace(l)).Select(n => JsonNode.Parse(n)).ToList();
        int count = nodes.Chunk(2).Select((p, i) => Evaluate(p[0], p[1]) < 0 ? i + 1 : 0).Sum();
        
        Console.WriteLine(count);
    }

    public static void PartTwo()
    {
        var nodes = Input.Where(l => !string.IsNullOrWhiteSpace(l)).Select(n => JsonNode.Parse(n)).ToList();
        var div = new[] { JsonNode.Parse("[[2]]"), JsonNode.Parse("[[6]]") };

        nodes.AddRange(div);

        nodes.Sort(Evaluate);

        Console.WriteLine((nodes.IndexOf(div[0]) + 1) * (nodes.IndexOf(div[1]) + 1));
    }

    private static int Evaluate(JsonNode left, JsonNode right)
    {
        if (left is JsonValue && right is JsonValue)
            return (int)left - (int)right;

        // Treat everything as arrays, then do comparisons
        var a = left as JsonArray ?? new JsonArray((int)left);
        var b = right as JsonArray ?? new JsonArray((int)right);

        return a.Zip(b).Select(p => Evaluate(p.First, p.Second)).FirstOrDefault(i => i != 0, a.Count - b.Count);
    }
}