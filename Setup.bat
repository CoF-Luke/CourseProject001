@echo off 
cd C:\Users\user\source\repos\CourseProject001\solver
dotnet run --generate 10000 -out_data_file C:\Users\user\source\repos\CourseProject001\output_file.txt --type_of_sort all -log_on C:\Users\user\source\repos\CourseProject001\log.txt -statistic_on C:\Users\user\source\repos\CourseProject001\statistic.csv -check
cd C:\Users\user\source\repos\CourseProject001
pause

:: --in_data_file C:\Users\user\source\repos\CourseProject001\input_file.txt