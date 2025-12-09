var input = File.ReadAllText("Input.txt");
// Console.WriteLine(Part1(input.Split("\n")));
Console.WriteLine(Part2(input.Split("\n")));

long Part1(string[] lines)
{
    var largestArea = 0L;
    for (var i = 0; i < lines.Length; i++)
    {
        var point1 = GetPoint(lines[i]);
        for (var j = i + 1; j < lines.Length; j++)
        {
            var point2 = GetPoint(lines[j]);
            var area = GetArea(point1, point2);
            largestArea = Math.Max(largestArea, area);
        }
    }

    return largestArea;
}

long Part2(string[] lines)
{
    var largestArea = 0L;

    for (var i = 0; i < lines.Length - 1; i++)
    {
        for (var j = i + 1; j < lines.Length; j++)
        {
            
            var point1 = GetPoint(lines[i]);
            var point2 = GetPoint(lines[j]);

            if (PointsInsideLine(point1, point2))
            {
                var area = GetArea(point1, point2);
                // if (area > largestArea)
                    Console.WriteLine($" Rectangle from {point1}, {point2} with area {area} > {largestArea}");
                largestArea = Math.Max(largestArea, area);
            }
        }
    }

    return largestArea;

    bool PointsInsideLine((int x, int y) point1, (int x, int y) point2)
    {
        var minXVertex = Math.Min(point1.x, point2.x);
        var maxXVertex = Math.Max(point1.x, point2.x);
        var minYVertex = Math.Min(point1.y, point2.y);
        var maxYVertex = Math.Max(point1.y, point2.y);

        for (var i = 0; i < lines.Length - 1; i++)
        {
            var boundary1 = GetPoint(lines[i]);
            var boundary2 = GetPoint(lines[i + 1]);
            var minXBoundary = Math.Min(boundary1.x, boundary2.x);
            var maxXBoundary = Math.Max(boundary1.x, boundary2.x);
            var minYBoundary = Math.Min(boundary1.y, boundary2.y);
            var maxYBoundary = Math.Max(boundary1.y, boundary2.y);
            
            if (maxXBoundary > minXVertex &&
                minXBoundary < maxXVertex &&
                maxYBoundary > minYVertex &&
                minYBoundary < maxYVertex
               )
            {
                // Console.WriteLine($"Found line crossing {boundary1}, {boundary2} for rectangle at {point1}, {point2}");
                return false;
            }
        }

        return true;
    }
}

(int x, int y) GetPoint(string line)
{
    var coords = line.Split(",");
    return (int.Parse(coords[0]), int.Parse(coords[1]));
}

long GetArea((int x, int y) point1, (int x, int y) point2)
{
    long xDistance = int.Abs(point1.x - point2.x) + 1;
    long yDistance = int.Abs(point1.y - point2.y) + 1;
    return xDistance * yDistance;
}