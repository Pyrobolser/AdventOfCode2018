using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public enum Cardinal
    {
        North,
        East,
        South,
        West
    }

    public enum Turn
    {
        Left,
        Straight,
        Right
    }

    public class Cart
    {
        public static int[,] Map;
        public int X { get; set; }
        public int Y { get; set; }
        public Cardinal Orientation { get; set; }
        public Turn Next { get; set; }

        public Cart(Cardinal orientation, int x, int y)
        {
            Orientation = orientation;
            Next = Turn.Left;
            X = x;
            Y = y;
        }

        public void Tick()
        {
            switch (Orientation)
            {
                case Cardinal.North:
                    Y--;
                    break;
                case Cardinal.East:
                    X++;
                    break;
                case Cardinal.South:
                    Y++;
                    break;
                case Cardinal.West:
                    X--;
                    break;
            }

            switch (Map[X, Y])
            {
                case 4:
                    switch (Next)
                    {
                        case Turn.Left:
                            GoLeft();
                            break;
                        case Turn.Straight:
                            break;
                        case Turn.Right:
                            GoRight();
                            break;
                    }
                    Intersect();
                    break;
                case 3:
                    switch (Orientation)
                    {
                        case Cardinal.North:
                        case Cardinal.South:
                            GoLeft();
                            break;
                        case Cardinal.East:
                        case Cardinal.West:
                            GoRight();
                            break;
                    }
                    break;
                case 2:
                    switch (Orientation)
                    {
                        case Cardinal.North:
                        case Cardinal.South:
                            GoRight();
                            break;
                        case Cardinal.East:
                        case Cardinal.West:
                            GoLeft();
                            break;
                    }
                    break;
                case 1:
                    break;
                default:
                    throw new System.Exception("Out of track!");
            }
        }

        private void GoLeft()
        {
            switch (Orientation)
            {
                case Cardinal.North:
                    Orientation = Cardinal.West;
                    break;
                case Cardinal.East:
                    Orientation = Cardinal.North;
                    break;
                case Cardinal.South:
                    Orientation = Cardinal.East;
                    break;
                case Cardinal.West:
                    Orientation = Cardinal.South;
                    break;
            }
        }

        private void GoRight()
        {
            switch (Orientation)
            {
                case Cardinal.North:
                    Orientation = Cardinal.East;
                    break;
                case Cardinal.East:
                    Orientation = Cardinal.South;
                    break;
                case Cardinal.South:
                    Orientation = Cardinal.West;
                    break;
                case Cardinal.West:
                    Orientation = Cardinal.North;
                    break;
            }
        }

        private void Intersect()
        {
            switch (Next)
            {
                case Turn.Left:
                    Next = Turn.Straight;
                    break;
                case Turn.Straight:
                    Next = Turn.Right;
                    break;
                case Turn.Right:
                    Next = Turn.Left;
                    break;
            }
        }
    }
    public static class Day13Part1
    {
        private static string[] InputLines = File.ReadAllLines(@"Inputs\Day13.txt");

        public static void Solve()
        {
            Cart.Map = new int[150, 150];
            List<Cart> carts = new List<Cart>();
            for (int i = 0; i <= Cart.Map.GetUpperBound(0); i++)
            {
                for(int j = 0; j < InputLines.Length; j++)
                {
                    switch(InputLines[j][i])
                    {
                        case '-':
                        case '|':
                            Cart.Map[i, j] = 1;
                            break;
                        case '/':
                            Cart.Map[i, j] = 2;
                            break;
                        case '\\':
                            Cart.Map[i, j] = 3;
                            break;
                        case '+':
                            Cart.Map[i, j] = 4;
                            break;
                        case '^':
                            Cart.Map[i, j] = 1;
                            carts.Add(new Cart(Cardinal.North, i, j));
                            break;
                        case '>':
                            Cart.Map[i, j] = 1;
                            carts.Add(new Cart(Cardinal.East, i, j));
                            break;
                        case 'v':
                            Cart.Map[i, j] = 1;
                            carts.Add(new Cart(Cardinal.South, i, j));
                            break;
                        case '<':
                            Cart.Map[i, j] = 1;
                            carts.Add(new Cart(Cardinal.West, i, j));
                            break;
                        default:
                            Cart.Map[i, j] = 0;
                            break;
                    }
                }
            }

            int tick = 0;
            while(!carts.GroupBy(c => new { c.X, c.Y } ).Any(g => g.Count() > 1))
            {
                foreach(Cart c in carts.OrderBy(c => c.Y).OrderBy(c => c.X))
                {
                    c.Tick();
                }
                tick++;
            }

            var result = carts.GroupBy(c => new { c.X, c.Y }).First(g => g.Count() > 1).First();
            Console.WriteLine($"The location of the first crash is { result.X },{ result.Y }.");
        }
    }

    public static class Day13Part2
    {
        private static string[] InputLines = File.ReadAllLines(@"Inputs\Day13.txt");

        public static void Solve()
        {
            Cart.Map = new int[150, 150];
            List<Cart> carts = new List<Cart>();
            for (int i = 0; i <= Cart.Map.GetUpperBound(0); i++)
            {
                for (int j = 0; j < InputLines.Length; j++)
                {
                    switch (InputLines[j][i])
                    {
                        case '-':
                        case '|':
                            Cart.Map[i, j] = 1;
                            break;
                        case '/':
                            Cart.Map[i, j] = 2;
                            break;
                        case '\\':
                            Cart.Map[i, j] = 3;
                            break;
                        case '+':
                            Cart.Map[i, j] = 4;
                            break;
                        case '^':
                            Cart.Map[i, j] = 1;
                            carts.Add(new Cart(Cardinal.North, i, j));
                            break;
                        case '>':
                            Cart.Map[i, j] = 1;
                            carts.Add(new Cart(Cardinal.East, i, j));
                            break;
                        case 'v':
                            Cart.Map[i, j] = 1;
                            carts.Add(new Cart(Cardinal.South, i, j));
                            break;
                        case '<':
                            Cart.Map[i, j] = 1;
                            carts.Add(new Cart(Cardinal.West, i, j));
                            break;
                        default:
                            Cart.Map[i, j] = 0;
                            break;
                    }
                }
            }

            int tick = 0;
            List<Cart> removed = null;
            while (carts.Count > 1)
            {
                foreach (Cart c in carts.OrderBy(c => c.Y).OrderBy(c => c.X))
                {
                    c.Tick();
                    removed = carts.GroupBy(m => new { m.X, m.Y })?.Where(g => g.Count() > 1)?.SelectMany(g => g)?.ToList();
                    if (removed.Count > 0)
                    {
                        carts = carts.Except(removed).ToList();
                    }
                }
                tick++;
            }
            var result = carts.First();
            Console.WriteLine($"The location of the last cart is { result.X },{ result.Y }.");
        }
    }
}