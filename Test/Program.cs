using System;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            for (int i = 0; i < 44; i++)
            {
                Console.WriteLine(random.Next(0, 2));
            }
        }
    }
}
