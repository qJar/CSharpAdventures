using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    public class PrefixModule
    {
        /// <summary>
        /// Wczytuje liste prefiksow wraz z odpowiednimi krajami
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<string> LoadPrefixes(string filepath)
        {
            //sprawdz czy plik istnieje
            if (File.Exists(filepath))
            {
                //wczytaj zawartosc z pliku csv
                var prefixes = File.ReadAllLines(filepath).ToList();
                //usun wiersz naglowkowy
                prefixes.RemoveAt(0);
                //zwroc liste
                return prefixes;
            }
            //jesli plik nie istnieje zwroc null
            return null;
        }

        /// <summary>
        /// Okresla nazwe kraju na podstawie prefiksu
        /// </summary>
        /// <param name="prefixes"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodePrefix(List<string> prefixes, string code)
        {
            //sprawdz czy lista z prefiksami zostala utworzona
            if (prefixes == null)
            {
                return "Prefixes are unavailable";
            }
            //iteruj po wierszach listy
            foreach (var prefix in prefixes)
            {
                //rozdziel dane wg separatora
                var vals = prefix.Split(';');
                //sprawdz zakres prefiksu
                if (Int32.Parse(code) >= Int32.Parse(vals[0]) && Int32.Parse(code) <= Int32.Parse(vals[1]))
                {
                    //zwroc nazwe kraju wg prefiksu
                    return vals[2];
                }
            }
            //zwroc jesli prefiks nie istnieje
            return "Prefix unknown";
        }
    }
}
