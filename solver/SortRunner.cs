using SortLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace solver
{
    public class SortRunner
    {
        protected string? InDataFile;
        protected int? LengthOfGeneration;

        protected string TypeOfSort;

        bool Out = false;
        protected string? OutDataFile;

        bool Log = false;
        string? LogFile;
        string? logData;

        bool Statistic = false;
        string? StatisticFile;
        string? statisticData;

        bool Check = false;

        int[] array = Array.Empty<int>();
        int[] outArray = Array.Empty<int>();


        public SortRunner(string? inDataFile, int? lengthOfGeneration,
            string? outDataFile, string typeOfSort, string? logFile,
            string? statisticFile, bool check)
        {
            InDataFile = inDataFile;
            LengthOfGeneration = lengthOfGeneration;

            TypeOfSort = typeOfSort;


            if (outDataFile != null)
            {
                Out = true;
                OutDataFile = outDataFile;
            }
            if (logFile != null)
            {
                Log = true;
                LogFile = logFile;
            }
            if (statisticFile != null)
            {
                Statistic = true;
                StatisticFile = statisticFile;
            }

            Check = check;
        }

        ////////////////////////////////////////////////////////////////////////////////////
        public void Start()
        {
            //заполнение массива
            if (InDataFile != null)
            {
                ReadFromFile(InDataFile, array);
            }
            else if (LengthOfGeneration != null)
            {
                GenerateSequense((int)LengthOfGeneration);
            }
            else throw new Exception("Input format is not correct.");


            //выбор типов сортировки
            Sort<int>[] algorithms = Array.Empty<Sort<int>>();
            if (TypeOfSort == "all")
            {
                algorithms = new Sort<int>[]
                {
                    new BubbleSort<int>(),
                    new SelectionSort<int>(),
                    new InsertionSort<int>(),
                    new MergeSort<int>(),
                    new QuickSort<int>(),
                    new HeapSort<int>(),
                    new ShellSort<int>(),
                    new CountingSort(),
                    new TimSort<int>()
                };
            }
            else if (TypeOfSort != "")
            {
                algorithms = new Sort<int>[TypeOfSort.Length];
                for (int i = 0; i < TypeOfSort.Length; i++)
                {
                    switch (TypeOfSort[i])
                    {
                        case 'b':
                            algorithms[i] = new BubbleSort<int>();
                            break;
                        case 's':
                            algorithms[i] = new SelectionSort<int>();
                            break;
                        case 'i':
                            algorithms[i] = new InsertionSort<int>();
                            break;
                        case 'm':
                            algorithms[i] = new MergeSort<int>();
                            break;
                        case 'q':
                            algorithms[i] = new QuickSort<int>();
                            break;
                        case 'h':
                            algorithms[i] = new HeapSort<int>();
                            break;
                        case 'd':
                            algorithms[i] = new ShellSort<int>();
                            break;
                        case 'c':
                            algorithms[i] = new CountingSort();
                            break;
                        case 't':
                            algorithms[i] = new TimSort<int>();
                            break;
                        default:
                            throw new Exception("TypeOfSort not correct.");

                    }
                }
            }
            
            
            //если нужно давать log и статистику?
            if (Log)
            {
                logData = $"Sorting Program || Logging || Time of exexution: {DateTime.Now}\n\n";

                if (InDataFile != null) logData += $"InDataFile: {InDataFile}\n";
                if (LengthOfGeneration != null) logData += $"LengthOfGeneration: {LengthOfGeneration}\n";
                if (OutDataFile != null) logData += $"OutDataFile: {OutDataFile}\n";
                if (TypeOfSort != null) logData += $"TypeOfSort: {TypeOfSort}\n";
                if (LogFile != null) logData += $"LogFile: {LogFile}\n";
                if (StatisticFile != null) logData += $"StatisticFile: {StatisticFile}\n";
                logData += $"Check: {Check}\n";

                logData += $"\n//////////////////  Sorting algorithms  ////////////////////\n\n";
            }

            if (Statistic)
            {
                statisticData = "Type;Length of array;Execution time;Compairings;Swaps\n";
            }
            
            
            //выполнение сортировки каждым методом
            foreach (var algorithm in algorithms)
            {
                algorithm.RunSort(array);

                if (Log)
                {
                    logData += algorithm.GetLog(Statistic);
                }
                if (Statistic)
                {
                    statisticData += algorithm.GetStatistic() + "\n";
                }
                
                if (Check && Log)
                {
                    bool flag = true;
                    bool flagSameArray = true;

                    int[] tempArray = algorithm.GetArray();
                    if (outArray.Length == 0)
                    {
                        outArray = new int[tempArray.Length];
                        tempArray.CopyTo(outArray, 0);

                        for (int i = 0; i + 1 < outArray.Length; i++)
                        {
                            if (outArray[i] > outArray[i + 1])
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i + 1 < tempArray.Length; i++)
                        {
                            if (tempArray[i] > tempArray[i + 1])
                            {
                                flag = false;
                                break;
                            }
                            if (tempArray[i] != outArray[i])
                            {
                                flagSameArray = false;
                            }
                        }
                    }

                    if (flag)
                    {
                        logData += "Checking was completed:  Array was sorted correctly    ^-^\n";

                        if (flagSameArray) logData += "This out array is the same to first sorted   ^-^\n";
                        else logData += "This out array is the same to first sorted   !*-*!\n";
                    }
                    else
                    {
                        logData += "Checking was completed:  Array was not sorted  correctly!!!    !*-*!\n";
                    }
                }

                if (Log) logData += "\n";
            }

            
            

            if (Log)
            {
                WriteToFile(LogFile, logData);
            }

            if (Statistic)
            {
                WriteToFile(StatisticFile, statisticData);
            }

            if (Out)
            {
                if (outArray.Length != 0)
                {
                    WriteToFile(OutDataFile, outArray);
                }
                else
                {
                    throw new Exception("Невозможно записать данные в выходной файл: массив пуст.");
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////


        public void GenerateSequense(int length)
        {
            Random rnd = new();

            array = new int[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = rnd.Next() - 1073500000;
            }
        }

        public void ReadFromFile(string FileName, int[]? arr)
        {
            if (!File.Exists(FileName))
            {
                throw new FileNotFoundException("Файл с массивом не найден");
            }

            using (var sr = new StreamReader(FileName))
            {
                int length;
                string? line;
                if ((line = sr.ReadLine()) != null)
                {
                    length = Convert.ToInt32(line);
                }
                else throw new FileLoadException("File is empty.");

                array = new int[length];

                for (int i = 0; i < length; i++)
                {
                    if ((line = sr.ReadLine()) == null) throw new FileLoadException("Not enough data in file.");
                    else array[i] = Convert.ToInt32(line);
                }
            }
            
        }

        public void WriteToFile(string? FileName, int[]? arr)
        {
            if (FileName == null || arr == null) return;
            using (var sw = new StreamWriter(FileName, false))
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    sw.WriteLine(arr[i]);
                }
            }
        }

        public void WriteToFile(string? FileName, string? data)
        {
            if (FileName == null || data == null) return;
            using (var sw = new StreamWriter(FileName, false))
            {
                sw.WriteLine(data);
            }
        }
    }
}
