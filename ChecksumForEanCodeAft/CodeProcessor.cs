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
        /// Weryfikuj kod pod wzgledem jego dlugosci i dozwolonych w nim znakow i okresl poprawnosc kodu
        /// </summary>
        /// <param name="inputCodeModel"></param>
        /// <param name="inputCodeType"></param>
        /// <returns></returns>
        public static bool IsCodeValid(CodeModel inputCodeModel, EanCodeType inputCodeType)
        {
            //check if code length is valid and set property
            inputCodeModel.IsLengthValid = IsLengthValid(inputCodeModel.Code, (int)inputCodeType);
            //check if code chars are valid and set property
            inputCodeModel.IsCharValid = IsCharValid(inputCodeModel.Code);
            //return main state of validation
            return inputCodeModel.IsCharValid && inputCodeModel.IsLengthValid;
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
            //wyliczona wartosc dla sumy kontrolnej
            int checkSumForEanCode = 0;

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
                    checkSumForEanCode = ((10 - (sumOfOddNumber * 3 + sumOfEvenNumber) % 10));
                    return checkSumForEanCode == 10 ? 0 : checkSumForEanCode;
                case EanCodeType.EAN13:
                    checkSumForEanCode = ((10 - (sumOfOddNumber + sumOfEvenNumber * 3) % 10));
                    return checkSumForEanCode == 10 ? 0 : checkSumForEanCode;
                default: throw new Exception("There is no such of code type!");
            }
        }

        /// <summary>
        /// Generuje liste losowych kodow
        /// </summary>
        /// <param name="howManyCodes"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public static List<CodeModel> GenerateListOfRandomCodes(int howManyCodes, EanCodeType codeType)
        {
            //tworzy instancje zwracanej listy
            List<CodeModel> outputCodeModelList = new List<CodeModel>();
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
                CodeModel codeModel = new CodeModel
                {
                    //wypelnia wyszystkie wlasciwosci obiektu
                    Code = sb.ToString(), IsLengthValid = true, IsCharValid = true
                };
                //dodaje obiekt do listy
                outputCodeModelList.Add(codeModel);
            }
            //zwraca liste kodow
            return outputCodeModelList;
        }

        /// <summary>
        /// Naprawia kod numeryczny modyfikujac niepoprawna sume kontrolna
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="codeType"></param>
        public static void FixChecksum(List<CodeModel> codeModelList, EanCodeType codeType)
        {
            if (codeModelList.Any())
            {
                for (int i = 0; i < codeModelList.Count; i++)
                {
                    if (IsCodeValid(codeModelList[i], codeType))
                    {
                        StringBuilder sb = new StringBuilder(codeModelList[i].Code);
                        sb[((int)codeType) - 1] = CalculateChecksum(codeModelList[i].Code, codeType).ToString()[0];
                        codeModelList[i].Code = sb.ToString();
                        codeModelList[i].IsLengthValid = true;
                        codeModelList[i].IsCharValid = true;
                    }
                }
            }
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