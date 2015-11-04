using CharMap_Plus.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CharMap_Plus.Views
{
    public sealed partial class CharDetail : UserControl
    {
        public FontChar FontChar { get; set; }

        public CharDetail()
        {
            this.InitializeComponent();
        }


        private void CharButton_Click(object sender, RoutedEventArgs e)
        {
            if (FontChar != null)
            {
                var dataPackage = new DataPackage();
                dataPackage.SetText(FontChar.Char.ToString());
                Clipboard.SetContent(dataPackage);
            }
        }

        private void Invert_Click(object sender, RoutedEventArgs e)
        {
            CharBorder.RequestedTheme = CharBorder.RequestedTheme == ElementTheme.Dark ? ElementTheme.Light : ElementTheme.Dark;
        }
    }
}
