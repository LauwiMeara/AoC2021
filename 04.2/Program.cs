using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _04._2
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n").ToArray();
            string[] draws = input[0].Split(',').ToArray();
            List<string[][]> boards = input[1..].Select(board => board.Split("\r\n").Select(column => column.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray()).ToList();


            for (int i = 0; i < draws.Length; i++)
            {
                // Get all boards that have bingo and remove them from the list, unless it's the last bingo board
                while (GetWinningBoard(boards) != boards.Count && boards.Count != 1)
                {
                    boards.RemoveAt(GetWinningBoard(boards));
                }

                // If the final board has bingo, calculate the score
                if (boards.Count == 1 && GetWinningBoard(boards) == 0)
                {
                    string[][] board = boards[0];

                    int sum = board.SelectMany(x => x).Sum(x => x != "#" ? int.Parse(x) : 0);
                    int calledNumber = int.Parse(draws[i - 1]);

                    Console.WriteLine($"The score is {sum * calledNumber}");
                    break;
                }

                // Mark the drawn number
                foreach (string[][] board in boards)
                {
                    for (int row = 0; row < board.Length; row++)
                    {
                        for (int column = 0; column < board[row].Length; column++)
                        {
                            if (board[row][column] == draws[i]) board[row][column] = "#";
                        }
                    }
                }
            }
        }

        static int GetWinningBoard(List<string[][]> boards)
        {
            // If none of the boards has bingo, the returned value of winningBoard will be 1 higher than the last index of boards
            int winningBoard = boards.Count;

            for (int i = 0; i < boards.Count; i++)
            {
                string[][] board = boards[i];

                // Check every row for bingo
                for (int row = 0; row < board.Length; row++)
                {
                    if (board[row].Where(column => column == "#").Count() == board[row].Length) winningBoard = i;
                }

                // Check every column for bingo
                for (int column = 0; column < board[0].Length; column++)
                {
                    if (board.Where(row => row[column] == "#").Count() == board[0].Length) winningBoard = i;
                }
            }

            return winningBoard;
        }
    }
}
