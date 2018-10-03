using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;

namespace yaml2csv
{
    class Program
    {
        static string ConvertToEscapedCsv(object value)
        {
            StringBuilder result = new StringBuilder();

            result.Append(value.ToString());
            result.Replace("\"", "\"\"");
            result.Replace("\n", "\\n");

            result.Insert(0, "\"");
            result.Append("\"");
            return result.ToString();
        }

        static void WriteCsvHeaders(CsvSerializer csv, Dictionary<string, object> data)
        {
            string[] keys = new string[data.Keys.Count];
            data.Keys.CopyTo(keys, 0);

            csv.Write(keys);
            csv.WriteLine();
        }

        static void WriteCsvValues(CsvSerializer csv, Dictionary<string, object> data)
        {
            object[] valueObjs = new object[data.Values.Count];
            data.Values.CopyTo(valueObjs, 0);

            string[] values = new string[valueObjs.Length];
            for (int i = 0; i < valueObjs.Length; i++)
            {
                values[i] = ConvertToEscapedCsv(valueObjs[i]);
            }

            csv.Write(values);
            csv.WriteLine();
        }

        static void Main(string[] args)
        {
            var yamlDeserializer = new YamlDotNet.Serialization.Deserializer();
            var csv = new CsvSerializer(Console.Out);

            string input = Console.In.ReadToEnd();
            bool firstRow = true;

            foreach (string yamlDoc in input.SplitYaml())
            {
                var data = yamlDeserializer.Deserialize<Dictionary<string, object>>(yamlDoc);

                if (firstRow)
                {
                    WriteCsvHeaders(csv, data);
                    firstRow = false;
                }

                WriteCsvValues(csv, data);
            }
        }
    }
}
