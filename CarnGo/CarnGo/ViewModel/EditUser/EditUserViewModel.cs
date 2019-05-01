using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    public class EditUserViewModel : BaseViewModel
    {
        #region Private Fields

        private UserModel _userModel;

        private ICommand _saveCommand;

        #endregion

        #region Default Constructor

        public EditUserViewModel(IApplication application)
        {
            _userModel = application.CurrentUser;
            FirstName = _userModel.Firstname;
            LastName = _userModel.Lastname;
            Email = _userModel.Email;
            Address = _userModel.Address;
        }
        #endregion

        #region Public Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        #endregion

        #region Public Commands
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveFunction));
        #endregion

        #region Command Helpers
        private void SaveFunction()
        {
            _userModel.Firstname = FirstName;
            _userModel.Lastname = LastName;
            _userModel.Email = Email;
            _userModel.Address = Address;
            OnPropertyChanged(nameof(UserModel));
        }

        #endregion
    }
}