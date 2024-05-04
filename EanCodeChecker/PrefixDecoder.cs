using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EanCodeChecker
{
    public class PrefixDecoder
    {
        /// <summary>
        /// Okresla nazwe kraju na podstawie prefiksu
        /// </summary>
        /// <param name="prefixes"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodePrefix(List<string> prefixes, string code)
        {
            //sprawdz czy lista z prefiksami zostala utworzona
            if (prefixes == null)
            {
                return "Prefixes are unavailable";
            }
            //iteruj po wierszach listy
            foreach (var prefix in prefixes)
            {
                //rozdziel dane wg separatora
                var vals = prefix.Split(';');
                //sprawdz zakres prefiksu
                if (Int32.Parse(code) >= Int32.Parse(vals[0]) && Int32.Parse(code) <= Int32.Parse(vals[1]))
                {
                    //zwroc nazwe kraju wg prefiksu
                    return vals[2];
                }
            }
            //zwroc jesli prefiks nie istnieje
            return "Prefix unknown";
        }
    }
}
