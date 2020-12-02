using System;
using System.Threading.Tasks;

namespace Advent
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Day1.PartOneAsync();
            await Day1.PartTwoAsync();

            Console.ReadLine();
        }
    }
}
