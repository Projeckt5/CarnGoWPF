using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace CarnGo
{
    /// <summary>
    /// Attached properties for monitoring encrypted passwords as <see cref="SecureString"/>
    /// </summary>
    public static class EncryptedPasswordAttachedProperty
    {
        #region Encrypted Password

        public static SecureString GetEncryptedPassword(DependencyObject obj)
        {
            return (SecureString)obj.GetValue(EncryptedPasswordProperty);
        }

        public static void SetEncryptedPassword(DependencyObject obj, SecureString value)
        {
            obj.SetValue(EncryptedPasswordProperty, value);
        }

        public static readonly DependencyProperty EncryptedPasswordProperty =
            DependencyProperty.RegisterAttached("EncryptedPassword", typeof(SecureString), typeof(EncryptedPasswordAttachedProperty));

        #endregion

        #region Monitor Encrypted Password

        public static bool GetMonitorEncryptedPassword(DependencyObject obj)
        {
            return (bool)obj.GetValue(MonitorEncryptedPassword);
        }

        public static void SetMonitorEncryptedPassword(DependencyObject obj, SecureString value)
        {
            obj.SetValue(MonitorEncryptedPassword, value);
        }

        public static readonly DependencyProperty MonitorEncryptedPassword =
            DependencyProperty.RegisterAttached("MonitorEncryptedPassword", typeof(bool), typeof(EncryptedPasswordAttachedProperty), new PropertyMetadata(OnPasswordPropertyChanged));

        #endregion

        #region Callbacks

        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox))
                return;
            passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;

            if ((bool)e.NewValue != true)
                return;
            FlagHasTextAttachedProperty.SetHasText(passwordBox, false);
            passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
        }
        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwb = sender as PasswordBox;
            SetEncryptedPassword(pwb, pwb?.SecurePassword);
        } 
        #endregion
    }
}
