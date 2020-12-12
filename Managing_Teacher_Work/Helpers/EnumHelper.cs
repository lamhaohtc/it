using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Managing_Teacher_Work.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            return value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }

        public static List<ValueName> GetEnumList<T>()
        {
            List<ValueName> valueNameList = new List<ValueName>();
            var dict = EnumDictionary<T>();
            foreach (KeyValuePair<int, string> entry in dict)
            {
                valueNameList.Add(new ValueName { Value = entry.Key, Name = entry.Value });
                // do something with entry.Value or entry.Key
            }
            return valueNameList;
        }

        public static Dictionary<int, string> EnumDictionary<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type must be an enum");
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(t => (int)(object)t, t => _GetEnumDescription(t));
        }

        private static string _GetEnumDescription(object value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}