using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day15 : BaseDay
{
    private string _input;
    private List<List<Node>> puzzle;

    public Day15()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public (List<List<char>>, string) ParseInput()
    {
        var x = _input.Split("\r\n\r\n");
        var y = x[0].Split("\r\n").Select(l => l.ToCharArray().ToList()).ToList();
        var z = x[1].Replace("\r\n", "");
        return (y, z);
    }

    public override ValueTask<string> Solve_1()
    {
        Scale();
        var x = ParseInput();
        Scale();
        GetPuzzle(x.Item1);
        Print();
        return new(MoveFromPattern(x.Item2).ToString());
    }

    private int MoveFromPattern(string item2)
    {
        var currentNode = new Node();
        foreach (var y in puzzle)
        {
            foreach (var x in y)
            {
                if (x.Letter == '@') currentNode = x;
            }
        }
        foreach (var (i, moveDir) in item2.Index())
        {
            Move(ref currentNode, moveDir);
            //Print();
        }
        Print();
        return Count();
    }

    private int Count()
    {
        var sum = 0;
        foreach (var (y, line) in puzzle.Index())
        {
            foreach (var (x, item) in line.Index())
            {
                if (item.Letter == '[')
                {
                    var u = item.Y;
                    var p = puzzle.Count() - item.Y - 1;
                    var g = item.X;
                    var h = line.Count() - item.Right.X - 1;
                    sum += (100 * u) + g;
                    //Console.WriteLine($"(100 * {(u < p ? u : p)}) + {(g < h ? g : h)}");
                }
            }
        }
        return sum;
    }

    private void Print()
    {
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < puzzle.Count(); y++)
        {
            for (int x = 0; x < puzzle[y].Count(); x++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (puzzle[y][x].Letter == '@') { Console.ForegroundColor = ConsoleColor.Red; }
                if (puzzle[y][x].Letter == '#') { Console.ForegroundColor = ConsoleColor.Green; }
                if (puzzle[y][x].Letter == '[' || puzzle[y][x].Letter == ']') { Console.ForegroundColor = ConsoleColor.Blue; }
                Console.Write(puzzle[y][x].Letter);
            }
            Console.WriteLine();
        }
        Thread.Sleep(5);
    }

    private void Move(ref Node currentNode, char moveDir)
    {
        if (moveDir == '^')
        {
            if (currentNode.Up.Letter != '#')
            {
                if (currentNode.Up.Letter == '[')
                {
                    var up = currentNode.Up;
                    var upRight = currentNode.Up.Right;
                    if(CanMove(ref up, moveDir) && CanMove(ref upRight, moveDir))
                    {
                        Move(ref up, moveDir);
                        Move(ref upRight, moveDir);
                    }
                }
                if (currentNode.Up.Letter == ']')
                {
                    var up = currentNode.Up;
                    var upLeft = currentNode.Up.Left;
                    if (CanMove(ref up, moveDir) && CanMove(ref upLeft, moveDir))
                    {
                        Move(ref up, moveDir);
                        Move(ref upLeft, moveDir);
                    }
                }
                if (currentNode.Up.Letter == '.')
                {
                    var t = currentNode.Letter;
                    currentNode.Letter = '.';
                    currentNode = currentNode.Up;
                    currentNode.Letter = t;
                }
            }
        }
        if (moveDir == 'v')
        {
            if (currentNode.Down.Letter != '#')
            {
                if (currentNode.Down.Letter == '[')
                {
                    var down = currentNode.Down;
                    var downRight = currentNode.Down.Right;
                    if (CanMove(ref down, moveDir) && CanMove(ref downRight, moveDir))
                    {
                        Move(ref down, moveDir);
                        Move(ref downRight, moveDir);
                    }
                }
                if (currentNode.Down.Letter == ']')
                {
                    var down = currentNode.Down;
                    var downLeft = currentNode.Down.Left;
                    if (CanMove(ref down, moveDir) && CanMove(ref downLeft, moveDir))
                    {
                        Move(ref down, moveDir);
                        Move(ref downLeft, moveDir);
                    }
                }
                if (currentNode.Down.Letter == '.')
                {
                    var t = currentNode.Letter;
                    currentNode.Letter = '.';
                    currentNode = currentNode.Down;
                    currentNode.Letter = t;
                }
            }
        }
        if (moveDir == '<')
        {
            if (currentNode.Left.Letter != '#')
            {
                if (currentNode.Left.Letter == ']')
                {
                    var left = currentNode.Left;
                    var leftLeft = currentNode.Left.Left;
                    if (CanMove(ref leftLeft, moveDir))
                    {
                        Move(ref leftLeft, moveDir);
                        Move(ref left, moveDir);
                    }
                }
                if (currentNode.Left.Letter == '.')
                {
                    var t = currentNode.Letter;
                    currentNode.Letter = '.';
                    currentNode = currentNode.Left;
                    currentNode.Letter = t;
                }
            }
        }
        if (moveDir == '>')
        {
            if (currentNode.Right.Letter != '#')
            {
                if (currentNode.Right.Letter == '[')
                {
                    var right = currentNode.Right;
                    var rightRight = currentNode.Right.Right;
                    if (CanMove(ref rightRight, moveDir))
                    {
                        Move(ref rightRight, moveDir);
                        Move(ref right, moveDir);
                    }
                }
                if (currentNode.Right.Letter == '.')
                {
                    var t = currentNode.Letter;
                    currentNode.Letter = '.';
                    currentNode = currentNode.Right;
                    currentNode.Letter = t;
                }
            }
        }
    }

    private bool CanMove(ref Node currentNode, char moveDir)
    {
        var canMove = false;
        if (moveDir == '^')
        {
            if (currentNode.Up.Letter != '#')
            {
                if (currentNode.Up.Letter == '[')
                {
                    var up = currentNode.Up;
                    if (CanMove(ref up, moveDir))
                    {
                        up = currentNode.Up.Right;
                        return CanMove(ref up, moveDir);
                    }
                }
                if (currentNode.Up.Letter == ']')
                {
                    var up = currentNode.Up;
                    if (CanMove(ref up, moveDir))
                    {
                        up = currentNode.Up.Left;
                        return CanMove(ref up, moveDir);
                    }
                }
                if (currentNode.Up.Letter == '.')
                {
                    canMove = true;
                }
            }
        }
        if (moveDir == 'v')
        {
            if (currentNode.Down.Letter != '#')
            {
                if (currentNode.Down.Letter == '[')
                {
                    var down = currentNode.Down;
                    if (CanMove(ref down, moveDir))
                    {
                        down = currentNode.Down.Right;
                        return CanMove(ref down, moveDir);
                    }
                }
                if (currentNode.Down.Letter == ']')
                {
                    var down = currentNode.Down;
                    if (CanMove(ref down, moveDir))
                    {
                        down = currentNode.Down.Left;
                        return CanMove(ref down, moveDir);
                    }
                }
                if (currentNode.Down.Letter == '.')
                {
                    canMove = true;
                }
            }
        }
        if (moveDir == '<')
        {
            if (currentNode.Left.Letter != '#')
            {
                if (currentNode.Left.Letter == ']')
                {
                    var left = currentNode.Left.Left;
                    return CanMove(ref left, moveDir);
                }
                if (currentNode.Left.Letter == '.')
                {
                    canMove = true;
                }
            }
        }
        if (moveDir == '>')
        {
            if (currentNode.Right.Letter != '#')
            {
                if (currentNode.Right.Letter == '[')
                {
                    var right = currentNode.Right.Right;
                    return CanMove(ref right, moveDir);
                }
                if (currentNode.Right.Letter == '.')
                {
                    canMove = true;
                }
            }
        }
        return canMove;
    }

    private void GetPuzzle(List<List<char>> item1)
    {
        puzzle = new List<List<Node>>();
        foreach (var (y, line) in item1.Index())
        {
            var d = new List<Node>();
            foreach (var (x, item) in line.Index())
            {
                d.Add(new Node() { Letter = item, X = x, Y = y });
            }
            puzzle.Add(d);
        }
        foreach (var (y, line) in puzzle.Index())
        {
            foreach (var (x, item) in line.Index())
            {
                //Up
                if (y != 0) item.Up = puzzle[item.Y - 1][item.X];
                //Down
                if (y != puzzle.Count() - 1) item.Down = puzzle[item.Y + 1][item.X];
                //Left
                if (x != 0) item.Left = puzzle[item.Y][item.X - 1];
                //Right
                if (x != puzzle[0].Count() - 1) item.Right = puzzle[item.Y][item.X + 1];
            }
        }
    }

    private void Scale()
    {
        var newStr = string.Empty;
        foreach (var x in _input)
        {
            if (x == '#') newStr += "##";
            else if (x == 'O') newStr += "[]";
            else if (x == '.') newStr += "..";
            else if (x == '@') newStr += "@.";
            else
            {
                newStr += x.ToString();
            }
        }
        _input = newStr;
    }

    public override ValueTask<string> Solve_2()
    {
        return new();
    }
}

public class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public Node Up { get; set; }
    public Node Down { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }
    public char Letter { get; set; }
}