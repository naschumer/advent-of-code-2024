using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day12 : BaseDay
{
    private readonly string[] _input;

    public Day12()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public List<Node12> ParseInput()
    {
        var a = new List<Node12>();
        foreach (var (y, line) in _input.Index())
        {
            a = a.Concat(line.Select((c, x) => new Node12(x, y, c)).ToList()).ToList();
        }
        return a;
    }

    public override ValueTask<string> Solve_1()
    {
        var puzzle = ParseInput();

        var currentGroup = 0;
        foreach (var node in puzzle)
        {
            if (node.Group == null)
            {
                Search(puzzle, node, currentGroup);
                currentGroup++;
            }
        }

        var totalGroups = puzzle.Aggregate(0, (total, next) => next.Group > total ? next.Group.Value : total );
        var totalCount = 0;
        foreach (var group in Enumerable.Range(0, totalGroups + 1))
        {
            var area = puzzle.Where(n=> n.Group == group).Count();
            //var per = puzzle.Where(n => n.Group == group).Select(n => n.Perimeter.Value).Sum();
            var per = GetSides(puzzle, group);
            Console.WriteLine(puzzle.Where(n => n.Group == group).FirstOrDefault().Letter);
            Console.WriteLine($"{area} * {per} = {area * per}");
            totalCount += (area * per);
        }
        return new(totalCount.ToString());
    }

    private int GetSides(List<Node12> puzzle, int group)
    {
        var map = new Dictionary<(string, int), Node12> ();
        foreach(var node in puzzle.Where(n => n.Group == group))
        {

        }
        return 0;
    }

    private void Search(List<Node12> puzzle, Node12 val, int currentGroup)
    {
        if (val.Group == null) val.Group = currentGroup;
        var right = puzzle.Where(n => n.X == val.X + 1 && n.Y == val.Y && n.Letter == val.Letter && n.Group == null).ToList();
        if (right.Count() > 0) Search(puzzle, right.FirstOrDefault(), currentGroup);

        var left = puzzle.Where(n => n.X == val.X - 1 && n.Y == val.Y && n.Letter == val.Letter && n.Group == null).ToList();
        if (left.Count() > 0) Search(puzzle, left.FirstOrDefault(), currentGroup);

        var up = puzzle.Where(n => n.X == val.X && n.Y == val.Y - 1 && n.Letter == val.Letter && n.Group == null).ToList();
        if (up.Count() > 0) Search(puzzle, up.FirstOrDefault(), currentGroup);

        var down = puzzle.Where(n => n.X == val.X && n.Y == val.Y + 1 && n.Letter == val.Letter && n.Group == null).ToList();
        if (down.Count() > 0) Search(puzzle, down.FirstOrDefault(), currentGroup);
        if (val.Perimeter == null)
        {
            val.Perimeter = 0;
            var right2 = puzzle.Where(n => n.X == val.X + 1 && n.Y == val.Y && n.Letter == val.Letter).ToList();
            var left2 = puzzle.Where(n => n.X == val.X - 1 && n.Y == val.Y && n.Letter == val.Letter).ToList();
            var up2 = puzzle.Where(n => n.X == val.X && n.Y == val.Y - 1 && n.Letter == val.Letter).ToList();
            var down2 = puzzle.Where(n => n.X == val.X && n.Y == val.Y + 1 && n.Letter == val.Letter).ToList();
            if (right2.Count() == 0) val.Perimeter++;
            if (left2.Count() == 0) val.Perimeter++;
            if (up2.Count() == 0) val.Perimeter++;
            if (down2.Count() == 0) val.Perimeter++;
        }
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}

public class Node12
{
    public int X { get; set; }
    public int Y { get; set; }
    public int? Group { get; set; }
    public int? Perimeter { get; set; }
    public char Letter { get; set; }

    public Node12(int x, int y, char letter)
    {
        X = x; Y = y; Letter = letter;
    }
}