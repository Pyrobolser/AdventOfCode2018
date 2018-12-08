using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Node
    {
        public List<int> Metadata { get; private set; } = new List<int>();
        public List<Node> Childnodes { get; private set; } = new List<Node>();
        public static int Total { get; private set; }
        
        public static void Unroot()
        {
            //Bye tree
            Total = 0;
            index = 0;
        }

        public int Value()
        {
            int result = 0;
            if (Childnodes.Count == 0)
            {
                result = Metadata.Sum();
            }
            else
            {
                foreach(int m in Metadata)
                {
                    result += Childnodes.ElementAtOrDefault(m - 1)?.Value() ?? 0;
                }
            }

            return result;
        }

        public static Node BuildTree(int[] input)
        {
            var current = new Node();
            int childnodes = input[index++];
            int metadata = input[index++];

            for (int j = 0; j < childnodes; j++)
            {
                current.Childnodes.Add(BuildTree(input));
            }

            for (int j = 0; j < metadata; j++)
            {
                Total += input[index];
                current.Metadata.Add(input[index++]);
            }

            return current;
        }

        private static int index = 0;
    }

    public static class Day8Part1
    {
        private static int[] InputNumbers => File.ReadAllText(@"Inputs\Day8.txt").Split(' ').Select(s => int.Parse(s)).ToArray();

        public static void Solve()
        {
            var tree = Node.BuildTree(InputNumbers);

            Console.WriteLine($"The sum of all the metadata entries is { Node.Total }.");
        }
    }

    public static class Day8Part2
    {
        private static int[] InputNumbers => File.ReadAllText(@"Inputs\Day8.txt").Split(' ').Select(s => int.Parse(s)).ToArray();

        public static void Solve()
        {
            Node.Unroot();
            var tree = Node.BuildTree(InputNumbers);
            int result = tree.Value();

            Console.WriteLine($"The value of the root node is { result }.");
        }
    }
}
