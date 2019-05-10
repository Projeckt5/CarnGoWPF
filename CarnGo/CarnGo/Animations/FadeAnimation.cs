using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace CarnGo.Animations
{
    public class FadeAnimation: IAnimationStrategy
    {
        public void AddAnimateIn(Storyboard sb, double duration)
        {
            var animationDuration = new Duration(TimeSpan.FromSeconds(duration));
            var animation = new DoubleAnimation(0, 1, animationDuration);
            Storyboard.SetTargetProperty(animation,new PropertyPath("Opacity"));
            sb.Children.Add(animation);
        }

        public void AddAnimateOut(Storyboard sb, double duration)
        {
            var animationDuration = new Duration(TimeSpan.FromSeconds(duration));
            var animation = new DoubleAnimation(1, 0, animationDuration);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            sb.Children.Add(animation);
        }
    }
}