namespace Task2;

class Multicasting
{
    public delegate void NotificationHandler(string message);

    private static void SendEmail(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }

    private static void SendSms(string message)
    {
        Console.WriteLine($"SMS sent: {message}");
    }
    
    public static void Main(string[] args)
    {
        NotificationHandler notify = SendEmail;
        notify += SendSms;
        
        notify("Hello World!");
    }
}