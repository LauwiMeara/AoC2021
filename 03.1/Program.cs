using System;
using System.IO;
using System.Linq;

namespace _03._1
{
    class Program
    {
        static void Main()
        {
            string[] report = File.ReadAllLines("input.txt");

            string gamma = "";
            string epsilon = "";

            for (int bit = 0; bit < report[0].Length; bit++)
            {
                char mostCommonValue = report.GroupBy(number => number[bit]).OrderByDescending(group => group.Count()).First().Key;
                
                if (mostCommonValue == '0')
                {
                    gamma += '0';
                    epsilon += '1';
                }
                else
                {
                    gamma += '1';
                    epsilon += '0';
                }
            }

            int gammaDecimal = Convert.ToInt32(gamma, 2);
            int epsilonDecimal = Convert.ToInt32(epsilon, 2);

            Console.WriteLine($"The power consumption is {gammaDecimal * epsilonDecimal}");
        }
    }
}
