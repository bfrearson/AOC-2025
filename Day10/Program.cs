using System.Text.RegularExpressions;

var input = File.ReadAllText("InputTest.txt");
// Console.WriteLine(Part1(input.Split("\n")));
// Console.WriteLine(Part2(input.Split("\n")));
// PrintPerms();


List<List<int>> GetIndexCombinations(int n, int r, int index)
{
    if (r == 0)
        return new List<List<int>>() { new List<int>(){} };
    
    var combinations = new List<List<int>>();
    for (var i = 0; i <= n - r; i++)
    {
        var subCombos = GetIndexCombinations(n - i - 1, r - 1, i + 1);
        foreach (var combo in subCombos)
        {
            var modifiedCombo = combo.Select(x=> x += 1 + i).ToList();
            modifiedCombo.Insert(0,i);
            combinations.Add(modifiedCombo);
        }
    }
    return combinations;
}


void PrintPerms()
{
    for (var r = 1; r <= 3; r++)
    {
        var perms = GetIndexCombinations(5, r, 0);
        foreach (var perm in perms)
        {
            Console.WriteLine(string.Join(",", perm));
        }
    }
}

int Part1(string[] input)
{
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
        var buttons = match.Groups[2].Value.Split(" ").Select(x => x[1..^1]).ToList();
        return new Machine(lights, buttons);
    });
    return machines.ToArray();
}


int FindFewestPresses(Machine machine)
{
    Console.WriteLine(" ");
    Console.WriteLine(string.Join("", machine.lights.Select(x => x ? "#" : ".")));
    Console.WriteLine(" ");
    var n = machine.buttons.Count;
  
    
    
    for (var r = 1; r <= n; r++)
    {

        
        
        
        // This is wrong. We're getting only adjacent ranges. We need to interate over all permutations
        for (var i = 0; i <= n - r; i++)
        {
            var range = machine.buttons.GetRange(i, r);
            var lightState = machine.lights.Clone() as bool[];

            foreach (var button in range.Select(x=>x.Split(',')))
            {
                foreach (var index in button)
                {
                    var lightIndex = int.Parse(index);
                    lightState[lightIndex] = lightState[lightIndex] == true ? false : true;
                }
            }

            // Console.WriteLine(string.Join(" ", lightState));
            
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
    public List<string> buttons;

    public Machine(bool[] lights, List<string> buttons)
    {
        this.lights = lights;
        this.buttons = buttons;
    }
}