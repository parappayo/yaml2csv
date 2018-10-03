using System.Collections.Generic;
using System.Text;
using CsvHelper;

namespace yaml2csv
{
    class Csv
    {
        public static string ToEscapedString(object value)
        {
            StringBuilder result = new StringBuilder();

            result.Append(value.ToString());
            result.Replace("\"", "\"\"");
            result.Replace("\n", "\\n");

            result.Insert(0, "\"");
            result.Append("\"");
            return result.ToString();
        }

        public static void WriteHeaders(CsvSerializer csv, Dictionary<string, object> data)
        {
            string[] keys = new string[data.Keys.Count];
            data.Keys.CopyTo(keys, 0);

            csv.Write(keys);
            csv.WriteLine();
        }

        public static void WriteValues(CsvSerializer csv, Dictionary<string, object> data)
        {
            object[] valueObjs = new object[data.Values.Count];
            data.Values.CopyTo(valueObjs, 0);

            string[] values = new string[valueObjs.Length];
            for (int i = 0; i < valueObjs.Length; i++)
            {
                values[i] = ToEscapedString(valueObjs[i]);
            }

            csv.Write(values);
            csv.WriteLine();
        }

    }
}
