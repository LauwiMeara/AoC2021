using System;
using System.IO;
using System.Linq;

namespace _03._1
{
    class Program
    {
        static void Main()
        {
            string[] report = File.ReadAllLines("input.txt").ToArray();

            string gamma = "";
            string epsilon = "";

            for (int bit = 0; bit < report[0].Length; bit++)
            {
                int numOf0 = 0;
                int numOf1 = 0;

                foreach (string number in report)
                {
                    if (number[bit] == '0') numOf0++;
                    else numOf1++;
                }

                if (numOf0 > numOf1)
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

            int powerConsumption = gammaDecimal * epsilonDecimal;

            Console.WriteLine($"The power consumption is {powerConsumption}");
        }
    }
}
