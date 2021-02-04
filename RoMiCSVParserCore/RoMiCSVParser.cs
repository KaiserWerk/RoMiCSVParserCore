using System;
using System.Collections.Generic;
using System.Reflection;

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
                string[] fields = new string[props.Length];
                int i = 0;
                foreach (PropertyInfo prop in props)
                {
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

        /// <How_to_deserialize>
        /// Pass the parameters for all properties of your object. E.g. an object with the properties 'int Age' and 'string Name' would be called as followed:
        /// IEnumerable<Person> result = RoMiCSVParser.Deserialize<Person>("18; Ingo")
        /// </How_to_deserialize>
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
                    throw new Exception($"wrong property count: props has {props.Length}, fields has {fields.Length}");

                int i = 0;
                foreach (PropertyInfo prop in props)
                {
                    bool canDoIt;
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        canDoIt = DateTime.TryParse(fields[i], out DateTime value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(double))
                    {
                        canDoIt = double.TryParse(fields[i], out double value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        canDoIt = int.TryParse(fields[i], out int value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        canDoIt = bool.TryParse(fields[i], out bool value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(item, fields[i]);
                    }
                    else if (prop.PropertyType == typeof(char))
                    {
                        canDoIt = char.TryParse(fields[i], out char value);
                        if (canDoIt)
                            prop.SetValue(item, value);
                    }

                    i++;
                }

                resultList.Add(item);
            }

            return resultList;
        }
    }
}
