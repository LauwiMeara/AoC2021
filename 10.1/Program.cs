using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10._1
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");
            char[] openingChunks = new char[] { '(', '[', '{', '<' };
            Dictionary<char, int> illegalChunks = new Dictionary<char, int>
            {
                { ')', 0 },
                { ']', 0 },
                { '}', 0 },
                { '>', 0 }
            };

            foreach (string line in input)
            {
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

                    illegalChunks[chunk]++;
                    break;
                }
            }

            int score = (illegalChunks[')'] * 3) + (illegalChunks[']'] * 57) + (illegalChunks['}'] * 1197) + (illegalChunks['>'] * 25137);
            Console.WriteLine($"My score is {score}!");
        }
    }
}
