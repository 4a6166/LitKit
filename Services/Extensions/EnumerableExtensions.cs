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
    }


}
