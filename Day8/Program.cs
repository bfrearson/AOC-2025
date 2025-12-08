var input = File.ReadAllText("Sample.txt");
Console.WriteLine(Part1(input.Split("\n")));
// Console.WriteLine(Part2(input.Split("\n")));

long Part1(string[] lines)
{
    /* 1. interate over all nodes from position i=0 to length
     *  - compute distance to all other nodes from current i to end
     *  - Store all distances as keys in a sorted dictionary against box pairs
     * 2. For each key in the dictionary's first 1000 entries
     *  - Keep a running count of loop numbers (index)
     *  - enter the two junction boxes into a new SortedDictionary, storing the loop number as the value
     *  - increment the loop number by 1
     *  - for the next key, check each of the junction boxes in that key in the dictionary. If present, store the new key with the existing key's value, else use the loop number index.
     * 3. Return the values of the loop lookup, aggregated by counting the number of each values
     */
    var sortedConnections = new SortedDictionary<double, (string box1, string box2)>();

    // 1. Store all distances between nodes
    for (var i = 0; i < lines.Length; i++)
    {
        for (var j = i+1; j < lines.Length; j++)
        {
            var box1 = GetBox(lines[i]);
            var box2 = GetBox(lines[j]);
            
            var label = box1.x < box2.x ? (lines[i], lines[j]) : (lines[j], lines[i]);
            var distance = GetDistance(box1, box2);
            sortedConnections[distance] = label;
        }
    }
    
    // !!! Consider that the distances may not be unique, so setting sortedConnections[distance] might overwrite an existing lookup! Probably better to keep a running list where we sort as we enter the values? Or try coercing the dictionary into something sortable by value (in which case, we can use a regular dictionary rather than a sorted one)

    // 2. Grab the first 1000 entries and 
    var shortestThousand = sortedConnections.Values.ToArray()[..int.Min(1000, sortedConnections.Count)];
    
    
    return 0;
}

(int x, int y, int z) GetBox(string line)
{
    var parts = line.Split(',');

    return (
        int.Parse(parts[0]),
        int.Parse(parts[1]),
        int.Parse(parts[2])
    );
}

double GetDistance((int x, int y, int z) box1, (int x, int y, int z) box2)
{
    double xDistance = box1.x - box2.x;
    double yDistance = box1.y - box2.y;
    double zDistance = box1.z - box2.z;
    
    return Math.Pow((xDistance * xDistance) + (yDistance * yDistance) + (zDistance * zDistance), 0.5);
}