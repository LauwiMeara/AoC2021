using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _15._1
{
    class Program
    {
        public static void Main()
        {
            // Get input
            int[][] cavern = File.ReadAllLines("input.txt").Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

            // Create RecordKeeper
            RecordKeeper r = new RecordKeeper(cavern);

            // Search for end node
            bool reachedEndNode = false;
            while (!reachedEndNode)
            {
                GraphSearcher.SearchPath(cavern, r);
                r.SortFrontier();

                // Print to the console the (x, y) where the search will begin in the next iteration
                (int x, int y) lastNode = r.Frontier[0].Nodes.Last();
                Console.WriteLine(lastNode); 

                // If the end node is reached, print the path cost and end the while loop
                if (lastNode == r.EndNode)
                {
                    int pathCost = r.Frontier[0].Cost;
                    Console.WriteLine($"The lowest risk is {pathCost}");
                    reachedEndNode = true;
                }
            }
        }

        public class RecordKeeper
        {
            private readonly (int x, int y) _startNode = (0, 0);

            public (int x, int y) EndNode { get; }
            public List<Path> Frontier { get; set; }
            public Dictionary<(int, int), int> CheckedNodes { get; set; }

            public RecordKeeper(int[][] graph)
            {
                Frontier = new List<Path>();
                EndNode = (graph.Length - 1, graph[0].Length - 1);
                Frontier.Add(new Path(new (int x, int y)[] { _startNode }, 0, EndNode));
                CheckedNodes = new Dictionary<(int, int), int>();
            }

            public void SortFrontier()
            {
                // Sort frontier by cost + hvalue (A*)
                Frontier.Sort((path1, path2) =>
                {
                    int value1 = path1.Cost + path1.Hvalue;
                    int value2 = path2.Cost + path2.Hvalue;
                    return value1.CompareTo(value2);
                });
            }
        }

        public class GraphSearcher
        {
            public static void SearchPath(int[][] cavern, RecordKeeper r)
            {
                Path currentPath = GetCurrentPath(r);
                (int x, int y) lastNode = currentPath.Nodes.Last();

                // Check neighbours
                (int x, int y)[] neighbours = { (-1, 0), (0, 1), (1, 0), (0, -1) };
                foreach ((int x, int y) neighbour in neighbours)
                {
                    (int x, int y) currentNode = (lastNode.x + neighbour.x, lastNode.y + neighbour.y);

                    // If the currentNode is outside of the cavern, or if the currentNode already exists in the currentPath, check the next neighbour
                    // The last check is to prevent cycles (cycle pruning)
                    if (IsOutsideGraph(cavern, currentNode) || currentPath.Nodes.Contains(currentNode))
                    {
                        continue;
                    }

                    // Calculate pathCost
                    int pathCost = currentPath.Cost + cavern[currentNode.x][currentNode.y];

                    // If the currentNode is already checked in another path, and the currentPath costs more, check the next neighbour
                    if (r.CheckedNodes.ContainsKey(currentNode) && r.CheckedNodes[currentNode] <= pathCost)
                    {
                        continue;
                    }

                    // If the currentNode is already checked in another path, but the currentPath costs less, delete all other paths in the frontier with the currentNode (multiple math pruning)
                    if (r.CheckedNodes.ContainsKey(currentNode) && r.CheckedNodes[currentNode] > pathCost)
                    {
                        r.Frontier.RemoveAll(path => path.Nodes.Contains(currentNode));
                    }

                    // Add the currentNode and the pathCost to the checkedNodes
                    r.CheckedNodes[currentNode] = pathCost;

                    // Create a new path and add this to the frontier
                    (int x, int y)[] nodes = new (int x, int y)[currentPath.Nodes.Length + 1];
                    for (int i = 0; i < currentPath.Nodes.Length; i++)
                    {
                        nodes[i] = currentPath.Nodes[i];
                    }
                    nodes[^1] = currentNode;
                    Path path = new Path(nodes, pathCost, r.EndNode);
                    r.Frontier.Add(path);
                }
            }

            private static Boolean IsOutsideGraph(int[][] graph, (int x, int y) currentNode)
            {
                return currentNode.x < 0 || currentNode.x >= graph.Length || currentNode.y < 0 || currentNode.y >= graph[currentNode.x].Length;
            }

            private static Path GetCurrentPath(RecordKeeper r)
            {
                // The current path is the path with the least cost + hvalue. It is found at the first index in the (sorted) frontier list
                Path currentPath = r.Frontier[0];
                r.Frontier.RemoveAt(0);
                return currentPath;
            }
            
        }

        public class Path
        {
            public (int x, int y)[] Nodes { get; }
            public int Cost { get; }
            public int Hvalue { get; }

            public Path ((int x, int y)[] nodes, int cost, (int x, int y) endNode)
            {
                this.Nodes = nodes;
                this.Cost = cost;
                this.Hvalue = CalcHValue(endNode, nodes[^1]);
            }

            private int CalcHValue((int x, int y) endNode, (int x, int y) currentNode)
            {
                return endNode.x - currentNode.x + endNode.y - currentNode.y;
            }
        }
    }
}
