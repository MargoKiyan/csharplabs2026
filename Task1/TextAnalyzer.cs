using System.IO;

namespace Task1;

class TextAnalyzer
{
    public static void Main(string[] args)
    {
        string inputPath = "story.txt";
        string reportPath = "report.txt";
        
        int lineCount = 0;
        int wordCount = 0;
        int charCount = 0;

        using (StreamReader sr = new StreamReader(inputPath))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                lineCount++;
                charCount += line.Length;

                string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                wordCount += words.Length;
            }
        }

        using (StreamWriter sw = new StreamWriter(reportPath))
        {
            sw.WriteLine("Text file analysis result: ");
            sw.WriteLine($"LineCount: {lineCount}");
            sw.WriteLine($"WordCount: {wordCount}");
            sw.WriteLine($"CharCount: {charCount}");
        }
        Console.WriteLine("Report created successfully! Check report.txt file.");
    }
}