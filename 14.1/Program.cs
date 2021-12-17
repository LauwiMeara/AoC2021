using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _14._1
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllText("input.txt")
                .Split($"{Environment.NewLine}{Environment.NewLine}");
            string template = input[0];
            Dictionary<string, string> rules = input[1]
                .Split(Environment.NewLine)
                .Select(line => line.Split(" -> "))
                .ToDictionary(line => line[0], line => line[1]);

            int finalStep = 10;

            for (int step = 0; step < finalStep; step++)
            {
                string newTemplate = "";

                for (int i = 1; i < template.Length; i++)
                {
                    newTemplate += template[i - 1];

                    string pair = $"{template[i - 1]}{template[i]}";
                    if (rules.ContainsKey(pair))
                    {
                        newTemplate += rules[pair];
                    }
                }

                template = newTemplate += template[^1];
            }

            var groupedTemplate = template.GroupBy(element => element).OrderBy(group => group.Count());
            int numLeastCommonElement = groupedTemplate.First().Count();
            int numMostCommonElement = groupedTemplate.Last().Count();

            Console.WriteLine($"The result is {numMostCommonElement - numLeastCommonElement}");
        }
    }
}
