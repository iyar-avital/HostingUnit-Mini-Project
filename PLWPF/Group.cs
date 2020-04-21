using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLWPF
{
    public static class Group
    {

        public static List<TKey> KeysInGroup<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> elements)
        {
            List<TKey> Keys = new List<TKey>();
            foreach (var item in elements)
            {
                Keys.Add(item.Key);
            }
            return Keys;
        }

        public static List<TElement> ItemsInKey<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> elements, TKey myKey) where TKey : struct 
        {
            List<TElement> itemsByKey = new List<TElement>();
            foreach (var item in elements)
            {
                if (item.Key.Equals(myKey))
                {
                    foreach (var item1 in item)
                    {
                        itemsByKey.Add(item1);
                    }
                    return itemsByKey;
                }
            }
            return null;
        }

    }
}
