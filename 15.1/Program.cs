using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _15._1
{
    class Program
    {
        static void Main()
        {
            int[][] cavern = File.ReadAllLines("input.txt").Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

            (int x, int y) startNode = (0, 0);
            (int x, int y) endNode = (cavern.Length - 1, cavern[0].Length - 1);

            List<Path> frontier = new List<Path>() {new Path(new (int x, int y)[] { startNode } , 0, GetHValue(endNode, startNode.x, startNode.y)) };
            Dictionary<(int, int), int> checkedNodes = new Dictionary<(int, int), int>();

            bool reachedEndNode = false;
            while (!reachedEndNode)
            {
                SearchPath(cavern, endNode, frontier, checkedNodes);
                (int x, int y) currentNode = frontier[0].nodes.Last();
                Console.WriteLine(currentNode);

                // If the end node is reached...
                if (currentNode == endNode)
                {
                    // ... print the path cost and end the while loop
                    int pathCost = frontier[0].cost;
                    Console.WriteLine($"The lowest risk is {pathCost}");
                    reachedEndNode = true;
                }
            }
        }

        static void SearchPath(int[][] cavern, (int x, int y) endNode, List<Path> frontier, Dictionary<(int, int), int> checkedNodes)
        {
            // Get the current path and remove it from the frontier
            Path currentPath = frontier[0];
            frontier.RemoveAt(0);

            // Get the current node
            (int x, int y) currentNode = currentPath.nodes.Last();

            // Check neighbours
            (int x, int y)[] neighbours = { (-1, 0), (0, 1), (1, 0), (0, -1) };
            foreach ((int x, int y) neighbour in neighbours)
            {
                int x = currentNode.x + neighbour.x;
                int y = currentNode.y + neighbour.y;

                if (x >= 0 && x < cavern.Length && y >= 0 && y < cavern[x].Length && !currentPath.nodes.Contains((x, y))) // Cycle pruning
                {
                    int pathCost = currentPath.cost + cavern[x][y];
                    
                    if (checkedNodes.ContainsKey((x, y)) && checkedNodes[(x, y)] > pathCost) // Multiple path pruning
                    {
                        frontier.RemoveAll(path => path.nodes.Contains((x, y)));
                    }

                    if (!(checkedNodes.ContainsKey((x, y)) && checkedNodes[(x, y)] <= pathCost))
                    {
                        (int x, int y)[] nodes = new (int x, int y)[currentPath.nodes.Length + 1];
                        for (int i = 0; i < currentPath.nodes.Length; i++)
                        {
                            nodes[i] = currentPath.nodes[i];
                        }
                        nodes[^1] = (x, y);

                        Path path = new Path(nodes, pathCost, GetHValue(endNode, x, y));

                        checkedNodes[(x, y)] = pathCost;

                        frontier.Add(path);
                    }
                }
            }

            // Sort frontier by cost + hvalue (A*)
            frontier.Sort((path1, path2) =>
            {
                int value1 = path1.cost + path1.hvalue;
                int value2 = path2.cost + path2.hvalue;
                return value1.CompareTo(value2);
            });
        }

        static int GetHValue((int x, int y) endNode, int x, int y)
        {
            // Heuristic value is the minimal number of steps between the node and the end node
            return endNode.x - x + endNode.y - y;
        }

        public class Path
        {
            public (int x, int y)[] nodes;
            public int cost;
            public int hvalue;

            public Path ((int x, int y)[] nodes, int cost, int hvalue)
            {
                this.nodes = nodes;
                this.cost = cost;
                this.hvalue = hvalue;
            }
        }
    }
}
