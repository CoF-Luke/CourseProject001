namespace SortLib0
{
    public abstract class Sort<T> where T : IComparable<T>
    {
        protected T [] arr { get; set; }

        public Sort()
        {
            arr = Array.Empty<T>();
        }
        public Sort(T[] Arr)
        {
            arr = Arr;
        }
        public T[] GetArr()
        {
            return arr;
        }

        public abstract void RunSort();

        public void WriteArray()
        {
            foreach (var elem in arr)
            {
                Console.WriteLine(elem);
            }
        }
    }

    public class BubbleSort<T> : Sort<T> where T : IComparable<T>
    {
        public BubbleSort(T[] Arr) : base(Arr)
        {
        }


        public override void RunSort()
        {
            int n = arr.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j].CompareTo(arr[j + 1]) > 0)
                    {
                        T temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                {
                    break; // Массив отсортирован, можно прервать
                }
            }
        }
    }


    public class SelectionSort<T> : Sort<T> where T : IComparable<T>
    {
        public SelectionSort(T[] Arr) : base(Arr)
        {
        }


        public override void RunSort()
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j].CompareTo(arr[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }
                // Обмен найденного минимального элемента с первым элементом неотсортированной части
                T temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }
        }
    }


    public class InsertionSort<T> : Sort<T> where T : IComparable<T>
    {
        public InsertionSort(T[] Arr) : base(Arr)
        {
        }


        public override void RunSort()
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                T key = arr[i];
                int j = i - 1;

                // Перемещаем элементы arr[0..i-1], которые больше key,
                // на одну позицию вправо от их текущей позиции
                while (j >= 0 && arr[j].CompareTo(key) > 0)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }
    }


    public class QuickSort<T> : Sort<T> where T : IComparable<T>
    {
        public QuickSort(T[] Arr) : base(Arr)
        {
        }


        public override void RunSort()
        {
            Quick(arr, 0, 1000); ///////////////////////////////// <= случайные значения
        }

        public static void Quick(T[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);

                Quick(arr, low, pi - 1);
                Quick(arr, pi + 1, high);
            }
        }

        private static int Partition(T[] arr, int low, int high)
        {
            T pivot = arr[high];
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (arr[j].CompareTo(pivot) < 0)
                {
                    i++;
                    T temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            T temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;
            return (i + 1);
        }
    }

}
