using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day10 : BaseDay
{
    private readonly string[] _input;

    public Day10()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public int[][] ParseInput()
    {
        return _input.Select(x=> x.ToCharArray().Select(y=>int.Parse(y.ToString())).ToArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var puzzle = ParseInput();
        var sum = 0;
        foreach(var (y, line) in puzzle.Index())
        {
            foreach(var (x, val) in line.Index())
            {
                if (val == 0)
                {
                    sum += GetTrailheadScore(puzzle, val, x, y);
                }
            }
        }
        return new(sum.ToString());
    }

    private int GetTrailheadScore(int[][] puzzle, int val, int x, int y)
    {
        var sum = 0;
        if (val == 9) return 1;
        //Up
        if (y != 0 && puzzle[y - 1][x] == val + 1) sum += GetTrailheadScore(puzzle, val + 1, x, y - 1);
        //Down
        if (y < puzzle.Count() - 1 && puzzle[y + 1][x] == val + 1) sum += GetTrailheadScore(puzzle, val + 1, x, y + 1);
        //Right
        if (x < puzzle[y].Count() - 1 && puzzle[y][x + 1] == val + 1) sum += GetTrailheadScore(puzzle, val + 1, x + 1, y);
        //Left
        if (x != 0 && puzzle[y][x - 1] == val + 1) sum += GetTrailheadScore(puzzle, val + 1, x - 1, y);
        return sum;
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}