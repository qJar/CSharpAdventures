using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    public class Helper
    {
        public static string[] GetValidFileNameArray(string[] fileNameArray)
        {
            int i = 0;
            char[] charsToTrim = { '.', '\\' };
            foreach (string fileName in fileNameArray)
            {
                string fileNameValid = fileName.Trim(charsToTrim);
                fileNameArray[i] = fileNameValid;
                i++;
            }
            return fileNameArray;
        }
    }
}
