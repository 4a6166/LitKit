using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Selects the object in a collection of objects with the minimum quality TKey. If collection is empty, returns null. Looks like .WithMinimum(x => x.number)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="criterion"></param>
        /// <returns></returns>
        public static T WithMinimum<T, TKey>(this IEnumerable<T> sequence, Func<T, TKey> criterion)
            where T : class
            where TKey : IComparable<TKey> =>
                sequence.Aggregate((T)null, (best, cur) =>
                   best == null || criterion(cur).CompareTo(criterion(best)) < 0 ? cur : best);

        public static bool Has<T>(this System.Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }
        public static bool Is<T>(this System.Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }
        public static T Add<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }
        public static T Remove<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }

        /// <summary>Validate that T is an actual Enum</summary>
        /// <param name="type">Enum type</param>
        private static void ValidateEnum(Type type)
        {
            if (!type.IsSubclassOf(typeof(Enum)))
                throw new InvalidCastException($"Cannot cast {(object)type.FullName} to System.Enum");
        }

    }


}
