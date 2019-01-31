using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumForEanCode
{
    public class EanCodeScanner
    {
        #region Private Fields

        private int _scannedCheckSumValue;
        private int _calculatedCheckSumValue;
        private string _eanNumericCode;
        private string _eanNumericCodeWithOddIndex;
        private string _eanNumericCodeWithEvenIndex;

        #endregion

        #region Properties
        public int CalculatedCheckSumValue
        {
            get { return _calculatedCheckSumValue; }
        }

        public int ScannedCheckSumValue
        {
            get { return _scannedCheckSumValue; }
        }
        #endregion

        #region Constructor
        public EanCodeScanner(string numericCode)
        {
            _eanNumericCode = numericCode;
            //pozyskanie cyfry z pozycji 13, która weryfikuje wyliczaną sumę kontrolną
            _scannedCheckSumValue = int.Parse((_eanNumericCode[12]).ToString());

            //implementacja alorytmu wyliczenia cyfry kontrolnej
            ProcessingWithScan();
        }
        #endregion

        #region Private Methods

        private string GetStringWithOddIndexes(string numericCode)
        {
            StringBuilder sb = new StringBuilder();
            //iteracja po łańuchu znaków
            for (int i = 0; i < numericCode.Length-1; i++)
            {
                //tworzenie łańcucha znaków ze znaków na pozycjach nieparzystych
                sb.Append(numericCode[i]);
                i++;
            }
            return sb.ToString();
        }

        private string GetStringWithEvenIndexes(string numericCode)
        {
            StringBuilder sb = new StringBuilder();
            //iteracja po łańuchu znaków
            for (int i = 1; i < numericCode.Length-1; i++)
            {
                //tworzenie łańcucha znaków ze znaków na pozycjach parzystych
                sb.Append(numericCode[i]);
                i++;
            }
            return sb.ToString();

        }

        private int SumOfConvertedStringToIntValue(string numericCode)
        {
            int sumOfConvertedValue = 0;
            //iteracja po każdej pozycji w łańcuchu znakowym
            foreach (var item in numericCode)
            {
                //konwersja pojedynczego znaku na cyfrę i zsumowanie tych cyfr.
                sumOfConvertedValue += int.Parse(item.ToString());
            }
            return sumOfConvertedValue;
        }

        private void ProcessingWithScan()
        {
            //Utworzenie łańcucha znaków z pozycji o indeksach nieparzystych
            _eanNumericCodeWithOddIndex = GetStringWithOddIndexes(_eanNumericCode);

            //Utworzenie łańcucha znaków z pozycji o indeksach parzystych
            _eanNumericCodeWithEvenIndex = GetStringWithEvenIndexes(_eanNumericCode);

           //Oblicznie sumy z pozycji o indeksach nieparzytych
            int sumValuesOnOddPosition = SumOfConvertedStringToIntValue(_eanNumericCodeWithOddIndex);
            
            //Oblicznie sumy z pozycji o indeksach parzytych
            int sumValuesOnEvenPosition = SumOfConvertedStringToIntValue(_eanNumericCodeWithEvenIndex) * 3;
            
            //Wylicznie sumy kontrolnej z cyfr na pozycjach 1-12
            _calculatedCheckSumValue = ((10 - (sumValuesOnOddPosition + sumValuesOnEvenPosition) % 10)) == 10 ? 0 :
                10 - ((sumValuesOnOddPosition + sumValuesOnEvenPosition) % 10);
        }

        #endregion
    }
}
