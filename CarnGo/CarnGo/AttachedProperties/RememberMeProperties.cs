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
    public class RememberMeProperties
    {



        LoginView Remem = new LoginView();

        //Saving Functions 
        private void RememberCheck(object sender, RoutedEventArgs e)
        {
            {
                // Save Username
                Properties.Settings.Default.Username = Remem.Email.Text;
                Properties.Settings.Default.Save();

                //Save Password
                Properties.Settings.Default.Password = Remem.Pass.Password;
                Properties.Settings.Default.Save();
            }
        }

        private void EmailLoad(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                Remem.Email.Text = Properties.Settings.Default.Username;
            }
        }

        private void PassLoad(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                Remem.Pass.Password = Properties.Settings.Default.Password;
            }
        }



    }
}