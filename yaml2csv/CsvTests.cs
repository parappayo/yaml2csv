using Xunit;

namespace yaml2csv
{
    public class CsvTests
    {
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
    }
}
