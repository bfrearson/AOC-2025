using Microsoft.VisualBasic.CompilerServices;

var input = File.ReadAllText("Input.txt");
Console.WriteLine(Part1(input.Split("\n")));

// var input = File.ReadAllText("Input.txt");
// Console.WriteLine(Part2(input.Split(\n\n")));

long Part1(string[] lines)
{
    long sum = 0;
    
    var numbers = lines[..^1];
    var symbols = lines[^1];
    for (var i = 0; i < symbols.Length; i++)
    {
        var symbol = symbols[i].ToString();
        if (symbol != " ")
        {
            var computation = new List<long>();
            for (var row = 0; row < numbers.Length; row++)
            {
                var numString = numbers[row]
                    .Substring(i)
                    .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .First();
                
                var number = long.Parse(numString);
                computation.Add(number);
            }

            var solution = computation.Aggregate((a, b) =>
            {
                switch (symbol)
                {
                    case "+":
                        a += b;
                        break;
                    case "*": 
                        a *= b;
                        break;
                }

                return a;
            });
            sum += solution;
        }
    }
    return sum;
}