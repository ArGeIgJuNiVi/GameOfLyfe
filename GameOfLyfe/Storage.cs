using System.Text.Json;

namespace GameOfLyfe;
class JsonStorage(string filepath = "") : IStorage
{
    readonly string filepath = filepath;

    public GameData Get()
    {
        string text;
        try
        {
            text = File.ReadAllText(filepath);
        }
        catch
        {
            throw new("Error: Json file doesn't exist");
        }
        GameData mapInf;
        // try
        // {
        mapInf = JsonSerializer.Deserialize<GameData>(text)!;
        // }
        // catch
        // {
        //     throw new("Error: Json file is not valid");
        // }
        return mapInf;
    }

    public void Store(GameData value)
    {
        string newMapInf = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
        try
        {
            File.WriteAllText(filepath, newMapInf);
        }
        catch
        {
            throw new("Error: The file does not exist");
        }
    }
}