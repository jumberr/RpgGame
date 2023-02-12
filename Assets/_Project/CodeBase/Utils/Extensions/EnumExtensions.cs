using System.Collections.Generic;
using System.Linq;

namespace _Project.CodeBase.Utils.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) =>
            self.Select((item, index) => (item, index));
    }
}