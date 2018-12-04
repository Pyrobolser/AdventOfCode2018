using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace AdventOfCode
{
    public class Claim
    {
        public int Id { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public HashSet<Coord> Coords { get; private set; }

        public Claim(int id, int x, int y, int width, int height)
        {
            Id = id;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Coords = new HashSet<Coord>();
            for(int i = x + 1; i <= x + width; i++)
            {
                for(int j = y + 1; j <= y + height; j++)
                {
                    Coords.Add(new Coord(i, j));
                }
            }
        }
    }

    public struct Coord
    {
        public int X, Y;

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }


    public static class Day3Part1
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day3.txt");

        public static void Solve()
        {
            var claims = new List<Claim>();
            var coords = new List<Coord>();
            foreach(string line in InputLines)
            {
                MatchCollection matches = Regex.Matches(line, @"\d+");
                claims.Add(
                    new Claim(int.Parse(matches[0].Value),
                              int.Parse(matches[1].Value),
                              int.Parse(matches[2].Value),
                              int.Parse(matches[3].Value),
                              int.Parse(matches[4].Value)
                             )
                          );
            }

            foreach(Claim c in claims)
            {
                coords.AddRange(c.Coords);
            }

            int result = coords.GroupBy(c => c).Where(g => g.Count() > 1).Count();

            Console.WriteLine($"There are { result } square inches of fabric within two or more claims.");
        }
    }

    public static class Day3Part2
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day3.txt");

        public static void Solve()
        {
            var claims = new List<Claim>();
            var coords = new List<Coord>();
            foreach (string line in InputLines)
            {
                MatchCollection matches = Regex.Matches(line, @"\d+");
                claims.Add(
                    new Claim(int.Parse(matches[0].Value),
                              int.Parse(matches[1].Value),
                              int.Parse(matches[2].Value),
                              int.Parse(matches[3].Value),
                              int.Parse(matches[4].Value)
                             )
                          );
            }

            foreach (Claim c in claims)
            {
                coords.AddRange(c.Coords);
            }

            var alone = coords.GroupBy(c => c).Where(g => g.Count() == 1).Select(g => g.First()).ToHashSet();

            int result = 0;
            int count = 0;
            foreach(Claim c in claims)
            {
                count = c.Coords.Count;
                c.Coords.IntersectWith(alone);
                if (c.Coords.Count == count)
                {
                    result = c.Id;
                    break;
                }
            }

            Console.WriteLine($"The ID of the only claim that doesn't overlap is { result }.");
        }
    }
}
