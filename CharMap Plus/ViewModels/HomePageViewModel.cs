using CharMap_Plus.Model;
using CharMap_Plus.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        public HomePageViewModel(Frame frame)
        {
            Frame = frame;

            //var g = new FontGroup()
            //{
            //    Description = "Test",
            //};
            //g.Add(new FontDetail() { Name = "Arial" });

            FontGroups = new ObservableCollection<FontGroup>();
            //{
            //g
            //};

        }

        public async Task Load()
        {
            FontGroups.Clear();
            await App.Repository.LoadAsync();
            foreach(var g in App.Repository.GetFontGroups())
            {
                FontGroups.Add(g);
            }
        }

        public void FontClicked(object sender, ItemClickEventArgs e)
        {
            var selectedFont = e.ClickedItem as FontDetail;
            Frame.Navigate(typeof(DetailsPage), selectedFont.Name, new DrillInNavigationTransitionInfo());
        }
    }
}
