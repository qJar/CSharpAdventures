using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumForEanCode
{
    public class EanCodeVerificator
    {
        #region Public Method

        public static bool IsLengthNumericCodeCorrect(string numericCode, int numericCodeLength)
        {
            return numericCode.Length == numericCodeLength;
        }

        public static bool IsCharInNumericCodeCorrect(string numericCode)
        {
            int result;
            //iteracja po łańcuchu znaków
            foreach (char c in numericCode)
            {
                //weryfikacja każdego znaku jako znaku cyfry
                if (!int.TryParse(c.ToString(), out result))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
