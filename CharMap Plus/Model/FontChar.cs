using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharMap_Plus.Model
{
    public class FontChar
    {

        public string Name { get; set; }
        public char Char { get; set; }

        public string CharCode { get { return ((int)Char).ToString("X").PadLeft(4, '0'); } }
        public string DecCharCode { get { return ((int)Char).ToString().PadLeft(4, '0'); } }

        public string XamlHexCode { get { return "&#x" + ((int)Char).ToString("X").PadLeft(4, '0') + ";"; } }
        public string XamlDecCode { get { return "&#" + ((int)Char).ToString().PadLeft(4, '0') + ";"; } }
        

        public string Family { get; set; }
        public int Size { get; set; }
    }
}
