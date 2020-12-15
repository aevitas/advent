using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    internal static class Day15
    {
        public static void PartOne()
        {
            var input = "0,13,1,8,6,15".Split(',').Select(int.Parse).ToArray();
            var numbers = new List<(int turn, int value)>();
            
            for (int i = 1; i < input.Length+1; i++)
                numbers.Add((i, input[i - 1]));
            
            for (int i = numbers.Count; i < 2020; i++)
            {
                var lastSpokenNumber = numbers[i - 1].value;
                var spokenOnce = numbers.Count(tuple => tuple.value == lastSpokenNumber) == 1;

                if (spokenOnce)
                {
                    numbers.Add((i + 1, 0));
                }
                else
                {
                    var nums = numbers.Where(n => n.value == lastSpokenNumber).OrderByDescending(n => n.turn).ToArray();
                    var lastSpokenOnTurn = nums.First();
                    var turnBeforeThat = nums.Skip(1).First();

                    numbers.Add((i + 1, lastSpokenOnTurn.turn - turnBeforeThat.turn));
                }
            }
            
            Console.WriteLine(numbers.FirstOrDefault(n => n.turn == 2020).value);
        }
    }
}
