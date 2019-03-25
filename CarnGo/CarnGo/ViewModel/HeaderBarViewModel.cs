using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;

namespace CarnGo
{
    public class HeaderBarViewModel : BaseViewModel
    {
        private ICommand _navigateHomeCommand;
        private ICommand _notificationCommand;
        private ICommand _navigateUserCommand;
        private ICommand _searchCommand;
        private ICommand _loginCommand;
        private string _searchKeyWord;

        public static HeaderBarViewModel Instance => new HeaderBarViewModel();

        public ICommand NavigateHomeCommand => _navigateHomeCommand?? new DelegateCommand(()=> 
                                                   ViewModelLocator.ApplicationViewModel
                                                       .GoToPage(ApplicationPage.StartPage));

        public ICommand NotificationCommand => _notificationCommand ?? new DelegateCommand(ShowNotification);


        public ICommand NavigateUserCommand => _navigateUserCommand ?? new DelegateCommand(() =>
                                                   ViewModelLocator.ApplicationViewModel
                                                       .GoToPage(ApplicationPage.EditUserPage));

        public ICommand SearchCommand => _searchCommand ?? new DelegateCommand(Search);


        public ICommand LoginCommand => _loginCommand ?? new DelegateCommand(() =>
                                            ViewModelLocator.ApplicationViewModel
                                                .GoToPage(ApplicationPage.LoginPage));

        public string SearchKeyWord
        {
            get => _searchKeyWord;
            set
            {
                if (_searchKeyWord == value)
                {
                    return;
                }
                _searchKeyWord = value;
                OnPropertyChanged(nameof(SearchKeyWord));
            }
        }
        private void Search()
        {
            MessageBox.Show(SearchKeyWord);
        }

        private void ShowNotification()
        {
            MessageBox.Show("*Notification shown*");
        }
    }
}