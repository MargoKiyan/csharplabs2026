namespace Task2;

public class FileLogger
{
    private readonly string _logFilePath;

    public FileLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public void OnMessageSent(string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logEntry = $"[{timestamp}] {message}";

        File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
        Console.WriteLine($"[FileLogger] Повідомлення записано у файл: {logEntry}");
    }
}