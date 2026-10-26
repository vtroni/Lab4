using System;

class Program
{
    //метод для злиття двох відсортованих частин масиву
    static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
    {
        int left = lowIndex;
        int right = middleIndex + 1;
        int[] tempArray = new int[highIndex - lowIndex + 1];
        int index = 0;

        while ((left <= middleIndex) && (right <= highIndex))
        {
            if (array[left] < array[right])
            {
                tempArray[index] = array[left];
                left++;
            }
            else
            {
                tempArray[index] = array[right];
                right++;
            }

            index++;
        }

        for (int i = left; i <= middleIndex; i++)
        {
            tempArray[index] = array[i];
            index++;
        }

        for (int i = right; i <= highIndex; i++)
        {
            tempArray[index] = array[i];
            index++;
        }

        for (int i = 0; i < tempArray.Length; i++)
        {
            array[lowIndex + i] = tempArray[i];
        }
    }

    //рекурсивне сортування злиттям
    static int[] MergeSort(int[] array, int lowIndex, int highIndex)
    {
        if (lowIndex < highIndex)
        {
            int middleIndex = (lowIndex + highIndex) / 2;
            MergeSort(array, lowIndex, middleIndex);
            MergeSort(array, middleIndex + 1, highIndex);
            Merge(array, lowIndex, middleIndex, highIndex);
        }

        return array;
    }

    //запуск сортування для всього масиву
    static int[] MergeSort(int[] array)
    {
        return MergeSort(array, 0, array.Length - 1);
    }

    // пошук першого входження ключового значення
    static int BinarySearchFirst(int[] array, int value)
    {
        int left = 0;
        int right = array.Length - 1;
        int result = -1;

        while(left <= right)
        {
            int middle = (left + right) / 2;

            if (array[middle] == value)
            {
                result = middle;
                right = middle - 1;
            }
            else if (array[middle] < value)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        return result;
    }

    // пошук останнього входження ключового значення
    static int BinarySearchLast(int[] array, int value)
    {
        int left = 0;
        int right = array.Length - 1;
        int result = -1;

        while(left <= right)
        {
            int middle = (left + right) / 2;

            if (array[middle] == value)
            {
                result = middle;
                left = middle + 1;
            }
            else if (array[middle] < value)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        return result;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Merge sort");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

        Random random = new Random();
        int k = 11;
        int n = (int)(20 + 0.6 * k);
        int[] array = new int[n];

        for (int i = 0; i < n; i++)
        {
            array[i] = random.Next(10, 101);
        }

        Console.WriteLine("Unsorted array: {0}", string.Join(", ", array));
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        MergeSort(array);
        Console.WriteLine("Sorted array: {0}", string.Join(", ", array));

        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.Write("Enter the search key: ");

        int value = int.Parse(Console.ReadLine());

        int first = BinarySearchFirst(array, value);
        int last = BinarySearchLast(array, value);

        if (first == -1)
        {
            Console.WriteLine("Element not found.");
        }
        else
        {
            int count = last - first + 1;
            Console.WriteLine($"Element {value} occurs {count} time(s).");
        }
    }
}