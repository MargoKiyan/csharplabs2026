using System.Text.Encodings.Web;
using System.Text.Json;

JsonSerializerOptions jsonOptions = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

string filePath = GetDataFilePath("old_player.json");

OldPlayer oldPlayer = new()
{
    Name = "Маргарита"
};

Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

string oldJson = JsonSerializer.Serialize(oldPlayer, jsonOptions);
File.WriteAllText(filePath, oldJson);

Console.WriteLine("Старий JSON з моделлю OldPlayer записано у файл data/old_player.json.");
Console.WriteLine("Старий JSON містить тільки поле Name.");

string readJson = File.ReadAllText(filePath);
Player newPlayer = JsonSerializer.Deserialize<Player>(readJson) ?? new Player { Name = "Без імені" };

Console.WriteLine();
Console.WriteLine("Дані після десеріалізації старого JSON у нову модель:");
Console.WriteLine($"Name: {newPlayer.Name}");
Console.WriteLine($"Level до перевірки: {newPlayer.Level}");

if (newPlayer.Level == 0)
{
    newPlayer.Level = 1;
}

Console.WriteLine("Старий JSON не містив поля Level, тому після десеріалізації було встановлено значення за замовчуванням.");
Console.WriteLine($"Level після перевірки: {newPlayer.Level}");

string GetDataFilePath(string fileName)
{
    string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    string dataDirectory = Path.Combine(projectDirectory, "data");
    Directory.CreateDirectory(dataDirectory);
    return Path.Combine(dataDirectory, fileName);
}

class OldPlayer
{
    public string Name { get; set; } = string.Empty;
}

class Player
{
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
}
