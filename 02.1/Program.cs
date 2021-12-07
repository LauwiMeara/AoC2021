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
                string direction = command[0];
                int number = int.Parse(command[1]);

                switch (direction)
                {
                    case "forward":
                        horizontalPosition += number;
                        break;
                    case "down":
                        depth += number;
                        break;
                    case "up":
                        depth -= number;
                        break;
                }
            }

            Console.WriteLine($"The depth multiplied by the horizontal position is {depth * horizontalPosition}");
        }
    }
}
