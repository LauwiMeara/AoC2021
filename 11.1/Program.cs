using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _11._1
{
    class Program
    {
        static void Main()
        {
            int[][] input = File.ReadAllLines("input.txt").Select(row => row.Select(energy => int.Parse(energy.ToString())).ToArray()).ToArray();
            int finalStep = 100;
            int numFlashes = 0;

            for (int step = 0; step < finalStep; step++)
            {
                List<(int, int)> flashed = new List<(int, int)>();

                for (int row = 0; row < input.Length; row++)
                {
                    for (int col = 0; col < input[row].Length; col++)
                    {
                        input[row][col]++;
                        
                        if (input[row][col] > 9 && !flashed.Contains((row, col)))
                        {
                            flashed.Add((row, col));
                            Flash(input, (row, col), flashed);
                        }
                    }
                }

                foreach ((int row, int col) f in flashed) 
                {
                    input[f.row][f.col] = 0;
                    numFlashes++;
                }
            }

            Console.WriteLine($"The number of flashes is {numFlashes}");
        }

        static void Flash(int[][] input, (int row, int col) f, List<(int, int)> flashed)
        {
            (int row, int col)[] adjacents = { (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1) };

            foreach ( (int row, int col) a in adjacents)
            {
                int row = f.row + a.row;
                int col = f.col + a.col;

                if (row >= 0 &&
                    row < input.Length &&
                    col >= 0 &&
                    col < input[f.row].Length)
                {
                    if (input[row][col] <= 9)
                    {
                        input[row][col]++;

                        if (input[row][col] > 9 && !flashed.Contains((row, col)))
                        {
                            flashed.Add((row, col));
                            Flash(input, (row, col), flashed);
                        }
                    }
                }
            }
        }
    }
}
