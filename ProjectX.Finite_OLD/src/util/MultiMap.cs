using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Finite
{
    public class MultiMap<TKey, TValue>
    {
        public List<KeyValuePair<TKey, TValue>> Map { get; private set; }

        public MultiMap()
        {
            Map = new List<KeyValuePair<TKey, TValue>>();
        }

        public void Add(TKey key, TValue value)
        {
            var pair = new KeyValuePair<TKey, TValue>(key, value);
            Map.Add(pair);
        }

        public List<TValue> this[TKey key]
        {
            get
            {
                var query =
                    from pair in Map
                    where pair.Key.Equals(key)
                    select pair.Value;

                return query.ToList();
            }
        } 
    }
}