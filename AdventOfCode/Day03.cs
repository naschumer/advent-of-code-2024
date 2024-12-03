using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var ans = Regex.Matches(_input, @"(?<=mul\()\d+,\d+(?=\))").Select(x => x.Value.Split(",").Select(y => int.Parse(y)).Aggregate(1, (a, b) => a * b)).Sum();
        return new(ans.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var ans = _input.Split("do()").Select(
            x => Regex.Matches(x, @"(?<!don't\(\).*)(?<=mul\()\d+,\d+(?=\))").Select(y => y.Value.Split(",").Select(z => int.Parse(z)).Aggregate(1, (a, b) => a * b)).Sum()
        ).Sum();
        return new(ans.ToString());
    }
}
