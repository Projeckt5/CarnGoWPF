using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CarnGo.Database.Models;
using Microsoft.Win32;
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
        public BitmapImage UserImage { get; set; }
        #endregion

        #region Public Commands
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveFunction));
        public ICommand UploadPhotoCommand => new DelegateCommand(() => UploadPhotoFunction());
        #endregion

        #region Command Helpers
        private void SaveFunction()
        {
            _userModel.Firstname = FirstName;
            _userModel.Lastname = LastName;
            _userModel.Email = Email;
            _userModel.Address = Address;
            _userModel.UserImage = UserImage;
            OnPropertyChanged(nameof(UserModel));
        }

        private void UploadPhotoFunction()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (fileDialog.ShowDialog() == true)
            {
                UserImage = new BitmapImage(new Uri(fileDialog.FileName));
            }
            OnPropertyChanged(nameof(UserImage));
        }

        #endregion
    }
}