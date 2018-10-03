using System;
using System.Collections.Generic;
using CsvHelper;

namespace yaml2csv
{
    class Program
    {
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
                    Csv.WriteHeaders(csv, data);
                    firstRow = false;
                }

                Csv.WriteValues(csv, data);
            }
        }
    }
}
