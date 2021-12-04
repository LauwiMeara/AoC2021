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
            
            
            foreach (string number in draws)
            {
                // If a board is bingo, but there are other boards left, remove the board from the list
                // Check with the while loop to see if there are other boards that simultaneously are bingo
                while (BoardIsBingo(boards) != boards.Count && boards.Count != 1)
                {
                    boards.RemoveAt(BoardIsBingo(boards));
                }

                int bingoBoard = BoardIsBingo(boards);

                // If the final board is bingo, calculate the score
                if (boards.Count == 1 && bingoBoard == 0)
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

                // Mark the drawn number
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
        }

        static int BoardIsBingo(List<string[][]> boards)
        {
            int boardIsBingo = boards.Count;

            for (int i = 0; i < boards.Count; i++)
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
