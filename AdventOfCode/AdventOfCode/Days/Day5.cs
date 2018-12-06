using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public static class Day5Part1
    {
        private static string InputLine => File.ReadAllText(@"Inputs\Day5.txt");

        public static void Solve()
        {
            var polymer = new Stack<char>();
            foreach (char c in InputLine)
            {
                if (polymer.Count == 0)
                {
                    polymer.Push(c);
                }
                else
                {
                    if (Math.Abs(polymer.Peek() - c) != 32)
                    {
                        polymer.Push(c);
                    }
                    else
                    {
                        polymer.Pop();
                    }
                }
            }

            Console.WriteLine($"After all possible reactions, the resulting polymer contains { polymer.Count } units.");
        }
    }

    public static class Day5Part2
    {
        private static string InputLine => File.ReadAllText(@"Inputs\Day5.txt");

        public static void Solve()
        {
            var min = InputLine.Length;
            for (char unit = 'A'; unit <= 'Z'; unit++)
            {
                var input = InputLine.Replace(unit.ToString(), string.Empty).Replace(unit.ToString().ToLowerInvariant(), string.Empty);
                var polymer = new Stack<char>();
                foreach (char c in input)
                {
                    if (polymer.Count == 0)
                    {
                        polymer.Push(c);
                    }
                    else
                    {
                        if (Math.Abs(polymer.Peek() - c) != 32)
                        {
                            polymer.Push(c);
                        }
                        else
                        {
                            polymer.Pop();
                        }
                    }
                }

                if (polymer.Count < min)
                    min = polymer.Count;
            }

            Console.WriteLine($"The length of the shortest polymer is { min } units.");
        }
    }
}
