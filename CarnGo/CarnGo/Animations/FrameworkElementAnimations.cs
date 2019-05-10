using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Animation;

namespace CarnGo.Animations
{
    public class FrameworkElementAnimations
    {
        private readonly List<IAnimationStrategy> _animationStrategies = new List<IAnimationStrategy>();

        public void AddAnimation(IAnimationStrategy animation)
        {
            _animationStrategies.Add(animation);
        }

        public async Task AnimateIn(FrameworkElement frameworkElement, double seconds)
        {
            var storyBoard = new Storyboard();
            foreach (var animationStrategy in _animationStrategies)
            {
                animationStrategy.AddAnimateIn(storyBoard, seconds);
            }
            storyBoard.Begin(frameworkElement);
            frameworkElement.Visibility = Visibility.Visible;
            await Task.Delay((int) (1000 * seconds));
        }

        public async Task AnimateOut(FrameworkElement frameworkElement, double seconds)
        {
            var storyBoard = new Storyboard();
            foreach (var animationStrategy in _animationStrategies)
            {
                animationStrategy.AddAnimateOut(storyBoard,seconds);
            }
            storyBoard.Begin(frameworkElement);
            frameworkElement.Visibility = Visibility.Visible;
            await Task.Delay((int)(1000 * seconds));
            frameworkElement.Visibility = Visibility.Hidden;
        }
    }
}