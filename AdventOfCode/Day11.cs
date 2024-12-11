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
        var map = new Dictionary<ulong, ulong>();
        var count = Blink(puzzle, map, 6);
        return new(count.ToString());
    }

    private ulong Blink(ulong[] puzzle, Dictionary<ulong, ulong> map, int v)
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
                if (!map.ContainsKey(1))
                {
                    var u = Blink([1], map, v - 1);
                    count += u;
                    if (!map.ContainsKey(1)) map.Add(1, u);
                }
                else
                {
                    count += map[1];
                }

            }
            else if (num.ToString().Length % 2 == 0)
            {
                var left = ulong.Parse(num.ToString().Substring(0, num.ToString().Length / 2));
                var right = ulong.Parse(num.ToString().Substring(num.ToString().Length / 2));
                if (!map.ContainsKey(left))
                {
                    var u = Blink([left], map, v - 1);
                    count += u;
                    if (!map.ContainsKey(left)) map.Add(left, u);
                }
                else
                {
                    count += map[left];
                }
                if (!map.ContainsKey(right))
                {
                    var u = Blink([right], map, v - 1);
                    count += u;
                    if (!map.ContainsKey(right)) map.Add(right, u);
                }
                else
                {
                    count += map[right];
                }
            }
            else
            {
                var newNum = num * 2024;
                if (!map.ContainsKey(newNum))
                {
                    var u = Blink([newNum], map, v - 1);
                    count += u;
                    if (!map.ContainsKey(newNum)) map.Add(newNum, u);
                }
                else
                {
                    count += map[newNum];
                }
            }
        }
        return count;
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}