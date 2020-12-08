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
            var outer = await GetBagsAsync();

            const string gold = "shiny gold";
            var sorted = new HashSet<Bag>();
            foreach (var b in outer)
            {
                foreach (var inner in b.Bags)
                {
                    sorted.Add(outer.First(o => o.Color == inner.Color));
                }

                sorted.Add(b);
            }
        }

        private static async Task<HashSet<Bag>> GetBagsAsync()
        {
            using var sr = new StreamReader("Day7.txt");
            var input = (await sr.ReadToEndAsync()).Split(Environment.NewLine);

            var bags = new HashSet<Bag>();

            foreach (var s in input)
            {
                bags.Add(ParseBag(s));
            }

            static Bag ParseBag(string s)
            {
                s = s.TrimEnd('.');
                s = s.Replace("bags", string.Empty);
                s = s.Replace("bag", string.Empty);

                var outerBag = s.Split(" contain")[0].Trim();

                if (s.Contains("no other"))
                    return new Bag {Color = outerBag, Bags = new List<Bag>()};

                var remainingBags = s.Split("contain ")[1];
                if (!s.Contains(','))
                {
                    var inner = ParseBag(remainingBags);

                    return new Bag{ Color = outerBag, Bags = new List<Bag>{inner}};
                }

                static Bag ParseBag(string line)
                {
                    var s = line.Trim(' ');
                    var slots = int.Parse(s[..1]);
                    var color = s[2..].Trim();

                    return new Bag {Color = color};
                }

                return new Bag {Color = outerBag, Bags = remainingBags.Split(',').Select(ParseBag).ToList()};
            }

            return bags;
        }

        [DebuggerDisplay("{Color}")]
        private class Bag
        {
            public string Color { get; set; }

            public List<Bag> Bags { get; set; }
        }
    }
}
