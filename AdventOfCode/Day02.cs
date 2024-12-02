namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _input;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public List<List<int>> ParseInput(string[] input)
    {
        return input.Select(x => x.Split(' ').Select(y => int.Parse(y)).ToList()).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var reports = ParseInput(_input);
        var sum = 0;
        foreach (var report in reports)
        {
            if (IsSafe(report)) sum += 1;
        }
        return new(sum.ToString());
    }

    public bool IsSafe(List<int> report)
    {
        var allIncreasing = true;
        var allDecreasing = true;
        var allDiffer = true;
        foreach ((int i, int level) in report.Index())
        {
            if (i == 0) continue;
            if (report[i - 1] < level) allIncreasing = false;
            if (report[i - 1] > level) allDecreasing = false;
            var differ = Math.Abs(report[i - 1] - level);
            if (differ == 0 || differ > 3) allDiffer = false;
        }
        return (allIncreasing || allDecreasing) && allDiffer;
    }

    public override ValueTask<string> Solve_2()
    {
        var reports = ParseInput(_input);
        var sum = 0;
        foreach (var report in reports)
        {
            if (IsSafe(report))
            {
                sum += 1;
            }
            else
            {
                foreach (var (i, item) in report.Index())
                {
                    var reportCopy = report.ToList();
                    reportCopy.RemoveAt(i);
                    if (IsSafe(reportCopy))
                    {
                        sum += 1;
                        break;
                    }
                }
            }
        }
        return new(sum.ToString());
    }
}
