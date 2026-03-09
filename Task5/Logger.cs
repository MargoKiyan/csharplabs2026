namespace Task5
{
    class Logger
    {
        public Action<string>? LogHandler;

        public void Log(string message)
        {
            LogHandler?.Invoke(message);
        }

        public static void Main(string[] args)
        {
            Logger logger = new Logger();

            logger.LogHandler = msg => Console.WriteLine(msg);

            logger.Log("Hello, world!");
            logger.Log("Logging message");

            logger.LogHandler = msg => Console.WriteLine(msg.ToUpper());

            logger.Log("Hello again!");
            logger.Log("this message will be upper case");
        }
    }
}