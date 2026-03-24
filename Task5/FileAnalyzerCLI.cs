using System.IO;

namespace Task5;

class FileAnalyzer
{
    public void Analyze(string path)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            
            if (!dir.Exists)
            {
                Console.WriteLine("Directory does not exist!");
                return;
            }

            int folderCount = 0;
            int fileCount = 0;
            long totalSize = 0;
            FileInfo largestFile = null;

            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);
            DirectoryInfo[] folders = dir.GetDirectories("*", SearchOption.AllDirectories);

            folderCount = folders.Length;
            fileCount = files.Length;

            foreach (FileInfo file in files)
            {
                totalSize += file.Length;

                if (largestFile == null || file.Length > largestFile.Length)
                {
                    largestFile = file;
                }
            }

            Console.WriteLine("Analysis result:\n");
            Console.WriteLine($"Folders: {folderCount}");
            Console.WriteLine($"Files: {fileCount}");
            Console.WriteLine($"Total size: {totalSize / (1024 * 1024)} MB");

            if (largestFile != null)
                Console.WriteLine($"Largest file: {largestFile.Name}");
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Access denied: {e.Message}");
        }
        catch (IOException e)
        {
            Console.WriteLine($"I/O error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: analyzer.exe <path>");
            return;
        }
        string path = args[0];
        FileAnalyzer analyzer = new FileAnalyzer();
        analyzer.Analyze(path);
    }
}