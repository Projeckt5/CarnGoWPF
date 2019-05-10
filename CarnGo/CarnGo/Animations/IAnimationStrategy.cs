using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace CarnGo.Animations
{
    public interface IAnimationStrategy
    {
        void AddAnimateIn(Storyboard sb, double duration);
        void AddAnimateOut(Storyboard sb, double duration);
    }
}