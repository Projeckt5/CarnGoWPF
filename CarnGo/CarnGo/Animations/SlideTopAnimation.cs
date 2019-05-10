using System;
using System.Windows;
using System.Windows.Media.Animation;
using CarnGo.Animations;

namespace CarnGo
{
    public class SlideTopAnimation : IAnimationStrategy
    {
        private readonly double _elementHeight;
        private readonly bool _keepMargin;

        public SlideTopAnimation(double elementHeight, bool keepMargin = true)
        {
            _elementHeight = elementHeight;
            _keepMargin = keepMargin;
        }
        public void AddAnimateIn(Storyboard sb, double duration)
        {
            var animationDuration = new Duration(TimeSpan.FromSeconds(duration));
            var fromMargin = new Thickness(0, - _elementHeight, 0, _keepMargin ? _elementHeight : 0);
            var toMargin = new Thickness(0);
            var animation = new ThicknessAnimation(fromMargin,toMargin,animationDuration);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            sb.Children.Add(animation);
        }

        public void AddAnimateOut(Storyboard sb, double duration)
        {
            var animationDuration = new Duration(TimeSpan.FromSeconds(duration));
            var fromMargin = new Thickness(0);
            var toMargin = new Thickness(0, -_elementHeight, 0, _keepMargin ? _elementHeight : 0);
            var animation = new ThicknessAnimation(fromMargin, toMargin, animationDuration);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            sb.Children.Add(animation);
        }
    }
}