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

        #endregion

        #region Default Constructor

        public EditUserViewModel(IApplication application, IQueryDatabase queryDatabase, IEventAggregator eventAggregator)
        {
            _application = application;
            _queryDatabase = queryDatabase;
            _eventAggregator = eventAggregator;
            FirstName = UserModel.Firstname;
            LastName = UserModel.Lastname;
            Email = UserModel.Email;
            Address = UserModel.Address;
        }
        #endregion

        #region Public Properties

        public UserModel UserModel => _application.CurrentUser;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public BitmapImage UserImage { get; set; }

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
        public ICommand UploadPhotoCommand => new DelegateCommand(() => UploadPhotoFunction());
        #endregion

        #region Command Helpers
        private async Task SaveFunction()
        {
            if (IsSaving)
                return;
            IsSaving = true;
            try
            {
                UserModel.Firstname = FirstName;
                UserModel.Lastname = LastName;
                UserModel.Email = Email;
                UserModel.Address = Address;
                UserModel.UserImage = UserImage;

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