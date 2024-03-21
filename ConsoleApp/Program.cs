using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataReader("data.csv");
            var importedData = reader.ImportData();

            DataPrinter.PrintData(importedData);
            Console.ReadLine();
        }
    }
}