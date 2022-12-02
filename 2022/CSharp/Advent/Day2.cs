namespace Advent;

public static class Day2
{
    private static readonly string[] Input = File.ReadAllLines("Day2.txt");

    private const string Rock = "Rock";
    private const string Paper = "Paper";
    private const string Scissors = "Scissors";

    public static void PartOne()
    {
        var score = 0;
        foreach (var line in Input)
        {
            var split = line.Split(' ');
            var opponentMove = OpponentMoves[split[0]];
            var playerMove = PlayerMoves[split[1]];

            // Draw
            if (opponentMove == playerMove)
            {
                score += 3;
                score += GetMovePoints(playerMove);
                
                continue;
            }

            // We win
            if (Moves[playerMove] == opponentMove)
            {
                score += 6;
                score += GetMovePoints(playerMove);

                continue;
            }
            
            // We lose
            score += GetMovePoints(playerMove);
        }

        Console.WriteLine(score);
    }

    public static void PartTwo()
    {
        var score = 0;
        foreach (var line in Input)
        {
            var split = line.Split(' ');
            var opponentMove = OpponentMoves[split[0]];
            string playerMove;
            var outcome = split[1];

            // We should lose
            if (outcome == "X")
            {
                playerMove = Moves.FirstOrDefault(m => m.Key == opponentMove).Value;
                score += GetMovePoints(playerMove);

                continue;
            }

            // We should win
            if (outcome == "Z")
            {
                playerMove = Moves.FirstOrDefault(m => m.Value == opponentMove).Key;
                score += GetMovePoints(playerMove);
                score += 6;

                continue;
            }

            // We should tie
            score += GetMovePoints(opponentMove);
            score += 3;
        }

        Console.WriteLine(score);
    }

    private static int GetMovePoints(string move)
    {
        return move switch
        {
            Rock => 1,
            Paper => 2,
            Scissors => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(move), move, null)
        };
    }

    public static Dictionary<string, string> Moves = new()
    {
        { Rock, Scissors },
        { Paper, Rock },
        { Scissors, Paper }
    };

    public static Dictionary<string, string> OpponentMoves = new()
    {
        { "A", Rock },
        { "B", Paper },
        { "C", Scissors }
    };

    public static Dictionary<string, string> PlayerMoves = new()
    {
        { "X", Rock },
        { "Y", Paper },
        { "Z", Scissors }
    };
}