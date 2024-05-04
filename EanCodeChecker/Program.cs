using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            var codeType = EanCodeType.EAN13;
            string inputChoice = string.Empty;
            //tworzy liste kodow
            List<EanCodeModel> codeModelList = new List<EanCodeModel>();
            //wypelnia liste kodami prefixow
            List<string> prefixes = PrefixLoader.LoadPrefixes("prefixean.csv");

            while ((inputChoice = MenuDisplay.LoadMenu(codeModelList.Count)) != "q")
            {
                switch (inputChoice)
                {
                    case "1":
                        codeModelList.Add(new EanCodeModel { Code = EanCodeLoader.InputCode(codeModelList.Count)});
                        Console.Clear();
                        break;
                    case "2":
                        EanCodeLoader.GenerateListOfRandomCodes(10, codeType).ForEach(x => { codeModelList.Add(x); });
                        Console.Clear();
                        break;
                    case "3":
                        EanCodeLoader.LoadCodesFromFile("codes.csv").ForEach(x => { codeModelList.Add(new EanCodeModel { Code = x }); });
                        Console.Clear();
                        break;
                    case "4":
                        EanCodeValidator.FixChecksum(codeModelList);
                        Console.Clear();
                        break;
                    case "5":
                        if (codeModelList.Count > 0)
                        {
                            Console.WriteLine("\nResults:");
                            codeModelList.ForEach(x =>
                            {
                                if (EanCodeValidator.IsCodeValid(x))
                                {
                                    Console.WriteLine($"Code: {x.Code} | Status: Good | " + 
                                        $"Checksum: {EanCodeValidator.CalculateChecksum(x.Code)} | " + 
                                        $"CheckS: {EanCodeValidator.IsChecksumValid(x.Code)} | " + 
                                        $" {PrefixDecoder.DecodePrefix(prefixes, x.Code.Substring(0, 3))}");
                                }
                                else
                                {
                                    Console.WriteLine($"Code: {x.Code} | Status: Bad | ");
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
