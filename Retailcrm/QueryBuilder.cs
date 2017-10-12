namespace Retailcrm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// QueryBuilder
    /// </summary>
    public class QueryBuilder
    {
        private readonly List<KeyValuePair<string, object>> _keyValuePairs
            = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Build PHP like query string
        /// </summary>
        /// <param name="queryData"></param>
        /// <param name="argSeperator"></param>
        /// <returns></returns>
        public static string BuildQueryString(object queryData, string argSeperator = "&")
        {
            var encoder = new QueryBuilder();
            encoder.AddEntry(null, queryData, allowObjects: true);

            return encoder.GetUriString(argSeperator);
        }

        private string GetUriString(string argSeperator)
        {
            return String.Join(argSeperator,
                _keyValuePairs.Select(kvp =>
                {
                    var key = Uri.EscapeDataString(kvp.Key);
                    var value = Uri.EscapeDataString(kvp.Value.ToString());
                    return $"{key}={value}";
                }));
        }

        private void AddEntry(string prefix, object instance, bool allowObjects)
        {
            var dictionary = instance as IDictionary;
            var collection = instance as ICollection;

            if (dictionary != null)
            {
                Add(prefix, GetDictionaryAdapter(dictionary));
            }
            else if (collection != null)
            {
                Add(prefix, GetArrayAdapter(collection));
            }
            else if (allowObjects)
            {
                Add(prefix, GetObjectAdapter(instance));
            }
            else
            {
                _keyValuePairs.Add(new KeyValuePair<string, object>(prefix, instance));
            }
        }

        private void Add(string prefix, IEnumerable<Entry> datas)
        {
            foreach (var item in datas)
            {
                var newPrefix = String.IsNullOrEmpty(prefix)
                    ? item.Key
                    : $"{prefix}[{item.Key}]";

                AddEntry(newPrefix, item.Value, allowObjects: false);
            }
        }

        private struct Entry
        {
            public string Key;
            public object Value;
        }

        private IEnumerable<Entry> GetObjectAdapter(object data)
        {
            var properties = data.GetType().GetProperties();

            foreach (var property in properties)
            {
                yield return new Entry()
                {
                    Key = property.Name,
                    Value = property.GetValue(data)
                };
            }
        }

        private IEnumerable<Entry> GetArrayAdapter(ICollection collection)
        {
            int i = 0;
            foreach (var item in collection)
            {
                yield return new Entry()
                {
                    Key = i.ToString(),
                    Value = item,
                };
                i++;
            }
        }
        
        private IEnumerable<Entry> GetDictionaryAdapter(IDictionary collection)
        {
            foreach (DictionaryEntry item in collection)
            {
                yield return new Entry()
                {
                    Key = item.Key.ToString(),
                    Value = item.Value,
                };
            }
        }
    }
}