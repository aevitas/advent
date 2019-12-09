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
                var o = i[0];

                if (!directOrbits.ContainsKey(i[0]))
                {
                    directOrbits.Add(o, new HashSet<string>
                    {
                        i[1]
                    });
                    continue;
                }

                directOrbits[o].Add(i[1]);
            }

            // For each mass, get the planets they're orbiting. Keep doing that ad nauseam until we eventually run out. Count along the way.
            var indirectOrbitCount = 0;
            foreach (var mass in input.Distinct())
            {
                var m = mass[1];

                var count = 0;
                bool any = true;
                while (any)
                {
                    var linkedMasses = GetLinkedMasses(m);
                    var c = linkedMasses.Sum(o => o.Value.Count);

                    any = c > count;

                    count += c;
                }

                indirectOrbitCount += count;
            }

            List<KeyValuePair<string, HashSet<string>>> GetLinkedMasses(string mass)
            {
                return directOrbits.Where(o => o.Value.Contains(mass)).ToList();
            }

            Console.WriteLine(directOrbits.Count + indirectOrbitCount);
        }
    }
}
