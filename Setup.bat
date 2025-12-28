@echo off 

:: 1. Открыть папку с решением..........................................................
cd C:\Users\user\source\repos\CourseProject001\solver

:: 2. Запуск программы с флагами........................................................
dotnet run --generate 100000 -out_data_file C:\Users\user\source\repos\CourseProject001\output_file.txt --type_of_sort all -log_on C:\Users\user\source\repos\CourseProject001\log.txt -statistic_on C:\Users\user\source\repos\CourseProject001\statistic.csv -check

:: 3. Открытие папки с проектом перед завершением работы................................
cd C:\Users\user\source\repos\CourseProject001

:: 4. Завершение........................................................................
pause

:: ################################ Памятка пользователя ###############################
:: 
:: Запуск программы осуществляют строки в начале текущего файла, они пронумерованы по действиям.
:: 
:: Флаги для запуска программы:
:: 	Обязательные флаги:
::		--in_data_file ...  (этот!          Но один из этих
::		--generate ...       или этот!)     двух точно обязательный!
::		--type_of_sort ...
::			Можно ввести all или последовательность из букв ->-> (например: bsqm)
::				b — BubbleSort
::				s — SelectionSort
::				i — InsertionSort
::				m — MergeSort
::				q — QuickSort
::				h — HeapSort
::				d — ShellSort
::				c — CountingSort
::				t — TimSort
:: 	Необязательные флаги:
::		-out_data_file ...
::		-log_on ...
::		-statistic_on ...
::		-check 
:: 




:: ################################ Заметки разработчика ###############################
::--generate 10000
::--in_data_file C:\Users\user\source\repos\CourseProject001\input_file.txt
::-statistic_on C:\Users\user\source\repos\CourseProject001\statistic.csv
::bsimqhdt