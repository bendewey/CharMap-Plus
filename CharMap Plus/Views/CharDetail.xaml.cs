using CharMap_Plus.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CharMap_Plus.Views
{
    public sealed partial class CharDetail : UserControl, INotifyPropertyChanged
    {
        private FontChar _fontChar;
        public FontChar FontChar { get { return _fontChar; } set { Set(ref _fontChar, value); } }

        public CharDetail()
        {
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
        }

        public void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field == null || !field.Equals(value))
            {
                field = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}
