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

                    illegalChunks[chunk]++;
                    break;
                }
            }

            int score = (illegalChunks[')'] * 3) + (illegalChunks[']'] * 57) + (illegalChunks['}'] * 1197) + (illegalChunks['>'] * 25137);
            Console.WriteLine($"My score is {score}!");
        }
    }
}
