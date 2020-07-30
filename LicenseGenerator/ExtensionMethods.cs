using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseGenerator
{
    public static class ExtensionMethods
    {
        public static List<object> ToSelectList<TEnum>(this TEnum enumObj) where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var selectList = new List<object>();
            foreach (TEnum listItem in Enum.GetValues(typeof(TEnum)))
            {
                selectList.Add(listItem.ToString());
            }
            return selectList;
        }

        public static T ParseEnum<T>(this string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }
}
