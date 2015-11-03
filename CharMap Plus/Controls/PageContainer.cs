using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CharMap_Plus.Controls
{
    public class PageContainer : ContentControl
    {
        public event RoutedEventHandler RefreshClicked;

        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(PageContainer), new PropertyMetadata(string.Empty));



        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleTemplte.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleTemplateProperty =
            DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(PageContainer), new PropertyMetadata(null));

        
        public PageContainer()
        {
            DefaultStyleKey = typeof(PageContainer);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var refreshButton = GetTemplateChild("RefreshButton") as Button;
            if (refreshButton != null)
            {
                refreshButton.Click += (s, e) =>
                {
                    if (RefreshClicked != null)
                    {
                        RefreshClicked(this, new RoutedEventArgs());
                    }
                };
            }
        }
    }
}
