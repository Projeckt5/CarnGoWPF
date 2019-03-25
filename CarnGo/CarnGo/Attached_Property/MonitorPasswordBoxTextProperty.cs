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
    public class MonitorPasswordBoxTextProperty 
    {
        public static MonitorPasswordBoxTextProperty Instance { get; private set; } = new MonitorPasswordBoxTextProperty();
        public bool MonitorPasswordBox { get; set; }
        public static readonly DependencyProperty MonitorPasswordBoxProperty = DependencyProperty.RegisterAttached(nameof(MonitorPasswordBox), typeof(bool), typeof(MonitorPasswordBoxTextProperty), new UIPropertyMetadata(MonitorTextChanged));

        public static void MonitorTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null)
                return;
            passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;

            if ((bool) e.NewValue == true)
            {
                FlagHasTextProperty.SetHasText(passwordBox,false);
                passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
            }
        }

        public static void SetMonitorText(DependencyObject element, bool value)
        {
            element.SetValue(MonitorPasswordBoxProperty, value);

        }

        public static bool GetMonitorText(DependencyObject element)
        {
            return (bool) element.GetValue(MonitorPasswordBoxProperty);
        }
        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwbPassword = sender as PasswordBox;
            if (pwbPassword.Password.Length > 0)
                FlagHasTextProperty.SetHasText(pwbPassword, true);
            else
                FlagHasTextProperty.SetHasText(pwbPassword, false);
        }
    }

}
