namespace Task3;

class ListFilter
{
    public delegate bool FilterPredicate(int number);

    public static void FilterArray(int[] numbers, FilterPredicate predicate)
    {
        foreach (int number in numbers)
        {
            if (predicate(number))
            {
                Console.Write(number + " ");
            }
        }
        Console.WriteLine();
    }

    private static bool IsEvenNumber(int number)
    {
        return number % 2 == 0;
    }

    private static bool IsGreaterThanFive(int number)
    {
        return number > 5;
    }
    
    public static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        Console.Write("IsEvenNumber: ");
        FilterArray(numbers, IsEvenNumber);
        
        Console.Write("IsGreaterThanFive: ");
        FilterArray(numbers, IsGreaterThanFive);
        
        Console.Write("IsOddNumber: ");
        FilterArray(numbers, n => n % 2 == 0);
    }
}