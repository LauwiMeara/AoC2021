using System;
using System.IO;
using System.Linq;

namespace _04._1
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n").ToArray();
            string[] draws = input[0].Split(',').ToArray();
            string[][][] boards = input[1..].Select(board => board.Split("\r\n").Select(column => column.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray()).ToArray();

            for (int i = 0; i < draws.Length; i++)
            {
                int winningBoard = GetWinningBoard(boards);

                // If a board has bingo, calculate the score
                if (winningBoard != boards.Length)
                {
                    string[][] board = boards[winningBoard];

                    int sum = board.SelectMany(x => x).Sum(x => x != "#" ? int.Parse(x) : 0);
                    int calledNumber = int.Parse(draws[i - 1]);

                    Console.WriteLine($"The score is {sum * calledNumber}");
                    break;
                }

                // Else, mark the drawn number
                else
                {
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
        }

        static int GetWinningBoard(string[][][] boards)
        {
            // If none of the boards has bingo, the returned value of winningBoard will be 1 higher than the last index of boards
            int winningBoard = boards.Length;

            for (int i = 0; i < boards.Length; i++)
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
