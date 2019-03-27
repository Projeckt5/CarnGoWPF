using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CarnGo
{
    public class IsBusyAttachedProperty : DependencyObject
    {
        public static IsBusyAttachedProperty Instance { get; private set; } = new IsBusyAttachedProperty();
        public bool IsBusy { get; set; } = false;
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.RegisterAttached(nameof(IsBusy), typeof(bool), typeof(IsBusyAttachedProperty));
        public static void SetIsBusy(DependencyObject element, bool value)
        {
            element.SetValue(IsBusyProperty, value);

        }
        public static bool GetIsBusy(DependencyObject element)
        {
            return (bool)element.GetValue(IsBusyProperty);
        }
    }
}