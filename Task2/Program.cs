using System.Text.Encodings.Web;
using System.Text.Json;

JsonSerializerOptions jsonOptions = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

string filePath = GetDataFilePath("students.json");

List<Student> students = new()
{
    new Student { Name = "Киян Маргарита", Age = 20, AverageScore = 91.5 },
    new Student { Name = "Петренко Олена", Age = 19, AverageScore = 88.2 },
    new Student { Name = "Шевченко Андрій", Age = 18, AverageScore = 79.4 },
    new Student { Name = "Іваненко Софія", Age = 20, AverageScore = 94.1 },
    new Student { Name = "Мельник Дмитро", Age = 19, AverageScore = 83.7 }
};

Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

string json = JsonSerializer.Serialize(students, jsonOptions);
File.WriteAllText(filePath, json);

Console.WriteLine("Список студентів серіалізовано у файл data/students.json.");

string readJson = File.ReadAllText(filePath);
List<Student> loadedStudents = JsonSerializer.Deserialize<List<Student>>(readJson) ?? new List<Student>();

Console.WriteLine();
Console.WriteLine("Дані студентів після десеріалізації:");
foreach (Student student in loadedStudents)
{
    Console.WriteLine($"Ім'я: {student.Name}; вік: {student.Age}; середній бал: {student.AverageScore}");
}

string GetDataFilePath(string fileName)
{
    string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    string dataDirectory = Path.Combine(projectDirectory, "data");
    Directory.CreateDirectory(dataDirectory);
    return Path.Combine(dataDirectory, fileName);
}

class Student
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public double AverageScore { get; set; }
}
