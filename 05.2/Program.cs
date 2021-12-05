using System;
using System.IO;
using System.Linq;

namespace _05._2
{
    class Program
    {
        static void Main()
        {
            string[][][] input = File.ReadAllLines("input.txt").Select(line => line.Split(" -> ").Select(xy => xy.Split(",")).ToArray()).ToArray();
            int[][] field = CreateField(input);

            foreach (string[][] line in input)
            {
                int x1 = int.Parse(line[0][0]);
                int y1 = int.Parse(line[0][1]);
                int x2 = int.Parse(line[1][0]);
                int y2 = int.Parse(line[1][1]);

                // If x1 and x2 are equal, the line is drawn on the y axis
                if (x1 == x2)
                {
                    int minY = y1 <= y2 ? y1 : y2;
                    int maxY = y1 > y2 ? y1 : y2;

                    for (int y = minY; y <= maxY; y++)
                    {
                        field[x1][y]++;
                    }
                }

                // If y1 and y2 are equal, the line is drawn on the x axis
                else if (y1 == y2)
                {
                    int minX = x1 <= x2 ? x1 : x2;
                    int maxX = x1 > x2 ? x1 : x2;

                    for (int x = minX; x <= maxX; x++)
                    {
                        field[x][y1]++;
                    }
                }

                // If neither x1 and x2, nor y1 and y2 are equal, the line is drawn diagonally
                else
                {
                    int diff = Math.Abs(x1 - x2);

                    for (int i = 0; i <= diff; i++)
                    {
                        if (x1 < x2 && y1 < y2)
                        {
                            field[x1 + i][y1 + i]++;
                        }
                        else if (x1 < x2 && y1 > y2)
                        {
                            field[x1 + i][y1 - i]++;
                        }
                        else if(x1 > x2 && y1 < y2)
                        {
                            field[x1 - i][y1 + i]++;
                        }
                        else
                        {
                            field[x1 - i][y1 - i]++;
                        }
                    }
                }
            }

            int numOverlappingPoints = field.SelectMany(xy => xy).Aggregate(0, (sum, xy) => xy >= 2 ? sum + 1 : sum);

            Console.WriteLine($"There are {numOverlappingPoints} dangerous areas.");
        }

        static int[][] CreateField(string[][][] input)
        {
            int maxValue = input.SelectMany(line => line.SelectMany(xy => xy)).Select(xy => int.Parse(xy)).Max();
            int[][] field = new int[maxValue + 1][];
            for (int i = 0; i < field.Length; i++)
            {
                field[i] = new int[maxValue + 1];
            }

            return field;
        }
    }
}
