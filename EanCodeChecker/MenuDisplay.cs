using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    public class MenuDisplay
    {
        public static string LoadMenu(int howManyCodes, EanCodeType eanCodeType)
        {
            Console.WriteLine($"Number of {eanCodeType} codes: {howManyCodes}");
            Console.WriteLine("Press '1' Input code");
            Console.WriteLine("Press '2' Generate list of random codes");
            //Console.WriteLine("Press '3' Load code list from file");
            Console.WriteLine("Press '4' Fix checksums");
            Console.WriteLine("Press '5' Print results");
            Console.WriteLine("Press 'q' to Exit");
            Console.Write("Choose option: ");
            return Console.ReadLine();
        }

        public static string LoadSubMenuForInputCode(int howManyCodes)
        {
            Console.Clear();
            Console.WriteLine($"Code counter: {howManyCodes}");
            Console.Write("Please input numeric code: ");
            return Console.ReadLine();
        }
    }
}
