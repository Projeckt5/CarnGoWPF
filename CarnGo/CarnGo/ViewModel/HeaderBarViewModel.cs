using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;

namespace CarnGo
{
    public class HeaderBarViewModel : BaseViewModel
    {
        #region Public Properties

        public static HeaderBarViewModel Instance => new HeaderBarViewModel();
        public string SearchKeyWord { get; set; }
        #endregion
        #region Public Commands

        public ICommand NavigateHomeCommand => new DelegateCommand(() =>
                                                   ViewModelLocator.ApplicationViewModel
                                                       .GoToPage(ApplicationPage.StartPage));

        public ICommand NotificationCommand => new DelegateCommand(ShowNotification);


        public ICommand NavigateUserCommand => new DelegateCommand(() =>
                                                   ViewModelLocator.ApplicationViewModel
                                                       .GoToPage(ApplicationPage.EditUserPage));

        public ICommand SearchCommand => new DelegateCommand(Search);


        public ICommand LoginCommand => new DelegateCommand(() =>
                                            ViewModelLocator.ApplicationViewModel
                                                .GoToPage(ApplicationPage.LoginPage));
        #endregion
        #region Command Helpers

        private void Search()
        {
            MessageBox.Show(SearchKeyWord);
        }

        private void ShowNotification()
        {
            MessageBox.Show("*Notification shown*");
        } 
        #endregion
    }
}