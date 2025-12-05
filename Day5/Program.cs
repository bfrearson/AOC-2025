// var input = File.ReadAllText("Input.txt");
// Console.WriteLine(Part1(input.Split("\n\n")));

var input = File.ReadAllText("Input.txt");
Console.WriteLine(Part2(input.Split("\n\n")));

long Part1(string[] inventory)
{
    var ingredientIds = inventory[0].Split("\n");
    var ingredients = inventory[1].Split("\n").Select(x => long.Parse(x)).ToList();

    var freshIngredients = new List<long>();
    foreach (var ingredient in ingredients)
    {
        foreach (var idRange in ingredientIds)
        {
            var range= idRange.Split("-").Select(x => long.Parse(x)).ToArray();
            if (ingredient >= range[0] && ingredient <= range[1])
            {
                freshIngredients.Add(ingredient);
                break;
            }
        }
    }
    return freshIngredients.Count;
}

long Part2(string[] inventory)
{
    var ingredientIds = inventory[0].Split("\n");
    var compressedIds = new List<(long,long)>();
    
    foreach (var idRange in ingredientIds)
    {
        // Console.WriteLine($"Searching for {idRange}");
        var range= idRange.Split("-").Select(x => long.Parse(x)).ToArray();
        (long min, long max) = (range[0], range[1]);
        
        var lowerOverlapIndex = 0;
        while (lowerOverlapIndex < compressedIds.Count && min > compressedIds[lowerOverlapIndex].Item2) 
        {
            lowerOverlapIndex++;
        }

        if (lowerOverlapIndex >= compressedIds.Count)
        {
            compressedIds.Add((min, max));
            continue;
        }
        
        if (max < compressedIds[lowerOverlapIndex].Item1)
        {
            compressedIds.Insert(lowerOverlapIndex, (min, max));
            continue;
        }

        var upperOverlapIndex = lowerOverlapIndex;
        while (upperOverlapIndex < compressedIds.Count && max > compressedIds[upperOverlapIndex].Item1)
        {
            upperOverlapIndex++;
        }

        upperOverlapIndex = int.Min(upperOverlapIndex, compressedIds.Count - 1);
        min = long.Min(min, compressedIds[lowerOverlapIndex].Item1);
        max = long.Max(max, compressedIds[upperOverlapIndex].Item2);
        
        compressedIds.Insert(lowerOverlapIndex, (min, max));
        // Console.WriteLine($"Inserted{compressedIds[lowerOverlapIndex]}");
        Console.WriteLine($"Removing {upperOverlapIndex - lowerOverlapIndex + 1} entries");
        compressedIds.RemoveRange(lowerOverlapIndex + 1,  upperOverlapIndex - lowerOverlapIndex + 1);
        
        if (compressedIds.Count == 1)
            Console.WriteLine($"New count: {compressedIds.Count}");
    }

    // foreach (var idRange in ingredientIds)
    // {
    //     var range = idRange.Split("-").Select(x => long.Parse(x)).ToArray();
    //     (long min, long max) = (range[0], range[1]);
    //     var count = 0;
    //     foreach (var id in compressedIds)
    //     {
    //         if (min >= id.Item1 && min <= id.Item2 || max <= id.Item2 && max >= id.Item1)
    //         {
    //             count++;
    //         }
    //     }
    //     Console.WriteLine($"{count}");
    // }

    return compressedIds.Select(x => x.Item2 - x.Item1 + 1).Sum();
}