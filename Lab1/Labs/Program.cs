
 class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        double average = CalculateAverage(numbers);
        Console.WriteLine($"Average: {average}");
        Console.WriteLine("Initial project");
        Console.WriteLine("Hello, World!");
        Console.WriteLine("Changing code v2");
        Console.WriteLine("Final changes v3");

        Console.Write("Modification v1");
        Console.Write("Modification v2");
        Console.Write("Modification v3");

        Console.WriteLine();
        double max = FindMax(numbers);
        Console.WriteLine($"max number: {max}");

    }
    
    

    static double CalculateAverage(int[] array)
    {
        if (array == null || array.Length == 0)
        {
            throw new ArgumentException("Array is null or empty");
        }

        int sum = 0;
        foreach (int num in array)
        {
            sum += num;
        }

        return (double)sum / array.Length;
    }
    
    static int FindMax(int[] array)
    {
        if (array == null || array.Length == 0)
        {
            Console.Write("No values within the array");
        }

        int max = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > max)
            {
                max = array[i];
            }
        }

        return max;
    }
}
