using System;

class ArrayProcessor
{
    private int[] arr;

    // Nhap mang tu ban phim
    public void Input()
    {
        Console.Write("Nhap so phan tu cua mang: ");
        int n = int.Parse(Console.ReadLine());
        arr = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"arr[{i}] = ");
            arr[i] = int.Parse(Console.ReadLine());
        }
    }

    // Hien thi mang
    public void Display()
    {
        foreach (int x in arr)
        {
            Console.Write(x + " ");
        }
        Console.WriteLine();
    }

    // Bubble Sort
    public void BubbleSort()
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    // Quick Sort
    public void QuickSort(int left, int right)
    {
        int i = left, j = right;
        int pivot = arr[(left + right) / 2];

        while (i <= j)
        {
            while (arr[i] < pivot) i++;
            while (arr[j] > pivot) j--;
            if (i <= j)
            {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                i++;
                j--;
            }
        }

        if (left < j) QuickSort(left, j);
        if (i < right) QuickSort(i, right);
    }

    // Linear Search
    public int LinearSearch(int key)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == key)
                return i;
        }
        return -1;
    }

    // Binary Search
    public int BinarySearch(int key)
    {
        int left = 0, right = arr.Length - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (arr[mid] == key) return mid;
            else if (arr[mid] < key) left = mid + 1;
            else right = mid - 1;
        }
        return -1;
    }

    public int[] GetArray()
    {
        return arr;
    }

    public void SetArray(int[] newArr)
    {
        arr = newArr;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ArrayProcessor ap = new ArrayProcessor();

        ap.Input();
        Console.WriteLine("Mang ban dau:");
        ap.Display();

        // Bubble Sort
        ap.BubbleSort();
        Console.WriteLine("Mang sau Bubble Sort:");
        ap.Display();

        // Quick Sort
        int[] arrCopy = (int[])ap.GetArray().Clone();
        ArrayProcessor apQuick = new ArrayProcessor();
        apQuick.SetArray(arrCopy);

        apQuick.QuickSort(0, arrCopy.Length - 1);
        Console.WriteLine("Mang sau Quick Sort:");
        apQuick.Display();

        // Search
        Console.Write("Nhap so can tim: ");
        int key = int.Parse(Console.ReadLine());

        int posLinear = ap.LinearSearch(key);
        if (posLinear != -1)
            Console.WriteLine($"Linear Search: Tim thay {key} tai vi tri {posLinear}");
        else
            Console.WriteLine("Linear Search: Khong tim thay");

        int posBinary = ap.BinarySearch(key);
        if (posBinary != -1)
            Console.WriteLine($"Binary Search: Tim thay {key} tai vi tri {posBinary}");
        else
            Console.WriteLine("Binary Search: Khong tim thay");
    }
}
