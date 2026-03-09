namespace Task1;

class Calculator
{
    public delegate double MathOperation(double a, double b);

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
        Console.WriteLine("Example: a = 12.5; b = 3.8\n");
        
        MathOperation operation = Add;
        
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
    }
}