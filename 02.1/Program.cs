using System;
using System.IO;
using System.Linq;

namespace _02._1
{
    class Program
    {
        static void Main()
        {
            string[][] course = File.ReadAllLines("input.txt").Select(command => command.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

            int depth = 0;
            int horizontalPosition = 0;

            foreach (string[] command in course)
            {
                switch (command[0])
                {
                    case "forward":
                        horizontalPosition += int.Parse(command[1]);
                        break;
                    case "down":
                        depth += int.Parse(command[1]);
                        break;
                    case "up":
                        depth -= int.Parse(command[1]);
                        break;
                }
            }

            int product = depth * horizontalPosition;

            Console.WriteLine($"The depth multiplied by the horizontal position is {product}");
        }
    }
}
