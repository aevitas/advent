using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace Advent
{
    internal static class Day12
    {
        public static void PartOne()
        {
            using var sr = new StreamReader("Day12.txt");
            var instructions = sr.ReadToEnd().Split(Environment.NewLine);

            Vector2 position = Vector2.Zero;
            char heading = 'E';
            foreach (var i in instructions)
            {
                var dir = i[0];
                var val = int.Parse(i[1..]);

                if (dir == 'L' || dir == 'R')
                {
                    heading = Rotate(heading, dir, val);
                    continue;
                }

                if (dir == 'F')
                {
                    position += GetVelocity($"{heading}{val}");
                    continue;
                }
                
                position += GetVelocity(i);
            }

            if (position.X < 0)
                position.X = -position.X;

            if (position.Y < 0)
                position.Y = -position.Y;
            
            Console.WriteLine(position.X + position.Y);

            static Vector2 GetVelocity(string instruction)
            {
                var direction = instruction[0];
                var count = int.Parse(instruction[1..]);

                return direction switch
                {
                    'N' => new Vector2(0, count),
                    'S' => new Vector2(0, -count),
                    'E' => new Vector2(count, 0),
                    'W' => new Vector2(-count, 0),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            static char Rotate(char currentHeading, char direction, int degrees)
            {
                // Get ready for some compass magic.
                var compass = new CircularQueue<char>();
                var rotateLeft = direction == 'L';

                // First, make sure the compass orientation aligns with the direction we'll be turning in.
                if (rotateLeft)
                {
                    compass.Enqueue('N');
                    compass.Enqueue('W');
                    compass.Enqueue('S');
                    compass.Enqueue('E');
                }
                else
                {
                    compass.Enqueue('N');
                    compass.Enqueue('E');
                    compass.Enqueue('S');
                    compass.Enqueue('W');
                }

                // Then, make sure the compass' initial heading aligns with our current heading.
                compass.CycleTo(currentHeading);
                // Dequeue once more so our next rotation would end up dequeueing the next bearing, instead of our current.
                compass.Dequeue();
                
                // We can only rotate per 90 degrees, as our compass doesn't really understand the notion of SW or NE.
                // Hence, as long as we have at least 90 degrees left, we can rotate our compass once, and update our heading.
                var heading = currentHeading;
                int degreesLeft = degrees;
                while (degreesLeft >= 90)
                {
                    heading = compass.Dequeue();
                    degreesLeft -= 90;
                }

                return heading;
            }
        }
    }

    public class CircularQueue<T> : Queue<T>
    {
        public new T Dequeue()
        {
            var ret = base.Dequeue();

            Enqueue(ret);

            return ret;
        }

        public void CycleTo(T value)
        {
            while (!Peek().Equals(value))
                Dequeue();
        }
    }
}
