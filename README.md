# RoMiCSVParserCore

Hier steht dann bald der interessante Text.

## License

See LICENSE.md

## Contributing

...

## Installation

Install the nuget package __RoMiCSVParserCore__ or use this command in the
package manager console:

```
Install-Package RoMiCSVParserCore -Version 1.0.1
```

## Usage

Import the namespace RoMiCSVParser.

The __RoMiCSVParserCore__ package supports the data types string, int, 
double, bool and char. DateTimes are Work In Progress!

In the following examples, *Person* is a class with some arbitrary fields with primitive data types.

How to serialize:
```csharp

List<Person> list = <some collection data>
string csvContent = RoMiCSVParser.Serialize<Person>(list);

// do stuff with the CSV content string
```


