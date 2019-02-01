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
            var codeType = EanCodeType.EAN13;
            string inputChoice = string.Empty;
            List<string> codes = new List<string>();

            while ((inputChoice = UIPrinter.LoadMenu(codes.Count, codeType)) != "q")
            {
                switch (inputChoice)
                {
                    case "1":
                        codes.Add(UIPrinter.LoadSubMenuForInputCode(codes.Count));
                        Console.Clear();
                        break;
                    case "2":
                        //
                        break;
                    case "3":
                        if (codes.Count > 0)
                        {
                            Console.WriteLine("\nResults:");
                            codes.ForEach(x =>
                            {
                                if (InputCodeValidator.IsCodeValid(x, codeType))
                                {
                                   Console.WriteLine($"Code: {x} is valid | Checksum: " +
                                        $" {InputCodeValidator.CalculateCheckSum(x, codeType)}");
                                }
                                else
                                {
                                    Console.WriteLine($"Code: {x} is invalid.");
                                }
                            });
                            Console.WriteLine("\n---Press any key---");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                    default:
                        Console.WriteLine("Incorrect command, try again!\n");
                        break;
                }
            }
        }
    }
}
