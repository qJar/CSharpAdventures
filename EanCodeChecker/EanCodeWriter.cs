using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    public class EanCodeWriter
    {
        public static void SaveCodesToFile(string filepath, List<EanCodeModel> eanCodeModelList)
        {
            List<string> eanCodeList = new List<string>();
            eanCodeList.Add("EANCode");
            foreach (var item in eanCodeModelList)
            {
                eanCodeList.Add(item.Code);
            }
            File.WriteAllLines(filepath, eanCodeList);
        }
    }
}