namespace Task2;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Виконала ст. гр. ПД-21: Киян Маргарита");

        string logFile = "logPD21.txt";

        MessagePublisher publisher = new MessagePublisher();
        FileLogger logger = new FileLogger(logFile);

        publisher.MessageSent += logger.OnMessageSent;

        Console.WriteLine("Починаємо роботу. Введіть 4 повідомлення:");

        for (int i = 1; i <= 4; i++)
        {
            Console.Write($"Рядок {i}: ");
            string input = Console.ReadLine();
            publisher.Send(input);
        }

        Console.WriteLine("\nОбробка завершена. Перевірте файл logPD21.txt");
        Console.ReadLine();
    }
}