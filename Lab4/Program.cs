using System;
using System.Diagnostics;
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

    // послідовний пошук кількості входжень
    static int LinearSearch(int[] array, int value)
    {
        int count = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                count++;
            }
        }

        return count;
    }

    // Дослідження часу виконання алгоритмів
    static void TestAlgorithms()
    {
        int[] sizes = { 100, 1000, 10000, 100000 };
        Random random = new Random();

        Console.WriteLine("\nAlgorithm performance research");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("Elements | Merge Sort (ms) | Binary Search (ms) | Linear Search (ms)");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

        foreach (int size in sizes)
        {
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(10, 101);
            }

            // Вимірювання часу сортування
            Stopwatch stopwatch = new Stopwatch();
            int sortRuns = 5;

            stopwatch.Start();

            for (int i = 0; i < sortRuns; i++)
            {
                int[] temp = (int[])array.Clone();
                MergeSort(temp);
            }

            stopwatch.Stop();
            double sortTime = stopwatch.Elapsed.TotalMilliseconds / sortRuns;

            int[] sortedArray = (int[])array.Clone();
            MergeSort(sortedArray);

            int key = sortedArray[size / 2];

            // Вимірювання часу бінарного пошуку
            int searchRuns = 1000;
            stopwatch.Restart();

            for (int i = 0; i < searchRuns; i++)
            {
                int first = BinarySearchFirst(sortedArray, key);
                int last = BinarySearchLast(sortedArray, key);
            }

            stopwatch.Stop();
            double binaryTime = stopwatch.Elapsed.TotalMilliseconds / searchRuns;

            // Вимірювання часу послідовного пошуку
            int linearRuns = Math.Max(10, 1000000 / size);
            stopwatch.Restart();

            for (int i = 0; i < linearRuns; i++)
            {
                LinearSearch(sortedArray, key);
            }

            stopwatch.Stop();
            double linearTime = stopwatch.Elapsed.TotalMilliseconds / linearRuns;

            Console.WriteLine($"{size,8} | {sortTime,10:F4} | {binaryTime,14:F6} | {linearTime,14:F6}");
        }

        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
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
            Console.WriteLine($"Binary search: element {value} occurs {count} time(s).");
        }

        int linearCount = LinearSearch(array, value);

        Console.WriteLine($"Linear search: element {value} occurs {linearCount} time(s).");

        TestAlgorithms();
    }
}