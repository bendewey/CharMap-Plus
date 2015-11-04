using CharMap_Plus.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharMap_Plus.ViewModels
{
    public class DetailsPageViewModel : BaseViewModel
    {
        public string _selectedFontFamily;
        public string SelectedFontFamily
        {
            get { return _selectedFontFamily; }
            set { Set(ref _selectedFontFamily, value); }
        }

        public ObservableCollection<string> AllFonts { get; set; }
        public IncrementalSource<PagedSource<FontChar>, FontChar> Chars { get; set; }
        public ObservableCollection<FontChar> Chars2 { get; set; }

        public DetailsPageViewModel()
        {
            Chars = new IncrementalSource<PagedSource<FontChar>, FontChar>();
            Chars2 = new ObservableCollection<FontChar> ();
        }

        public Task Load(string parameter)
        {
            Debug.WriteLine("Loading Font " + parameter);
            AllFonts = new ObservableCollection<string>(App.Repository.AllFonts.Select(f => f.Name));
            SelectedFontFamily = parameter;
            Chars.Clear();
            Chars2.Clear();

            var fontMatch = App.Repository.GetFont(SelectedFontFamily);
            if (fontMatch.Type == "Online")
            {
                SelectedFontFamily = fontMatch.Family;
                if (fontMatch.CharacterCodes != null)
                {
                    var codes = fontMatch.CharacterCodes.Select(c => new FontChar()
                    {
                        Name = c.Key,
                        Char = (char)c.Value,
                        Family = SelectedFontFamily,
                        Size = 48
                    });
                    Chars.Source = new PagedSource<FontChar>(codes);
                    foreach(var c in codes)
                    {
                        Chars2.Add(c);
                    }
                }
            }
            else
            {
                //    var enumer = new FontEnumeration.FontEnumerator();
                //    var codes = enumer.ListSupportedChars(SelectedFontFamily).Where(c => c > 0 && c != 10 && c != 13 && c != 20).Select(c => new FontChar()
                //    {
                //        Name = App.Repository.GetCharName(c),
                //        Char = (char)c,
                //        Family = SelectedFontFamily,
                //        Size = 38
                //    });
                //Chars.Source = new PagedSource<FontChar>(codes);
                foreach (var c in fontMatch.FontChars)
                {
                    Chars2.Add(c);
                }
            }
            return Task.FromResult<object>(null);
        }

    }
}
