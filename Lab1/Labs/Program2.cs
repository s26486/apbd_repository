namespace Labs;

public class Program2
{
    static void Main2()
    {
        int[] numbers = { 1, 6, 3, 8, 2 };
        int max = FindMax(numbers);
        Console.WriteLine($"Max value: {max}");
    }

    static int FindMax(int[] array)
    {
        if (array == null || array.Length == 0)
        {
            throw new ArgumentException("Array is null or empty");
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