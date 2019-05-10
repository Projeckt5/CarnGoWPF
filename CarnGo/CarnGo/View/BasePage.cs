using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CarnGo.Animations;

namespace CarnGo
{
    public class BasePage : Page
    {
        private readonly FrameworkElementAnimations _frameworkElementAnimations;

        public BasePage()
        {
            _frameworkElementAnimations = new FrameworkElementAnimations();
            _frameworkElementAnimations.AddAnimation(new FadeAnimation());
            Loaded += async (s, e) =>
            {
                if(ShouldAnimateIn)
                    await AnimateIn();
            };
        }
        public async Task AnimateOut()
        {
            await _frameworkElementAnimations.AnimateOut(this, 0.3);
        }
        public async Task AnimateIn()
        {
            await _frameworkElementAnimations.AnimateIn(this, 0.3);
        }

        public bool ShouldAnimateIn { get; set; } = true;
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
