using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace CarnGo
{
    public static class MonitorPasswordBoxAttachedProperty
    {
        public static bool GetMonitorPasswordBox(DependencyObject element)
        {
            return (bool) element.GetValue(MonitorPasswordBoxProperty);
        }
        public static void SetMonitorPasswordBox(DependencyObject element, bool value)
        {
            element.SetValue(MonitorPasswordBoxProperty,value);
        }
        public static readonly DependencyProperty MonitorPasswordBoxProperty = DependencyProperty.RegisterAttached("MonitorPasswordBox", typeof(bool), typeof(MonitorPasswordBoxAttachedProperty), new UIPropertyMetadata(MonitorTextChanged));

        public static void MonitorTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
                return;
            passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;

            if ((bool) e.NewValue != true)
                return;
            FlagHasTextAttachedProperty.SetHasText(passwordBox,false);
            passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
        }
        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwbPassword = sender as PasswordBox;
            FlagHasTextAttachedProperty.SetHasText(pwbPassword, pwbPassword?.Password.Length > 0);
        }
    }

}
