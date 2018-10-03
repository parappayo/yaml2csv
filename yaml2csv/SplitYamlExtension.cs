
using System.Collections.Generic;

namespace yaml2csv
{
    public static class SplitYamlExtension
    {
        public static IEnumerable<string> SplitYaml(this string input)
        {
            const string yamlStart = "---";
            const string yamlEnd = "...";

            int docStart, docEnd = 0;

            while (docEnd != -1 && (docStart = input.IndexOf(yamlStart, docEnd)) != -1)
            {
                docEnd = input.IndexOf(yamlEnd, docStart);

                if (docEnd == -1)
                {
                    yield return input.Substring(docStart);
                }
                else
                {
                    yield return input.Substring(docStart, docEnd - docStart + yamlEnd.Length);
                }
            }
        }

    }
}
