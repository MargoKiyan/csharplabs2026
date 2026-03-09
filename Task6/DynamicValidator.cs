namespace Task6;

class DynamicValidator
{
    public delegate bool Validator(string input);
    
    static Validator GetValidator(int minLength)
    {
        return input => input.Length >= minLength;
    }
    
    public static void Main(string[] args)
    {
        Validator passwordValidator = GetValidator(8);
        Validator loginValidator = GetValidator(3);

        string[] testInputs = { "ok", "admin", "12345678", "pass" };

        foreach (string input in testInputs)
        {
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Login valid: {loginValidator(input)}");
            Console.WriteLine($"Password valid: {passwordValidator(input)}");
            Console.WriteLine("-----------------------------");
        }
    }
}