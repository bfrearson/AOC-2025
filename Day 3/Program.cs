// See https://aka.ms/new-console-template for more information

// var input = File.ReadAllText("Input.txt");
// Console.WriteLine(Part1(input.Split("\n")));

var input = File.ReadAllText("Input.txt");
Console.WriteLine(Part2(input.Split("\n")));

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

long Part2(string[] banks)
{
    var joltages = new List<long>();
    
    foreach(var bank in banks)
    {
        var numberOfIndices = bank.Length - 12;
        var lowestIndices = new List<int>();
        var runningSubstring = bank;
        var maximumJoltage = "";
        while (maximumJoltage.Length < 12)
        {
            var substring = HighestSubstring(runningSubstring, 12 - maximumJoltage.Length);
            runningSubstring = substring[1..];
            maximumJoltage += substring.First().ToString();
        }
        
        // Console.WriteLine($"Maximum joltage for bank {bank} is {maximumJoltage}");
        joltages.Add(long.Parse(maximumJoltage));
    }
    
    return joltages.Sum();
}

int GetLowestIndex(string bank)
{
    var runningIndex = bank.Length - 1;
    var runningValue = bank.Last();

    for (var i = 0; i < bank.Length; i++)
    {
        if (bank[i] < runningValue)
        {
            runningValue = bank[i];
            runningIndex = i;
        }
    }

    return runningIndex;
}

string HighestSubstring(string bank, int length)
{
    // Console.WriteLine($"Finding {length} long substring in {bank}");
    // var runningIndex = bank.Length - length;
    // var runningValue = bank[^length];
    var runningIndex = 0;
    var runningValue = bank[0];

    for (var i = 0; i <= (bank.Length - length); i++)
    {
        // Console.WriteLine($"position {i}, value {bank[i]}, running value {runningValue}@{runningIndex}");
        if (bank[i] > runningValue)
        {
            runningValue = bank[i];
            runningIndex = i;
        }
    }
    // Console.WriteLine($"Running value {runningValue}@{runningIndex}");
    var substring = bank[runningIndex..];
    // Console.WriteLine($"Highest substring {substring} for bank {bank}");
    return substring;
}