# RoMiCSVParserCore

A simple and easy-to-use library to serialize and deserialize CSV data. 

## License

MIT, see LICENSE.md

## Installation

Install the nuget package __RoMiCSVParserCore__ or use this command in the
package manager console:

```
PM> Install-Package RoMiCSVParserCore
```

## Usage

Import the namespace RoMiCSVParser.

The __RoMiCSVParserCore__ package supports the data types DateTime, string, int, 
double, bool and char.

In the following examples, *Person* is a class with some arbitrary fields with primitive data types 
which looks like this:

```csharp
public class Person
{
	public string Firstname { get; set; }
	public string Lastname { get; set; }
	public int Age { get; set; }
	public double Salary { get; set; }
	public bool IsMarried { get; set; }
}
```

How to serialize:
```csharp

List<Person> list = <some collection data>
string csvContent = RoMiCSVParser.Serialize<Person>(list);

// do stuff with the CSV content string
```

How to deserialize
```csharp
string csvContent = File.ReadAllText("people.csv");
IEnumerable<Person> people = RoMiCSVParser.Deserialize<Person>(csvContent);

// use the people like the tyrant you are
```

Coming with version 1.1.0, there are also two convenience methods:

```csharp
public static void SerializeToFile<T>(IEnumerable<T> objList, string filename, string fieldSeparator = ";") where T : new()
// and
public static IEnumerable<T> DeserializeFromFile<T>(string filename, string fieldSeparator = ";") where T : new()
```
