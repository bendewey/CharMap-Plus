using CharMap_Plus.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharMap_Plus.Services
{
    public class OnlineAndLocalFontRepository : FontRepository
    {
        public FontGroup InstalledFonts;
        public FontGroup OnlineFonts;

        public override Task RefreshAsync()
        {
            InstalledFonts = null;
            OnlineFonts = null;
            return base.RefreshAsync();
        }

        public override async Task LoadAsync()
        {
            await base.LoadAsync();

            if (InstalledFonts == null)
            {
                var installedFonts = AllFonts;

                InstalledFonts = new FontGroup();
                InstalledFonts.Description = "Installed Locally";
                foreach (var f in installedFonts)
                {
                    f.Type = "Installed";
                    InstalledFonts.Add(f);
                }
            }

            if (OnlineFonts == null)
            {
                OnlineFonts = new FontGroup();
                OnlineFonts.Description = "Online";
                foreach (var f in await GetOnlineFonts())
                {
                    f.Type = "Online";
                    OnlineFonts.Add(f);
                }

                // load online fonts
                foreach (var font in OnlineFonts)
                {
                    var codes = font.CharacterCodes.Select(c => new FontChar()
                    {
                        Name = c.Key,
                        Char = (char)c.Value,
                        Family = font.Name,
                        Size = 48
                    });
                }
            }

            AllFonts = OnlineFonts.Union(InstalledFonts).ToList();
        }

        public Task<List<FontDetail>> GetOnlineFonts()
        {
            return GetFromFile<List<FontDetail>>("ms-appx:///Fonts/onlinefonts.json");
        }

        public override List<FontGroup> GetFontGroups()
        {
            return new List<FontGroup>() {
               OnlineFonts,
               InstalledFonts
           };
        }
    }
}
