using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Day6
    {
        public static void Solve()
        {
            var input = File.ReadAllText("Day6.txt").Split(Environment.NewLine).Select(i => i.Split(')'));

            var directOrbits = new Dictionary<string, HashSet<string>> { { "COM", new HashSet<string>() } };

            foreach (var i in input)
            {
                var key = i[1];
                var value = i[0];

                if (!directOrbits.ContainsKey(key))
                {
                    directOrbits.Add(key, new HashSet<string>
                    {
                        value
                    });
                    continue;
                }

                directOrbits[key].Add(value);
            }

            // For each mass, get the planets they're orbiting. Keep doing that ad nauseam until we eventually run out. Count along the way.
            foreach (var o in directOrbits)
            {
                foreach (var p in o.Value.ToList())
                    o.Value.UnionWith(directOrbits[p]);
            }

            Console.WriteLine($"{directOrbits.Sum(o => o.Value.Count)}");
        }
    }
}
