using System;

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

        static void Main(string[] args)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            Object data = deserializer.Deserialize<Object>(TestYamlDoc);

            Console.Write(data);
        }
    }
}
