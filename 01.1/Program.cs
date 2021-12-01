using System;
using System.IO;
using System.Linq;

namespace _01._1
{
    class Program
    {
        static void Main()
        {
            int[] report = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
            int numIncreases = 0;

            for (int i = 1; i < report.Length; i++)
            {
                if (report[i] > report [i - 1]) numIncreases++;
            }

            Console.WriteLine($"The number of increases is {numIncreases}");
        }
    }
}
