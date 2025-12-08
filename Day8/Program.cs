var input = File.ReadAllText("Sample.txt");
// Console.WriteLine(Part1(input.Split("\n")));
Console.WriteLine(Part2(input.Split("\n")));

long Part1(string[] lines)
{
    var distances = new List<(double distance, (string, string) boxes)>();
    // 1. Store all distances between nodes
    for (var i = 0; i < lines.Length; i++)
    {
        for (var j = i+1; j < lines.Length; j++)
        {
            var box1 = GetBox(lines[i]);
            var box2 = GetBox(lines[j]);
            var distance = GetDistance(box1, box2);
            distances.Add((distance, (lines[i], lines[j])));
        }
    }
    
    distances.Sort((a, b) =>a.distance.CompareTo(b.distance));

    var maxConnections = int.Min(1000, distances.Count() - 1);
    distances = distances[..(maxConnections)];

    List<string> nodes = new List<string>(lines);
    var connections = new List<List<string>>();
    
    foreach (var link in distances)
    {
        var isBox1Present = !nodes.Contains(link.boxes.Item1);
        var isBox2Present = !nodes.Contains(link.boxes.Item2);

        switch (isBox1Present, isBox2Present)
        {
            case (true, true):
             {
                 int box1ListIndex = 0;
                 int box2ListIndex = 0;
                 for (var i = 0; i < connections.Count; i++)
                 {
                     var circuit = connections[i];
                     if (circuit.Contains(link.boxes.Item1))
                         box1ListIndex = i;
                     if (circuit.Contains(link.boxes.Item2))
                         box2ListIndex = i;
                 }
                 
                 if (box1ListIndex == box2ListIndex)
                     break;
                 var combinedList = connections[box1ListIndex].Concat(connections[box2ListIndex]).ToList();
                 connections[box1ListIndex] = combinedList;
                 connections.Remove(connections[box2ListIndex]);
                 break;
             }
            case (false, true):
            case (true, false):
            {
                var matchedBox = isBox1Present ? link.boxes.Item1 : link.boxes.Item2;
                var unmatchedBox = isBox2Present ? link.boxes.Item1 : link.boxes.Item2;
                {
                    foreach (var circuit in connections)
                    {
                        if (circuit.Contains(matchedBox))
                        {
                            circuit.Add(unmatchedBox);
                            nodes.Remove(unmatchedBox);
                        }
                    }
                }
                break;
            }
            case (false, false):
            {
                var list = new List<string>{link.boxes.Item1, link.boxes.Item2};
                connections.Add(list);
                nodes.Remove(link.boxes.Item1);
                nodes.Remove(link.boxes.Item2);
                break;
            }
        }
    }

    connections.Sort((a, b) => b.Count.CompareTo(a.Count));
    return connections[..3].Aggregate(1L, (acc, list) => acc *= list.Count());
}

(int x, int y, int z) GetBox(string line)
{
    var parts = line.Split(',');
    return (x: int.Parse(parts[0]), y: int.Parse(parts[1]), z: int.Parse(parts[2]));
}

double GetDistance((int x, int y, int z) box1, (int x, int y, int z) box2)
{
    double xDistance = box1.x - box2.x;
    double yDistance = box1.y - box2.y;
    double zDistance = box1.z - box2.z;
    
    return Math.Pow((xDistance * xDistance) + (yDistance * yDistance) + (zDistance * zDistance), 0.5);
}