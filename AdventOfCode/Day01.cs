namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        GetLists(_input, out var list1, out var list2);
        list1 = list1.OrderBy(x => x).ToList();
        list2 = list2.OrderBy(x => x).ToList();
        list1 = list1.Select((x, i) => Math.Abs(x - list2[i])).ToList();
        return new(list1.Sum().ToString());
    }

    public void GetLists(string[] input, out List<int> list1, out List<int> list2)
    {
        list1 = new List<int>();
        list2 = new List<int>();
        foreach (var line in _input)
        {
            var c = line.Split("   ");
            list1.Add(int.Parse(c[0]));
            list2.Add(int.Parse(c[1]));
        }
    }

    public override ValueTask<string> Solve_2()
    {
        GetLists(_input, out var list1, out var list2);
        list1 = list1.Select((x, i) => x * list2.Where(y=> y == x).Count()).ToList();
        return new(list1.Sum().ToString());
    }
}
