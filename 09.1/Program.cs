using System;
using System.IO;
using System.Linq;

namespace _09._1
{
    class Program
    {
        static void Main()
        {
            int[][] input = File.ReadAllLines("input.txt").Select(row => row.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
            int sum = 0;


            for (int row = 0; row < input.Length; row++)
            {
                for (int column = 0; column < input[row].Length; column++)
                {
                    int point = input[row][column];
                    
                    // Check all neighbours; if a neighbour has a lower value, continue to the next point
                    if (row != 0) // Above
                        if (point >= input[row - 1][column]) continue;
                    if (column != 0) // Left
                        if (point >= input[row][column - 1]) continue;
                    if (row != input.Length - 1) // Below
                        if (point >= input[row + 1][column]) continue;
                    if (column != input[row].Length - 1) // Right
                        if (point >= input[row][column + 1]) continue;

                    // If you reach this, the point is the lowest point and has to be added to the sum
                    sum += point + 1;
                }
            }

            Console.WriteLine($"The sum of the risk levels is {sum}");
        }
    }
}
