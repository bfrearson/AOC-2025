namespace Day1;

public struct Utility
{
    public static int[] ParseInput(string input)
    {
        // foreach (string line in input.Split('\n'))
        string[] lines = input.Split('\n');
        return lines.Select(ConvertInstructionToInt).ToArray();
    }

    static int ConvertInstructionToInt(string instruction)
    {
        string direction = instruction[0].ToString();
        int magnitude = short.Parse(instruction[1..]);

        return direction == "R" ? magnitude : -1 * magnitude;
    }
}