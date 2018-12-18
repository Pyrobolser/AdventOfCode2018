using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace AdventOfCode
{
    public static class Day14Part1
    {
        private static int Input = int.Parse(File.ReadAllText(@"Inputs\Day14.txt"));

        public static void Solve()
        {
            List<int> board = new List<int> { 3, 7 };
            int elf1 = 0, elf2 = 1, recipe = 0;
            while(board.Count < Input + 10)
            {
                recipe = board[elf1] + board[elf2];
                if (recipe >= 10)
                {
                    board.Add(1);
                }
                board.Add(recipe % 10);
                elf1 = (elf1 + board[elf1] + 1) % board.Count;
                elf2 = (elf2 + board[elf2] + 1) % board.Count;
            }

            var result = string.Join("", board.Skip(Input).Take(10));
            Console.WriteLine($"The scores of the ten recipes immediately after the number of recipes in the puzzle input is { result }.");
        }
    }

    public static class Day14Part2
    {
        private static int Input = int.Parse(File.ReadAllText(@"Inputs\Day14.txt"));

        public static void Solve()
        {
            List<int> pattern = Input.ToString().Select(c => int.Parse(c.ToString())).ToList();
            List<int> board = new List<int> { 3, 7 };
            int elf1 = 0, elf2 = 1, recipe = 0, result = 0, current = 0;
            bool isPatternFound = false;
            while (!isPatternFound)
            {
                recipe = board[elf1] + board[elf2];
                if (recipe >= 10)
                {
                    board.Add(1);
                }
                board.Add(recipe % 10);
                elf1 = (elf1 + board[elf1] + 1) % board.Count;
                elf2 = (elf2 + board[elf2] + 1) % board.Count;

                while(result + current < board.Count)
                {
                    if(board[result + current] == pattern[current])
                    {
                        if(current == pattern.Count - 1)
                        {
                            isPatternFound = true;
                            break;
                        }
                        current++;
                    }
                    else
                    {
                        current = 0;
                        result++;
                    }
                }
            }

            Console.WriteLine($"There are { result } recipes on the scoreboard to the left of the score sequence in the puzzle input.");
        }
    }
}
