// Практична робота №6 Kyian M. PD-21

using System.Threading;

class Program
{
    private static int counter = 0;
    private static bool isPaused = false;
    private static bool isRunning = true;
    private static readonly object locker = new object();

    private static readonly ConsoleColor[] colors =
    {
        ConsoleColor.White,
        ConsoleColor.Green,
        ConsoleColor.Yellow,
        ConsoleColor.Cyan,
        ConsoleColor.Magenta
    };

    private static int currentColorIndex = 0;

    static void Main(string[] args)
    {
        lock (locker)
        {
            Console.ForegroundColor = colors[currentColorIndex];
        }

        PrintMessage("Практична робота №6");
        PrintMessage("Виконала: Kyian M. PD-21");
        PrintMessage("");
        PrintMessage("Керування:");
        PrintMessage("P - пауза / продовження");
        PrintMessage("R - скинути лічильник");
        PrintMessage("C - змінити колір тексту");
        PrintMessage("Q - завершити програму");
        PrintMessage("");

        Thread keyboardThread = new Thread(HandleKeyboardInput);
        keyboardThread.IsBackground = true;
        keyboardThread.Start();

        while (GetIsRunning())
        {
            int currentCounter;
            bool shouldPrint;

            lock (locker)
            {
                shouldPrint = !isPaused && isRunning;

                if (shouldPrint)
                {
                    counter++;
                    currentCounter = counter;
                }
                else
                {
                    currentCounter = counter;
                }
            }

            if (shouldPrint)
            {
                PrintMessage($"Counter: {currentCounter}");
            }

            Thread.Sleep(1000);
        }

        Console.ResetColor();
    }

    static void HandleKeyboardInput()
    {
        while (GetIsRunning())
        {
            ConsoleKeyInfo keyInfo;

            try
            {
                keyInfo = Console.ReadKey(true);
            }
            catch (InvalidOperationException)
            {
                StopProgram();
                return;
            }

            switch (keyInfo.Key)
            {
                case ConsoleKey.P:
                    TogglePause();
                    break;
                case ConsoleKey.R:
                    ResetCounter();
                    break;
                case ConsoleKey.C:
                    ChangeColor();
                    break;
                case ConsoleKey.Q:
                    StopProgram();
                    break;
            }
        }
    }

    static void TogglePause()
    {
        bool pausedNow;

        lock (locker)
        {
            isPaused = !isPaused;
            pausedNow = isPaused;
        }

        if (pausedNow)
        {
            PrintMessage("Лічильник поставлено на паузу.");
        }
        else
        {
            PrintMessage("Лічильник продовжено.");
        }
    }

    static void ResetCounter()
    {
        lock (locker)
        {
            counter = 0;
        }

        PrintMessage("Лічильник скинуто до 0.");
    }

    static void ChangeColor()
    {
        lock (locker)
        {
            currentColorIndex++;

            if (currentColorIndex >= colors.Length)
            {
                currentColorIndex = 0;
            }

            Console.ForegroundColor = colors[currentColorIndex];
        }

        PrintMessage("Колір тексту змінено.");
    }

    static void StopProgram()
    {
        lock (locker)
        {
            isRunning = false;
        }

        PrintMessage("Завершення програми...");
    }

    static bool GetIsRunning()
    {
        lock (locker)
        {
            return isRunning;
        }
    }

    static void PrintMessage(string message)
    {
        lock (locker)
        {
            Console.WriteLine(message);
        }
    }
}
