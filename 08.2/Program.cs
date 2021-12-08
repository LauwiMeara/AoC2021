// This code works, but it REALLY needs to be made prettier

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _08._2
{
    class Program
    {
        static void Main()
        {
            string[][][] input = File.ReadAllLines("input.txt").Select(signals => signals.Split(" | ")).Select(signals => signals.Select(values => values.Split(" ")).ToArray()).ToArray();
            long sum = 0;

            foreach (string[][] entry in input)
            {
                Dictionary<int, string[]> inputValues = entry[0].GroupBy(signals => signals.Count()).ToDictionary(x => x.Key, x => x.ToArray());
                string[] outputValues = entry[1].Select(values => SortString(values)).ToArray();

                string[] decodedSignals = new string[10];
                char top;
                char topRight;
                char bottomRight;

                // The digits 1, 4, 7 and 8 use respectively 2, 4, 3 and 7 segments
                string[] v = new string[1];
                inputValues.TryGetValue(2, out v);
                decodedSignals[1] = SortString(v[0]);
                inputValues.TryGetValue(4, out v);
                decodedSignals[4] = SortString(v[0]);
                inputValues.TryGetValue(3, out v);
                decodedSignals[7] = SortString(v[0]);
                inputValues.TryGetValue(7, out v);
                decodedSignals[8] = SortString(v[0]);

                // The difference between digits 1 and 7 is the top segment
                top = decodedSignals[7].ToCharArray().Except(decodedSignals[1].ToCharArray()).First();

                // Digit 1 has two segments: bottom right and top right
                // The bottom right segment is in all signals, except for digit 2
                // The top right segment is in all signals, except for digits 5 and 6. These use respectively 5 and 6 segments
                char firstLetter = decodedSignals[1][0];
                char secondLetter = decodedSignals[1][1];
                var valuesFirstLetter = inputValues.Values.SelectMany(value => value).Where(value => !value.Contains(firstLetter));
                var valuesSecondLetter = inputValues.Values.SelectMany(value => value).Where(value => !value.Contains(secondLetter));
                if (valuesFirstLetter.Count() == 1)
                {
                    bottomRight = firstLetter;
                    topRight = secondLetter;
                    decodedSignals[2] = SortString(valuesFirstLetter.First());
                    foreach (string value in valuesSecondLetter)
                    {
                        if (value.Length == 5) decodedSignals[5] = SortString(value);
                        else decodedSignals[6] = SortString(value);
                    }
                }
                else
                {
                    bottomRight = secondLetter;
                    topRight = firstLetter;
                    decodedSignals[2] = SortString(valuesSecondLetter.First());
                    foreach (string value in valuesFirstLetter)
                    {
                        if (value.Length == 5) decodedSignals[5] = SortString(value);
                        else decodedSignals[6] = SortString(value);
                    }
                }

                // Digit 3 is the only digit left with 5 segments; digits 2 and 5 are already known
                decodedSignals[3] = SortString(inputValues[5].Where(value => SortString(value) != decodedSignals[2] && SortString(value) != decodedSignals[5]).First());

                // Digits 0, 6 and 9 have 6 segments
                // Digit 6 is already known
                // Digit 9 has all the letters of digit 4
                // Digit 0 is the last one remaining
                string[] test = inputValues[6].Where(value => SortString(value) != decodedSignals[6]).ToArray();
                for (int i = 0; i < decodedSignals[4].Length; i++)
                {
                    if (!test[0].Contains(decodedSignals[4][i]))
                    {
                        decodedSignals[0] = SortString(test[0]);
                        decodedSignals[9] = SortString(test[1]);
                    }
                }
                if (decodedSignals[0] == null)
                {
                    decodedSignals[0] = SortString(test[1]);
                    decodedSignals[9] = SortString(test[0]);
                }

                // Now we know the digits, let's decode the output!
                string output = "";
                foreach (string signal in outputValues)
                {
                    output += Array.FindIndex(decodedSignals, value => value == signal);
                }

                sum += int.Parse(output);
            }
        }

        static string SortString(string inputStr)
        {
            char[] chars = inputStr.ToArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }
}
