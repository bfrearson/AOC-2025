// See https://aka.ms/new-console-template for more information

// var input = File.ReadAllText("Input.txt");
// Console.WriteLine(Part1(input.Split("\n")));

var input = File.ReadAllText("Input.txt");
Console.WriteLine(Part2(input.Split("\n")));

int Part1(string[] lines)
{
    var positions = new List<(int, int)>();
    for (var y = 0; y < lines.Length; y++)
    {
        var row = lines[y];
        var yMin =  Math.Max(y - 1, 0);
        // Add 2 because array subrange is closed
        var yMax =  Math.Min(y + 2, lines.Length);
        for (var x = 0; x < row.Length; x++)
        {
            if (row[x] != '@')
                continue;
            var xMin =  Math.Max(x - 1, 0);
            var xMax =  Math.Min(x + 2, row.Length);
            var values = new List<(int, int)>();
            // Console.WriteLine($"searching position ({x}, {y}) with y range {yMin}..{yMax}, x range {xMin}..{xMax}");
            foreach (var targetRow in lines[yMin..yMax])
            {
                foreach (var character in targetRow[xMin..xMax])
                {
                    // Console.WriteLine($"{character} -> {targetRow[xMin..xMax]}");
                    if (character == '@')
                    {
                        values.Add((x, y));
                    }
                }
            }
            // Console.WriteLine($"Count for position {(x,y)}: {values.Count}");
            if (values.Count < 5)
            {
                positions.Add((x, y));
            }
        }
    }
    return positions.Count;
}

int Part2(string[] lines)
{
    var positions = new List<(int, int)>();
    var didModify = true;

    while (didModify)
    {
        didModify = false;
        for (var y = 0; y < lines.Length; y++)
        {
            var row = lines[y];
            var yMin = Math.Max(y - 1, 0);
            // Add 2 because array subrange is closed
            var yMax = Math.Min(y + 2, lines.Length);
            for (var x = 0; x < row.Length; x++)
            {
                if (row[x] != '@')
                    continue;
                var xMin = Math.Max(x - 1, 0);
                var xMax = Math.Min(x + 2, row.Length);
                var values = new List<(int, int)>();
                // Console.WriteLine($"searching position ({x}, {y}) with y range {yMin}..{yMax}, x range {xMin}..{xMax}");
                foreach (var targetRow in lines[yMin..yMax])
                {
                    foreach (var character in targetRow[xMin..xMax])
                    {
                        // Console.WriteLine($"{character} -> {targetRow[xMin..xMax]}");
                        if (character == '@')
                        {
                            values.Add((x, y));
                        }
                    }
                }

                // Console.WriteLine($"Count for position {(x,y)}: {values.Count}");
                if (values.Count < 5)
                {
                    positions.Add((x, y));
                    row = row.Remove(x, 1);
                    row = row.Insert(x, ".");
                    lines[y] = row;
                    didModify = true;
                }
            }
        }
    }

    Console.WriteLine(lines);
    return positions.Count;
}