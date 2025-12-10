using System.Text.RegularExpressions;

var input = File.ReadAllText("Sample.txt");
Console.WriteLine(Part1(input.Split("\n")));
// Console.WriteLine(Part2(input.Split("\n")));

int Part1(string[] input)
{
    
    // Sample passes, but Input doesn't. 
    // Re-reading the challenge, you can press buttons multiple times.
    var machines = ParseMachines(input);
    var sum = 0;
    foreach (var machine in machines)
    {
        var fewestPresses = FindFewestPresses(machine);
        Console.WriteLine($"{fewestPresses}");
        sum += fewestPresses;
    }

    return sum;
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


int FindFewestPresses(Machine machine)
{
    var n = machine.buttons.Count;
    for (var r = 1; r <= n; r++)
    {
        for (var i = 0; i <= n - r; i++)
        {
            var range = machine.buttons.GetRange(i, r);
            var lightState = machine.lights.Clone() as bool[];

            foreach (var button in range)
            {
                foreach (var index in button)
                {
                    var lightIndex = index;
                    lightState[lightIndex] = lightState[lightIndex] ? false : true;
                }
            }

            Console.WriteLine(string.Join(" ", lightState));
            
            if (lightState.All(x => x == false))
            {
                return r;
            }
        }
    }

    return n;
}

class Machine
{
    public bool[] lights;
    public List<List<int>> buttons;

    public Machine(bool[] lights, List<List<int>> buttons)
    {
        this.lights = lights;
        this.buttons = buttons;
    }
}