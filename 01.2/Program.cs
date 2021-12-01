using System;
using System.IO;
using System.Linq;

namespace _01._2
{
    class Program
    {
        static void Main()
        {
            int[] report = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
            
            int numIncreases = 0;
            int firstSum = report[0] + report[1] + report[2];

            for (int i = 1; i < report.Length - 2; i++)
            {
                int secondSum = report[i] + report[i + 1] + report[i + 2];
                if(secondSum > firstSum) numIncreases++;
                firstSum = secondSum;
            }

            Console.WriteLine($"The number of increases is {numIncreases}");
        }
    }
}
