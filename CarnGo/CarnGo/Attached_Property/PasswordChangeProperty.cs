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
    public class PasswordChangeProperty 
    {
        public bool MonitorHasText { get; set; }
        public bool HasText { get; set; }
        public static DependencyProperty MonitorHasTextProperty = DependencyProperty.RegisterAttached(nameof(MonitorHasText), typeof(bool), typeof(PasswordBox), new PropertyMetadata(new PropertyChangedCallback(Monitortextchanged)));
        public static DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached(nameof(HasText), typeof(bool), typeof(PasswordBox), new PropertyMetadata(null));

        public static void Monitortextchanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null)
                return;
            passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;

            if ((bool) e.NewValue == true)
            {
                SetHasText(passwordBox,false);
                passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
            }



        }
        public static void SetHasText(DependencyObject element, bool value)
        {
            element.SetValue(HasTextProperty, value);

        }

        public static void SetMonitorText(DependencyObject element, bool value)
        {
            element.SetValue(MonitorHasTextProperty, value);

        }

        public static bool GetMonitorText(DependencyObject element)
        {
            return (bool) element.GetValue(MonitorHasTextProperty);
        }
        public static bool GetHasText(DependencyObject element)
        {
            return (bool) element.GetValue(HasTextProperty);
        }

        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs e)

        {
            var pwbPassword = sender as PasswordBox;
            if (pwbPassword.Password.Length > 0)
                SetHasText(pwbPassword, true);
            else
                SetHasText(pwbPassword, false);

        }
    }
}
