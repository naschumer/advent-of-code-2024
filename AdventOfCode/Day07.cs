using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day7 : BaseDay
{
    private readonly string[] _input;

    public Day7()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public Dictionary<ulong, List<ulong>> ParseInput()
    {
        var x = new Dictionary<ulong, List<ulong>>();
        foreach (var line in _input)
        {
            var c = line.Split(": ");
            var d = c[1].Split(" ").Select(x => ulong.Parse(x.Trim())).ToList();
            x[ulong.Parse(c[0])] = d;
        }
        return x;
    }

    public override ValueTask<string> Solve_1()
    {
        var puzzle = ParseInput();

        ulong totalSum = 0;
        var ops = new Dictionary<int, HashSet<string>>();
        foreach (var line in puzzle)
        //Parallel.ForEach(puzzle, line =>
        {
            var possible = false;
            if (!ops.ContainsKey(line.Value.Count())) GenerateOperators(ops, line.Value);
            foreach (var opline in ops[line.Value.Count()])
            {
                var j = 0;
                ulong sum = line.Value[0];
                for (int i = 1; i < line.Value.Count; i++)
                {
                    if (opline[j] == '+')
                    {
                        sum += line.Value[i];
                    }
                    else if (opline[j] == '*')
                    {
                        sum *= line.Value[i];
                    }
                    else
                    {
                        sum = ulong.Parse(sum.ToString() + line.Value[i].ToString());
                    }
                    j++;
                }
                if (sum == line.Key) { possible = true; break; }
            }
            if (possible)
            {
                totalSum += line.Key;
            }
            //});
        }

        return new(totalSum.ToString());
    }

    private void GenerateOperators(Dictionary<int, HashSet<string>> ops, List<ulong> value)
    {
        var op = new HashSet<string>();
        while (op.Count < (Math.Pow(3, value.Count - 1)))
        {
            if (op.Count == 0)
            {
                op.Add(string.Join("", value.Select(x => "+").Take(value.Count - 1)));
            }
            else
            {
                var y = op.Last();
                foreach (var (i, c) in y.Index())
                {
                    var newChar = c;
                    if (newChar == '+') { newChar = '*'; }
                    else if (newChar == '*') { newChar = '|'; }
                    else if (newChar == '|') { newChar = '+'; }
                    var newString = y.Remove(i, 1).Insert(i, newChar.ToString());
                    if (!op.Contains(newString))
                    {
                        op.Add(newString);
                        break;
                    }
                }
            }
        }
        ops[value.Count()] = op;
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}