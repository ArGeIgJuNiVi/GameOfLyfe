namespace GameOfLyfe;

class Program
{
    public static void Main()
    {
        int choice = -1;
        do
        {
            Console.WriteLine(
            """
            Choose an option:
            1.Create a new board
            2.Load board from json file
            """
            );
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
                if (choice != 1 && choice != 2)
                    Console.WriteLine("Invalid choice");
            }
            catch
            {
                Console.WriteLine("Invalid number");
            }
        } while (choice != 1 && choice != 2);

        switch (choice)
        {
            case 1:
                NewBoard();
                break;
            case 2:
                LoadFromJson();
                break;
        }

    }

    public static void NewBoard()
    {
        int rows = 0, columns = 0;

        do
        {
            Console.WriteLine("Enter the number of rows for the grid (4-100): ");
            try
            {
                rows = Convert.ToInt32(Console.ReadLine());
                if (rows < 4)
                    Console.WriteLine("The number of rows is too small");
                if (rows > 100)
                    Console.WriteLine("The number of rows is too big");
            }
            catch
            {
                Console.WriteLine("Invalid number");
            }
        } while (rows < 4 && rows > 100);

        do
        {
            Console.WriteLine("Enter the number of columns for the grid (4-100): ");
            try
            {
                columns = Convert.ToInt32(Console.ReadLine());
                if (columns < 4)
                    Console.WriteLine("The number of columns is too small");
                if (columns > 100)
                    Console.WriteLine("The number of columns is too big");
            }
            catch
            {
                Console.WriteLine("Invalid number");
            }
        } while (columns < 4 && columns > 100);

        bool redo;
        bool[,] newGrid = new bool[columns, rows];
        do
        {
            redo = false;
            Console.WriteLine("Enter the board values (0 for a living cell, . for a dead one)");
            for (int i = 0; i < rows; i++)
            {
                string row = "";
                try
                {
                    row = Console.ReadLine()!;
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                    redo = true;
                    break;
                }
                if (row.Length != columns)
                {
                    Console.WriteLine("Incorrect length");
                    redo = true;
                    break;
                }
                for (int j = 0; j < columns; j++)
                {
                    if (row[j] == '0') newGrid[j, i] = true;
                    else if (row[j] != '.')
                    {
                        Console.WriteLine("Invalid input");
                        redo = true;
                        break;
                    }
                }
                if (redo == true) break;
            }
        } while (redo);
        Grid grid = new(rows: rows, columns: columns);
        grid.SetValues(newGrid);
        Rendering(grid);
    }

    public static void LoadFromJson()
    {

        string? path;
        do
        {
            Console.WriteLine("Enter the json file's path:");
            path = Console.ReadLine();
            if (!File.Exists(path))
                Console.WriteLine("File not found");
        } while (!File.Exists(path));
        GameData gameData = new JsonStorage(path).Get();
        bool[,] newGrid = new bool[gameData.Columns, gameData.Rows];
        for (int i = 0; i < gameData.Columns; i++)
            for (int j = 0; j < gameData.Rows; j++)
                newGrid[i, j] = gameData.Grid[j][i];
        Grid grid = new Grid(gameData.Columns, gameData.Rows);
        grid.SetValues(newGrid);
        Rendering(grid);
    }

    public static void Rendering(Grid grid)
    {
        AutomationSimulator simulator = new(grid);
        bool exit = false;
        Console.Clear();
        Console.WriteLine("Simulation started; type continue or c for the next iteration and quit or q to exit");
        do
        {
            for (int i = 0; i < simulator.Grid.ReturnValues().GetLength(1); i++)
            {
                for (int j = 0; j < simulator.Grid.ReturnValues().GetLength(0); j++)
                    Console.Write(simulator.Grid.ReturnValues()[j, i] ? '0' : '.');
                Console.WriteLine();
            }

            switch (Console.ReadLine())
            {
                case "continue":
                case "c":
                    simulator.Update();
                    break;
                case "quit":
                case "q":
                    exit = true;
                    break;
            }
            Console.Clear();
        } while (!exit);

    }
}