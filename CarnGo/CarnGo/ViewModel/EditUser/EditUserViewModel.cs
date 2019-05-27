using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CarnGo.Database.Models;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{
    public class EditUserViewModel : BaseViewModel
    {
        private readonly IApplication _application;
        private readonly IQueryDatabase _queryDatabase;
        private readonly IEventAggregator _eventAggregator;

        #region Private Fields


        private ICommand _saveCommand;
        private bool _isSaving;
        private string _registerUnregisterMessage;
        private UserType _userType;
        private byte[] _userImage;

        #endregion

        #region Default Constructor

        public EditUserViewModel(IApplication application, IQueryDatabase queryDatabase, IEventAggregator eventAggregator)
        {
            _application = application;
            _queryDatabase = queryDatabase;
            _eventAggregator = eventAggregator;
            FirstName = UserModel.FirstName;
            LastName = UserModel.LastName;
            Email = UserModel.Email;
            Address = UserModel.Address;
            UserType = UserModel.UserType;
            UserImage = UserModel.UserPicture;
        }
        #endregion

        #region Public Properties

        public UserModel UserModel => _application.CurrentUser;

        public string RegisterUnregisterMessage => UserType == UserType.Lessor
            ? "Unregister as lessor here..."
            : "Register as lessor here...";

        public UserType UserType
        {
            get => _userType;
            set
            {
                if (_userType == value)
                    return;
                _userType = value;
                OnPropertyChanged(nameof(UserType));
                OnPropertyChanged(nameof(RegisterUnregisterMessage));
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public byte[] UserImage
        {
            get => _userImage;
            set
            {
                if(_userImage == value)
                    return;
                _userImage = value;
                OnPropertyChanged(nameof(UserImage));
            }
        }

        public bool IsSaving
        {
            get=>_isSaving;
            set
            {
                if (_isSaving == value)
                    return;
                _isSaving = value;
                OnPropertyChanged(nameof(IsSaving));
            }
        }
        #endregion

        #region Public Commands
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(async () => await SaveFunction()));
        public ICommand RegisterAsCarRenterCommand => new DelegateCommand(async () => await RegisterAsCarRenter());

        private async Task RegisterAsCarRenter()
        {
            //TODO: MAKE A REAL PROCESS INSTEAD OF JUST PROMOTING THEM LMAO
            UserType = UserType == UserType.OrdinaryUser ? UserType.Lessor : UserType.OrdinaryUser;
            UserModel.UserType = UserType;
            await _queryDatabase.UpdateUser(UserModel);
            _eventAggregator.GetEvent<NewUserDataReadyEvent>().Publish();
        }

        #endregion

        #region Command Helpers
        private async Task SaveFunction()
        {
            if (IsSaving)
                return;
            IsSaving = true;
            try
            {
                UserModel.FirstName = FirstName;
                UserModel.LastName = LastName;
                UserModel.Email = Email;
                UserModel.Address = Address;
                UserModel.UserPicture = UserImage;

                await _queryDatabase.UpdateUser(UserModel);
                _eventAggregator.GetEvent<NewUserDataReadyEvent>().Publish();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsSaving = false;
            }
        }
        #endregion
    }
}