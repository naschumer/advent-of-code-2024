using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day8 : BaseDay
{
    private readonly string[] _input;
    private char[][] _puzzle;

    public Day8()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public char[][] ParseInput()
    {
        return _input.Select(x => x.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        _puzzle = ParseInput();

        var freqs = GetFreqs();

        var antinodes = new HashSet<(int, int)>();
        foreach (var freq in freqs)
        {
            foreach (var (y, line) in _puzzle.Index())
            {
                foreach (var (x, val) in line.Index())
                {
                    if (val == freq)
                    {
                        foreach (var (y1, line1) in _puzzle.Index())
                        {
                            foreach (var (x1, val1) in line1.Index())
                            {
                                if (val1 == val && (y != y1 || x != x1))
                                {
                                    GetAntinodes(x, y, x1, y1, antinodes);
                                }
                            }
                        }
                    }
                }
            }
        }
        PrintNodes(antinodes);
        return new(antinodes.Count().ToString());
    }

    private void PrintNodes(HashSet<(int, int)> antinodes)
    {
        foreach (var (y, line) in _puzzle.Index())
        {
            foreach (var (x, val) in line.Index())
            {
                if (antinodes.Contains((x, y))) { Console.Write('#'); } else
                {
                    Console.Write(val.ToString());
                }
            }
            Console.WriteLine();
        }
    }

    private void GetAntinodes(int x, int y, int x1, int y1, HashSet<(int, int)> antinodes)
    {
        antinodes.Add((x1, y1));
        var off = false;
        var node = (0, 0);
        var distX = Math.Abs(x - x1);
        var distY = Math.Abs(y - y1);
        do
        {
            if (x1 > x && y1 > y) { node = (x1 + distX, y1 + distY); }
            if (x1 < x && y1 > y) { node = (x1 - distX, y1 + distY); }
            if (x1 < x && y1 < y) { node = (x1 - distX, y1 - distY); }
            if (x1 > x && y1 < y) { node = (x1 + distX, y1 - distY); }
            if (node.Item1 <= _puzzle[0].Count() - 1 && node.Item1 >= 0 && node.Item2 <= _puzzle.Count() - 1 && node.Item2 >= 0)
            {
                antinodes.Add(node);
                x1 = node.Item1;
                y1 = node.Item2;
            }
            else
            {
                off = true;
            }
        } while (!off);
    }

    public HashSet<char> GetFreqs()
    {
        var freqs = new HashSet<char>();
        foreach (var (y, line) in _puzzle.Index())
        {
            foreach (var (x, val) in line.Index())
            {
                if (val != '.') freqs.Add(val);
            }
        }
        return freqs;
    }

    public override ValueTask<string> Solve_2()
    {
        return new("");
    }
}