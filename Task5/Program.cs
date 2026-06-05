using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

JsonSerializerOptions jsonOptions = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

string filePath = GetDataFilePath("animals.json");

List<Animal> animals = new()
{
    new Dog { Name = "Барон", BarkVolume = 8 },
    new Cat { Name = "Мурка", Lives = 9 },
    new Dog { Name = "Рекс", BarkVolume = 6 },
    new Cat { Name = "Луна", Lives = 7 }
};

Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
string json = JsonSerializer.Serialize(animals, jsonOptions);
File.WriteAllText(filePath, json);

Console.WriteLine("Список тварин серіалізовано у файл data/animals.json.");
Console.WriteLine("У JSON використано discriminator \"$type\" для збереження типів Dog і Cat.");

string readJson = File.ReadAllText(filePath);
List<Animal> loadedAnimals = JsonSerializer.Deserialize<List<Animal>>(readJson, jsonOptions) ?? new List<Animal>();

Console.WriteLine();
Console.WriteLine("Дані після десеріалізації:");
foreach (Animal animal in loadedAnimals)
{
    Console.WriteLine($"Ім'я: {animal.Name}; фактичний тип: {animal.GetType().Name}");

    if (animal is Dog dog)
    {
        Console.WriteLine($"  BarkVolume: {dog.BarkVolume}");
    }
    else if (animal is Cat cat)
    {
        Console.WriteLine($"  Lives: {cat.Lives}");
    }
}

string GetDataFilePath(string fileName)
{
    string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    string dataDirectory = Path.Combine(projectDirectory, "data");
    Directory.CreateDirectory(dataDirectory);
    return Path.Combine(dataDirectory, fileName);
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Dog), "dog")]
[JsonDerivedType(typeof(Cat), "cat")]
abstract class Animal
{
    public string Name { get; set; } = string.Empty;
}

class Dog : Animal
{
    public int BarkVolume { get; set; }
}

class Cat : Animal
{
    public int Lives { get; set; }
}
