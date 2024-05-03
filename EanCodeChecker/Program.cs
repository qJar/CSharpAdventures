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
            List<CodeModel> codeModelList = new List<CodeModel>();
            //wypelnia liste kodami prefixow
            //List<string> prefixes = TextFileProcessor.LoadPrefixes("prefixean.csv");

            while ((inputChoice = MenuDisplay.LoadMenu(codeModelList.Count, codeType)) != "q")
            {
                switch (inputChoice)
                {
                    case "1":

                        codeModelList.Add(new CodeModel
                        {
                            Code = MenuDisplay.LoadSubMenuForInputCode(codeModelList.Count), CodeType = (int)codeType
                        });
                        Console.Clear();
                        break;
                    case "2":
                        //CodeProcessor.GenerateListOfRandomCodes(10, codeType).ForEach(x => { codeModelList.Add(x); });
                        Console.Clear();
                        break;
                    case "3":
                        //TextFileProcessor.LoadCodesFromFile("codes.csv").ForEach(x => {
                        //    codeModelList.Add(new CodeModel { Code = x, IsLengthValid = false, IsCharValid = false });
                        //});
                        Console.Clear();
                        break;
                    case "4":
                        //CodeProcessor.FixChecksum(codeModelList, codeType);
                        Console.Clear();
                        break;
                    case "5":
                        if (codeModelList.Count > 0)
                        {
                            Console.WriteLine("\nResults:");
                            //codeModelList.ForEach(x =>
                            //{
                            //    if (CodeProcessor.IsCodeValid(x, codeType))
                            //    {
                            //        Console.WriteLine($"Code: {x.Code} | " +
                            //            $"Length: {UIPrinter.LabelForValidation(x.IsLengthValid)} | " +
                            //            $"Structure: {UIPrinter.LabelForValidation(x.IsCharValid)} | " +
                            //            $"Checksum: {CodeProcessor.CalculateChecksum(x.Code, codeType)} | " +
                            //            $" {CodeProcessor.DecodePrefix(prefixes, x.Code.Substring(0, 3))}");
                            //    }
                            //    else
                            //    {
                            //        Console.WriteLine($"Code: {x.Code} | " +
                            //            $"Length: {UIPrinter.LabelForValidation(x.IsLengthValid)} | " +
                            //            $"Structure: {UIPrinter.LabelForValidation(x.IsCharValid)} | ");
                            //    }
                            //});
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
