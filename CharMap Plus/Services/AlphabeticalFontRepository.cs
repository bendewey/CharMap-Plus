using System;
using System.Collections.Generic;
using System.Linq;
using CharMap_Plus.Model;
using System.Text.RegularExpressions;

namespace CharMap_Plus.Services
{
    public class AlphabeticalFontRepository :FontRepository
    {
        public override List<FontGroup> GetFontGroups()
        {
            var groups = new List<FontGroup>();
            var group = GetFontGroup("#", f => Regex.IsMatch(f.Name, "$^[A-Z]"));
            if (group.Count > 0)
            {
                groups.Add(group);
            }
            for(var i = 65; i <= 90; i++)
            {
                var letter = ((char)i).ToString();
                group = GetFontGroup(letter, letter);
                if (group.Count > 0)
                {
                    groups.Add(group);
                }
            }

            return groups;
        }

        private FontGroup GetFontGroup(string name, string firstChar)
        {
            return GetFontGroup(name, f => f.Name.ToUpper().StartsWith(firstChar));
        }
            
        private FontGroup GetFontGroup(string name, Func<FontDetail, bool> predicate)
        {
            var group = new FontGroup()
            {
                Description = name
            };
            foreach(var f in AllFonts.Where(predicate))
            {
                group.Add(f);
            }
            return group;
        }
    }
}
