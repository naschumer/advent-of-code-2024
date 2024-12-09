using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class CopyMe : BaseDay
{
    private readonly string[] _input;

    public CopyMe()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        return new("");
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}