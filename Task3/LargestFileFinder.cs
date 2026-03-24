using System.IO;

namespace Task3;

public class LargestFileFinder
{
    public void FindLargestFile(string folderPath)
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

            FileInfo largestFile = null;

            FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

            foreach (FileInfo file in files)
            {
                if (largestFile == null || file.Length > largestFile.Length)
                {
                    largestFile = file;
                }
            }

            if (largestFile != null)
            {
                Console.WriteLine("Largest file found:\n");
                Console.WriteLine($"Name: {largestFile.Name}");
                Console.WriteLine($"Size: {largestFile.Length} bytes");
                Console.WriteLine($"Path: {largestFile.FullName}");
            }
            else
            {
                Console.WriteLine("No files found in the directory.");
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
        var finder = new LargestFileFinder();
        Console.Write("Enter the path to the folder: ");
        string folderPath = Console.ReadLine();
        finder.FindLargestFile(folderPath);
    }
}