using System;
using System.Collections.Generic;

namespace SortLib
{
    
    public abstract class Sort<T> where T : IComparable<T>
    {
        protected T[] array = Array.Empty<T>();
        protected int comparisonCount;
        protected int swapCount;
        protected TimeSpan elapsedTime;

        public void RunSort(T[] array)
        {
            if (array == null || array.Length <= 1)
                return;

            this.array = (T[])array.Clone();
            comparisonCount = 0;
            swapCount = 0;

            var startTime = DateTime.Now;
            SortArray();
            elapsedTime = DateTime.Now - startTime;

            //array.CopyTo(this.array, 0);
        }

        public string GetLog(bool statistic)
        {
            string output = "";
            if (statistic)
            {
                string sortingName = GetType().Name;
                if (sortingName.EndsWith("`1")) sortingName = sortingName[..^2];
                output = $"{sortingName}:\n" +
                                $"  Length of array: {array.Length}\n" +
                                $"  Execution time: {elapsedTime.TotalMilliseconds:F2} мс;\n" +
                                $"  Compairings: {comparisonCount}\n" +
                                $"  Swaps: {swapCount}\n";
            }
            else
            {
                output = $"{GetType().Name}" +
                                $"  Length of array: {array.Length}\n";
            }
            return output;
        }

        public string GetStatistic()
        {
            string output = "";

            string sortingName = GetType().Name;
            if (sortingName.EndsWith("`1")) sortingName = sortingName[..^2];
            output = $"{sortingName};" +
                            $"{array.Length};" +
                            $"{elapsedTime.TotalMilliseconds:F2};" +
                            $"{comparisonCount};" +
                            $"{swapCount}";
            return output;
        }

        public T[] GetArray()
        {
            T[] result = new T[array.Length];
            array.CopyTo(result, 0);
            return result;
        }

        protected abstract void SortArray();

        protected int Compare(T a, T b)
        {
            comparisonCount++;
            return a.CompareTo(b);
        }

        protected void Swap(int index1, int index2)
        {
            swapCount++;
            (array[index1], array[index2]) = (array[index2], array[index1]);
        }
    }

    // 1. Bubble Sort (Пузырьковая сортировка)
    public class BubbleSort<T> : Sort<T> where T : IComparable<T>
    {
        protected override void SortArray()
        {
            bool swapped;
            for (int i = 0; i < array.Length - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (Compare(array[j], array[j + 1]) > 0)
                    {
                        Swap(j, j + 1);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }
    }

    // 2. Selection Sort (Сортировка выбором)
    public class SelectionSort<T> : Sort<T> where T : IComparable<T>
    {
        protected override void SortArray()
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (Compare(array[j], array[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    Swap(i, minIndex);
                }
            }
        }
    }

    // 3. Insertion Sort (Сортировка вставками)
    public class InsertionSort<T> : Sort<T> where T : IComparable<T>
    {
        protected override void SortArray()
        {
            for (int i = 1; i < array.Length; i++)
            {
                T key = array[i];
                int j = i - 1;

                while (j >= 0 && Compare(array[j], key) > 0)
                {
                    array[j + 1] = array[j];
                    swapCount++;
                    j--;
                }
                array[j + 1] = key;
            }
        }
    }

    // 4. Merge Sort (Сортировка слиянием)
    public class MergeSort<T> : Sort<T> where T : IComparable<T>
    {
        protected override void SortArray()
        {
            MergeSortRecursive(0, array.Length - 1);
        }

        private void MergeSortRecursive(int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                MergeSortRecursive(left, middle);
                MergeSortRecursive(middle + 1, right);

                Merge(left, middle, right);
            }
        }

        private void Merge(int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;

            T[] leftArray = new T[n1];
            T[] rightArray = new T[n2];

            Array.Copy(array, left, leftArray, 0, n1);
            Array.Copy(array, middle + 1, rightArray, 0, n2);

            int i = 0, j = 0, k = left;

            while (i < n1 && j < n2)
            {
                if (Compare(leftArray[i], rightArray[j]) <= 0)
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                array[k] = leftArray[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                array[k] = rightArray[j];
                j++;
                k++;
            }
        }
    }

    // 5. Quick Sort (Быстрая сортировка)
    public class QuickSort<T> : Sort<T> where T : IComparable<T>
    {
        protected override void SortArray()
        {
            QuickSortRecursive(0, array.Length - 1);
        }

        private void QuickSortRecursive(int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(low, high);

                QuickSortRecursive(low, pi - 1);
                QuickSortRecursive(pi + 1, high);
            }
        }

        private int Partition(int low, int high)
        {
            T pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (Compare(array[j], pivot) <= 0)
                {
                    i++;
                    Swap(i, j);
                }
            }
            Swap(i + 1, high);
            return i + 1;
        }
    }

