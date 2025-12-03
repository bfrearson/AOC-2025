// Console.WriteLine(Part1("11-22,95-115,998-1012,1188511880-1188511890,222220-222224,\n1698522-1698528,446443-446449,38593856-38593862,565653-565659,\n824824821-824824827,2121212118-2121212124"));
// Console.WriteLine(Part2("11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"));
// Console.WriteLine(Part2("1188511885-1188511885"));
// Console.WriteLine(Part2("11-12"));

// Console.WriteLine(Part2("4487-9581"));
// Console.WriteLine(Part2("6256098346-6256303872"));
// Console.WriteLine(Part2("6256262562-6256262562"));

var input = File.ReadAllText("Input.txt");
// Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));


long Part1(string inputString)
{
    var invalidIds = new List<long>();
    var ranges = inputString.Split(",");

    foreach (var range in ranges)
    {
        long[] indices = range.Split("-").Select(long.Parse).ToArray();
        for (var i = indices[0]; i <= indices[1]; i++)
        {
            var numberAsString = i.ToString();
            
            if ((numberAsString.Length % 2) != 0)
                continue;
            
            var firstSubstring = numberAsString.Substring(0, numberAsString.Length / 2);
            var secondSubstring = numberAsString.Substring(numberAsString.Length / 2);
            
            if (firstSubstring == secondSubstring)
                invalidIds.Add(i);
        }
    }

    return invalidIds.Sum();
}

long Part2(string inputString)
{
    var invalidIds = new List<long>();
    var ranges = inputString.Split(",");

    foreach (var range in ranges)
    {
        long[] indices = range.Split("-").Select(long.Parse).ToArray();
        for (var i = indices[0]; i <= indices[1]; i++)
        {
            var numberAsString = i.ToString();

            string pattern = numberAsString[0].ToString();
            string buffer = "";
            
            for (var j = 0; j < numberAsString.Length; j++)
            {
                var character = numberAsString[j];
                buffer += character;

                if (buffer != pattern[..buffer.Length])
                {
                    pattern = numberAsString[..(pattern.Length + 1)];
                    j = pattern.Length - 1;
                    buffer = "";
                    continue;
                }
                
                if (buffer.Length < pattern.Length)
                    continue;

                if (buffer == pattern)
                    buffer = "";
            }

            if ((pattern.Length <= numberAsString.Length / 2) && buffer == "")
                invalidIds.Add(i);

        }
    }

    return invalidIds.Sum();
}