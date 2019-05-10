using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarnGo
{
    /// <summary>
    /// Interaction logic for PageNavigation.xaml
    /// </summary>
    public partial class PageNavigation : UserControl
    {
        public PageNavigation()
        {
            InitializeComponent();
        }
        public Page CurrentPage
        {
            get => (Page)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(Page), typeof(PageNavigation), new UIPropertyMetadata(OnCurrentPageChanged));

        private static async void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var currentPageFrame = (d as PageNavigation)?.CurrentPageFrame;
            var newPageFrame = (d as PageNavigation)?.NewPageFrame;
            if (currentPageFrame == null || newPageFrame == null)
                return;

            var oldContent = newPageFrame.Content;

            newPageFrame.Content = null;

            currentPageFrame.Content = oldContent;

            if (oldContent is BasePage oldPage)
            {
                oldPage.ShouldAnimateIn = false;
                await oldPage?.AnimateOut()
                    .ContinueWith((T) => Application.Current.Dispatcher.Invoke(() => currentPageFrame.Content = null));
            }
            
            newPageFrame.Content = e.NewValue;
        }
    }


}
