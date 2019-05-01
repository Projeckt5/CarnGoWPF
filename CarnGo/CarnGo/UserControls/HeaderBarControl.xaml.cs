using System.Windows.Controls;

namespace CarnGo
{
    /// <summary>
    /// Interaction logic for HeaderBarControl.xaml
    /// </summary>
    public partial class HeaderBarControl : UserControl
    {
        public HeaderBarControl()
        {
            InitializeComponent();
            DataContext = IoCContainer.Resolve<HeaderBarViewModel>();
        }
    }
}
