using System.Text.Encodings.Web;
using System.Text.Json;

JsonSerializerOptions jsonOptions = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

string tasksFilePath = GetDataFilePath("tasks.json");
List<TaskItem> tasks = LoadTasks(tasksFilePath);

Console.WriteLine("Task Tracker");
Console.WriteLine("Стан програми відновлено з файлу data/tasks.json, якщо він існував.");

bool isRunning = true;
while (isRunning)
{
    Console.WriteLine();
    Console.WriteLine("Меню:");
    Console.WriteLine("1. Додати задачу");
    Console.WriteLine("2. Змінити статус задачі");
    Console.WriteLine("3. Переглянути список задач");
    Console.WriteLine("4. Вийти з програми");
    Console.Write("Ваш вибір: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            AddTask(tasks);
            break;
        case "2":
            ChangeTaskStatus(tasks);
            break;
        case "3":
            ShowTasks(tasks);
            break;
        case "4":
            SaveTasks(tasksFilePath, tasks);
            Console.WriteLine("Список задач збережено у файл data/tasks.json.");
            isRunning = false;
            break;
        default:
            Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            break;
    }
}

List<TaskItem> LoadTasks(string filePath)
{
    try
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        if (!File.Exists(filePath))
        {
            return new List<TaskItem>();
        }

        string json = File.ReadAllText(filePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<TaskItem>();
        }

        return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
    }
    catch (JsonException)
    {
        Console.WriteLine("Файл tasks.json пошкоджений. Створено новий порожній список задач.");
        return new List<TaskItem>();
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Помилка читання файлу: {ex.Message}");
        return new List<TaskItem>();
    }
}

void SaveTasks(string filePath, List<TaskItem> items)
{
    try
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        string json = JsonSerializer.Serialize(items, jsonOptions);
        File.WriteAllText(filePath, json);
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Помилка збереження файлу: {ex.Message}");
    }
}

void AddTask(List<TaskItem> items)
{
    Console.Write("Введіть назву задачі: ");
    string? title = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("Назва задачі не може бути порожньою.");
        return;
    }

    items.Add(new TaskItem
    {
        Title = title.Trim(),
        IsCompleted = false
    });

    Console.WriteLine("Задачу додано.");
}

void ChangeTaskStatus(List<TaskItem> items)
{
    if (items.Count == 0)
    {
        Console.WriteLine("Список задач порожній.");
        return;
    }

    ShowTasks(items);
    Console.Write("Введіть номер задачі для зміни статусу: ");
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int taskNumber) || taskNumber < 1 || taskNumber > items.Count)
    {
        Console.WriteLine("Невірний номер задачі.");
        return;
    }

    TaskItem selectedTask = items[taskNumber - 1];
    selectedTask.IsCompleted = !selectedTask.IsCompleted;

    Console.WriteLine($"Статус задачі \"{selectedTask.Title}\" змінено на: {GetStatusText(selectedTask.IsCompleted)}.");
}

void ShowTasks(List<TaskItem> items)
{
    if (items.Count == 0)
    {
        Console.WriteLine("Список задач порожній.");
        return;
    }

    Console.WriteLine("Список задач:");
    for (int i = 0; i < items.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {items[i].Title} - {GetStatusText(items[i].IsCompleted)}");
    }
}

string GetStatusText(bool isCompleted)
{
    return isCompleted ? "Виконано" : "Не виконано";
}

string GetDataFilePath(string fileName)
{
    string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    string dataDirectory = Path.Combine(projectDirectory, "data");
    Directory.CreateDirectory(dataDirectory);
    return Path.Combine(dataDirectory, fileName);
}

class TaskItem
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
