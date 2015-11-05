using CharMap_Plus.Model;
using CharMap_Plus.ViewModels;
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

namespace CharMap_Plus.Views
{
  
    public sealed partial class DetailsPage : Page
    {
        public DetailsPageViewModel ViewModel { get; set; }

        public DetailsPage()
        {
            this.InitializeComponent();

            ViewModel = new DetailsPageViewModel();
        }

        private void SetupTitleBarBackButton()
        {
            var frame = Window.Current.Content as Frame;
            var showTitleBack = frame.CanGoBack && !ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = showTitleBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                base.OnNavigatedTo(e);
                SetupTitleBarBackButton();

                await ViewModel.Load(e.Parameter as string);
            }
            catch (Exception ex)
            {
                ViewModel.HandleError(ex);
            }
        }

        private async void FontSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedFont = ((ComboBox)sender).SelectedItem as string;
                if (ViewModel.SelectedFontFamily != selectedFont)
                {
                    await ViewModel.Load(selectedFont);
                }
            }
            catch (Exception ex)
            {
                ViewModel.HandleError(ex);
            }
        }
    }
}
