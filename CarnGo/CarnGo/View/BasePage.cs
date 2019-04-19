using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarnGo
{
    public class BasePage : Page
    {
    }

    public class BasePage<TViewModel> : BasePage
        where TViewModel : BaseViewModel
    {
        private TViewModel _pageViewModel;

        public BasePage()
        {
            PageViewModel = IoCContainer.Resolve<TViewModel>();
        }

        public TViewModel PageViewModel
        {
            get => _pageViewModel;
            set
            {
                if (_pageViewModel == value)
                    return;
                _pageViewModel = value;
                DataContext = _pageViewModel;
            }
        }
    }
}
