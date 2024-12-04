using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }
    public override ValueTask<string> Solve_1()
    {
        var puzzle = ParseInput(_input);

        var count = 0;
        foreach (var (i, row) in puzzle.Index())
        {
            var rowStr = string.Join("", row);
            foreach (var (j, item) in row.Index())
            {
                if (item == "X")
                {
                    var rightOk = j <= row.Count() - 4;
                    var leftOk = j >= 3;
                    var upOk = i >= 3;
                    var downOk = i <= puzzle.Count() - 4;
                    //Right
                    if (rightOk && rowStr.Substring(j, 4) == "XMAS")
                    {
                        count++;
                    }
                    //Left
                    if (leftOk && rowStr.Substring(j - 3, 4) == "SAMX")
                    {
                        count++;
                    }
                    //Up
                    if (upOk && puzzle[i - 1][j] == "M" && puzzle[i - 2][j] == "A" && puzzle[i - 3][j] == "S")
                    {
                        count++;
                    }
                    //Up Right
                    if (upOk && rightOk && puzzle[i - 1][j + 1] == "M" && puzzle[i - 2][j + 2] == "A" && puzzle[i - 3][j + 3] == "S")
                    {
                        count++;
                    }
                    //Up Left
                    if (upOk && leftOk && puzzle[i - 1][j - 1] == "M" && puzzle[i - 2][j - 2] == "A" && puzzle[i - 3][j - 3] == "S")
                    {
                        count++;
                    }
                    //Down
                    if (downOk && puzzle[i + 1][j] == "M" && puzzle[i + 2][j] == "A" && puzzle[i + 3][j] == "S")
                    {
                        count++;
                    }
                    //Down Right
                    if (downOk && rightOk && puzzle[i + 1][j + 1] == "M" && puzzle[i + 2][j + 2] == "A" && puzzle[i + 3][j + 3] == "S")
                    {
                        count++;
                    }
                    //Down Left
                    if (downOk && leftOk && puzzle[i + 1][j - 1] == "M" && puzzle[i + 2][j - 2] == "A" && puzzle[i + 3][j - 3] == "S")
                    {
                        count++;
                    }
                }
            }
        }

        return new(count.ToString());
    }

    public List<List<string>> ParseInput(string[] input)
    {
        return input.Select(x => x.Select(y => y.ToString()).ToList()).ToList();
    }

    public override ValueTask<string> Solve_2()
    {
        var puzzle = ParseInput(_input);

        var count = 0;
        foreach (var (i, row) in puzzle.Index())
        {
            var rowStr = string.Join("", row);
            foreach (var (j, item) in row.Index())
            {
                if (item == "A")
                {
                    var rightOk = j <= row.Count() - 2;
                    var leftOk = j >= 1;
                    var upOk = i >= 1;
                    var downOk = i <= puzzle.Count() - 2;
                    var diag1Good = false;
                    var diag2Good = false;
                    //Up Right
                    if (upOk && rightOk && puzzle[i - 1][j + 1] == "M")
                    {
                        //Down Left
                        if (downOk && leftOk && puzzle[i + 1][j - 1] == "S")
                        {
                            diag1Good = true;
                        }
                    }
                    //Up Right
                    if (upOk && rightOk && puzzle[i - 1][j + 1] == "S")
                    {
                        //Down Left
                        if (downOk && leftOk && puzzle[i + 1][j - 1] == "M")
                        {
                            diag1Good = true;
                        }
                    }
                    //Up Left
                    if (upOk && leftOk && puzzle[i - 1][j - 1] == "M")
                    {
                        //Down Right
                        if (downOk && rightOk && puzzle[i + 1][j + 1] == "S")
                        {
                            diag2Good = true;
                        }
                    }
                    //Up Left
                    if (upOk && leftOk && puzzle[i - 1][j - 1] == "S")
                    {
                        //Down Right
                        if (downOk && rightOk && puzzle[i + 1][j + 1] == "M")
                        {
                            diag2Good = true;
                        }
                    }
                    count = count + ((diag1Good && diag2Good) ? 1 : 0);
                }
            }
        }

        return new(count.ToString());
    }
}
