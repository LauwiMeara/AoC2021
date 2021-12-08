using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _06._2
{
    class Program
    {
        static void Main()
        {
            int maxTimer = 8;
            int finalDay = 256;

            // Create empty dictionary for all timers up until maxTimer
            Dictionary<int, long> timers = new Dictionary<int, long>();
            for (int i = 0; i <= maxTimer; i++)
            {
                timers.Add(i, 0);
            }

            // Fill dictionary with initial state
            int[] init = File.ReadAllText("input.txt").Split(",").Select(int.Parse).ToArray();
            foreach (int timer in init)
            {
                timers[timer]++;
            }

            // Calculate the number of fish per timer for each day until the final day
            for (int day = 0; day < finalDay; day++)
            {
                long pregnantFish = timers[0];

                for (int i = 0; i < maxTimer; i++)
                {
                    timers[i] = timers[i + 1];
                }

                timers[6] += pregnantFish;
                timers[8] = pregnantFish;
            }

            long numFish = timers.Sum(timer => timer.Value);

            Console.WriteLine($"The number of lanternfish after {finalDay} days is {numFish}");
        }
    }
}
