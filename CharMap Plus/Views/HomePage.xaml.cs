using CharMap_Plus.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
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
            try
            {
                ViewModel.Frame = this.Frame;
                await ViewModel.Load();
                base.OnNavigatedTo(e);
            }
            catch (Exception ex)
            {
                ViewModel.HandleError(ex);
            }
        }

        private async void PageContainer_RefreshClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                await ViewModel.Load();
            }
            catch (Exception ex)
            {
                ViewModel.HandleError(ex);
            }
        }
    }

    
}
