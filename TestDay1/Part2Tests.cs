namespace TestDay1;
using Day1;

[TestFixture]
public class Part2Tests
{
    
    
    [Test]
    public void TestExampleCount()
    {
        string input = File.ReadAllText("Example.txt");
        Assert.That(Part2.Code(input), Is.EqualTo(6));
    }
}