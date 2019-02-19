using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumForEanCodeAft
{
    /// <summary>
    /// Klasa odpowiedzialna za operacja na plikach
    /// </summary>
    public class TextFileProcessor
    {
        /// <summary>
        /// Wczytuje kody z pliku i zwraca w formie listy do aplikacji
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<string> LoadCodesFromFile(string filepath)
        {
            //utworz instancje pustej listy
            List<string> outputList = new List<string>();
            //sprawdz czy plik istnieje
            if (File.Exists(filepath))
            {
                //wcztaj zawartosc pliku do listy i zwroc ja do aplikacji
                outputList = File.ReadAllLines(filepath).ToList();
                //usun wiersz naglowkowy
                outputList.RemoveAt(0);
            }
            //zwroc liste nawet kiedy plik z danymi do jej wypelnienia nie istnieje
            return outputList;
        }

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
