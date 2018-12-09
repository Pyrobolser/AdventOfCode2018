using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Marble
    {
        public int Value { get; private set; }
        public Marble Previous { get; set; }
        public Marble Next { get; set; }

        public Marble(int value)
        {
            Value = value;
        }
    }

    public class MarbleCircle
    {
        private Marble current = null;

        public void AddClock(Marble marble)
        {
            if (current == null)
            {
                current = marble;
                current.Next = current;
                current.Previous = current;
            }
            else
            {
                Marble left = current.Next;
                Marble right = left.Next;
                left.Next = marble;
                marble.Previous = left;
                marble.Next = right;
                right.Previous = marble;
                current = marble;
            }
        }

        public int RemoveCounterClock(int space)
        {
            int result = 0;
            if (current != null)
            {
                Marble removed = current;
                for (int i = 0; i < space; i++)
                    removed = removed.Previous;
                Marble left = removed.Previous;
                Marble right = removed.Next;
                removed.Next = null;
                removed.Previous = null;
                left.Next = right;
                right.Previous = left;
                current = right;
                result = removed.Value;
            }

            return result;
        }
    }

    public static class Day9Part1
    {
        private static string InputLine => File.ReadAllText(@"Inputs\Day9.txt");

        public static void Solve()
        {
            MarbleCircle circle = new MarbleCircle();
            MatchCollection matches = Regex.Matches(InputLine, @"\d+");
            int[] scores = new int[int.Parse(matches[0].Value)];
            int marbles = int.Parse(matches[1].Value);

            circle.AddClock(new Marble(0));

            for(int i = 1; i <= marbles; i++)
            {
                if(i % 23 != 0)
                {
                    circle.AddClock(new Marble(i));
                }
                else
                {
                    scores[(i - 1) % scores.Length] += i + circle.RemoveCounterClock(7);
                }
            }
            Console.WriteLine($"The winning Elf's score is { scores.Max() }.");
        }
    }

    public static class Day9Part2
    {
        private static string InputLine => File.ReadAllText(@"Inputs\Day9.txt");

        public static void Solve()
        {
            MarbleCircle circle = new MarbleCircle();
            MatchCollection matches = Regex.Matches(InputLine, @"\d+");
            long[] scores = new long[int.Parse(matches[0].Value)];
            int marbles = int.Parse(matches[1].Value) * 100;

            circle.AddClock(new Marble(0));

            for (int i = 1; i <= marbles; i++)
            {
                if (i % 23 != 0)
                {
                    circle.AddClock(new Marble(i));
                }
                else
                {
                    scores[(i - 1) % scores.Length] += i + circle.RemoveCounterClock(7);
                }
            }
            Console.WriteLine($"The winning Elf's score would be { scores.Max() } if the number of the last marble were 100 times larger.");
        }
    }
}
