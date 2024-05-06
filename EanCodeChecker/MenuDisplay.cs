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
            Console.WriteLine("Press '4' Load code list from file (codes.csv)");
            Console.WriteLine("Press '5' Load code list from file (codeSave.csv)");
            Console.WriteLine("Press '6' Fix checksums");
            Console.WriteLine("Press '7' Print results");
            Console.WriteLine("Press '8' Save to file");
            Console.WriteLine("Press '9' Listing CSV files");
            Console.WriteLine("Press 'q' to Exit");
            Console.Write("Choose option: ");
            return Console.ReadLine();
        }

        public static string LoadMenuInputCode(int howManyCodes)
        {
            Console.Clear();
            Console.WriteLine($"Code counter: {howManyCodes}");
            Console.Write("Please input numeric code: ");
            return Console.ReadLine();
        }

        public static string LoadMenuListingFiles(string[] fileNameArray)
        {
            Console.Clear();
            int i = 0;
            char[] charsToTrim = { '.', '\\' };
            foreach (string fileName in fileNameArray)
            {
                i++;
                Console.WriteLine($"{i} - " + fileName.Trim(charsToTrim));
            }
            Console.WriteLine("0 - Exit");
            Console.Write("\nChoose option: ");
            return Console.ReadLine();
        }
    }
}
