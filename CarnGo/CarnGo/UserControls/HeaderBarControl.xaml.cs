using System.Windows;
using System.Windows.Controls;
using CarnGo.Animations;

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
