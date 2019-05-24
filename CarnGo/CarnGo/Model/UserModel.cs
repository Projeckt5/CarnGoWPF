using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CarnGo
{
    public class UserModel : BaseViewModel
    {
        #region Constructor 
        /// <summary>
        /// Default Constructor
        /// </summary>
        public UserModel()
        {
            _messageModels = new List<MessageModel>();
            FirstName = "";
            LastName = "";
            Email = "";
            Address = "";
            UserType = UserType.NonUser;
        }

        /// <summary>
        /// Explicit Constructor 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="userType"></param>
        public UserModel(string firstName, string lastName, string email,
            string address, UserType userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            UserType = userType; 
            _messageModels = new List<MessageModel>();
        }
        #endregion

        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _address;
        private UserType _usertype;
        private List<MessageModel> _messageModels;
        private BitmapImage _userImage;

        #endregion

        //TODO Try/Catch block for Database exceptions
        #region Properties

        /// <summary>
        /// FirstName of the current user
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public Guid AuthenticationString { get; set; }

        /// <summary>
        /// LastName of the current user
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        /// <summary>
        /// Email of the current user
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public UserType UserType
        {
            get { return _usertype; }
            set
            {
                _usertype = value;
                OnPropertyChanged(nameof(UserType));
            }
        }

        public List<MessageModel> MessageModels
        {
            get => _messageModels;
            set
            {
                if (_messageModels == value)
                    return;
                _messageModels = value;
                OnPropertyChanged(nameof(MessageModels));
            }
        }

        public BitmapImage UserImage
        {
            get => _userImage;
            set
            {
                _userImage = value;
                OnPropertyChanged(nameof(UserImage));
            }
        }
        #endregion
    }
}
