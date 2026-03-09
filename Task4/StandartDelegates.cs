namespace Task4;

class StandartDelegates
{
    private static double Add(double x, double y)
    {
        return x + y;
    }

    private static double Subtract(double x, double y)
    {
        return x - y;
    }

    private static double Multiply(double x, double y)
    {
        return x * y;
    }

    private static double Divide(double x, double y)
    {
        return x / y;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("--- Task 1 with standart delegates ---");
        
        Console.WriteLine("Example: a = 12.5; b = 3.8\n");

        Func<double, double, double> operation;

        operation = Add;
        double result = operation(12.5, 3.8);
        Console.WriteLine($"Result of Add: {result}");

        operation = Subtract;
        result = operation(12.5, 3.8);
        Console.WriteLine($"Result of Subtract: {result}");

        operation = Multiply;
        result = operation(12.5, 3.8);
        Console.WriteLine($"Result of Multiply: {result}");

        operation = Divide;
        result = operation(12.5, 3.8);
        Console.WriteLine($"Result of Divide: {result}");

        Console.WriteLine("\n--- Students with the letter A ---");

        List<string> students = new List<string>
        {
            "Anna",
            "Andrii",
            "Olena",
            "Oleh",
            "Maria",
            "Artem"
        };

        Predicate<string> startsWithA = name => name.StartsWith("A");

        List<string> resultStudents = students.FindAll(startsWithA);

        foreach (string student in resultStudents)
        {
            Console.WriteLine(student);
        }
    }
}