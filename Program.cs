using System;

namespace Dflat
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine("What is your name? Type 'exit' to exit");
                string line = Console.ReadLine();

                if (line == "exit") {
                    Console.WriteLine("Goodbye");
                    break;
                }

                string output = String.Format("Hello, {0}", line);
                Console.WriteLine(output);

            }
        }
    }
}
