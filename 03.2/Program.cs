using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _03._2
{
    class Program
    {
        static void Main()
        {
            List<string> reportO = File.ReadAllLines("input.txt").ToList();
            List<string> reportCO2 = File.ReadAllLines("input.txt").ToList();

            while (reportO.Count != 1)
            {
                for (int bit = 0; bit < reportO[0].Length; bit++)
                {
                    if (DetermineMostCommonValue(reportO, bit) == '0') DiscardNumbers(reportO, bit, '1');
                    else DiscardNumbers(reportO, bit, '0');
                }
            }

            while (reportCO2.Count != 1)
            {
                for (int bit = 0; bit < reportO[0].Length; bit++)
                {
                    if (DetermineMostCommonValue(reportCO2, bit) == '0') DiscardNumbers(reportCO2, bit, '0');
                    else DiscardNumbers(reportCO2, bit, '1');
                }
            }

            int O = Convert.ToInt32(reportO[0], 2);
            int CO2 = Convert.ToInt32(reportCO2[0], 2);

            int lifeSupport = O * CO2;

            Console.WriteLine($"The life support rating is {lifeSupport}");
        }

        static char DetermineMostCommonValue(List<string> report, int bit)
        {
            int numOf0 = 0;
            int numOf1 = 0;

            foreach (string number in report)
            {
                if (number[bit] == '0') numOf0++;
                else numOf1++;
            }

            if (numOf0 > numOf1) return '0';
            else return '1';
        }

        static void DiscardNumbers(List<string> report, int bit, char binarySymbol)
        {
            for (int i = report.Count - 1; i >= 0; i--)
            {
                if (report[i][bit] == binarySymbol && report.Count != 1)
                {
                    report.RemoveAt(i);
                }
            }
        }
    }
}
