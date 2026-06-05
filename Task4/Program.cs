using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

Order order = new()
{
    Id = 101,
    Status = OrderStatus.Processing
};

JsonSerializerOptions defaultOptions = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

string jsonWithoutConverter = JsonSerializer.Serialize(order, defaultOptions);
Console.WriteLine("Enum без конвертера серіалізується як число:");
Console.WriteLine(jsonWithoutConverter);

JsonSerializerOptions stringEnumOptions = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    Converters = { new JsonStringEnumConverter() }
};

string filePath = GetDataFilePath("order.json");
Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

string jsonWithConverter = JsonSerializer.Serialize(order, stringEnumOptions);
File.WriteAllText(filePath, jsonWithConverter);

Console.WriteLine();
Console.WriteLine("Правильний JSON з enum як текст записано у файл data/order.json:");
Console.WriteLine(jsonWithConverter);

string readJson = File.ReadAllText(filePath);
Order? loadedOrder = JsonSerializer.Deserialize<Order>(readJson, stringEnumOptions);

Console.WriteLine();
Console.WriteLine("Дані після десеріалізації:");
Console.WriteLine($"Id: {loadedOrder?.Id}");
Console.WriteLine($"Status: {loadedOrder?.Status}");

string GetDataFilePath(string fileName)
{
    string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    string dataDirectory = Path.Combine(projectDirectory, "data");
    Directory.CreateDirectory(dataDirectory);
    return Path.Combine(dataDirectory, fileName);
}

enum OrderStatus
{
    Pending,
    Processing,
    Completed
}

class Order
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
}
