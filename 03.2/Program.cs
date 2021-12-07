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

            ReduceReport(reportO, '1');
            ReduceReport(reportCO2, '0');

            int O = Convert.ToInt32(reportO[0], 2);
            int CO2 = Convert.ToInt32(reportCO2[0], 2);

            Console.WriteLine($"The life support rating is {O * CO2}");
        }

        static void ReduceReport(List<string> report, char preferredValue)
        {
            char otherValue = preferredValue == '0' ? '1' : '0';

            while (report.Count != 1)
            {
                for (int bit = 0; bit < report[0].Length; bit++)
                {
                    char mostCommonValue = report.GroupBy(number => number[bit]).OrderByDescending(group => (group.Count(), group.Key)).First().Key;
                    char discardValue = mostCommonValue == '0' ? preferredValue : otherValue;
                    DiscardNumbers(report, bit, discardValue);
                }
            }
        }

        static void DiscardNumbers(List<string> report, int bit, char discardValue)
        {
            for (int i = report.Count - 1; i >= 0; i--)
            {
                if (report[i][bit] == discardValue && report.Count != 1)
                {
                    report.RemoveAt(i);
                }
            }
        }
    }
}
