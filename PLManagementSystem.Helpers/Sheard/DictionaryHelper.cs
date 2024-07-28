using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Helpers.Sheard
{
    public static class DictionaryHelper
    {
        public static Dictionary<TKey, TValue> MergeDictionaries<TKey, TValue>(params Dictionary<TKey, TValue>[] dictionaries)
        {
            var mergedDictionary = new Dictionary<TKey, TValue>();
            foreach (var dictionary in dictionaries)
            {
                foreach (var kvp in dictionary)
                {
                    if (!mergedDictionary.ContainsKey(kvp.Key))
                    {
                        mergedDictionary[kvp.Key] = kvp.Value;
                    }
                }
            }
            return mergedDictionary;
        }
    }
}
