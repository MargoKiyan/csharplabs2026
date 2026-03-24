using System.IO;

namespace Task4.Recursive;

public class CacheCleanerRecursive
{
    private int deletedFilesCount = 0;
    private long totalSize = 0;

    public void CleanCache(string cachePath)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(cachePath);

            if (!directoryInfo.Exists)
            {
                Console.WriteLine($"{cachePath} Directory doesn't exist!");
                return;
            }

            DeleteFilesRecursive(directoryInfo);

            Console.WriteLine("Cache cleared!\n");
            Console.WriteLine($"Deleted files: {deletedFilesCount}");
            Console.WriteLine($"Total freed space: {totalSize} bytes");
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Insufficient rights: {e.Message}");
        }
        catch (IOException e)
        {
            Console.WriteLine($"I/O error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Internal error: {e.Message}");
        }
    }

    private void DeleteFilesRecursive(DirectoryInfo directory)
    {
        FileInfo[] files = directory.GetFiles();
        foreach (FileInfo file in files)
        {
            totalSize += file.Length;
            file.Delete();
            deletedFilesCount++;
        }

        DirectoryInfo[] subdirectories = directory.GetDirectories();
        foreach (DirectoryInfo subdirectory in subdirectories)
        {
            DeleteFilesRecursive(subdirectory);
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        var cleaner = new CacheCleanerRecursive();
        Console.Write("Enter cache folder path: ");
        string path = Console.ReadLine();
        cleaner.CleanCache(path);
    }
}