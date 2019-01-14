using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumForEanCodeAft
{
    /// <summary>
    /// Typ wyliczeniowy kodow EAN ze wzgledu na ich dlugosc znakowa
    /// </summary>
    public enum EanCodeTypeLength
    {
        Ean8 = 8,
        Ean13 = 13
    }

    /// <summary>
    /// Glowna klasa walidujaca podany kod. Sprawdzana jest dlugosc, poprawnosc znakowa. Wyliczana jest suma kontrolna
    /// </summary>
    public class InputCodeValidator
    {
        #region Private Methods

        /// <summary>
        /// sprawdzenie poprawnej dlugosci kodu
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="inputCodeLength"></param>
        /// <returns></returns>
        private static bool IsLengthValid(string inputCode, int inputCodeLength)
        {
            return inputCode.Length == inputCodeLength;
        }

        /// <summary>
        /// sprawdzanie czy kazdy znak jest cyfra
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        private static bool IsCharValid(string inputCode)
        {
            foreach (char c in inputCode)
            {
                //weryfikacja każdego znaku jako cyfry
                if (!int.TryParse(c.ToString(), out int result))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// sprawdzanie ogolnej poprawnosci wprowadzonego kodu (dlugosc, dopuszczalne znaki)
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="inputCodeLength"></param>
        /// <returns></returns>
        public static bool IsCodeValid(string inputCode, EanCodeTypeLength inputCodeLength)
        {
            return IsLengthValid(inputCode, (int)inputCodeLength) && IsCharValid(inputCode);
        }

        /// <summary>
        /// wyliczenie sumy kontrolnej
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public static int CalculateCheckSum (string inputCode)
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
            return (sumOfOddNumber + sumOfEvenNumber * 3) % 10 == 10 ? 0 : 10 - ((sumOfOddNumber + sumOfEvenNumber * 3) % 10) ;
        }

        #endregion
    }
}