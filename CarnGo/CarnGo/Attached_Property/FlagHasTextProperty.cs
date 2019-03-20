using System.Windows;

namespace CarnGo
{
    public class FlagHasTextProperty
    {
        public static FlagHasTextProperty Instance { get; private set; } = new FlagHasTextProperty();
        public bool HasText { get; set; }
        public static readonly DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached(nameof(HasText), typeof(bool), typeof(FlagHasTextProperty));
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