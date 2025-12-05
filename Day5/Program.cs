// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText("Input.txt");
Console.WriteLine(Part1(input.Split("\n\n")));

// var input = File.ReadAllText("Input.txt");
// Console.WriteLine(Part2(input.Split("\n")));

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