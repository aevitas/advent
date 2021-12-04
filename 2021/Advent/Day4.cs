using System.Numerics;

namespace Advent;

internal static class Day4
{
    private static readonly string[] Input = File.ReadAllLines("Day4.txt");

    public static void PartOne()
    {
        var input = Input.ToArray();
        int[] draws = input[0].Split(',').Select(int.Parse).ToArray();

        var boards = GetBoards(input);

        foreach (var num in draws)
        {
            foreach (var b in boards)
            {
                if (b.State.ContainsKey(num))
                    b.Hits.Add(num);

                if (b.HasBingo())
                {
                    var unmarkedSum = b.GetUnmarked().Sum();

                    Console.WriteLine(unmarkedSum * num);

                    return;
                }
            }
        }
    }

    private static List<Board> GetBoards(string[] input)
    {
        var boards = new List<Board>();
        for (int i = 2; i < input.Length; i += 6)
        {
            var boardLines = input.Skip(i).Take(5).ToArray();

            boards.Add(Board.FromLines(boardLines));
        }

        return boards;
    }

    internal class Board
    {
        public Dictionary<int, Vector2> State { get; } = new();

        public List<int> Hits { get; } = new();

        public static Board FromLines(string[] lines)
        {
            var b = new Board();

            for (int row = 0; row < lines.Length; row++)
            {
                var nums = lines[row].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => int.Parse(s.Trim())).ToArray();
                for (int col = 0; col < nums.Length; col++)
                {
                    b.State.Add(nums[col], new Vector2(row, col));
                }
            }

            return b;
        }

        public bool HasBingo()
        {
            var positions = new List<Vector2>();

            foreach (var hit in Hits)
                if (State.TryGetValue(hit, out var pos))
                    positions.Add(pos);

            foreach (var pos in positions)
            {
                if (positions.Count(p => (int)p.X == (int)pos.X) == 5)
                    return true;

                if (positions.Count(p => (int)p.Y == (int)pos.Y) == 5)
                    return true;
            }

            return false;
        }

        public List<int> GetUnmarked() => State.Select(s => s.Key).Except(Hits).ToList();
    }
}