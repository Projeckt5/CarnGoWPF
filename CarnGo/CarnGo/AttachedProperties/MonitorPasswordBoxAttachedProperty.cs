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
    public class MonitorPasswordBoxAttachedProperty : DependencyObject
    {
        public static MonitorPasswordBoxAttachedProperty Instance { get; private set; } = new MonitorPasswordBoxAttachedProperty();

        public bool MonitorPasswordBox
        {
            get=> (bool)GetValue(MonitorPasswordBoxProperty);
            set=> SetValue(MonitorPasswordBoxProperty, value);
        }
        public static readonly DependencyProperty MonitorPasswordBoxProperty = DependencyProperty.RegisterAttached(nameof(MonitorPasswordBox), typeof(bool), typeof(MonitorPasswordBoxAttachedProperty), new UIPropertyMetadata(MonitorTextChanged));

        public static void MonitorTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null)
                return;
            passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;

            if ((bool) e.NewValue == true)
            {
                FlagHasTextAttachedProperty.SetHasText(passwordBox,false);
                passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
            }
        }
        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwbPassword = sender as PasswordBox;
            if (pwbPassword.Password.Length > 0)
                FlagHasTextAttachedProperty.SetHasText(pwbPassword, true);
            else
                FlagHasTextAttachedProperty.SetHasText(pwbPassword, false);
        }
    }

}
