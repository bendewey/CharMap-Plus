using CharMap_Plus.Model;
using System.Collections.ObjectModel;
using System.Linq;
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

        public FontChar _selectedChar;
        public FontChar SelectedChar
        {
            get { return _selectedChar; }
            set { Set(ref _selectedChar, value); }
        }

        public ObservableCollection<string> AllFonts { get; set; }
        public ObservableCollection<FontChar> Chars { get; set; }

        public DetailsPageViewModel()
        {
            Chars = new ObservableCollection<FontChar> ();
        }

        public Task Load(string parameter)
        {
            Log("Loading Font Details Page for" + parameter);
            AllFonts = new ObservableCollection<string>(App.Repository.AllFonts.Select(f => f.Name));
            SelectedFontFamily = parameter;
            Chars.Clear();

            var fontMatch = App.Repository.GetFont(SelectedFontFamily);
            if (fontMatch.Type == "Online")
            {
                SelectedFontFamily = fontMatch.Family;   
            }

            if (fontMatch.FontChars != null)
            {
                foreach (var c in fontMatch.FontChars)
                {
                    Chars.Add(c);
                }
            }
            return Task.FromResult<object>(null);
        }

    }
}
