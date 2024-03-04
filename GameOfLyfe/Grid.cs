namespace GameOfLyfe;

class Grid(int columns, int rows) : IGrid
{
    bool[,] Values = new bool[columns, rows];
    public bool[,] ReturnValues()
    {
        return Values;
    }

    public void SetValues(bool[,] values)
    {
        Values = values;
    }


}