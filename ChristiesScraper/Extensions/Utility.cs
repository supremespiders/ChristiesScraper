using ChristiesScraper.Models;
using Newtonsoft.Json;

namespace ChristiesScraper.Extensions
{
    public static class Utility
    {

        public static string ColumnIndexToColumnLetter(this int colIndex)
        {
            var div = colIndex;
            var colLetter = String.Empty;
            var mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }

            return colLetter;
        }

        public static bool MyContains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV defaultValue = default(TV))
        {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }
        

        public static Dictionary<string, string> ReadMapFile(this string fileName)
        {
            if (fileName == null) throw new KnownException($"File name can't be null");
            var dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (!File.Exists(fileName))
                File.WriteAllText(fileName, "");
            var lines = File.ReadAllLines(fileName);
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var c = line.Split("|");
                if (c.Length != 2) throw new KnownException($"Failed to parse map file : {fileName} , at line {i + 1}, it should be Key|Value");
                dic.Add(c[0], c[1]);
            }

            return dic;
        }

        public static void Save<T>(this List<T> items, string path = null)
        {
            var name = typeof(T).Name;
            if (path != null) name = path;
            File.WriteAllText(name, JsonConvert.SerializeObject(items));
        }

        public static List<T> Load<T>(this string path)
        {
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
        }

        public static string StringBetween(this string main, string s1, string s2)
        {
            if (!main.Contains(s1) || !main.Contains(s2)) return null;
            var x1 = main.IndexOf(s1, StringComparison.Ordinal) + s1.Length;
            var x2 = main.IndexOf(s2, x1 + 1, StringComparison.Ordinal);
            return (main.Substring(x1, x2 - x1));
        }
    }
}