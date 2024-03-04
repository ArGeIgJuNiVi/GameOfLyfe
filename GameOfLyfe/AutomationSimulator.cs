namespace GameOfLyfe;
class AutomationSimulator(Grid grid)
{
    Grid grid = grid;
    public Grid Grid { get => grid; set => grid = value; }

    public void Update()
    {
        bool[,] grid = Grid.ReturnValues();
        bool[,] modifiedGrid = new bool[grid.GetLength(0), grid.GetLength(1)];
        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                int neighbors = 0;
                //funny nest
                for (int k = -1; k <= 1; k++)
                    for (int l = -1; l <= 1; l++)
                        if (i + k >= 0 && i + k < grid.GetLength(0))
                            if (j + l >= 0 && j + l < grid.GetLength(1))
                                if (!(k == 0 && l == 0))
                                    if (grid[i + k, j + l] == true)
                                        neighbors++;
                if ((neighbors == 2 && grid[i, j] == true) || neighbors == 3) modifiedGrid[i, j] = true;
            }
        Grid.SetValues(modifiedGrid);
    }
}