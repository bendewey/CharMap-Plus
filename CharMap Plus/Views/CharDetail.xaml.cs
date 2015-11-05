using CharMap_Plus.Model;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
