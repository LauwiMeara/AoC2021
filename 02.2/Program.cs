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
                string direction = command[0];
                int number = int.Parse(command[1]);

                switch (direction)
                {
                    case "forward":
                        horizontalPosition += number;
                        depth += aim * number;
                        break;
                    case "down":
                        aim += number;
                        break;
                    case "up":
                        aim -= number;
                        break;
                }
            }

            Console.WriteLine($"The depth multiplied by the horizontal position is {depth * horizontalPosition}");
        }
    }
}
