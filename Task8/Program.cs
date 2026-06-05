using System.Text.Encodings.Web;
using System.Text.Json;

JsonSerializerOptions jsonOptions = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

string settingsFilePath = GetDataFilePath("settings.json");
string brokenSettingsFilePath = GetDataFilePath("broken_settings.json");

AppSettings normalSettings = new()
{
    Theme = "Dark",
    NotificationsEnabled = true
};

Directory.CreateDirectory(Path.GetDirectoryName(settingsFilePath)!);

string normalJson = JsonSerializer.Serialize(normalSettings, jsonOptions);
File.WriteAllText(settingsFilePath, normalJson);

string brokenJson = """
{
  "Theme": "Dark",
  "NotificationsEnabled": tru
}
""";
File.WriteAllText(brokenSettingsFilePath, brokenJson);

Console.WriteLine("Створено нормальний JSON-файл data/settings.json.");
Console.WriteLine("Створено пошкоджений JSON-файл data/broken_settings.json.");

Console.WriteLine();
Console.WriteLine("Читання нормального JSON-файлу:");
AppSettings loadedNormalSettings = LoadSettings(settingsFilePath);
ShowSettings(loadedNormalSettings);

Console.WriteLine();
Console.WriteLine("Спроба прочитати пошкоджений JSON-файл:");
AppSettings safeSettings = LoadSettings(brokenSettingsFilePath);
ShowSettings(safeSettings);

AppSettings LoadSettings(string filePath)
{
    try
    {
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<AppSettings>(json) ?? CreateDefaultSettings();
    }
    catch (JsonException)
    {
        Console.WriteLine("Помилка читання JSON-файлу. Файл пошкоджений або має неправильний формат.");
        Console.WriteLine("Створено об'єкт із безпечними значеннями за замовчуванням.");
        return CreateDefaultSettings();
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Помилка доступу до файлу: {ex.Message}");
        return CreateDefaultSettings();
    }
}

AppSettings CreateDefaultSettings()
{
    return new AppSettings
    {
        Theme = "Light",
        NotificationsEnabled = false
    };
}

void ShowSettings(AppSettings settings)
{
    Console.WriteLine($"Theme: {settings.Theme}");
    Console.WriteLine($"NotificationsEnabled: {settings.NotificationsEnabled}");
}

string GetDataFilePath(string fileName)
{
    string projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    string dataDirectory = Path.Combine(projectDirectory, "data");
    Directory.CreateDirectory(dataDirectory);
    return Path.Combine(dataDirectory, fileName);
}

class AppSettings
{
    public string Theme { get; set; } = string.Empty;
    public bool NotificationsEnabled { get; set; }
}
