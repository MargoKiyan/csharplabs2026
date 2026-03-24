using System.IO;

namespace Task4;

public class CacheCleaner
{
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

            int deletedFilesCount = 0;
            long totalSize = 0;

            FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

            foreach (FileInfo file in files)
            {
                totalSize += file.Length;
                file.Delete();
                deletedFilesCount++;
            }

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
}

class Program
{
    public static void Main(string[] args)
    {
        var cleaner = new CacheCleaner();
        Console.Write("Enter cache folder path: ");
        string path = Console.ReadLine();
        cleaner.CleanCache(path);
    }
}