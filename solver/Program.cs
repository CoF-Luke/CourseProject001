using SortLib;
using System.ComponentModel;
using solver;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            throw new Exception("CommandLine parameters have not given.");
        }

        //обязательные
        string? InDataFile = null;
        string? TypeOfSort = null;
        int? LengthOfGeneration = null;

        //необязательные
        string? OutDataFile = null;
        string? LogFile = null;
        string? StatisticFile = null;
        bool Check = false;


        for (int i = 0; i < args.Length; i++)
        {
            //обязательные
            if (args[i] == "--in_data_file" && i + 1 < args.Length)
            {
                InDataFile = args[i + 1];
                Console.WriteLine(args[i + 1]);
            }
            //или
            if (args[i] == "--generate" && i + 1 < args.Length)
            {
                LengthOfGeneration = Convert.ToInt16(args[i + 1]);
            }
            if (args[i] == "--type_of_sort" && i + 1 < args.Length)
            {
                TypeOfSort = args[i + 1];
            }
            //необязательные
            if (args[i] == "-out_data_file" && i + 1 < args.Length)
            {
                OutDataFile = args[i + 1];
            }
            if (args[i] == "-log_on" && i + 1 < args.Length)
            {
                LogFile = args[i + 1];
            }
            if (args[i] == "-statistic_on" && i + 1 < args.Length)
            {
                StatisticFile = args[i + 1];
            }
            if (args[i] == "-check")
            {
                Check = true;
            }
        }

        if (((InDataFile != null) || (LengthOfGeneration != null)) && (TypeOfSort != null))
        {
            SortRunner Runner = new SortRunner(InDataFile,
                LengthOfGeneration, OutDataFile,
                TypeOfSort, LogFile, StatisticFile, Check);
            Runner.Start();
        }
        else
        {
            throw new Exception($"Not enough parameters.");
        }
    }
}