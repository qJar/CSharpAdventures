using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    /// <summary>Typ wyliczeniowy kodow EAN z przypisana dlugoscia znakow
    /// </summary>
    public enum EanCodeType
    {
        EAN1 = 0,
        EAN8 = 8,
        EAN13 = 13
    }

    public class EanCodeValidator
    {
        #region Private Methods

       /// <summary>
        /// sprawdzanie czy kazdy znak jest cyfra
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        private static bool IsCharValid(string inputCode) => double.TryParse(inputCode, out double result);

        /// <summary>
        /// sprawdza czy znany jest dany kod
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        private static bool IsTypeEanCodeKnown(string inputCode)
        {
            switch ((GetEanCodeType(inputCode)))
            {
                case EanCodeType.EAN8: return true;
                case EanCodeType.EAN13:return true;
                default: return false;
            }
        }

        /// <summary>
        /// Okresla typ kodu EAN
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        private static Enum GetEanCodeType(string inputCode) 
        {
            if (inputCode.Length == 8)
            {
                return EanCodeType.EAN8;
            }
            if (inputCode.Length == 13)
            {
                return EanCodeType.EAN13;
            }
            return EanCodeType.EAN1;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Weryfikuj kod pod wzgledem poprawnosci typu i dozwolonych w nim znakow i okresl poprawnosc kodu
        /// </summary>
        /// <param name="inputCodeModel"></param>
        /// <param name="inputCodeType"></param>
        /// <returns></returns>
        public static bool IsCodeValid(EanCodeModel inputCodeModel)
        {
           return IsTypeEanCodeKnown(inputCodeModel.Code) && IsCharValid(inputCodeModel.Code);
        }

        /// <summary>
        /// sprawdza poprawnosc sumy kontrolnej
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public static bool IsChecksumValid(string inputCode)
        {
            return inputCode[inputCode.Length - 1] - '0' == CalculateChecksum(inputCode);
        }

        /// <summary>
        /// Wyliczenie sumy kontrolnej
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public static int CalculateChecksum(string inputCode)
        {
            //suma cyfr z pozycji nieparzystystych
            int sumOfOddNumber = 0;
            //suma cyfr z pozycji parzystystych
            int sumOfEvenNumber = 0;
            //wyliczona wartosc dla sumy kontrolnej
            int checkSumForEanCode = 0;

            for (int i = 0; i < inputCode.Length - 1; i++)
            {
                if (int.TryParse(inputCode[i].ToString(), out int result))
                {
                    if (((i + 1) % 2) == 0)
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
            switch ((GetEanCodeType(inputCode)))
            {
                case EanCodeType.EAN8:
                    checkSumForEanCode = ((10 - (sumOfOddNumber * 3 + sumOfEvenNumber) % 10));
                    return checkSumForEanCode == 10 ? 0 : checkSumForEanCode;
                case EanCodeType.EAN13:
                    checkSumForEanCode = ((10 - (sumOfOddNumber + sumOfEvenNumber * 3) % 10));
                    return checkSumForEanCode == 10 ? 0 : checkSumForEanCode;
                default: throw new Exception("There is no such of code type!");
            }
        }

        /// <summary>
        /// Naprawia kod numeryczny modyfikujac niepoprawna sume kontrolna
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="codeType"></param>
        public static void FixChecksum(List<EanCodeModel> codeModelList)
        {
            if (codeModelList.Any())
            {
                foreach (var eanCode in codeModelList)
                {
                    if (IsCodeValid(eanCode))
                    {
                        StringBuilder sb = new StringBuilder(eanCode.Code);
                        sb[eanCode.Code.Length - 1] = CalculateChecksum(eanCode.Code).ToString()[0];
                        eanCode.Code = sb.ToString();
                    }
                }
            }
        }

        #endregion

    }
}
