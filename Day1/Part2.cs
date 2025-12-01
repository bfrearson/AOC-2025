namespace Day1;

public struct Part2
{
    public static int Code(string input)
    {
        int[] intLines = Utility.ParseInput(input);

        int count = 50;
        int code = 0;

        foreach (var line in intLines)
        {

            int increment = count + line;
            Console.WriteLine($"{line}: {increment}");
            
            // Calculate the number of times we pass 0
            if (increment < 0 && count != 0)
            {
                Console.WriteLine($"{line} crossed zero");
                code += 1;
            }
            
            // Set the absolute count difference
            count += line % 100;
            
            // Convert the count to a value between 0 and 99
            var remainder = ((count % 100) + 100) % 100;
            count = remainder;
            Console.WriteLine($"The dial is rotated {line} to point at {count}");
            
            int quotient = Math.Abs(increment) / 100;
            code += quotient;
            if (quotient != 0)
            {
                Console.WriteLine($"{line} points at 0 {quotient} times");
            }
            
            if (count == 0 && quotient == 0)
            {
                code += 1;
            }
            Console.WriteLine(code);
        }

        return code;
    }

    static int zeroCrossings(int input)
    {
        if (input == 0)
        {
            return 0; 
        }
        return (1 - input / Math.Abs(input)) / 2;
    }
    
}

/*
The quotient is calculated as the absolute new count, divided by 100, as an int. However, when the new count is negative, this isn't correctly calculated and returns 0 (because this doesn't factor in crossing 0).

We need a way to consider the zero crossing. We can use an if statement, but is there some mathematical operation that can increment the count by 0 if positive, and 1 if negative?
*/