using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GraphQL;

namespace Galaxy.Api.Core.Helpers
{
    public static class UpdateObjectByReflection
    {
        public static void SetProperties(Dictionary<string, object> input, object target)
        {
            var objectDictionary = input.ToDictionary(x => x.Key, x => (object) x.Value);
            foreach (var key in objectDictionary)
            {
                SetProperty(target,  key);
            }
        }       
        
        private static void SetProperty(object target, KeyValuePair<string, object> key)
        {
            var type = target.GetType();
            var property = type.GetProperty(TitleCase(key.Key));
            
            if (property == null) return;

            if (IsGenericType(key.Value))           
            {
                var dict = (Dictionary<string, object>) key.Value;                
                var objectValue = property.GetValue(target) ?? Activator.CreateInstance(property.PropertyType);
                SetProperties(dict, objectValue);
                property.SetValue(target, objectValue.GetValue());
            }
            else
            {
                HandleNonGenericType(target, key, property);
            }
        }

        private static void HandleNonGenericType(object target, KeyValuePair<string, object> key, PropertyInfo property)
        {
            if (IsArrayType(key.Value) || IsListType(key.Value))
            {
                var value = key.Value is List<object> listOfObjects ? listOfObjects.ToArray() : key.Value;
               
                var list = (object[]) value;
                var objectValue = (IList) Activator.CreateInstance(property.PropertyType);
                var firstItem = list.FirstOrDefault();

                if (IsGenericType(firstItem))
                {
                    foreach (Dictionary<string, object> item in list)
                    {
                        var newItem = Activator.CreateInstance(objectValue.GetType().GetGenericArguments()[0]);
                        SetProperties(item, newItem);
                        objectValue.Add(newItem);
                    }
                }
                else
                {
                    foreach (object item in list)
                    {
                        objectValue.Add(item);
                    }
                }

                property.SetValue(target, objectValue);
            }
            else
            {
                property.SetValue(target, key.Value);
            }
        }

        private static string TitleCase(string original)
        {
            return original.First().ToString().ToUpper() + original.Substring(1);
        }

        private static bool IsArrayType(object item)
        {
            return item != null && item.GetType() == typeof(object[]);
        }

        private static bool IsListType(object item)
        {
            return item is List<object>;
        }

        private static bool IsGenericType(object item)
        {
            return item != null
                   && item.GetType().IsGenericType
                   && item.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }
    }
}