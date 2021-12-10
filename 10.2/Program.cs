using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10._2
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");
            char[] openingChunks = new char[] { '(', '[', '{', '<' };
            List<long> scores = new List<long>();

            foreach (string line in input)
            {
                bool corrupted = false;

                Stack<char> chunks = new Stack<char>();

                foreach (char chunk in line)
                {
                    if (chunks.Count == 0)
                    {
                        chunks.Push(chunk);
                        continue;
                    }

                    if (openingChunks.Contains(chunk))
                    {
                        chunks.Push(chunk);
                        continue;
                    }

                    char prevChunk = chunks.Peek();

                    if ((prevChunk == '(' && chunk == ')') ||
                        (prevChunk == '[' && chunk == ']') ||
                        (prevChunk == '{' && chunk == '}') ||
                        (prevChunk == '<' && chunk == '>'))
                    {
                        chunks.Pop();
                        continue;
                    }

                    corrupted = true;
                    break;
                }

                if (!corrupted)
                {
                    long subScore = 0;

                    while (chunks.Count != 0)
                    {
                        subScore *= 5;

                        switch (chunks.Peek())
                        {
                            case '(':
                                subScore += 1;
                                break;
                            case '[':
                                subScore += 2;
                                break;
                            case '{':
                                subScore += 3;
                                break;
                            case '<':
                                subScore += 4;
                                break;
                        }

                        chunks.Pop();
                    }

                    scores.Add(subScore);
                }
            }

            scores.Sort();
            long totalScore = scores[(scores.Count) / 2];

            Console.WriteLine($"My score is {totalScore}!");
        }
    }
}
