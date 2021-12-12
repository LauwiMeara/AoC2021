using System;
using System.IO;
using System.Linq;

namespace _08._2
{
    class Program
    {
        static void Main()
        {
            string[][][] input = File.ReadAllLines("input.txt")
                .Select(entry => entry.Split(" | "))
                .Select(entry => entry.Select(signal => signal.Split(" "))
                .Select(entry => entry.Select(signal => SortString(signal))
                .ToArray()).ToArray()).ToArray();
            
            int sum = 0;

            foreach (string[][] entry in input)
            {
                string[] signalPatterns = entry[0];
                string[] outputValues = entry[1];

                string[] decodedSignals = GetDecodedSignals(signalPatterns); // Careful analysis

                string output = "";
                foreach (string value in outputValues)
                {
                    output += Array.FindIndex(decodedSignals, signal => signal == value);
                }

                sum += int.Parse(output);
            }

            Console.WriteLine($"The sum of all output values is {sum}");
        }

        static string SortString(string inputStr)
        {
            char[] chars = inputStr.ToArray();
            Array.Sort(chars);
            return new string(chars);
        }

        static string[] GetDecodedSignals(string[] signalPatterns)
        {
            string[] decodedSignals = new string[10];

            // The digits 1, 4, 7 and 8 use respectively 2, 4, 3 and 7 segments
            foreach (string signal in signalPatterns)
            {
                switch (signal.Length)
                {
                    case 2:
                        decodedSignals[1] = signal;
                        break;
                    case 4:
                        decodedSignals[4] = signal;
                        break;
                    case 3:
                        decodedSignals[7] = signal;
                        break;
                    case 7:
                        decodedSignals[8] = signal;
                        break;
                    default:
                        break;
                }
            }

            // Digit 2, 5 and 6 are the only digits that don't have both segments of digit 1
            // Digit 2 is the only digit that doesn't have the bottom right segment
            // Digits 5 and 6 are the only digits that don't have the top right segment
            // Digits 5 and 6 can be differentiated by their amount of segments, respectively 5 and 6
            char firstSegment = decodedSignals[1][0];
            char secondSegment = decodedSignals[1][1];
            var signalsFirstSegment = signalPatterns.Where(signal => !signal.Contains(firstSegment));
            var signalsSecondSegment = signalPatterns.Where(signal => !signal.Contains(secondSegment));
            if (signalsFirstSegment.Count() == 1)
            {
                decodedSignals[2] = signalsFirstSegment.Single();
                decodedSignals[5] = signalsSecondSegment.Single(signal => signal.Length == 5);
                decodedSignals[6] = signalsSecondSegment.Single(signal => signal.Length == 6);
            }
            else
            {
                decodedSignals[2] = signalsSecondSegment.Single();
                decodedSignals[5] = signalsFirstSegment.Single(signal => signal.Length == 5);
                decodedSignals[6] = signalsFirstSegment.Single(signal => signal.Length == 6);
            }

            // Digit 3 is the only digit left with 5 segments; digits 2 and 5 are already known
            decodedSignals[3] = signalPatterns.Single(signal => signal.Length == 5 && signal != decodedSignals[2] && signal != decodedSignals[5]);

            // Digits 0, 6 and 9 have 6 segments
            // Digit 6 is already known
            // Digit 9 has all the segments of digit 4; digit 0 doesn't
            decodedSignals[9] = signalPatterns.Single(signal => signal.Length == 6 && signal != decodedSignals[6] && signal.Intersect(decodedSignals[4]).Count() == decodedSignals[4].Length);
            decodedSignals[0] = signalPatterns.Single(signal => signal.Length == 6 && signal != decodedSignals[6] && signal.Intersect(decodedSignals[4]).Count() != decodedSignals[4].Length);

            return decodedSignals;
        }
    }
}
