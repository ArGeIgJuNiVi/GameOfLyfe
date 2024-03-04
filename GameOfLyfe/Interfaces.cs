namespace GameOfLyfe;

interface ICell { /*cells are literally booleans*/ }

class Cell : ICell {/*why are we supposed to add an entire class around that*/}

interface IGrid
{
    bool[,] ReturnValues();

    void SetValues(bool[,] values);
}

class GameData(int columns, int rows)
{
    public int Columns = columns;
    public int Rows = rows;
    public List<List<bool>> Grid = new();

    //lowercase properties to match serialized schema
    public int columns { get => Columns; set => Columns = value; }
    public int rows { get => Rows; set => Rows = value; }
    public List<List<bool>> grid { get => Grid; set => Grid = value; }
}

interface IStorage
{
    void Store(GameData value);
    GameData Get();
}