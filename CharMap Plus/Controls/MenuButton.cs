using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CharMap_Plus.Controls
{
    public class MenuButton : Button
    {
        public string NavigateUri
        {
            get { return (string)GetValue(NavigateUriProperty); }
            set { SetValue(NavigateUriProperty, value); }
        }   

        // Using a DependencyProperty as the backing store for NavigateUri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigateUriProperty =
            DependencyProperty.Register("NavigateUri", typeof(string), typeof(MenuButton), new PropertyMetadata(0));
        

        public MenuButton()
        {
            DefaultStyleKey = typeof(MenuButton);
            Click += MenuButton_Click;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                frame.Navigate(GetPageType(NavigateUri), null);
            }
        }

        private Type GetPageType(string pageToken)
        {
            var viewFullName = "CharMap_Plus.Views." + pageToken;
            var viewType = Type.GetType(viewFullName);

            if (viewType == null)
            {
                throw new ArgumentException("Page not found", "pageToken");
            }

            return viewType;
        }
    }
}
