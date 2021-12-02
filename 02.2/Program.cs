using System;
using System.IO;
using System.Linq;

namespace _02._2
{
    class Program
    {
        static void Main()
        {
            string[][] course = File.ReadAllLines("input.txt").Select(command => command.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

            int depth = 0;
            int horizontalPosition = 0;
            int aim = 0;

            foreach (string[] command in course)
            {
                switch (command[0])
                {
                    case "forward":
                        horizontalPosition += int.Parse(command[1]);
                        depth += aim * int.Parse(command[1]);
                        break;
                    case "down":
                        aim += int.Parse(command[1]);
                        break;
                    case "up":
                        aim -= int.Parse(command[1]);
                        break;
                }
            }

            int product = depth * horizontalPosition;

            Console.WriteLine($"The depth multiplied by the horizontal position is {product}");
        }
    }
}
