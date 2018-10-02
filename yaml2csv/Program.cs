using System;
using System.Collections.Generic;
using CsvHelper;

namespace yaml2csv
{
    class Program
    {
        public static string TestYamlDoc =
            "---\n" +
            "id: 123\n" +
            "name: \"Test Data Set\"\n" +
            "myList:\n" +
            "  - one\n" +
            "  - two\n" +
            "  - three\n" +
            "...";

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
                values[i] = valueObjs[i].ToString();
            }

            csv.Write(values);
            csv.WriteLine();
        }

        static void Main(string[] args)
        {
            var yamlDeserializer = new YamlDotNet.Serialization.Deserializer();
            var data = yamlDeserializer.Deserialize<Dictionary<string, Object>>(TestYamlDoc);

            var csv = new CsvSerializer(Console.Out);
            WriteCsvHeaders(csv, data);
            WriteCsvValues(csv, data);
        }
    }
}
