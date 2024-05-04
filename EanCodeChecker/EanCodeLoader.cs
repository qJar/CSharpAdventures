using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    public class EanCodeLoader
    {
        /// <summary>
        /// Generuje liste randowomowych kodow
        /// </summary>
        /// <param name="howManyCodes"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public static List<EanCodeModel> GenerateListOfRandomCodes(int howManyCodes, EanCodeType codeType)
        {
            //tworzy instancje zwracanej listy
            List<EanCodeModel> outputCodeModelList = new List<EanCodeModel>();
            //inicjalizacja generatora liczb losowych
            Random rnd = new Random();

            //generuje zadana ilosc kodow
            for (int j = 0; j < howManyCodes; j++)
            {
                StringBuilder sb = new StringBuilder();
                //generuje kod o zadanej liczbie znakow
                for (int i = 0; i < (int)codeType; i++)
                {
                    sb.Append(rnd.Next(0, 9));

                }
                //tworzy instancje modelu kodu
                EanCodeModel codeModel = new EanCodeModel
                {
                    //wypelnia wyszystkie wlasciwosci obiektu
                    Code = sb.ToString()
                };
                //dodaje obiekt do listy
                outputCodeModelList.Add(codeModel);
            }
            //zwraca liste kodow
            return outputCodeModelList;
        }

        /// <summary>
        /// Reczne wstawianie kodow
        /// </summary>
        /// <param name="howManyCodes"></param>
        /// <returns></returns>
        public static string InputCode(int howManyCodes)
        {
            Console.Clear();
            Console.WriteLine($"Code counter: {howManyCodes}");
            Console.Write("Please input numeric code: ");
            return Console.ReadLine();
        }

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
    }
}
