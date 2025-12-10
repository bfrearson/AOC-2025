using System.Text.RegularExpressions;

var input = File.ReadAllText("Sample.txt");
Console.WriteLine(Part1(input.Split("\n")));
// Console.WriteLine(Part2(input.Split("\n")));

int Part1(string[] input)
{
    var machines = ParseMachines(input);

    return 0;
}

Machine[] ParseMachines(string[] input)
{
    Regex regex = new Regex("\\[(.+)\\]\\s(.+)\\s\\{(.+)}");
    var machines = input.Select(line =>
    {
        var match = regex.Match(line);
        var lights = match.Groups[1].Value.ToArray().Select(x => x == '.' ? false : true).ToArray();
        var buttons = match.Groups[2].Value.Split(" ").Select(x => x[1..^1].Split(",").Select(int.Parse).ToList()).ToList();
        return new Machine(lights, buttons);
    });
    return machines.ToArray();
}

class Machine
{
    private bool[] lights;
    private List<List<int>> buttons;

    public Machine(bool[] lights, List<List<int>> buttons)
    {
        this.lights = lights;
        this.buttons = buttons;
    }
}