    // 6. Heap Sort (Пирамидальная сортировка)
    public class HeapSort<T> : Sort<T> where T : IComparable<T>
    {
        protected override void SortArray()
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(n, i);

            for (int i = n - 1; i > 0; i--)
            {
                Swap(0, i);
                Heapify(i, 0);
            }
        }

        private void Heapify(int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && Compare(array[left], array[largest]) > 0)
                largest = left;

            if (right < n && Compare(array[right], array[largest]) > 0)
                largest = right;

            if (largest != i)
            {
                Swap(i, largest);
                Heapify(n, largest);
            }
        }
    }

    // 7. Shell Sort (Сортировка Шелла)
    public class ShellSort<T> : Sort<T> where T : IComparable<T>
    {
        protected override void SortArray()
        {
            int n = array.Length;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = array[i];
                    int j;

                    for (j = i; j >= gap && Compare(array[j - gap], temp) > 0; j -= gap)
                    {
                        array[j] = array[j - gap];
                        swapCount++;
                    }

                    array[j] = temp;
                }
            }
        }
    }

    // 8. Counting Sort (Сортировка подсчетом) - только для целых чисел
    public class CountingSort : Sort<int>
    {
        protected override void SortArray()
        {
            if (array.Length == 0) return;

            int max = array[0];
            int min = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (Compare(array[i], max) > 0) max = array[i];
                if (Compare(array[i], min) < 0) min = array[i];
            }

            int range = max - min + 1;
            int[] count = new int[range];
            int[] output = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
                count[array[i] - min]++;

            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            for (int i = array.Length - 1; i >= 0; i--)
            {
                output[count[array[i] - min] - 1] = array[i];
                count[array[i] - min]--;
            }

            Array.Copy(output, array, array.Length);
        }
    }


    // 9. TimSort (Гибридная сортировка - комбинация Insertion Sort и Merge Sort)
    public class TimSort<T> : Sort<T> where T : IComparable<T>
    {
        private const int RUN = 32;

        protected override void SortArray()
        {
            int n = array.Length;

            for (int i = 0; i < n; i += RUN)
                InsertionSortForTim(i, Math.Min(i + RUN - 1, n - 1));

            for (int size = RUN; size < n; size = 2 * size)
            {
                for (int left = 0; left < n; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min(left + 2 * size - 1, n - 1);

                    if (mid < right)
                        MergeForTim(left, mid, right);
                }
            }
        }

        private void InsertionSortForTim(int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                T temp = array[i];
                int j = i - 1;

                while (j >= left && Compare(array[j], temp) > 0)
                {
                    array[j + 1] = array[j];
                    swapCount++;
                    j--;
                }
                array[j + 1] = temp;
            }
        }

        private void MergeForTim(int left, int mid, int right)
        {
            int len1 = mid - left + 1;
            int len2 = right - mid;

            T[] leftArray = new T[len1];
            T[] rightArray = new T[len2];

            Array.Copy(array, left, leftArray, 0, len1);
            Array.Copy(array, mid + 1, rightArray, 0, len2);

            int i = 0, j = 0, k = left;

            while (i < len1 && j < len2)
            {
                if (Compare(leftArray[i], rightArray[j]) <= 0)
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            while (i < len1)
            {
                array[k] = leftArray[i];
                i++;
                k++;
            }

            while (j < len2)
            {
                array[k] = rightArray[j];
                j++;
                k++;
            }
        }
    }

    // Пример использования
    class Program
    {
        static void Main()
        {
            int[] testArray = GenerateRandomArray(1000);

            Console.WriteLine("Тестирование алгоритмов сортировки на массиве из 1000 элементов:");
            Console.WriteLine("=============================================================\n");

            Sort<int>[] algorithms = new Sort<int>[]
            {
            new BubbleSort<int>(),
            new SelectionSort<int>(),
            new InsertionSort<int>(),
            new MergeSort<int>(),
            new QuickSort<int>(),
            new HeapSort<int>(),
            new ShellSort<int>(),
            new CountingSort(),
            new RadixSort(),
            new TimSort<int>()
            };

            foreach (var algorithm in algorithms)
            {
                algorithm.RunSort(testArray);
            }
        }

        static int[] GenerateRandomArray(int size)
        {
            Random rnd = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rnd.Next(0, 10000);
            }
            return array;
        }
    }
}
