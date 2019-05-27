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

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            if (PageViewModel.RememberUser)
            {
                Properties.Settings.Default.RememberMe = PageViewModel.RememberUser;
                Properties.Settings.Default.Username = PageViewModel.Email;
                Properties.Settings.Default.Save();
            }
        }

        private void LoginView_OnLoaded(object sender, RoutedEventArgs e)
        {

            if (Properties.Settings.Default.Username != string.Empty)
            {
                PageViewModel.RememberUser = Properties.Settings.Default.RememberMe;
                PageViewModel.Email = Properties.Settings.Default.Username;
            }
        }
    }
}
