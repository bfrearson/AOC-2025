using System.Text.RegularExpressions;

var input = File.ReadAllText("Input.txt");
Console.WriteLine(Part1(input.Split("\n")));
Console.WriteLine(Part2(input.Split("\n")));

long Part1(string[] lines)
{
    var splits = 0;
    for (var row = 1; row < lines.Length; row++)
    {
        var line = lines[row];
        for (var col = 0; col < lines[row].Length; col++)
        {
            var cell = line[col];

            if (cell == '.' && (lines[row - 1][col] == '|' || lines[row - 1][col] == 'S'))
            {
                line = line.Remove(col, 1);
                line = line.Insert(col, "|");
            }

            if (cell == '^' && lines[row - 1][col] != '.')
            {
                line = line.Remove(col - 1, 1);
                line = line.Insert(col - 1, "|");
                line = line.Remove(col + 1, 1);
                line = line.Insert(col + 1, "|");

                splits += 1;
            }
        }
        Console.WriteLine(line);
        lines[row] = line;
    }

    return splits;
}


long Part2(string[] lines)
{
    var grid = lines
        .Select(line => line.Select(x => x.ToString()).ToList())
        .ToList();

    grid[1][grid[0].IndexOf("S")] = "1";

    for (var row = 1; row < grid.Count; row++)
    {
        var line = grid[row];
        for (var col = 0; col < grid[row].Count; col++)
        {
            var cell = line[col];
            if (cell != "^")
            {
                long.TryParse(cell, out var currentCellValue);
                long.TryParse(grid[row - 1][col], out var inputCellValue);

                line[col] = (currentCellValue + inputCellValue).ToString();
            }

            if (cell == "^")
            {
                long.TryParse(grid[row - 1][col], out var inputCellValue);
                long.TryParse(line[col - 1], out var leadingCellValue);
                long.TryParse(line[col + 1], out var trailingCellValue);
                line[col - 1] = (leadingCellValue + inputCellValue).ToString();
                line[col + 1] = (trailingCellValue + inputCellValue).ToString();
            }
        }
        grid[row] = line;
    }

    return grid.Last().Aggregate(0, (long acc, string x) =>
    {
        long.TryParse(x, out var intValue);
        return acc + intValue;
    });
}