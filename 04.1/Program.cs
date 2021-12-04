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
            
            
            foreach (string number in draws)
            {
                int bingoBoard = BoardIsBingo(boards);

                if (bingoBoard == boards.Length)
                {
                    foreach (string[][] board in boards)
                    {
                        for (int row = 0; row < board.Length; row++)
                        {
                            for (int column = 0; column < board[row].Length; column++)
                            {
                                if (board[row][column] == number) board[row][column] = "#";
                            }
                        }
                    }
                }
                else
                {
                    string[][] board = boards[bingoBoard];
                    int sum = 0;

                    for (int row = 0; row < board.Length; row++)
                    {
                        for (int column = 0; column < board[row].Length; column++)
                        {
                            if (board[row][column] != "#") sum += int.Parse(board[row][column]);
                        }
                    }

                    int calledNumber = int.Parse(draws[Array.IndexOf(draws, number) - 1]);

                    int score = sum * calledNumber;
                    Console.WriteLine($"The score is {score}");
                    break;
                }
            }
        }

        static int BoardIsBingo(string[][][] boards)
        {
            int boardIsBingo = boards.Length;

            for (int i = 0; i < boards.Length; i++)
            {
                string[][] board = boards[i];

                // Check every row
                for (int row = 0; row < board.Length; row++)
                {
                    int markedNumbers = 0;

                    for (int column = 0; column < board[row].Length; column++)
                    {
                        if (board[row][column] == "#") markedNumbers++;
                    }

                    if (markedNumbers == board[row].Length)
                    {
                        boardIsBingo = i;
                        break;
                    }
                }

                // Check every column
                for (int column = 0; column < board[0].Length; column++)
                {
                    int markedNumbers = 0;

                    for (int row = 0; row < board.Length; row++)
                    {
                        if (board[row][column] == "#") markedNumbers++;
                    }

                    if (markedNumbers == board.Length)
                    {
                        boardIsBingo = i;
                        break;
                    }
                }
            }

            return boardIsBingo;
        }
    }
}
