namespace TestDay1;
using Day1;
using System.IO;

[TestFixture]
public class Part1Tests
{
    [Test]
    public void TestExampleCount()
    {
        string input = File.ReadAllText("Example.txt");
        Assert.That(Part1.Code(input), Is.EqualTo(3));
    }
}