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
            
            int groupSize = 3;

            int numIncreases = 0;
            int firstSum = report[0..groupSize].Sum();

            for (int i = 1; i <= report.Length - groupSize; i++)
            {
                int secondSum = report[i..(i + groupSize)].Sum();
                if (secondSum > firstSum) numIncreases++;
                firstSum = secondSum;
            }

            Console.WriteLine($"The number of increases is {numIncreases}");
        }
    }
}
