using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;

namespace CarnGo
{
    public class HeaderBarViewModel : BaseViewModel
    {
        private string _searchKeyWord;

        public static HeaderBarViewModel Instance => new HeaderBarViewModel();

        public ICommand NavigateHomeCommand => new DelegateCommand(()=> 
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