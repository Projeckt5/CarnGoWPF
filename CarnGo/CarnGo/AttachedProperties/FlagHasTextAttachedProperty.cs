using System.Windows;

namespace CarnGo
{
    public static class FlagHasTextAttachedProperty
    {
        public static readonly DependencyProperty HasTextProperty = DependencyProperty.Register("HasText", typeof(bool), typeof(FlagHasTextAttachedProperty));
        public static void SetHasText(DependencyObject element, bool value)
        {
            element.SetValue(HasTextProperty, value);

        }
        public static bool GetHasText(DependencyObject element)
        {
            return (bool)element.GetValue(HasTextProperty);
        }
    }
}