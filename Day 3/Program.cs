// See https://aka.ms/new-console-template for more information

// var input = File.ReadAllText("Input.txt");
// Console.WriteLine(Part1(input.Split("\n")));
// Isaac's Algorithm
long Part1(string[] banks)
{
    var joltages = new List<long>();
    
    foreach(var bank in banks)
    {
        int firstDigitPosition = 0;
        int secondDigitPosition = 0;
        
        for (var i = 9; i >= 0; i--)
        {
            // Console.WriteLine($"Scanning for number {i} in bank {bank}");
            
            if (bank[..^1].Contains(i.ToString()))
            {
                firstDigitPosition = bank[..^1].IndexOf(i.ToString());
                break;
            }
        }
        
        for (var i = 9; i >= 0; i--)
        {
            if (bank[(firstDigitPosition + 1)..].Contains(i.ToString()))
            {
                secondDigitPosition = bank[(firstDigitPosition + 1)..].IndexOf(i.ToString()) + firstDigitPosition + 1;
                break;
            }
        }

        
        var maximumJoltage = bank[firstDigitPosition].ToString() + bank[secondDigitPosition].ToString();
        Console.WriteLine($"Maximum joltage for bank {bank} is {maximumJoltage}");
        joltages.Add(long.Parse(maximumJoltage));
    }
    return joltages.Sum();
}
