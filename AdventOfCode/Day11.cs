using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day11 : BaseDay
{
    private readonly string _input;

    public Day11()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public ulong[] ParseInput()
    {
        return _input.Split(" ").Select(x => ulong.Parse(x)).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var puzzle = ParseInput();
        var map = new Dictionary<(ulong, int), ulong>();
        var count = Blink(puzzle, map, 75);
        return new(count.ToString());
    }

    private ulong Blink(ulong[] puzzle, Dictionary<(ulong, int), ulong> map, int v)
    {
        ulong count = 0;
        if (v == 0)
        {
            return 1;
        }
        foreach (var num in puzzle)
        {
            if (num == 0)
            {
                count += AddToMap(map, 1, v);
            }
            else if (num.ToString().Length % 2 == 0)
            {
                var left = ulong.Parse(num.ToString().Substring(0, num.ToString().Length / 2));
                var right = ulong.Parse(num.ToString().Substring(num.ToString().Length / 2));
                count += AddToMap(map, left, v);
                count += AddToMap(map, right, v);
            }
            else
            {
                var newNum = num * 2024;
                count += AddToMap(map, newNum, v);
            }
        }
        return count;
    }

    private ulong AddToMap(Dictionary<(ulong, int), ulong> map, ulong x, int v)
    {
        ulong count = 0;
        if (!map.ContainsKey((x, v)))
        {
            var u = Blink([x], map, v - 1);
            count += u;
            if (!map.ContainsKey((x, v))) map.Add((x, v), u);
        }
        else
        {
            count += map[(x, v)];
        }
        return count;
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}