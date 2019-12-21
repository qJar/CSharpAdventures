using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumForEanCodeAft
{
    public class CodeModel
    {
        public string Code { get; set; }
        public bool IsLengthValid { get; set; }
        public bool IsCharValid { get; set; }
    }
}