using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    public class DataReader
    {
        private readonly string fileName;

        public DataReader(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<ImportedObject> ImportData()
        {
            var importedObjects = new List<ImportedObject>();
            var childrenCount = new Dictionary<string, int>();

            var streamReader = new StreamReader(fileName);

            var importedLines = new List<string>();
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                
                if(line == string.Empty)
                    continue;

                importedLines.Add(line);
            }

            for (int i = 1; i < importedLines.Count; i++)
            {
                var values = importedLines[i].Split(';');

                var importedObject = new ImportedObject(
                    type: values.ElementAtOrDefault(0) ?? "",
                    name: values.ElementAtOrDefault(1) ?? "",
                    schema: values.ElementAtOrDefault(2) ?? "",
                    parentName: values.ElementAtOrDefault(3) ?? "",
                    parentType: values.ElementAtOrDefault(4) ?? "",
                    dataType: values.ElementAtOrDefault(5) ?? "",
                    isNullable: values.ElementAtOrDefault(6) ?? "");

                var parentKey = string.Concat(importedObject.ParentType, importedObject.ParentName);
                if (childrenCount.ContainsKey(parentKey))
                {
                    childrenCount[parentKey]++;
                }
                else
                {
                    childrenCount[parentKey] = 1;
                }

                importedObjects.Add(importedObject);
            }

            foreach (var importedObject in importedObjects)
            {
                var importedObjectKey = string.Concat(importedObject.Type, importedObject.Name);
                if (childrenCount.ContainsKey(importedObjectKey))
                {
                    importedObject.NumberOfChildren = childrenCount[importedObjectKey];
                }
            }

            return importedObjects;
        }
    }
}
