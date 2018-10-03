# yaml2csv

Converts an input stream of YAML into an output stream of CSV. Each YAML document becomes a row of CSV.

Built for [.NET Core](https://dotnet.github.io/) using [YamlDotNet](https://github.com/aaubry/YamlDotNet), [CsvHelper](https://joshclose.github.io/CsvHelper/), and [xUnit.net](https://xunit.github.io/).

## Problem Description

Suppose you have a set of YAML documents that each have the same fields. Perhaps they live in separate files, such that you could generate a stream of these docs with `cat *.yaml`. Suppose that you would like to have this data in a tabular format as CSV, where each row of the CSV data corresponds to one of the YAML docs.

The goal of this tool is to enable that workflow, such that you could run `cat *.yaml | yaml2csv >data.csv` and be able to use that CSV data set to populate a database table, create a spreadsheet, or print and frame on your wall such as your specific use case may require.

## Getting Started

For now the way to install this tool is to clone the git repo, build it in Visual Studio, and copy the binary somewhere in your `$PATH`.

The default build for .NET Core apps is a DLL, not an exe file. To run it, use `dotnet yaml2csv.dll`, or change the project config to build a stand-alone exe.

## Links

* [ConvertYamlToJson Example](https://github.com/aaubry/YamlDotNet/wiki/Samples.ConvertYamlToJson)

## TODO

* Handle list fields
    * They should be converted as a comma-delimited string value
* Create a test suite to document edge cases
    * Particularly problem strings
* Warn if row contains a column count mis-match
* Read input a line at a time, instead of all at once
    * May be able to rely on delimiters being alone on a line
* Command-line flag to filter columns by name
* Command line flag -f to read input from file instead of stdin
    * Also support globbing
