using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string[] _input;

    public Day06()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public char[][] ParseInput()
    {
        return _input.Select(x => x.ToCharArray()).ToArray();
    }

    public void PrintMap(char[][] map, Point pos)
    {
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                if (y == pos.y && x == pos.x) Console.ForegroundColor = ConsoleColor.Green;

                if (map[y][x] == '#') Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(x, y);
                Console.Write(map[y][x]);

            }
        }
        Thread.Sleep(50);
    }

    public override ValueTask<string> Solve_2()
    {
        var map = ParseInput();

        var count = 0;

        Parallel.For(0, map.Length, j =>
        {
            Parallel.For(0, map[j].Length, i =>
            {
                //Console.WriteLine($"{j}, {i}");
                if (map[j][i] == '.')
                {
                    var mapCopy = ParseInput();
                    mapCopy[j][i] = '#';
                    //PrintMap(mapCopy);
                    var t = NavigateUntilOff(mapCopy);
                    if (t) {
                        //Console.WriteLine($"{i} {j}");
                        Interlocked.Increment(ref count); 
                    }
                    //Console.WriteLine(count);
                }
            });
        });

        return new(count.ToString());
    }

    public bool NavigateUntilOff(char[][] map)
    {
        //Find Start
        var pos = new Point(0, 0);
        string dir = null;
        foreach (var (j, y) in map.Index())
        {
            foreach (var (i, x) in y.Index())
            {
                if (x == '^')
                {
                    pos = new Point(i, j);
                    map[j][i] = '.';
                    dir = "up";
                    break;
                }
            }
            if (!string.IsNullOrEmpty(dir)) break;
        }
        var off = false;
        var loop = false;
        var posDict = new Dictionary<string, int>();
        while (!off && !loop)
        {
            switch (dir)
            {
                case "up":
                    if (pos.y == 0) { off = true; }
                    else
                    {
                        Move(map, pos, new Point(pos.x, pos.y - 1), ref dir, posDict, out loop);
                    }
                    break;
                case "left":
                    if (pos.x == 0) { off = true; }
                    else
                    {
                        Move(map, pos, new Point(pos.x - 1, pos.y), ref dir, posDict, out loop);
                    }
                    break;
                case "right":
                    if (pos.x == map[pos.x].Count() - 1) { off = true; }
                    else
                    {
                        Move(map, pos, new Point(pos.x + 1, pos.y), ref dir, posDict, out loop);
                    }
                    break;
                case "down":
                    if (pos.y == map.Count() - 1) { off = true; }
                    else
                    {
                        Move(map, pos, new Point(pos.x, pos.y + 1), ref dir, posDict, out loop);
                    }
                    break;
            }
            //PrintMap(map, pos);
        }
        return loop;
    }

    public void Move(char[][] map, Point from, Point to, ref string dir, Dictionary<string, int> posDict, out bool loop)
    {
        loop = false;
        var key = $"{from.y}-{from.x}{dir}";
        if (posDict.ContainsKey(key)) { 
            loop = true; 
            //Console.WriteLine("Loop Detected!");
        }
        posDict[key] = 1;
        if (map[to.y][to.x] == '#')
        {
            if (dir == "up") dir = "right";
            else if (dir == "down") dir = "left";
            else if (dir == "left") dir = "up";
            else if (dir == "right") dir = "down";
        }
        else
        {
            from.x = to.x;
            from.y = to.y;
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var map = ParseInput();
        return new("");
    }
}

public class Point
{
    public int x { get; set; }
    public int y { get; set; }
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}