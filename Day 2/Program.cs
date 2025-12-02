// Console.WriteLine(Part1("11-22,95-115,998-1012,1188511880-1188511890,222220-222224,\n1698522-1698528,446443-446449,38593856-38593862,565653-565659,\n824824821-824824827,2121212118-2121212124"));

var input = File.ReadAllText("Input.txt");

Console.WriteLine(Part1(input));


long Part1(string input)
{
    var invalidIds = new List<long>();
    var ranges = input.Split(",");

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