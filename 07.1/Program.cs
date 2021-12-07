using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _07._1
{
    class Program
    {
        static void Main()
        {
            int[] crabs = File.ReadAllText("input.txt").Split(",").Select(position => int.Parse(position)).ToArray();

            // Create and fill dictionary with position (key) and number of crabs on that position (value)
            Dictionary<int, int> positions = new Dictionary<int, int>();
            for (int i = 0; i < crabs.Length; i++)
            {
                int position = crabs[i];
                if (positions.ContainsKey(position)) positions[position]++;
                else positions.Add(position, 1);
            }

            // Calculate the amount of fuel needed to align every crab to position 0
            int minFuel = positions.Sum(position => position.Key * position.Value);

            // Calculate the amount of fuel needed to align every crab to every position
            for (int i = 0; i <= positions.Keys.Max(); i++)
            {
                int fuel = positions.Sum(position => (Math.Abs(position.Key - i)) * position.Value);
                if (fuel < minFuel) minFuel = fuel;
            }

            Console.WriteLine($"Aligning the crabs takes at least {minFuel} fuel");
        }
    }
}
