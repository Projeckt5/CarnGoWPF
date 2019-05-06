using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarnGo
{
    public partial class LoginView : BasePage<LoginPageViewModel>
    {
        public LoginView()
        {
            InitializeComponent();
        }

       //Saving Functions 
        private void UserSave()
        {
            Properties.Settings.Default.Username = Email.Text;
            Properties.Settings.Default.Save();

        }

        private void PassSave()
        {
            Properties.Settings.Default.Password = Pass.Password;
            Properties.Settings.Default.Save();

        }

      private void RememberCheck(object sender, RoutedEventArgs e)
        {
            {
                // Save Username
                UserSave();

                //Save Password
                PassSave();
            }
        }

        private void EmailLoad(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                Email.Text = Properties.Settings.Default.Username;
            }
        }

        private void PassLoad(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                Pass.Password = Properties.Settings.Default.Password;
            }
        }
    }
}
