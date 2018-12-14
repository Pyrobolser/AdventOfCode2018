using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day12Part1
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day12.txt");

        public static void Solve()
        {
            string plants = InputLines[0].Remove(0, 15);
            HashSet<string> rules = new HashSet<string>();
            for (int i = 2; i < InputLines.Length; i++)
            {
                if (InputLines[i].Last() == '#')
                {
                    rules.Add(InputLines[i].Remove(5));
                }
            }
            
            string previous = $"...{ plants }...";
            StringBuilder next = null;
            for (int i = 0; i < 20; i++)
            {
                next = new StringBuilder();
                for (int p = 2; p < previous.Length - 2; p++)
                {
                    if (rules.Any(r => r == previous.Substring(p - 2, 5)))
                    {
                        next.Append('#');
                    }
                    else
                    {
                        next.Append('.');
                    }
                }
                previous = next.ToString();
                previous = $"...{ previous }...";
            }

            int result = 0;
            for (int i = 0; i < previous.Length; i++)
            {
                result += previous[i] == '#' ? i - 23 : 0;
            }

            Console.WriteLine($"After 20 generations, the sum of the numbers of all pots which contain a plant is { result }.");
        }
    }

    public class Day12Part2
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day12.txt");

        public static void Solve()
        {
            string plants = InputLines[0].Remove(0, 15);
            HashSet<string> rules = new HashSet<string>();
            for (int i = 2; i < InputLines.Length; i++)
            {
                if (InputLines[i].Last() == '#')
                {
                    rules.Add(InputLines[i].Remove(5));
                }
            }

            string previous = $"...{ plants }...";
            StringBuilder next = null;
            long result = 0, prevResult = 0;
            int minus = 3;
            for (long i = 0; i < 50000000000; i++)
            {
                next = new StringBuilder();
                for (int p = 2; p < previous.Length - 2; p++)
                {
                    if (rules.Any(r => r == previous.Substring(p - 2, 5)))
                    {
                        next.Append('#');
                    }
                    else
                    {
                        next.Append('.');
                    }
                }
                previous = next.ToString();
                previous = $"...{ previous }...";
                minus++;
                result = 0;
                for (int j = 0; j < previous.Length; j++)
                {
                    result += previous[j] == '#' ? j - minus : 0;
                }

                if (result - prevResult == 69)
                {
                    // After looking at the result difference in high iterations, it stabilize around 69 with my puzzle input.
                    result = result + (50000000000 - (i + 1)) * 69;
                    break;
                }
                prevResult = result;
            }

            Console.WriteLine($"After fifty billion (50000000000) generations, the sum of the numbers of all pots which contain a plant is { result }.");
        }
    }
}
