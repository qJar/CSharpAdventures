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
            //var codeTypeEan13 = EanCodeType.EAN13;
            //var codeTypeEan8 = EanCodeType.EAN8;
            string inputChoice = string.Empty;
            //tworzy liste kodow
            List<EanCodeModel> eanCodeModelList = new List<EanCodeModel>();
            //wypelnia liste kodami prefixow
            List<string> prefixes = PrefixLoader.LoadPrefixes("prefixean.csv");

            while ((inputChoice = MenuDisplay.LoadMenu(eanCodeModelList.Count)) != "q")
            {
                switch (inputChoice)
                {
                    case "1":
                        eanCodeModelList.Add(new EanCodeModel { Code = EanCodeLoader.InputCode(eanCodeModelList.Count)});
                        Console.Clear();
                        break;
                    case "2":
                        EanCodeLoader.GenerateListOfRandomCodes(10, EanCodeType.EAN13).ForEach(x => { eanCodeModelList.Add(x); });
                        Console.Clear();
                        break;
                    case "3":
                        EanCodeLoader.GenerateListOfRandomCodes(10, EanCodeType.EAN8).ForEach(x => { eanCodeModelList.Add(x); });
                        Console.Clear();
                        break;
                    case "4":
                        EanCodeLoader.LoadCodesFromFile("codes.csv").ForEach(x => { eanCodeModelList.Add(new EanCodeModel { Code = x }); });
                        Console.Clear();
                        break;
                    case "5":
                        EanCodeLoader.LoadCodesFromFile("codeSave.csv").ForEach(x => { eanCodeModelList.Add(new EanCodeModel { Code = x }); });
                        Console.Clear();
                        break;
                    case "6":
                        EanCodeValidator.FixChecksum(eanCodeModelList);
                        Console.Clear();
                        break;
                    case "7":
                        if (eanCodeModelList.Count > 0)
                        {
                            Console.WriteLine("\nResults:");
                            eanCodeModelList.ForEach(x =>
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
                    case "8":
                        if (eanCodeModelList.Count > 0)
                        {
                            EanCodeWriter.SaveCodesToFile("codeSave.csv", eanCodeModelList);
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
