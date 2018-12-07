using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Day6Part1
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day6.txt");
        public static void Solve()
        {
            var points = InputLines.Select(s => Regex.Matches(s, @"\d+")).Select(m => (x: int.Parse(m[0].Value), y: int.Parse(m[1].Value))).ToArray();
            int xMax = points.Max(p => p.x);
            int yMax = points.Max(p => p.y);
            HashSet<int> infinite = new HashSet<int>();
            Dictionary<int, int> areas = Enumerable.Range(0, InputLines.Length).ToDictionary(d => d, d => 0);
            for (int x = 0; x <= xMax; x++)
            {
                for(int y = 0; y <= yMax; y++)
                {
                    var distances = points.Select((p, idx) => (idx, dist: Math.Abs(p.x - x) + Math.Abs(p.y - y))).OrderBy(d => d.dist).ToArray();

                    if (distances[0].dist < distances[1].dist)
                    {
                        areas[distances[0].idx]++;

                        if (x == 0 || y == 0 || x == xMax || y == yMax)
                            infinite.Add(distances[0].idx);
                    }
                }
            }

            var result = areas.Where(a => !infinite.Contains(a.Key)).OrderByDescending(a => a.Value).First();

            Console.WriteLine($"The size of the largest area that isn't infinite is { result.Value }.");
        }
    }

    public static class Day6Part2
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day6.txt");
        public static void Solve()
        {
            var points = InputLines.Select(s => Regex.Matches(s, @"\d+")).Select(m => (x: int.Parse(m[0].Value), y: int.Parse(m[1].Value))).ToArray();
            int xMax = points.Max(p => p.x);
            int yMax = points.Max(p => p.y);
            int area = 0;
            for (int x = 0; x <= xMax; x++)
            {
                for (int y = 0; y <= yMax; y++)
                {
                    var distances = points.Select((p, idx) => (idx, dist: Math.Abs(p.x - x) + Math.Abs(p.y - y)));

                    if (distances.Sum(d => d.dist) < 10000)
                    {
                        area++;
                    }
                }
            }

            Console.WriteLine($"The size of the region containing all locations which have a total distance to all given coordinates of less than 10000 is { area }.");
        }
    }
}
