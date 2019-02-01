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
    public class InputCodeValidator
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
        /// wyliczenie sumy kontrolnej
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public static int CalculateCheckSum (string inputCode, EanCodeType inputCodeLength)
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

        #endregion
    }
}