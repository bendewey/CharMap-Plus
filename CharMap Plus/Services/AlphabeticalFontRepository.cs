using System;
using System.Collections.Generic;
using System.Linq;
using CharMap_Plus.Model;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CharMap_Plus.Services
{
    public class AlphabeticalFontRepository :FontRepository
    {
        public List<FontGroup> FontGroupOptions { get; set; }
        public List<FontGroup> FontGroups { get; set; }

        public override List<FontGroup> GetFontGroups()
        {
            return FontGroups;
        }

        public override List<FontGroup> GetFontGroupOptions()
        {
            return FontGroupOptions;
        }

        public override Task LoadAsync()
        {
            var loadTask = base.LoadAsync();

            FontGroups = new List<FontGroup>();
            FontGroupOptions = new List<FontGroup>();

            var group = GetFontGroup("#", f => Regex.IsMatch(f.Name, "$^[A-Z]"));
            FontGroupOptions.Add(group);
            if (group.Count > 0)
            {
                FontGroups.Add(group);
            }
            for (var i = 65; i <= 90; i++)
            {
                var letter = ((char)i).ToString();
                group = GetFontGroup(letter, letter);
                FontGroupOptions.Add(group);
                if (group.Count > 0)
                {
                    FontGroups.Add(group);
                }
            }

            return loadTask;
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
            group.HasFonts = group.Count > 0;
            return group;
        }
    }
}
