using System;
using System.IO;
using System.Linq;

namespace _09._2
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllLines("input.txt")
                .Select(row => row.Select(c => new Point
                {
                    value = int.Parse(c.ToString())
                })
                .ToArray()).ToArray();

            AssignInitialBasinNumbers(input);
            JoinAdjacentBasins(input);

            int[] orderedBasinSizes = input.SelectMany(row => row).GroupBy(point => point.basinNumber).OrderByDescending(group => group.Count()).Where(group => group.Key != 0).Select(group => group.Count()).ToArray();
            Console.WriteLine($"If you multiply the sizes of the three largest basins, you get {orderedBasinSizes[0] * orderedBasinSizes[1] * orderedBasinSizes[2]}");
        }

        public class Point
        {
            public int value;
            public int basinNumber;
        }

        static void AssignInitialBasinNumbers(Point[][] input)
        {
            int nextBasinNumber = 1;

            for (int row = 0; row < input.Length; row++)
            {
                for (int column = 0; column < input[row].Length; column++)
                {
                    Point point = input[row][column];

                    // If the value is 9, continue to the next point
                    if (point.value == 9) continue;

                    // Check all neighbours for basin numbers; if one already has a number, give the current point the same number
                    if (row != 0) // Above
                    {
                        Point neighbour = input[row - 1][column];
                        if (neighbour.basinNumber != 0) point.basinNumber = neighbour.basinNumber;
                    }
                    if (point.basinNumber == 0 && column != 0) // Left
                    {
                        Point neighbour = input[row][column - 1];
                        if (neighbour.basinNumber != 0) point.basinNumber = neighbour.basinNumber;
                    }
                    if (point.basinNumber == 0 && row != input.Length - 1) // Below
                    {
                        Point neighbour = input[row + 1][column];
                        if (neighbour.basinNumber != 0) point.basinNumber = neighbour.basinNumber;
                    }
                    if (point.basinNumber == 0 && column != input[row].Length - 1) // Right
                    {
                        Point neighbour = input[row][column + 1];
                        if (neighbour.basinNumber != 0) point.basinNumber = neighbour.basinNumber;
                    }

                    // If the basin number is still 0, it's a new basin
                    if (point.basinNumber == 0)
                    {
                        point.basinNumber = nextBasinNumber;
                        nextBasinNumber++;
                    }
                }
            }
        }

        static void JoinAdjacentBasins(Point[][] input)
        {
            int numChanges = 0;

            for (int row = 0; row < input.Length; row++)
            {
                for (int column = 0; column < input[row].Length; column++)
                {
                    Point point = input[row][column];

                    // If the value is 9, continue to the next point
                    if (point.value == 9) continue;

                    // Check all neighbours for basin numbers; if it has a different number, use the highest number
                    if (row != 0) // Above
                    {
                        Point neighbour = input[row - 1][column];
                        if (point.basinNumber < neighbour.basinNumber)
                        {
                            point.basinNumber = neighbour.basinNumber;
                            numChanges++;
                        }
                    }
                    if (column != 0) // Left
                    {
                        Point neighbour = input[row][column - 1];
                        if (point.basinNumber < neighbour.basinNumber)
                        {
                            point.basinNumber = neighbour.basinNumber;
                            numChanges++;
                        }
                    }
                    if (row != input.Length - 1) // Below
                    {
                        Point neighbour = input[row + 1][column];
                        if (point.basinNumber < neighbour.basinNumber)
                        {
                            point.basinNumber = neighbour.basinNumber;
                            numChanges++;
                        }
                    }
                    if (column != input[row].Length - 1) // Right
                    {
                        Point neighbour = input[row][column + 1];
                        if (point.basinNumber < neighbour.basinNumber)
                        {
                            point.basinNumber = neighbour.basinNumber;
                            numChanges++;
                        }
                    }
                }
            }

            // Keep joining basins until there are no changes (because all basins are already joined)
            if (numChanges != 0) JoinAdjacentBasins(input);
        }
    }
}
