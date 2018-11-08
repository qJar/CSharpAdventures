using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumForEanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please input numeric code: ");
            string numericCode = Console.ReadLine();

            //weryfikacja ilości znaków w kodzie
            bool isCorrectLength = EanCodeVerificator.IsLengthNumericCodeCorrect(numericCode, 13);
            //weryfikacja poszczególnych znakow w kodzie
            bool isCorrectChar = EanCodeVerificator.IsCharInNumericCodeCorrect(numericCode);

            //alerty dodane czysto informacyjnie, opcjonalne
            string numericCodeLengthInfo = isCorrectLength ? "CORRECT" : "INCORRECT";
            string numericCodeCharInfo = isCorrectChar ? "CORRECT" : "INCORRECT";

            //sprawdzenie czy warunki poprawnego kodu są spełnione
            if (isCorrectLength && isCorrectChar)
            {
                Console.WriteLine($"Checking lenght numeric code: {numericCodeLengthInfo}");
                Console.WriteLine($"Checking correct char in numeric code: {numericCodeCharInfo}");
                
                //zanalizuj kod numeryczny w skanerze
                EanCodeScanner codeScanner = new EanCodeScanner(numericCode);
                //wyświetlenie komunikatu o wartości sumy kontrolnej i jej porawności.
                Console.Write($"Calculated checksum of EAN code is {codeScanner.CalculatedCheckSumValue}. ");
                string alert = (codeScanner.ScannedCheckSumValue == codeScanner.CalculatedCheckSumValue) ? "Correct!" : "Incorrect";
                Console.WriteLine(alert);
            }
            else
            {
                Console.WriteLine($"Checking lenght passcode: {numericCodeLengthInfo}");
                Console.WriteLine($"Checking correct char in passcode: {numericCodeCharInfo}");
            }
            Console.ReadLine();
        }
    }
}
