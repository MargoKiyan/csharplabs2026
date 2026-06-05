using System.Text.Encodings.Web;
using System.Text.Json;

JsonSerializerOptions jsonOptions = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

string playerFilePath = GetDataFilePath("player.json");
string playerWithoutInventoryFilePath = GetDataFilePath("player_without_inventory.json");

Player player = new()
{
    Name = "Маргарита",
    Inventory = new Inventory
    {
        Items = new List<string> { "Меч", "Аптечка", "Карта", "Ключ" }
    }
};

Directory.CreateDirectory(Path.GetDirectoryName(playerFilePath)!);

string playerJson = JsonSerializer.Serialize(player, jsonOptions);
File.WriteAllText(playerFilePath, playerJson);

object playerWithoutInventory = new
{
    player.Name
};

string playerWithoutInventoryJson = JsonSerializer.Serialize(playerWithoutInventory, jsonOptions);
File.WriteAllText(playerWithoutInventoryFilePath, playerWithoutInventoryJson);

Console.WriteLine("Повний об'єкт Player записано у файл data/player.json.");
Console.WriteLine("Окремо створено файл data/player_without_inventory.json без поля Inventory.");

string readJson = File.ReadAllText(playerWithoutInventoryFilePath);
Player loadedPlayer = JsonSerializer.Deserialize<Player>(readJson) ?? new Player { Name = "Без імені" };

bool inventoryWasMissing = loadedPlayer.Inventory == null;

loadedPlayer.Inventory ??= new Inventory();
loadedPlayer.Inventory.Items ??= new List<string>();

Console.WriteLine();
if (inventoryWasMissing)
{
    Console.WriteLine("Inventory був відсутній і був створений заново.");
}

Console.WriteLine($"Ім'я гравця: {loadedPlayer.Name}");
Console.WriteLine($"Кількість предметів в Inventory: {loadedPlayer.Inventory.Items.Count}");

string GetDataFilePath(string fileName)
{
    string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    string dataDirectory = Path.Combine(projectDirectory, "data");
    Directory.CreateDirectory(dataDirectory);
    return Path.Combine(dataDirectory, fileName);
}

class Player
{
    public string Name { get; set; } = string.Empty;
    public Inventory? Inventory { get; set; }
}

class Inventory
{
    public List<string>? Items { get; set; }
}
