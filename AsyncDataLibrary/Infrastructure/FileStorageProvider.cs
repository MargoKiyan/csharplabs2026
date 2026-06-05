namespace AsyncDataLibrary.Infrastructure;

public class FileStorageProvider
{
    private readonly string _dataDirectory;

    public FileStorageProvider(string? dataDirectory = null)
    {
        _dataDirectory = dataDirectory ?? Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "data"));
        Directory.CreateDirectory(_dataDirectory);
    }

    public string GetFilePath(string fileName)
    {
        Directory.CreateDirectory(_dataDirectory);
        return Path.Combine(_dataDirectory, fileName);
    }

    public string Read(string fileName)
    {
        string filePath = GetFilePath(fileName);

        if (!File.Exists(filePath))
        {
            return string.Empty;
        }

        return File.ReadAllText(filePath);
    }

    public void Write(string fileName, string content)
    {
        string filePath = GetFilePath(fileName);
        File.WriteAllText(filePath, content);
    }

    public async Task<string> ReadAsync(string fileName)
    {
        string filePath = GetFilePath(fileName);

        if (!File.Exists(filePath))
        {
            return string.Empty;
        }

        return await File.ReadAllTextAsync(filePath);
    }

    public async Task WriteAsync(string fileName, string content)
    {
        string filePath = GetFilePath(fileName);
        await File.WriteAllTextAsync(filePath, content);
    }
}
