using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using static System.Net.Mime.MediaTypeNames;

namespace EanCodeChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputChoice = string.Empty;
            //tworzy liste kodow
            List<EanCodeModel> eanCodeModelList = new List<EanCodeModel>();
            //wypelnia liste kodami prefixow
            List<string> prefixes = PrefixModule.LoadPrefixes("prefixean.csv");
            //string path = Directory.GetCurrentDirectory();
            string[] fileNameArray = Directory.GetFiles(@".\", "codes*.csv");
            
            while ((inputChoice = UIManager.LoadMenu(eanCodeModelList.Count)) != "q")
            {
                switch (inputChoice)
                {
                    case "1":
                        eanCodeModelList.Add(EanCodeLoader.CreateEanCodeModel(UIManager.LoadMenuInputCode(eanCodeModelList.Count)));
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
                        EanCodeLoader.LoadCodesFromFile(@".\codes.csv").ForEach(x => { eanCodeModelList.Add(new EanCodeModel { Code = x }); });
                        Console.Clear();
                        break;
                    case "5":
                        EanCodeLoader.LoadCodesFromFile(@".\codeSave.csv").ForEach(x => { eanCodeModelList.Add(new EanCodeModel { Code = x }); });
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
                                    UIManager.PrintResult(x.Code, EanCodeValidator.CalculateChecksum(x.Code),
                                        EanCodeValidator.IsChecksumValid(x.Code), PrefixModule.DecodePrefix(prefixes, x.Code.Substring(0, 3)));
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
                            EanCodeWriter.SaveCodesToFile(@".\codeSave.csv", eanCodeModelList);
                        }
                        Console.Clear();
                        break;
                    case "9":
                        if (fileNameArray.Any())
                        {
                            UIManager.LoadMenuListingFiles(Helper.GetValidFileNameArray(fileNameArray), eanCodeModelList.Count);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("No files...");
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
