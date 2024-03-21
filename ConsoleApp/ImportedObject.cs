using System;

namespace ConsoleApp
{
    public class ImportedObject
    {
        private string _type;
        private string _name;
        private string _schema;
        private string _parentType;
        private string _parentName;
        private string _dataType;

        public string Type
        {
            get => _type;
            set => _type = CleanInput(value, true);
        }
        public string Name
        {
            get => _name;
            set => _name = CleanInput(value);
        }
        public string Schema
        {
            get => _schema;
            set => _schema = CleanInput(value);
        }
        public string ParentType
        {
            get => _parentType;
            set => _parentType = CleanInput(value, true);
        }
        public string ParentName
        {
            get => _parentName;
            set => _parentName = CleanInput(value);
        }
        public string DataType
        {
            get => _dataType; set
            {
                var cleaned = CleanInput(value);

                var index = cleaned.IndexOf(':');
                if (index >= 0)
                {
                    cleaned = cleaned.Substring(index + 1);
                }
                _dataType = cleaned;
            }
        }
        public bool IsNullable { get; set; }

        public int NumberOfChildren { get; set; }

        public ImportedObject(
            string type, string name, string schema, string parentName, string parentType, string dataType, string isNullable)
        {
            Type = type;
            Name = name;
            Schema = schema;
            ParentName = parentName;
            ParentType = parentType;
            DataType = dataType;
            IsNullable = isNullable == "1";
        }

        private string CleanInput(string input, bool toUpper = false)
        {
            var cleaned = input
            .Trim()
            .Replace(" ", "")
            .Replace(Environment.NewLine, "");

            return toUpper ? cleaned.ToUpper() : cleaned;
        }
    }
}
