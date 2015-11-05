using CharMap_Plus.Model;
using CharMap_Plus.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace CharMap_Plus.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public Frame Frame;

        private ObservableCollection<FontGroup> _fontGroups;
        public ObservableCollection<FontGroup> FontGroups
        {
            get { return _fontGroups; }
            set { Set(ref _fontGroups, value); }
        }

        private ObservableCollection<FontGroup> _fontGroupOptions;
        public ObservableCollection<FontGroup> FontGroupOptions
        {
            get { return _fontGroupOptions; }
            set { Set(ref _fontGroupOptions, value); }
        }

        public HomePageViewModel(Frame frame)
        {
            Frame = frame;
            
            FontGroups = new ObservableCollection<FontGroup>();
            FontGroupOptions = new ObservableCollection<FontGroup>();
        }

        public async Task Load()
        {
            if (FontGroups.Count == 0)
            {
                await App.Repository.LoadAsync();
                Log("Repository Loaded");
                AddFontsFromRepository();
            }   
        }

        public async Task Refresh()
        {
            FontGroups.Clear();
            FontGroupOptions.Clear();
            await App.Repository.RefreshAsync();
            AddFontsFromRepository();
        }

        public void AddFontsFromRepository()
        {
            foreach (var g in App.Repository.GetFontGroups())
            {
                FontGroups.Add(g);
            }
            foreach (var g in App.Repository.GetFontGroupOptions())
            {
                FontGroupOptions.Add(g);
            }
        }

        public void FontClicked(object sender, ItemClickEventArgs e)
        {
            var selectedFont = e.ClickedItem as FontDetail;
            Frame.Navigate(typeof(DetailsPage), selectedFont.Name, new DrillInNavigationTransitionInfo());
        }
    }
}
