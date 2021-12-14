using System;
using System.IO;
using System.Linq;

namespace _13._2
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllText("input.txt").Split($"{Environment.NewLine}{Environment.NewLine}");
            int[][] coords = input[0]
                .Split(Environment.NewLine)
                .Select(line => line.Split(",")
                    .Select(number => int.Parse(number))
                    .ToArray())
                .ToArray();
            string[][] folds = input[1]
                .Split(Environment.NewLine)
                .Select(line => line[(line.LastIndexOf(" ") + 1)..].Split("="))
                .ToArray();

            int maxX = coords.Select(line => line[0]).Max() + 1;
            int maxY = coords.Select(line => line[1]).Max() + 1;
            char[][] paper = CreateEmptyPaper(maxX, maxY);
            FillPaper(coords, paper);

            for (int i = 0; i < folds.Length; i++)
            {
                string direction = folds[i][0];
                int foldLine = int.Parse(folds[i][1]);

                if (direction == "x")
                {
                    maxX = foldLine;
                    maxY = paper[0].Length;
                    char[][] foldedPaper = CreateEmptyPaper(maxX, maxY);

                    // Fill folded paper
                    for (int x = 0; x < paper.Length; x++)
                    {
                        for (int y = 0; y < paper[0].Length; y++)
                        {
                            // If x < maxX, the folded paper and previous paper are the same
                            if (x < maxX) foldedPaper[x][y] = paper[x][y];
                            // If x == maxX, we've reached the (empty) fold line; this won't be used
                            else if (x == maxX) continue;
                            // If x > maxX, check if the folded paper already is marked at these coords
                            // If not, mirror the value of the previous paper on the folded paper
                            else if (foldedPaper[maxX - (x - maxX)][y] != '#') foldedPaper[maxX - (x - maxX)][y] = paper[x][y];
                        }
                    }

                    paper = foldedPaper;
                }
                else
                {
                    maxX = paper.Length;
                    maxY = foldLine;
                    char[][] foldedPaper = CreateEmptyPaper(maxX, maxY);

                    // Fill folded paper
                    for (int x = 0; x < paper.Length; x++)
                    {
                        for (int y = 0; y < paper[0].Length; y++)
                        {
                            // If y < maxY, the folded paper and previous paper are the same
                            if (y < maxY) foldedPaper[x][y] = paper[x][y];
                            // If y == maxY, we've reached the (empty) fold line; this won't be used
                            else if (y == maxY) continue;
                            // If y > maxY, check if the folded paper already is marked at these coords
                            // If not, mirror the value of the previous paper on the folded paper
                            else if (foldedPaper[x][maxY - (y - maxY)] != '#') foldedPaper[x][maxY - (y - maxY)] = paper[x][y];
                        }
                    }

                    paper = foldedPaper;
                }
            }

            PrintPaper(paper);
        }

        static char[][] CreateEmptyPaper(int maxX, int maxY)
        {
            char[][] paper = new char[maxX][];
            for (int x = 0; x < paper.Length; x++)
            {
                paper[x] = new char[maxY];
            }

            return paper;
        }

        static void FillPaper(int[][] coords, char[][] paper)
        {
            foreach (int[] xy in coords)
            {
                int x = xy[0];
                int y = xy[1];
                paper[x][y] = '#';
            }
        }

        static void PrintPaper(char[][] paper)
        {
            for (int y = 0; y < paper[0].Length; y++)
            {
                for (int x = 0; x < paper.Length; x++)
                {
                    char c = paper[x][y];
                    if (c == '#') Console.Write(c);
                    else Console.Write('.');
                }
                Console.WriteLine();
            }
        }
    }
}
