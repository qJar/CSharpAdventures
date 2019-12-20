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
            List<string> prefixes = TextFileProcessor.LoadPrefixes("prefixean.csv");

            while ((inputChoice = UIPrinter.LoadMenu(codes.Count, codeType)) != "q")
            {
                switch (inputChoice)
                {
                    case "1":
                        codes.Add(UIPrinter.LoadSubMenuForInputCode(codes.Count));
                        Console.Clear();
                        break;
                    case "2":
                        CodeProcessor.GenerateListOfRandomCodes(10, codeType, false).ForEach(x => { codes.Add(x); });
                        Console.Clear();
                        break;
                    case "3":
                        CodeProcessor.GenerateListOfRandomCodes(10, codeType, true).ForEach(x => { codes.Add(x); });
                        Console.Clear();
                        break;
                    case "4":
                        TextFileProcessor.LoadCodesFromFile("codes.csv").ForEach(x => { codes.Add(x); });
                        Console.Clear();
                        break;
                    case "5":
                        CodeProcessor.FixChecksum(codes, codeType);
                        Console.Clear();
                        break;
                    case "6":
                        if (codes.Count > 0)
                        {
                            Console.WriteLine("\nResults:");
                            codes.ForEach(x =>
                            {
                                if (CodeProcessor.IsCodeValid(x, codeType))
                                {
                                   Console.WriteLine($"Code: {x} is valid | Checksum: " +
                                        $" {CodeProcessor.CalculateChecksum(x, codeType)} | " +
                                        $"{CodeProcessor.DecodePrefix(prefixes,x.Substring(0,3))}");
                                }
                                else
                                {
                                    Console.WriteLine($"Code: {x} is invalid.");
                                }
                            });
                            Console.WriteLine("\n---Press any key---");
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Incorrect command, try again!\n");
                        break;
                }
            }
        }
    }
}
