using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _12._2
{
    class Program
    {
        static void Main()
        {
            string[][] input = File.ReadAllLines("input.txt").Select(line => line.Split("-")).ToArray();
            List<string> paths = new List<string>();
            string p = "";

            Dictionary<string, List<string>> connections = GetConnections(input);
            GetPaths(paths, p, connections, "start");

            Console.WriteLine($"The number of paths is {paths.Count()}");
        }

        static Dictionary<string, List<string>> GetConnections(string[][] input)
        {
            Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
            
            foreach (string[] line in input)
            {
                // A connection can be travelled both ways, so add them both ways
                // Exceptions: start and end

                if (connections.ContainsKey(line[0])) connections[line[0]].Add(line[1]);
                else if (line[0] != "end" && line[1] != "start") connections.Add(line[0], new List<string> { line[1] });

                if (connections.ContainsKey(line[1])) connections[line[1]].Add(line[0]);
                else if (line[0] != "start" && line[1] != "end") connections.Add(line[1], new List<string> { line[0] });
            }

            return connections;
        }

        static void GetPaths(List<string> paths, string p, Dictionary<string, List<string>> connections, string c)
        {
            // If c is the start and we've already started, don't do anything with the path
            if (!(c == "start" && p.Contains(c)))
            {
                // If c is the end, add the path to the list of paths
                if (c == "end")
                {
                    p += " end ";
                    paths.Add(p);
                }
                // If c is a small cave that's already been visited...
                else if (c == c.ToLower() && p.Contains($" {c} "))
                {
                    // ...check if any small cave has already been visited twice
                    bool visitedSmallCaveTwice = p.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Where(c => c == c.ToLower() && c != "start" && c != "end")
                        .GroupBy(c => c)
                        .Any(group => group.Count() == 2);

                    // Only continue with the path if c is the first small cave to be visited twice
                    if (!visitedSmallCaveTwice)
                    {
                        p += $" {c} ";
                        foreach (string connection in connections[c])
                        {
                            GetPaths(paths, p, connections, connection);
                        }
                    }
                }
                else
                {
                    p += $" {c} ";
                    foreach (string connection in connections[c])
                    {
                        GetPaths(paths, p, connections, connection);
                    }
                }
            }
        }
    }
}
