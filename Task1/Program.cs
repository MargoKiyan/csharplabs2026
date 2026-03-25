using System;
using System.IO;

namespace Task1;

public delegate string TextOperation(string text);

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Виконала ст. гр. ПД-21: Киян Маргарита");

        string inputFilePath = "textPD21.txt";
        string outputFilePath = "resultPD21.txt";

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Файл {inputFilePath} не знайдено.");
        }

        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);

        ProcessFile(inputFilePath, outputFilePath, ToUpperCase);
        ProcessFile(inputFilePath, outputFilePath, CountCharacters);
        ProcessFile(inputFilePath, outputFilePath, CountWords);

        Console.WriteLine("Обробка файлу завершена.");
    }

    static void ProcessFile(string inputPath, string outputPath, TextOperation operation)
    {
        try
        {
            string text;
            using (StreamReader reader = new StreamReader(inputPath))
            {
                text = reader.ReadToEnd();
            }

            string result = operation(text);

            using (StreamWriter writer = new StreamWriter(outputPath, true))
            {
                writer.WriteLine(result);
                writer.WriteLine();
            }

            Console.WriteLine($"Результат операції записано у файл: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static string ToUpperCase(string text)
    {
        return "РЕГІСТР UPPERCASE:\n" + text.ToUpper();
    }
    static string CountCharacters(string text)
    {
        return $"Загальна кількість символів: {text.Length}";
    }
    static string CountWords(string text)
    {
        string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return $"Загальна кількість слів: {words.Length}";
    }
}