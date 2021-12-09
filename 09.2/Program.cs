using System;
using System.Collections.Generic;
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

            int nextBasinNumber = 1;

            for (int row = 0; row < input.Length; row++)
            {
                for (int column = 0; column < input[row].Length; column++)
                {
                    Point point = input[row][column];
                    
                    // If the value is 9, continue to the next point
                    if (point.value == 9) continue;

                    // Check all horizontal and vertical neighbours for basin numbers; if one already has a number, give the current point the same number
                    // Check neighbour above
                    if (row != 0) 
                    {
                        Point neighbour = input[row - 1][column];
                        if (neighbour.basinNumber != 0) point.basinNumber = neighbour.basinNumber;
                    }
                    // Check left neighbour
                    if (point.basinNumber == 0 && column != 0)
                    {
                        Point neighbour = input[row][column - 1];
                        if (neighbour.basinNumber != 0) point.basinNumber = neighbour.basinNumber;
                    }
                    // Check neighbour below
                    if (point.basinNumber == 0 && row != input.Length - 1)
                    {
                        Point neighbour = input[row + 1][column];
                        if (neighbour.basinNumber != 0) point.basinNumber = neighbour.basinNumber;
                    }
                    // Check right neighbour
                    if (point.basinNumber == 0 && column != input[row].Length - 1)
                    {
                        Point neighbour = input[row][column + 1];
                        if (neighbour.basinNumber != 0) point.basinNumber = neighbour.basinNumber;
                    }

                    // Check all diagonal neighbours, only if they aren't seperated from the current point with 9's!
                    // Check neighbour above-left
                    if (point.basinNumber == 0 && row != 0 && column != 0)
                    {
                        Point neighbourAbove = input[row - 1][column];
                        Point neighbourLeft = input[row][column - 1];
                        Point neighbourDiagonal = input[row - 1][column - 1];
                        if (!(neighbourAbove.value == 9 && neighbourLeft.value == 9) && neighbourDiagonal.basinNumber != 0) 
                            point.basinNumber = neighbourDiagonal.basinNumber;
                    }
                    // Check neighbour above-right
                    if (point.basinNumber == 0 && column != input[row].Length - 1 && row != 0)
                    {
                        Point neighbourRight = input[row][column + 1];
                        Point neighbourAbove = input[row - 1][column];
                        Point neighbourDiagonal = input[row - 1][column + 1];
                        if (!(neighbourRight.value == 9 && neighbourAbove.value == 9) && neighbourDiagonal.basinNumber != 0)
                            point.basinNumber = neighbourDiagonal.basinNumber;
                    }
                    // Check neighbour below-left
                    if (point.basinNumber == 0 && column != 0 && row != input.Length - 1)
                    {
                        Point neighbourLeft = input[row][column - 1];
                        Point neighbourBelow = input[row + 1][column];
                        Point neighbourDiagonal = input[row + 1][column - 1];
                        if (!(neighbourLeft.value == 9 && neighbourBelow.value == 9) && neighbourDiagonal.basinNumber != 0)
                            point.basinNumber = neighbourDiagonal.basinNumber;
                    }
                    // Check neighbour below-right
                    if (point.basinNumber == 0 && row != input.Length - 1 && column != input[row].Length - 1)
                    {
                        Point neighbourBelow = input[row + 1][column];
                        Point neighbourRight = input[row][column + 1];
                        Point neighbourDiagonal = input[row + 1][column + 1];
                        if (!(neighbourBelow.value == 9 && neighbourRight.value == 9) && neighbourDiagonal.basinNumber != 0)
                            point.basinNumber = neighbourDiagonal.basinNumber;
                    }
                    
                    // If the basin number is still 0, it's a new basin
                    if (point.basinNumber == 0)
                    {
                        point.basinNumber = nextBasinNumber;
                        nextBasinNumber++;
                    }
                }
            }

            // Join all adjecent basins
            JoinBasins(input);

            int[] orderedBasinSizes = input.SelectMany(row => row).GroupBy(point => point.basinNumber).OrderByDescending(group => group.Count()).Where(group => group.Key != 0).Select(group => group.Count()).ToArray();
            Console.WriteLine($"If you multiply the sizes of the three largest basins, you get {orderedBasinSizes[0] * orderedBasinSizes[1] * orderedBasinSizes[2]}");
        }

        public class Point
        {
            public int value;
            public int basinNumber;
        }

        static void JoinBasins(Point[][] input)
        {
            int numChanges = 0;

            for (int row = 0; row < input.Length; row++)
            {
                for (int column = 0; column < input[row].Length; column++)
                {
                    Point point = input[row][column];

                    // If the value is 9, continue to the next point
                    if (point.value == 9) continue;

                    // Check all horizontal and vertical neighbours for basin numbers; if it has a different number, use the highest number
                    // Check neighbour above
                    if (row != 0)
                    {
                        Point neighbour = input[row - 1][column];
                        if (point.basinNumber != neighbour.basinNumber)
                        {
                            if (point.basinNumber < neighbour.basinNumber)
                            {
                                point.basinNumber = neighbour.basinNumber;
                                numChanges++;
                            }
                        }
                    }
                    // Check left neighbour
                    if (column != 0)
                    {
                        Point neighbour = input[row][column - 1];
                        if (point.basinNumber != neighbour.basinNumber)
                        {
                            if (point.basinNumber < neighbour.basinNumber)
                            {
                                point.basinNumber = neighbour.basinNumber;
                                numChanges++;
                            }
                        }
                    }
                    // Check neighbour below
                    if (row != input.Length - 1)
                    {
                        Point neighbour = input[row + 1][column];
                        if (point.basinNumber != neighbour.basinNumber)
                        {
                            if (point.basinNumber < neighbour.basinNumber)
                            {
                                point.basinNumber = neighbour.basinNumber;
                                numChanges++;
                            }
                        }
                    }
                    // Check right neighbour
                    if (column != input[row].Length - 1)
                    {
                        Point neighbour = input[row][column + 1];
                        if (point.basinNumber != neighbour.basinNumber)
                        {
                            if (point.basinNumber < neighbour.basinNumber)
                            {
                                point.basinNumber = neighbour.basinNumber;
                                numChanges++;
                            }
                        }
                    }

                    // Check all diagonal neighbours, only if they aren't seperated from the current point with 9's!
                    // Check neighbour above-left
                    if (row != 0 && column != 0)
                    {
                        Point neighbourAbove = input[row - 1][column];
                        Point neighbourLeft = input[row][column - 1];
                        Point neighbourDiagonal = input[row - 1][column - 1];
                        if (!(neighbourAbove.value == 9 && neighbourLeft.value == 9) && point.basinNumber != neighbourDiagonal.basinNumber)
                        {
                            if (point.basinNumber < neighbourDiagonal.basinNumber)
                            {
                                point.basinNumber = neighbourDiagonal.basinNumber;
                                numChanges++;
                            }
                        }
                    }
                    // Check neighbour above-right
                    if (column != input[row].Length - 1 && row != 0)
                    {
                        Point neighbourRight = input[row][column + 1];
                        Point neighbourAbove = input[row - 1][column];
                        Point neighbourDiagonal = input[row - 1][column + 1];
                        if (!(neighbourRight.value == 9 && neighbourAbove.value == 9) && point.basinNumber != neighbourDiagonal.basinNumber)
                        {
                            if (point.basinNumber < neighbourDiagonal.basinNumber)
                            {
                                point.basinNumber = neighbourDiagonal.basinNumber;
                                numChanges++;
                            }
                        }
                    }
                    // Check neighbour below-left
                    if (column != 0 && row != input.Length - 1)
                    {
                        Point neighbourLeft = input[row][column - 1];
                        Point neighbourBelow = input[row + 1][column];
                        Point neighbourDiagonal = input[row + 1][column - 1];
                        if (!(neighbourLeft.value == 9 && neighbourBelow.value == 9) && point.basinNumber != neighbourDiagonal.basinNumber)
                        {
                            if (point.basinNumber < neighbourDiagonal.basinNumber)
                            {
                                point.basinNumber = neighbourDiagonal.basinNumber;
                                numChanges++;
                            }
                        }
                    }
                    // Check neighbour below-right
                    if (row != input.Length - 1 && column != input[row].Length - 1)
                    {
                        Point neighbourBelow = input[row + 1][column];
                        Point neighbourRight = input[row][column + 1];
                        Point neighbourDiagonal = input[row + 1][column + 1];
                        if (!(neighbourBelow.value == 9 && neighbourRight.value == 9) && point.basinNumber != neighbourDiagonal.basinNumber)
                        {
                            if (point.basinNumber < neighbourDiagonal.basinNumber)
                            {
                                point.basinNumber = neighbourDiagonal.basinNumber;
                                numChanges++;
                            }
                        }
                    }
                }
            }

            // Keep joining basins until there are no changes (because all basins are already joined)
            if (numChanges != 0) JoinBasins(input);
        }
    }
}
