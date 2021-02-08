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

## Roadmap

* Implement the rest of the primitive, built-in data types [See MSDN docs](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types).
* Optional parameter `fieldEncloser` with the default value of string.Empty (just an empty string), 
  which can be used to enclose every field, if the need arises

## Usage

Import the namespace __RoMiCSVParserCore__.

The __RoMiCSVParserCore__ package supports the following data types:

* ```DateTime, DateTime?, Nullable<DateTime>```
* int, int?, Nullable<int>, 
* float, float?, Nullable<float>
* double, double?, Nullable<double>
* decimal, decimal?, Nullable<decimal>
* bool, bool?, Nullable<bool>
* byte byte?, Nullable<byte>
* char, char?, Nullable<char> 
* string :)



## Example

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
// the second parameter is optional
string csvContent = RoMiCSVParser.Serialize<Person>(list);

// do stuff with the CSV content string
```

How to deserialize
```csharp
string csvContent = File.ReadAllText("people.csv");
// the second parameter is optional
IEnumerable<Person> people = RoMiCSVParser.Deserialize<Person>(csvContent);

// use the people like the tyrant you are
```

Alternatively, you can use the convenience methods:

```csharp
void SerializeToFile<T>(IEnumerable<T> objList, string filename, string fieldSeparator = ";")
// and
IEnumerable<T> DeserializeFromFile<T>(string filename, string fieldSeparator = ";")
```
