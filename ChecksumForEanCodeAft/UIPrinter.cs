using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumForEanCodeAft
{
    public class UIPrinter
    {
        /// <summary>
        /// Wyswietla glowne menu wraz z informacja o liczbie wprowadzonych kodow
        /// </summary>
        /// <param name="howManyCodes"></param>
        /// <returns></returns>
        public static string LoadMenu(int howManyCodes, EanCodeType eanCodeType)
        {
            Console.WriteLine($"Number of {eanCodeType} codes: {howManyCodes}");
            Console.WriteLine("Press '1' Input code");
            Console.WriteLine("Press '2' Generate random codes");
            Console.WriteLine("Press '3' Generate random codes with proper checksum");
            Console.WriteLine("Press '4' Load code list from file");
            Console.WriteLine("Press '5' Fix checksums");
            Console.WriteLine("Press '6' Print results");
            Console.WriteLine("Press 'q' to Exit");
            Console.Write("Choose option: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Wyswietla podmenu do wprowadzania kodow
        /// </summary>
        /// <param name="howManyCodes"></param>
        /// <returns></returns>
        public static string LoadSubMenuForInputCode(int howManyCodes)
        {
            Console.Clear();
            Console.WriteLine($"Code counter: {howManyCodes}");
            Console.Write("Please input numeric code: ");
            return Console.ReadLine();
        }
    }
}