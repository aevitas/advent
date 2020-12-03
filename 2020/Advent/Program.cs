using System;
using System.Threading.Tasks;

namespace Advent
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Day3.PartOneAsync();
            await Day3.PartTwoAsync();

            Console.ReadLine();
        }
    }
}
