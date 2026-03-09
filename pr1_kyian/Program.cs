using Task1;

namespace pr1_kyian;

class Program
{
    public static void Main(string[] args)
    {
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Task1.Calculator.Main(args);
                break;
        }
    }
}