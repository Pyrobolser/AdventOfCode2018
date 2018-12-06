using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Guard
    {
        public int Id { get; private set; }
        public Dictionary<int, int> Sleep { get; private set; }

        public Guard(int id)
        {
            Id = id;
            Sleep = Enumerable.Range(0, 60).ToDictionary(d => d, d => 0);
        }
    }

    public static class Day4Part1
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day4.txt").OrderBy(s => s).ToArray();

        public static void Solve()
        {
            HashSet<Guard> guards = new HashSet<Guard>();
            Guard guard = null;
            int start = 0, end = 0;
            foreach (string line in InputLines)
            {
                Match match = Regex.Match(line, @":(\d+)\] (Guard|falls|wakes) #?(\d+)?");
                switch (match.Groups[2].Value)
                {
                    case "Guard":
                        guard = guards.FirstOrDefault(g => g.Id == int.Parse(match.Groups[3].Value));
                        if (guard == null)
                        {
                            guard = new Guard(int.Parse(match.Groups[3].Value));
                            guards.Add(guard);
                        }
                        break;
                    case "falls":
                        start = int.Parse(match.Groups[1].Value);
                        break;
                    case "wakes":
                        end = int.Parse(match.Groups[1].Value);
                        for (int i = start; i < end; i++)
                            guard.Sleep[i]++;
                        break;
                }
            }

            var sleepy = guards.OrderByDescending(g => g.Sleep.Sum(s => s.Value)).First();
            var most = sleepy.Sleep.OrderByDescending(g => g.Value).Select(s => s.Key).First();

            Console.WriteLine($"The ID of the guard multiplied by the minute is { sleepy.Id * most }.");
        }
    }

    public static class Day4Part2
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day4.txt").OrderBy(s => s).ToArray();

        public static void Solve()
        {
            HashSet<Guard> guards = new HashSet<Guard>();
            Guard guard = null;
            int start = 0, end = 0;
            foreach (string line in InputLines)
            {
                Match match = Regex.Match(line, @":(\d+)\] (Guard|falls|wakes) #?(\d+)?");
                switch (match.Groups[2].Value)
                {
                    case "Guard":
                        guard = guards.FirstOrDefault(g => g.Id == int.Parse(match.Groups[3].Value));
                        if (guard == null)
                        {
                            guard = new Guard(int.Parse(match.Groups[3].Value));
                            guards.Add(guard);
                        }
                        break;
                    case "falls":
                        start = int.Parse(match.Groups[1].Value);
                        break;
                    case "wakes":
                        end = int.Parse(match.Groups[1].Value);
                        for (int i = start; i < end; i++)
                            guard.Sleep[i]++;
                        break;
                }
            }

            var sleepy = guards.OrderByDescending(g => g.Sleep.Max(s => s.Value)).First();
            var most = sleepy.Sleep.OrderByDescending(g => g.Value).Select(s => s.Key).First();

            Console.WriteLine($"The ID of the guard multiplied by the minute is { sleepy.Id * most }.");
        }
    }
}
