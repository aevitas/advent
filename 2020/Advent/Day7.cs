using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    internal static class Day7
    {
        public static async Task PartOneAsync()
        {
            var bags = await GetBagsAsync();


        }

        private static async Task<Dictionary<string, Bag[]>> GetBagsAsync()
        {
            using var sr = new StreamReader("Day7.txt");
            var input = (await sr.ReadToEndAsync()).Split(Environment.NewLine);

            var bags = new Dictionary<string, Bag[]>();

            foreach (var s in input)
            {
                var (color, innerBags) = ParseBags(s);

                bags.Add(color, innerBags);
            }

            static (string color, Bag[] bags) ParseBags(string s)
            {
                // Alright boys, get out of here.
                s = s.TrimEnd('.');
                s = s.Replace("bags", string.Empty);
                s = s.Replace("bag", string.Empty);

                var outerBag = s.Split(" contain")[0].Trim();

                if (s.Contains("no other"))
                    return (outerBag, Array.Empty<Bag>());

                var remainingBags = s.Split("contain ")[1];
                if (!s.Contains(','))
                {
                    var inner = ParseBag(remainingBags);

                    return (outerBag, new [] {inner});
                }

                static Bag ParseBag(string line)
                {
                    var s = line.Trim(' ');
                    var slots = int.Parse(s[..1]);
                    var color = s[2..].Trim();

                    return new Bag {Color = color, Slots = slots};
                }

                return (outerBag, remainingBags.Split(',').Select(ParseBag).ToArray());
            }

            return bags;
        }

        [DebuggerDisplay("{Slots} {Color}")]
        private class Bag
        {
            public string Color { get; set; }

            public int Slots { get; set; }
        }
    }
}
