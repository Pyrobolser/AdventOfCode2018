using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Star
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int VelX { get; set; }
        public int VelY { get; set; }

        public Star(int posX, int posY, int velX, int velY)
        {
            PosX = posX;
            PosY = posY;
            VelX = velX;
            VelY = velY;
        }

        public void Move()
        {
            PosX += VelX;
            PosY += VelY;
        }
    }

    public static class Day10
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day10.txt");

        public static void Solve()
        {
            List<Star> stars = new List<Star>();
            foreach (string line in InputLines)
            {
                MatchCollection matches = Regex.Matches(line, @"[-]?\d+");
                stars.Add(new Star(int.Parse(matches[0].Value), int.Parse(matches[1].Value), int.Parse(matches[2].Value), int.Parse(matches[3].Value)));
            }
            int minX = 0, maxX = 0, minY = 0, maxY = 0, time = 0;
            while(true)
            {
                maxX = stars.Max(s => s.PosX);
                minX = stars.Min(s => s.PosX);
                maxY = stars.Max(s => s.PosY);
                minY = stars.Min(s => s.PosY);

                stars.ForEach(s => s.Move());
                time++;
                if (maxX - minX < 120)
                {
                    Console.WriteLine($"Seconds: { time }s");
                    for (var y = minY - 1 ; y <= maxY + 1; y++)
                    {
                        for (var x = minX - 1; x <= maxX + 1; x++)
                        {
                            Console.Write(stars.Any(s => s.PosX == x && s.PosY == y) ? '#' : '.');
                        }
                        Console.WriteLine();
                    }
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
