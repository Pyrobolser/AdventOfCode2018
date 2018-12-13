using System;
using System.IO;

namespace AdventOfCode
{
    public static class Day11Part1
    {
        private static int Input = int.Parse(File.ReadAllText(@"Inputs\Day11.txt"));

        public static void Solve()
        {
            int[,] grid = new int[300, 300];
            int xaxis = grid.GetUpperBound(0);
            int yaxis = grid.GetUpperBound(1);
            int rackId = 0;
            for(int x = 0; x <= xaxis; x++)
            {
                for(int y = 0; y <= yaxis; y++)
                {
                    rackId = (x + 1) + 10;
                    grid[x, y] = (((((rackId * (y + 1)) + Input) * rackId) / 100) % 10) - 5;
                }
            }

            int total = 0, max = 0, cellx = 0, celly = 0;
            for(int x = 0; x <= xaxis - 3; x++)
            {
                for(int y = 0; y <= yaxis - 3; y++)
                {
                    total = grid[x, y]     + grid[x + 1, y]     + grid[x + 2, y]
                          + grid[x, y + 1] + grid[x + 1, y + 1] + grid[x + 2, y + 1]
                          + grid[x, y + 2] + grid[x + 1, y + 2] + grid[x + 2, y + 2];

                    if(total > max)
                    {
                        max = total;
                        cellx = (x + 1);
                        celly = (y + 1);
                    }
                }
            }
            Console.WriteLine($"The X,Y coordinate of the top-left fuel cell of the 3x3 square with the largest total power is { cellx },{ celly }.");
        }
    }

    public static class Day11Part2
    {
        private static int Input = int.Parse(File.ReadAllText(@"Inputs\Day11.txt"));

        public static void Solve()
        {
            int[,] grid = new int[300, 300];
            int xaxis = grid.GetUpperBound(0);
            int yaxis = grid.GetUpperBound(1);
            int rackId = 0;
            for (int x = 0; x <= xaxis; x++)
            {
                for (int y = 0; y <= yaxis; y++)
                {
                    rackId = (x + 1) + 10;
                    grid[x, y] = (((((rackId * (y + 1)) + Input) * rackId) / 100) % 10) - 5;
                }
            }

            int total = 0, max = 0, cellx = 0, celly = 0, size = 0;
            for (int s = 1; s <= 50; s++) // 50 is an arbitrary value just to speed up the process if luck is here.
            {
                for (int x = 0; x <= xaxis - s; x++)
                {
                    for (int y = 0; y <= yaxis - s; y++)
                    {
                        total = 0;
                        for (int i = 0; i < s; i++)
                        {
                            for (int j = 0; j < s; j++)
                            {
                                total += grid[x + i, y + j];
                            }
                        }

                        if (total > max)
                        {
                            max = total;
                            cellx = (x + 1);
                            celly = (y + 1);
                            size = s;
                        }
                    }
                }
            }
            Console.WriteLine($"The X,Y,size identifier of the square with the largest total power is { cellx },{ celly },{ size }.");
        }
    }
}
