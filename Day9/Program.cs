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
    // 1. iterate over the coords, the find the next and next + 1 points.
    // 2. Calculate the missing 4th point
    // 3. Check the remaining list to see if it's green
    // - green if there are coords pairs that "contain" the point in 4 directions
    // 4. If green, calculate area

    var largestArea = 0L;

    for (var i = 0; i < lines.Length - 2; i++)
    {
        var oppositeVertex1 = GetPoint(lines[i]);
        var adjacentVertex = GetPoint(lines[i + 1]);
        var oppositeVertex2 = GetPoint(lines[i + 2]);
        var missingVertex = (x: oppositeVertex1.x - adjacentVertex.x + oppositeVertex2.x,
            y: oppositeVertex1.y - adjacentVertex.y + oppositeVertex2.y);
        // Console.WriteLine($"Checking rectangle {oppositeVertex1}, {adjacentVertex}, {oppositeVertex2}, {missingVertex}");
        (int x, int y) checkDirections = GetDirections(missingVertex, adjacentVertex);

        if (IsPointInsideBounds(missingVertex, checkDirections))
        {
            var area = GetArea(oppositeVertex1, oppositeVertex2);
            // Console.WriteLine($"Found point {missingVertex} for corners {oppositeVertex1}, {oppositeVertex2} with area {area}");
            largestArea = Math.Max(largestArea, area);
        }
    }

    return largestArea;

    bool IsPointInsideBounds((int x, int y) point, (int x, int y) checkDirections)
    {
        var isXValid = false;
        var isYValid = false;
        for (var i = 0; i < lines.Length - 1; i++)
        {
            if (isXValid && isYValid)
                return true;

            var bound1 = GetPoint(lines[i]);
            var bound2 = GetPoint(lines[i + 1]);

            var maxX = Math.Max(bound1.x, bound2.x);
            var minX = Math.Min(bound1.x, bound2.x);
            var maxY = Math.Max(bound1.y, bound2.y);
            var minY = Math.Min(bound1.y, bound2.y);

            if (point.x >= minX && point.x <= maxX && (checkDirections.y * (point.y - minY)) <= 0)
            {
                // Console.WriteLine($"Point {point} is bound by {bound1}, {bound2} in x for direction y{checkDirections.y}");
                isXValid = true;
            }

            if (point.y >= minY && point.y <= maxY && (checkDirections.x * (point.x - minX)) <= 0)
            {
                // Console.WriteLine($"Point {point} is bound by {bound1}, {bound2} in y for direction x{checkDirections.x}");
                isYValid = true;
            }
        }

        return (isXValid && isYValid);
    }
}

(int x, int y) GetPoint(string line)
{
    var coords = line.Split(",");
    return (int.Parse(coords[0]), int.Parse(coords[1]));
}

long GetArea((int x, int y) point1, (int x, int y) point2)
{
    long xDistance = point1.x - point2.x + 1;
    long yDistance = point1.y - point2.y + 1;
    return Math.Abs(xDistance * yDistance);
}

(int x, int y) GetDirections((int x, int y) point1, (int x, int y) point2)
{
    var xDiff = point1.x - point2.x;
    var yDiff = point1.y - point2.y;

    var xDirection = xDiff == 0 ? 0 : xDiff / int.Abs(xDiff);
    var yDirection = yDiff == 0 ? 0 : yDiff / int.Abs(yDiff);

    return (xDirection, yDirection);
}