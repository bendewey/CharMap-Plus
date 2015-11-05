using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharMap_Plus.Model
{
    public class FontGroup : List<FontDetail>
    {
        public string Description { get; set;}

        public bool HasFonts { get; set; }

        public override bool Equals(object obj)
        {
            var g = obj as FontGroup;
            if (g == null)
            {
                return false;
            } 
            if (Description == null)
            {
                if (g.Description == null)
                {
                    return true;
                }
                return false;
            }
            return Description.Equals(g.Description);
        }

        public override int GetHashCode()
        {
            return (Description ?? string.Empty).GetHashCode();
        }

    }
}
