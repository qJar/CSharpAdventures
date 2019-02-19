using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumForEanCodeAft
{
    /// <summary>
    /// Typ wyliczeniowy kodow EAN z przypisana dlugoscia znakow
    /// </summary>
    public enum EanCodeType
    {
        EAN8 = 8,
        EAN13 = 13
    }

    /// <summary>
    /// Glowna klasa walidujaca podany kod. Sprawdzana jest dlugosc, poprawnosc znakowa. Wyliczana jest suma kontrolna
    /// </summary>
    public class CodeProcessor
    {
        #region Private Methods

        /// <summary>
        /// sprawdzenie poprawnej dlugosci kodu
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="inputCodeLength"></param>
        /// <returns></returns>
        private static bool IsLengthValid(string inputCode, int inputCodeLength) => inputCode.Length == inputCodeLength;

        /// <summary>
        /// sprawdzanie czy kazdy znak jest cyfra
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        private static bool IsCharValid(string inputCode) => double.TryParse(inputCode, out double result);

        #endregion

        #region Public Methods

        /// <summary>
        /// Weryfikuj kod pod wzgledem jego dlugosci i dozwolonych w nim znakow
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="inputCodeType"></param>
        /// <returns></returns>
        public static bool IsCodeValid(string inputCode, EanCodeType inputCodeType)
        {
            return IsCharValid(inputCode) && IsLengthValid(inputCode, (int)inputCodeType);
        }

        /// <summary>
        /// Wyliczenie sumy kontrolnej
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public static int CalculateChecksum(string inputCode, EanCodeType inputCodeLength)
        {
            //suma cyfr z pozycji nieparzystystych
            int sumOfOddNumber = 0;
            //suma cyfr z pozycji parzystystych
            int sumOfEvenNumber = 0;

            for (int i = 0; i < inputCode.Length - 1; i++)
            {
                if (int.TryParse(inputCode[i].ToString(), out int result))
                {
                    if (((i+1) % 2) == 0)
                    {
                        sumOfEvenNumber += result;
                    }
                    else
                    {
                        sumOfOddNumber += result;
                    }
                }
            }
            //wyliczenie sumy kontrolnej dla odp. kodu EAN.
            switch (inputCodeLength)
            {
                case EanCodeType.EAN8:
                    return ((10 - (sumOfOddNumber * 3 + sumOfEvenNumber) % 10)) == 10 ? 0 : 10 - ((sumOfOddNumber * 3 + sumOfEvenNumber) % 10);
                case EanCodeType.EAN13:
                    return ((10 - (sumOfOddNumber + sumOfEvenNumber * 3) % 10)) == 10 ? 0 : 10 - ((sumOfOddNumber + sumOfEvenNumber * 3) % 10);
                default: throw new Exception("There is no such of code type!");
            }
        }

        /// <summary>
        /// Generuje liste losowych kodow
        /// </summary>
        /// <param name="howManyCodes"></param>
        /// <param name="codeType"></param>
        /// <param name="withProperChecksum"></param>
        /// <returns></returns>
        public static List<string> GenerateListOfRandomCodes(int howManyCodes, EanCodeType codeType, bool withProperChecksum)
        {
            //zwracana lista
            List<string> outputCodeList = new List<string>();
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
                //wylicza poprawna sume kontrolna
                if (withProperChecksum)
                {
                    sb[((int)codeType)-1] = CalculateChecksum(sb.ToString(), codeType).ToString()[0];
                }
                //dodaje utworzony kod do listy
                outputCodeList.Add(sb.ToString());
            }
            //zwraca liste kodow
            return outputCodeList;
        }

        /// <summary>
        /// Naprawia kod numeryczny modyfikujac niepoprawna sume kontrolna
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="codeType"></param>
        public static void FixChecksum(List<string> codes, EanCodeType codeType)
        {
            if (codes.Any())
            {
                for (int i = 0; i < codes.Count; i++)
                {
                    if (IsCodeValid(codes[i], codeType))
                    {
                        StringBuilder sb = new StringBuilder(codes[i]);
                        sb[((int)codeType) - 1] = CalculateChecksum(codes[i], codeType).ToString()[0];
                        codes[i] = sb.ToString();
                    }
                }
            }
        }

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
                if (Int32.Parse(code) >= Int32.Parse(vals[1]) && Int32.Parse(code) <= Int32.Parse(vals[2]))
                {
                    //zwroc nazwe kraju wg prefiksu
                    return vals[0];
                }
            }
            //zwroc jesli prefiks nie istnieje
            return "Prefix unknown";
        }

        #endregion
    }
}