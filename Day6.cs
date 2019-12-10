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

            // Grab the orbits and arrange them outwards in - i.e. the key is the "orbiter", and the values are the masses we orbit.
            // That way, when we calculate indirect orbits later on, we can simply iterate the values instead of having to do reverse lookups.
            //
            // Assuming C orbits B, orbits A, you'd get:
            //
            //           3     2    1
            //          COM <- A <- B <- C
            // 
            // Where C's values can be enumerated to grab B, B's to grab A, etc.
            //
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

            // For each orbit, we grab its linked masses, and union their orbits with the current planet's orbit.
            // Because A orbits B orbits C is three orbits, we'll end up with a fully inclusive list of all orbits, which we then simply have to count.
            foreach (var orbit in directOrbits)
            {
                foreach (var linked in orbit.Value.ToArray())
                    orbit.Value.UnionWith(directOrbits[linked]);
            }

            Console.WriteLine($"{directOrbits.Sum(o => o.Value.Count)}");
        }
    }
}
