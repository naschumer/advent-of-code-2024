using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day8 : BaseDay
{
    private readonly string[] _input;

    public Day8()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public char[][] ParseInput()
    {
        return _input.Select(x=> x.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var puzzle = ParseInput();
        return new("");
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}