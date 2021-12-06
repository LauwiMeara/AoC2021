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
            Dictionary<int, long> timers = new Dictionary<int, long>
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
                { 8, 0 }
            };

            List<int> input = File.ReadAllText("input.txt").Split(",").Select(timer => int.Parse(timer)).ToList();

            foreach (int timer in input)
            {
                timers[timer]++;
            }

            for (int day = 0; day < 256; day++)
            {
                long pregnantFish = timers[0];

                timers[0] = timers[1];
                timers[1] = timers[2];
                timers[2] = timers[3];
                timers[3] = timers[4];
                timers[4] = timers[5];
                timers[5] = timers[6];
                timers[6] = timers[7] + pregnantFish;
                timers[7] = timers[8];
                timers[8] = pregnantFish;
            }

            long numLanternfish = timers[0] + timers[1] + timers[2] + timers[3] + timers[4] + timers[5] + timers[6] + timers[7] + timers[8];

            Console.WriteLine($"The number of lanternfish after 256 days is {numLanternfish}");
        }
    }
}
