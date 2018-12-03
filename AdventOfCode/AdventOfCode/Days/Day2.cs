using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Day2Part1
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day2.txt");

        public static void Solve()
        {
            var boxes2 = 0;
            var boxes3 = 0;
            
            foreach(var line in InputLines)
            {
                var grp = line.GroupBy(c => c).GroupBy(g => g.Count());

                boxes2 += grp.Count(g => g.Key == 2);

                boxes3 += grp.Count(g => g.Key == 3);
            }

            Console.WriteLine($"The box IDs list checksum is { boxes2 * boxes3 }.");
        }
    }

    public static class Day2Part2
    {
        private static string[] InputLines => File.ReadAllLines(@"Inputs\Day2.txt");

        public static void Solve()
        {
            string result = null;
            for (int i = 0; i < InputLines[0].Length; i++)
            {
                var box = InputLines.Select(s => s.Remove(i, 1)).GroupBy(s => s).FirstOrDefault(g => g.Count() > 1);
                if (box != null)
                {
                    result = box.First();
                    break;
                }
            }
            Console.WriteLine($"The letters in common between the two correct box IDs are '{ result }'.");
        }
    }
}
