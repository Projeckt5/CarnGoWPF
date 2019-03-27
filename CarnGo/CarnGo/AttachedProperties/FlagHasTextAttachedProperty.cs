using System.Windows;

namespace CarnGo
{
    public class FlagHasTextAttachedProperty
    {
        public static FlagHasTextAttachedProperty Instance { get; private set; } = new FlagHasTextAttachedProperty();
        public bool HasText { get; set; }
        public static readonly DependencyProperty HasTextProperty = DependencyProperty.Register(nameof(HasText), typeof(bool), typeof(FlagHasTextAttachedProperty));
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