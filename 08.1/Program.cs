using System;
using System.IO;
using System.Linq;

namespace _08._1
{
    class Program
    {
        static void Main()
        {
            string[][][] input = File.ReadAllLines("input.txt").Select(signals => signals.Split(" | ")).Select(signals => signals.Select(values => values.Split(" ")).ToArray()).ToArray();
            string[] outputValues = input.Select(signals => signals[1]).SelectMany(signals => signals).ToArray();

            int[] lengthUniqueDigits = new int[] { 2, 4, 3, 7 };
            int numUniqueDigits = outputValues.Where(signals => lengthUniqueDigits.Contains(signals.Count())).Count();

            Console.WriteLine($"The digits 1, 4, 7 or 8 appear {numUniqueDigits} times");
        }
    }
}
