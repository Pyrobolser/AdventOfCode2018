using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day1Part1
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day1.txt");

        private static IEnumerable<int> CleanInput(string[] input)
        {
            foreach(string freq in input)
            {
                yield return freq.StartsWith('+') ? int.Parse(freq.Remove(0, 1)) : int.Parse(freq);
            }
        }

        public static void Solve()
        {
            var frequencies = CleanInput(InputLines);
            var total = frequencies.Sum();

            Console.WriteLine($"The resulting frequency is { total }.");
        }
    }

    public static class Day1Part2
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day1.txt");

        private static IEnumerable<int> CleanInput(string[] input)
        {
            foreach (string freq in input)
            {
                yield return freq.StartsWith('+') ? int.Parse(freq.Remove(0, 1)) : int.Parse(freq);
            }
        }

        private static int? FindDuplicate(IEnumerable<int> frequencies, List<int> duplicates)
        {
            int current = duplicates.Last();
            int? result = null;
            foreach(int freq in frequencies)
            {
                current += freq;
                if (duplicates.Contains(current))
                {
                    result = current;
                    break;
                }
                duplicates.Add(current);
            }

            if(result != null)
            {
                return result;
            }
            else
            {
                return FindDuplicate(frequencies, duplicates);
            }
        }

        public static void Solve()
        {
            IEnumerable<int> frequencies = CleanInput(InputLines);
            var duplicates = new List<int> { 0 };

            int? result = FindDuplicate(frequencies, duplicates);
            Console.WriteLine($"The first frequency reached twice is { result }.");
        }
    }
}
