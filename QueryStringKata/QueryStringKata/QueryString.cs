using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryStringKata
{
    public class QueryString
    {
        readonly Dictionary<string, List<string>> pairs;

        public bool IsEmpty => pairs.Count == 0;

        public QueryString(IEnumerable<(string key, string value)> pairs)
        {
            this.pairs = pairs.GroupBy(x => x.Item1)
                .ToDictionary(x => x.Key, x => x.Select(v => v.value).ToList());
        }

        public string Lookup(string key)
        {
            if (!pairs.ContainsKey(key))
                return null;

            return pairs[key].First();
        }

        public int LookupInt32(string key) =>
            int.Parse(Lookup(key));

        public static QueryString Parse(string input) =>
            String.IsNullOrWhiteSpace(input)
                ? new QueryString(new (string key, string value)[0])
                : new QueryString(ParsePairs(input));

        static IEnumerable<(string, string)> ParsePairs(string input)
        {
            if (DecodingMap.Any(x => input.Contains(x.Value)))
                throw new ArgumentException("Invalid query string: " + input);

            return input.Split(new[] {'&'})
                .Select(ParsePair);
        }

        static (string, string) ParsePair(string token)
        {
            var parts = token.Split(new[] {'='});
            return (parts[0], Decode(parts[1]));
        }

        static string Decode(string value) =>
            DecodingMap.Aggregate(value, (current, item) => current.Replace(item.Key, item.Value));

        static readonly Dictionary<String, String> DecodingMap = new()
        {
            {"%20", " "},
            {"+", " "},
        };
    }
}
