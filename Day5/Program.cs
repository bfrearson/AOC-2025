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
    List<(long, long)> ingredientIds = inventory[0].Split("\n").Select(x =>
        {
            var range = x.Split("-").Select(x => long.Parse(x)).ToArray();
            return (range[0], range[1]);
        })
        .ToList();

    ingredientIds.Sort(delegate((long, long) a, (long, long) b)
    {
        if (a.Item1 < b.Item1)
            return -1;
        else return 1;
    });
    
    var compressedIds = new List<(long, long)>();
    compressedIds.Add(ingredientIds[0]);
    foreach (var ingredientRange in ingredientIds)
    {
        var lastRange = compressedIds.Last();
            if (lastRange.Item2 < ingredientRange.Item1)
            {
                compressedIds.Add(ingredientRange);
            }
            else
            {
                var min = long.Min(ingredientRange.Item1, compressedIds[^1].Item1);
                var max = long.Max(ingredientRange.Item2, compressedIds[^1].Item2);
                compressedIds[^1] = (min, max);
            }
    }

    return compressedIds.Select(x => x.Item2 - x.Item1 + 1).Sum();
}