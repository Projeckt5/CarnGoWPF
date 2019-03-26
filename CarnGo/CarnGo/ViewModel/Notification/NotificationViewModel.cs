using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    public class NotificationViewModel : BaseViewModel
    {
        #region Constructors
        #endregion

        #region Properties
        private List<CarProfileModel> _carProfiles;
        private List<UserModel> _users;
        private List<NotificationItemViewModel> _items;

        public List<CarProfileModel> CarProfiles
        {
            get { return _carProfiles; }
            set
            {
                _carProfiles = value; 
                OnPropertyChanged(nameof(CarProfiles));
            }

        }

        public List<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(CarProfiles));
            }
        }

        public List<NotificationItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        #endregion

        #region Commands
        private ICommand _notificationPressedCommand;
        public ICommand NotificationPressedCommand => _notificationPressedCommand ?? (_notificationPressedCommand = new DelegateCommand(NotificationExecute));
        #endregion

        #region Executes & CanExecutes
        private void NotificationExecute()
        {
            ViewModelLocator.ApplicationViewModel.GoToPage(ApplicationPage.MessageView);
        }
        #endregion

    }
}
