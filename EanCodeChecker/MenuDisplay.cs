using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    public class MenuDisplay
    {
        public static string LoadMenu(int howManyCodes)
        {
            Console.WriteLine($"Number of EANCODES: {howManyCodes}");
            Console.WriteLine("Press '1' Input code");
            Console.WriteLine("Press '2' Generate list of random codes (EAN13)");
            Console.WriteLine("Press '3' Generate list of random codes (EAN8)");
            Console.WriteLine("Press '4' Load code list from file");
            Console.WriteLine("Press '5' Fix checksums");
            Console.WriteLine("Press '6' Print results");
            Console.WriteLine("Press '7' Save to file");
            Console.WriteLine("Press 'q' to Exit");
            Console.Write("Choose option: ");
            return Console.ReadLine();
        }
    }
}
