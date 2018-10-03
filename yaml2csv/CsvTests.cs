using CsvHelper;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace yaml2csv
{
    public class CsvTests
    {
        private static string TestYamlDoc =
            "---\n" +
            "id: 123\n" +
            "name: \"Test Data Set\"\n" +
            "myList:\n" +
            "  - one\n" +
            "  - two\n" +
            "  - three\n" +
            "...";

        private Dictionary<string, object> TestData;

        public CsvTests()
        {
            var yamlDeserializer = new YamlDotNet.Serialization.Deserializer();
            TestData = yamlDeserializer.Deserialize<Dictionary<string, object>>(TestYamlDoc);
        }

        [Fact]
        public void ToEscapedString_AddsQuotes()
        {
            string result = Csv.ToEscapedString("test");
            Assert.True(result[0] == '"');
            Assert.True(result[result.Length - 1] == '"');
        }

        [Fact]
        public void ToEscapedString_EscapesEmbeddedQuotes()
        {
            Assert.True(Csv.ToEscapedString("\"") == "\"\"\"\"");
        }

        [Fact]
        public void ToEscapedString_EscapesEmbeddedNewlines()
        {
            Assert.True(Csv.ToEscapedString("\n") == "\"\\n\"");
        }

        [Fact]
        public void WriteHeaders_Works()
        {
            StringWriter stringWriter = new StringWriter();
            var csv = new CsvSerializer(stringWriter);
            Csv.WriteHeaders(csv, TestData);

            Assert.StartsWith(stringWriter.ToString(), "id,name,myList");
        }
    }
}
