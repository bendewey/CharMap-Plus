using CharMap_Plus.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CharMap_Plus.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsPage : Page
    {
        public string SelectedFontFamily { get; set; }

        public ObservableCollection<string> AllFonts { get; set; }
        public IncrementalSource<PagedSource<FontChar>, FontChar> Chars { get; set; }


        public DetailsPage()
        {
            this.InitializeComponent();
        }

        private void SetupTitleBarBackButton()
        {
            var frame = Window.Current.Content as Frame;
            var showTitleBack = frame.CanGoBack && !ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = showTitleBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetupTitleBarBackButton();

            AllFonts = new ObservableCollection<string>(App.Repository.AllFonts.Select(f => f.Name));
            SelectedFontFamily = e.Parameter as string;

            var fontMatch = App.Repository.GetFont(SelectedFontFamily);
            if (fontMatch.Type == "Online")
            {
                SelectedFontFamily = fontMatch.Family;
                if (fontMatch.CharacterCodes != null)
                {
                    Chars = new IncrementalSource<PagedSource<FontChar>, FontChar>(new PagedSource<FontChar>(fontMatch.CharacterCodes.Select(c => new FontChar()
                    {
                        Char = (char)c.Value,
                        Family = SelectedFontFamily,
                        Size = 48
                    })));
                }
            }
            else
            {
                var enumer = new FontEnumeration.FontEnumerator();
                var codes = enumer.ListSupportedChars(SelectedFontFamily);
                Chars = new IncrementalSource<PagedSource<FontChar>, FontChar>(new PagedSource<FontChar>(codes.Where(c => c > 0 && c != 10 && c != 13 && c != 20).Select(c => new FontChar()
                {
                    Char = (char)c,
                    Family = SelectedFontFamily,
                    Size = 38
                })));
            }

            
        }
        
    }
}
