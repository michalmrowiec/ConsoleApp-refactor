using System.Collections.Generic;
using System;
using System.Linq;

namespace ConsoleApp
{
    public static class DataPrinter
    {
        public static void PrintData(IEnumerable<ImportedObject> importedObjects)
        {
            var databases = importedObjects.Where(d => d.Type == "DATABASE").OrderBy(d => d.NumberOfChildren).ToList();
            var tables = importedObjects.Where(t => t.Type == "TABLE").OrderBy(t => t.NumberOfChildren).ToList();
            var columns = importedObjects.Where(c => c.Type == "COLUMN").ToList();

            foreach (var database in databases)
            {
                Console.WriteLine("______________________________________________________________________________________________");
                Console.WriteLine($"| Database: {database.Name,-64} | Tables: {database.NumberOfChildren,-6}|");
                Console.WriteLine("|____________________________________________________________________________________________|");

                foreach (var table in tables.Where(x => x.ParentName == database.Name))
                {
                    Console.WriteLine("\t______________________________________________________________________________________");
                    Console.WriteLine($"\tTable: {string.Concat(table.Schema, '.', table.Name),-61} | Columns: {table.NumberOfChildren,-5}");

                    foreach (var column in columns.Where(x => x.ParentName == table.Name))
                    {
                        Console.WriteLine($"\t\tColumn: {column.Name,-22} | Data type: {column.DataType,-16} | Nullable: {column.IsNullable,-8}");
                    }
                }
            }
        }
    }
}
