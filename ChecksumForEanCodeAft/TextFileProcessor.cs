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
        /// Wczytuje liste kodow z pliku txt i zwraca ja do aplikacji
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<string> LoadCodes(string filepath)
        {
            //utworz instancje pustej listy
            List<string> outputCodeList = new List<string>();
            //wcztaj zawartosc pliku do listy i zwroc ja do aplikacji
            outputCodeList = File.ReadAllLines(filepath).ToList();
            return outputCodeList;
        }
    }
}
