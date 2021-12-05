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
            
            int maxValue = input.SelectMany(line => line.SelectMany(xy => xy)).Select(xy => int.Parse(xy)).Max();
            int[][] field = new int[maxValue + 1][];
            for (int i = 0; i < field.Length; i++)
            {
                field[i] = new int[maxValue + 1];
            }

            foreach (string[][] line in input)
            {
                int x1 = int.Parse(line[0][0]);
                int y1 = int.Parse(line[0][1]);
                int x2 = int.Parse(line[1][0]);
                int y2 = int.Parse(line[1][1]);

                if (x1 == x2)
                {
                    if (y1 <= y2)
                    {
                        for (int y = y1; y <= y2; y++)
                        {
                            field[x1][y]++;
                        }
                    }
                    else
                    {
                        for (int y = y2; y <= y1; y++)
                        {
                            field[x1][y]++;
                        }
                    }
                }

                else if (y1 == y2)
                {
                    if (x1 <= x2)
                    {
                        for (int x = x1; x <= x2; x++)
                        {
                            field[x][y1]++;
                        }
                    }
                    else
                    {
                        for (int x = x2; x <= x1; x++)
                        {
                            field[x][y1]++;
                        }
                    }
                }

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

            int numOverlappingPoints = 0;

            for (int x = 0; x < field.Length; x++)
            {
                for (int y = 0; y < field[x].Length; y++)
                {
                    if (field[x][y] >= 2)
                    {
                        numOverlappingPoints++;
                    }
                }
            }

            Console.WriteLine($"There are {numOverlappingPoints} dangerous areas.");
        }
    }
}
