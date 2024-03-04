namespace GameOfLyfe.tests;

public class Debug
{
    [Fact]
    public void UnitTestTest()
    {
        string expected = "UnitTest";
        var actual = GameOfLyfe.Debug.UnitTestTest();
        Assert.Equal(expected, actual);
    }
}