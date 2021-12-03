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
                    int numOf0 = 0;
                    int numOf1 = 0;

                    foreach (string number in reportO)
                    {
                        if (number[bit] == '0') numOf0++;
                        else numOf1++;
                    }

                    if (numOf0 > numOf1)
                    {
                        // Verwijder alle strings die de 1-bit hebben
                        for (int i = reportO.Count - 1; i >= 0; i--)
                        {
                            if (reportO[i][bit] == '1' && reportO.Count != 1)
                            {
                                reportO.RemoveAt(i);
                            }
                        }
                    }
                    else
                    {
                        // Verwijder alle strings die de 0-bit hebben
                        for (int i = reportO.Count - 1; i >= 0; i--)
                        {
                            if (reportO[i][bit] == '0' && reportO.Count != 1)
                            {
                                reportO.RemoveAt(i);
                            }
                        }
                    }
                }
            }

            while (reportCO2.Count != 1)
            {
                for (int bit = 0; bit < reportO[0].Length; bit++)
                {
                    int numOf0 = 0;
                    int numOf1 = 0;

                    foreach (string number in reportCO2)
                    {
                        if (number[bit] == '0') numOf0++;
                        else numOf1++;
                    }

                    if (numOf0 > numOf1)
                    {
                        // Verwijder alle strings die de 0-bit hebben
                        for (int i = reportCO2.Count - 1; i >= 0; i--)
                        {
                            if (reportCO2[i][bit] == '0' && reportCO2.Count != 1)
                            {
                                reportCO2.RemoveAt(i);
                            }
                        }
                    }
                    else
                    {
                        // Verwijder alle strings die de 1-bit hebben
                        for (int i = reportCO2.Count - 1; i >= 0; i--)
                        {
                            if (reportCO2[i][bit] == '1' && reportCO2.Count != 1)
                            {
                                reportCO2.RemoveAt(i);
                            }
                        }
                    }
                }

                int O = Convert.ToInt32(reportO[0], 2);
                int CO2 = Convert.ToInt32(reportCO2[0], 2);

                int lifeSupport = O * CO2;

                Console.WriteLine($"The life support rating is {lifeSupport}");
            }
        }
    }
}
