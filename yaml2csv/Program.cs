using System;
using System.Collections.Generic;
using CsvHelper;

namespace yaml2csv
{
    class Program
    {
        static void WriteCsvHeaders(CsvSerializer csv, Dictionary<string, Object> data)
        {
            string[] keys = new string[data.Keys.Count];
            data.Keys.CopyTo(keys, 0);

            csv.Write(keys);
            csv.WriteLine();
        }

        static void WriteCsvValues(CsvSerializer csv, Dictionary<string, Object> data)
        {
            object[] valueObjs = new object[data.Values.Count];
            data.Values.CopyTo(valueObjs, 0);

            string[] values = new string[valueObjs.Length];
            for (int i = 0; i < valueObjs.Length; i++)
            {
                values[i] = "\"" +
                    valueObjs[i].ToString()
                        .Replace("\"", "\"\"")
                        .Replace("\n", "\\n") +
                    "\"";
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
                var data = yamlDeserializer.Deserialize<Dictionary<string, Object>>(yamlDoc);

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
