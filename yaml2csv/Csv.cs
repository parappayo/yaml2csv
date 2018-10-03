using System.Collections.Generic;
using System.Text;
using CsvHelper;

namespace yaml2csv
{
    class Csv
    {
        private static void Enquote(StringBuilder value)
        {
            value.Insert(0, "\"");
            value.Append("\"");
        }

        private static void EscapeCsvChars(StringBuilder value)
        {
            value.Replace("\"", "\"\"");
            value.Replace("\n", "\\n");
        }

        private static void Append(StringBuilder destination, List<object> source)
        {
            bool isFirstItem = true;

            foreach (var item in source)
            {
                if (!isFirstItem)
                {
                    destination.Append(",");
                }
                else
                {
                    isFirstItem = false;
                }

                destination.Append(item.ToString());
            }
        }

        private static void Append(StringBuilder destination, Dictionary<object, object> source)
        {
            bool isFirstItem = true;

            foreach (var key in source.Keys)
            {
                var item = source[key];

                if (!isFirstItem)
                {
                    destination.Append(",");
                }
                else
                {
                    isFirstItem = false;
                }

                destination.Append(key.ToString() + ":" + item.ToString());
            }
        }

        public static string ToEscapedString(object value)
        {
            StringBuilder result = new StringBuilder();

            var valueAsList = value as List<object>;
            var valueAsDictionary = value as Dictionary<object, object>;

            if (valueAsList != null)
            {
                Append(result, valueAsList);
            }
            else if (valueAsDictionary != null)
            {
                Append(result, valueAsDictionary);
            }
            else
            {
                result.Append(value.ToString());
            }

            EscapeCsvChars(result);
            Enquote(result);
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
