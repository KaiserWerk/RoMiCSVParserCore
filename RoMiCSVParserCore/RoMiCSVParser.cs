using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using RoMiCSVParserCore.Attribute;
using RoMiCSVParserCore.Exception;

namespace RoMiCSVParserCore
{
    public static class RoMiCSVParser
    {
        /// <How_to_Serialize>
        /// Call method from any type that implements IEnumerable. e.g. a List named personList of type Person could be called with:
        /// string result = RoMiCSVParser.Serialize<Person>(personList);
        /// </How_to_Serialize>
        public static string Serialize<T>(IEnumerable<T> objList, string fieldSeparator = ";") where T : new()
        {
            List<string> resultList = new List<string>();

            foreach (T obj in objList)
            {
                Type type = obj.GetType();
                PropertyInfo[] props = type.GetProperties();
                int arrLen = props.Length;
                foreach (PropertyInfo prop in props)
                {
                    if (System.Attribute.IsDefined(prop, typeof(CsvPropertyIgnoreAttribute)))
                        arrLen--;
                }

                string[] fields = new string[arrLen];
                int i = 0;
                foreach (PropertyInfo prop in props)
                {
                    if (System.Attribute.IsDefined(prop, typeof(CsvPropertyIgnoreAttribute)))
                        continue;

                    if (prop.GetValue(obj) != null)
                        fields[i] = prop.GetValue(obj).ToString();
                    else
                        fields[i] = "NULL";
                    i++;
                }
                resultList.Add(string.Join(fieldSeparator, fields));
            }

            return string.Join(Environment.NewLine, resultList);
        }

        public static void SerializeToFile<T>(IEnumerable<T> objList, string filename, string fieldSeparator = ";") where T : new()
        {
            File.WriteAllText(filename, Serialize(objList, fieldSeparator));
        }

        /// <summary>
        /// Pass the parameters for all properties of your object. E.g. an object with the properties 'int Age' and 'string Name' would be called as followed:
        /// IEnumerable<Person> result = RoMiCSVParser.Deserialize<Person>("18;Ingo")
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="csv"></param>
        /// <param name="fieldSeparator"></param>
        /// <returns>IEnumerable</returns>
        /// <exception cref="PropertyCountMismatchException"></exception>
        /// <exception cref="UnsupportedPropertyTypeException"></exception>
        public static IEnumerable<T> Deserialize<T>(string csv, string fieldSeparator = ";") where T : new()
        {
            List<T> resultList = new List<T>();

            string[] lines = csv.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                T item = Activator.CreateInstance<T>();
                Type type = item.GetType();
                PropertyInfo[] props = type.GetProperties();

                string[] fields = line.Split(fieldSeparator);
                if (fields.Length != props.Length)
                    throw new PropertyCountMismatchException($"Wrong property count: there are {props.Length} properties, but {fields.Length} fields");

                int i = 0;
                foreach (PropertyInfo prop in props)
                {
                    if (System.Attribute.IsDefined(prop, typeof(CsvPropertyIgnoreAttribute)))
                        continue;

                    bool canDoIt;
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        canDoIt = DateTime.TryParse(fields[i], out DateTime value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    if (prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(Nullable<DateTime>))
                    {
                        canDoIt = DateTime.TryParse(fields[i], out DateTime value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                        else
                            prop.SetValue(item, null);
                    }
                    else if (prop.PropertyType == typeof(float))
                    {
                        canDoIt = float.TryParse(fields[i], out float value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(float?) || prop.PropertyType == typeof(Nullable<float>))
                    {
                        canDoIt = float.TryParse(fields[i], out float value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                        else
                            prop.SetValue(item, null);
                    }
                    else if (prop.PropertyType == typeof(double))
                    {
                        canDoIt = double.TryParse(fields[i], out double value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(double?) || prop.PropertyType == typeof(Nullable<double>))
                    {
                        canDoIt = double.TryParse(fields[i], out double value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                        else
                            prop.SetValue(item, null);
                    }
                    else if (prop.PropertyType == typeof(decimal))
                    {
                        canDoIt = decimal.TryParse(fields[i], out decimal value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(decimal?) || prop.PropertyType == typeof(Nullable<decimal>))
                    {
                        canDoIt = decimal.TryParse(fields[i], out decimal value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                        else
                            prop.SetValue(item, null);
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        canDoIt = int.TryParse(fields[i], out int value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(Nullable<int>))
                    {
                        canDoIt = int.TryParse(fields[i], out int value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                        else
                            prop.SetValue(item, null);
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        canDoIt = bool.TryParse(fields[i], out bool value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(bool?) || prop.PropertyType == typeof(Nullable<bool>))
                    {
                        canDoIt = bool.TryParse(fields[i], out bool value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                        else
                            prop.SetValue(item, null);
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        if (fields[i] == string.Empty || fields[i].ToLowerInvariant() == "null")
                            prop.SetValue(item, null);
                        else
                            prop.SetValue(item, fields[i]);
                    }
                    else if (prop.PropertyType == typeof(char))
                    {
                        canDoIt = char.TryParse(fields[i], out char value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(char?) || prop.PropertyType == typeof(Nullable<char>))
                    {
                        canDoIt = char.TryParse(fields[i], out char value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                        else
                            prop.SetValue(item, null);
                    }
                    else if (prop.PropertyType == typeof(byte))
                    {
                        canDoIt = byte.TryParse(fields[i], out byte value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(byte?) || prop.PropertyType == typeof(Nullable<byte>))
                    {
                        canDoIt = byte.TryParse(fields[i], out byte value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                        else
                            prop.SetValue(item, null);
                    }
                    else
                    {
                        throw new UnsupportedPropertyTypeException($"The property type {prop.PropertyType.ToString()} is not supported");
                    }

                    i++;
                }

                resultList.Add(item);
            }

            return resultList;
        }

        public static IEnumerable<T> DeserializeFromFile<T>(string filename, string fieldSeparator = ";") where T : new()
        {
            return Deserialize<T>(File.ReadAllText(filename), fieldSeparator);
        }
    }
}
