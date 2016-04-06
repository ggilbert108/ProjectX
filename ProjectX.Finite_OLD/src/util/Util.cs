using System.Collections.Generic;

namespace ProjectX.Finite
{
    public static class Util
    {
        public static HashSet<T> ToSet<T>(IEnumerable<T> other)
        {
            HashSet<T> result = new HashSet<T>();
            foreach (T value in other)
            {
                result.Add(value);
            }
            return result;
        }
    }
}