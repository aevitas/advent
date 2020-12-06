using System;
using System.Threading.Tasks;

namespace Advent
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Day6.PartOneAsync();
            await Day6.PartTwoAsync();

            Console.ReadLine();
        }
    }
}
