using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    public class PrefixLoader
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
    }
}
