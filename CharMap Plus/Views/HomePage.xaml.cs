using CharMap_Plus.Model;
using CharMap_Plus.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CharMap_Plus.Views
{
    public sealed partial class HomePage : Page
    {
        public HomePageViewModel ViewModel { get; set; }
        public HomePage()
        {
            this.InitializeComponent();
            ViewModel = new HomePageViewModel(this.Frame);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Frame = this.Frame;
            await ViewModel.Load();
            base.OnNavigatedTo(e);
        }
    }

    
}
