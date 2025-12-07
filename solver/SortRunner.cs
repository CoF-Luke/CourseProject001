using SortLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solver
{
    public class SortRunner
    {
        protected string? InDataFile = null;
        protected int? LengthOfGeneration = null;

        protected string TypeOfSort;

        bool Out = false;
        protected string? OutDataFile = null;

        bool Log = false;
        string? LogFile = null;
        string logData;

        bool Statistic = false;
        string? StatisticFile = null;
        string statisticData;

        bool Copy;
        string? CopyFile;

        int[] array = Array.Empty<int>();

        public SortRunner()
        {
            InDataFile = "";
            OutDataFile = "";
            TypeOfSort = "";
        }
        public SortRunner(string? inDataFile, int? lengthOfGeneration, string outDataFile, string typeOfSort, string? logFile, string? statisticFile, bool copy)
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

            Copy = copy;
        }

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
                    //new RadixSort(),
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
                        case 'r':
                            algorithms[i] = new RadixSort();
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
                if (CopyFile != null) logData += $"CopyFile: {CopyFile}\n";

                logData += $"\n//////////////////  Sorting algorithms  ////////////////////\n\n";
            }

            if (Statistic)
            {
                statisticData = "Type,Length of array,Execution time,Compairings,Swaps\n";
            }
            
            
            //выполнение сортировки каждым методом
            foreach (var algorithm in algorithms)
            {
                algorithm.RunSort(array);

                if (Log)
                {
                    logData += algorithm.GetLog(Statistic) + "\n";
                }
                if (Statistic)
                {
                    statisticData += algorithm.GetStatistic() + "\n";
                }
                
            }

            
            //if (Out)
            //{
            //    WriteToFile(OutDataFile, )
            //}

            if (Log)
            {
                WriteToFile(LogFile, logData);
            }

            if (Statistic)
            {
                WriteToFile(StatisticFile, statisticData);
            }
        }




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
                string line;
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

        public void WriteToFile(string FileName, int[]? arr)
        {
            using (var sw = new StreamWriter(FileName, false))
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    sw.WriteLine(arr[i]);
                }
            }
        }

        public void WriteToFile(string FileName, string data)
        {
            using (var sw = new StreamWriter(FileName, false))
            {
                sw.WriteLine(data);
            }
        }
    }
}
