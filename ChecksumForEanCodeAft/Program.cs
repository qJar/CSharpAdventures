using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumForEanCodeAft
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Please input numeric code: ");
            string inputData = Console.ReadLine();
            var codeType = EanCodeType.Ean8;
            
            //Sprawdz poprawnosc kodu tj. jego dlugosc i dopuszczalne znaki 
            if (InputCodeValidator.CheckCodeValid(inputData, codeType))
            {
                //Wylicz sume kontrolna
                Console.WriteLine($"Checksum: {InputCodeValidator.CalculateCheckSum(inputData, codeType)}");
            }
            else
            {
                Console.WriteLine("Invalid code!");
            }
            
            Console.ReadLine();
        }
    }
}
