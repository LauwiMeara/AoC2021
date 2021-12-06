using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _06._1
{
    class Program
    {
        static void Main()
        {
            List<int> timers = File.ReadAllText("input.txt").Split(",").Select(timer => int.Parse(timer)).ToList();

            for (int day = 0; day < 80; day++)
            {
                for (int i = timers.Count() - 1; i >= 0; i--)
                {
                    if (timers[i] == 0)
                    {
                        timers.Add(8);
                        timers[i] = 6;
                    }
                    else
                    {
                        timers[i]--;
                    }
                }
            }

            long numLanternfish = timers.Count();

            Console.WriteLine($"The number of lanternfish after 80 days is {numLanternfish}");
        }
    }
}
