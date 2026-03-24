using System.IO;

namespace Task2;

public class FolderInspector
{
    public void InspectFolder(string folderPath)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

            if (!directoryInfo.Exists)
            {
                Console.WriteLine($"{folderPath} Directory doesn't exist!");
                return;
            }

            Console.WriteLine($"{folderPath} Directory exists!\n");
            Console.WriteLine(directoryInfo.FullName);
            Console.WriteLine();

            DirectoryInfo[] subdirectories = directoryInfo.GetDirectories();
            Console.WriteLine("Subdirectories: ");

            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                Console.WriteLine($"Name of subdirectory: {subdirectory.Name};\nCreation time: {subdirectory.CreationTime}");
                Console.WriteLine();
            }

            FileInfo[] files = directoryInfo.GetFiles();
            Console.WriteLine("Files: ");

            foreach (FileInfo file in files)
            {
                Console.WriteLine($"Name of file: {file.Name};\nSize: {file.Length};\nCreation time: {file.CreationTime}");
                Console.WriteLine();
            }
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Insufficient rights to access the folder: {e.Message}");
        }
        catch (IOException e)
        {
            Console.WriteLine($"Input/output error: {e.Message}");
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
        var inspector = new FolderInspector();
        Console.Write("Enter the path to the folder: ");
        string folderPath = Console.ReadLine();
        inspector.InspectFolder(folderPath);
    }
}