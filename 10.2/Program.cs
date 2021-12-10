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

                List<char> chunks = new List<char>
                {
                    line[0]
                };

                for (int i = 1; i < line.Length; i++)
                {
                    char chunk = line[i];

                    if (openingChunks.Contains(chunk))
                    {
                        chunks.Add(chunk);
                        continue;
                    }

                    char prevChunk = chunks.Last();

                    if ((prevChunk == '(' && chunk == ')') ||
                        (prevChunk == '[' && chunk == ']') ||
                        (prevChunk == '{' && chunk == '}') ||
                        (prevChunk == '<' && chunk == '>'))
                    {
                        chunks.RemoveAt(chunks.Count - 1);
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

                        switch (chunks.Last())
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

                        chunks.RemoveAt(chunks.Count - 1);
                    }

                    scores.Add(subScore);
                }
            }

            scores.Sort();
            long totalScore = scores[(scores.Count - 1) / 2];

            Console.WriteLine($"My score is {totalScore}!");
        }
    }
}
