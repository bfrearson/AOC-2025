namespace Day1;

public struct Part1
{
    public static int Code(string input)
    {
        int[] intLines = Utility.ParseInput(input);

        int count = 50;
        int code = 0;

        foreach (var line in intLines)
        {
            // Set the absolute count difference
            count += line % 100;
            
            // Convert the count to a value between 0 and 99
            count = ((count % 100) + 100) % 100;
            
            if (count == 0)
            {
                code += 1;
            }
        }

        return code;
    }
}