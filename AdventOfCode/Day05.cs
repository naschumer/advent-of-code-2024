using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string _input;

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
    }
    public override ValueTask<string> Solve_1()
    {
        var (rules, printer) = ParseInput();

        var values = new List<int>();
        foreach (var set in printer)
        {
            var valid = DetermineValid(set, rules);
            if (valid) values.Add(set[(set.Count() - 1) / 2]);
        }

        return new(values.Sum().ToString());
    }

    public bool DetermineValid(IEnumerable<int> set, Dictionary<int, HashSet<int>> rules)
    {
        var valid = true;
        foreach (var (i, item) in set.Index())
        {
            if (i == 0) continue;
            if (rules.ContainsKey(item))
            {
                var subset = set.Take(i);
                if (subset.Any(x => rules[item].Contains(x)))
                {
                    valid = false;
                    break;
                }
            }
        }
        if(valid) Console.WriteLine("Valid!");
        return valid;
    }

    public (Dictionary<int, HashSet<int>>, List<List<int>>) ParseInput()
    {
        var out1 = new Dictionary<int, HashSet<int>>();
        var out2 = new List<List<int>>();
        var c = _input.Split("\r\n\r\n");
        var c1 = c[0].Split("\r\n");
        var c2 = c[1].Split("\r\n");
        foreach (var x in c1)
        {
            var y = x.Split("|").Select(g => int.Parse(g)).ToList();
            if (out1.ContainsKey(y[0]))
            {
                out1[y[0]].Add(y[1]);
            }
            else
            {
                out1.Add(y[0], new HashSet<int>() { y[1] });
            }
        }
        out2 = c2.Select(x => x.Split(",").Select(g => int.Parse(g)).ToList()).ToList();
        return (out1, out2);
    }

    public override ValueTask<string> Solve_2()
    {
        var (rules, printer) = ParseInput();

        var values = new List<int>();
        foreach (var (i,set) in printer.Index())
        {
            Console.WriteLine($"Set {i}");
            var valid = DetermineValid(set, rules);
            if(valid) { continue; }
            while (!valid)
            {
                Sort(set, rules);
                Console.WriteLine(string.Join(", ", set));
                valid = DetermineValid(set, rules);
            }
            values.Add(set[(set.Count() - 1) / 2]);
        }

        return new(values.Sum().ToString());
    }

    public void Sort(List<int> set, Dictionary<int, HashSet<int>> rules)
    {
        for (int i = 1; i < set.Count(); i++)
        {
            var item = set[i];
            if (rules.ContainsKey(item))
            {
                var subset = set.Take(i);
                if (subset.Any(x => rules[item].Contains(x)))
                {
                    var c = set[i - 1];
                    set[i - 1] = set[i];
                    set[i] = c;
                }
            }
        }
    }
}
