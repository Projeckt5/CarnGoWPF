using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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
          
        }

        /// <summary>
        /// Explicit Constructor 
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="userType"></param>
        public UserModel(string firstname, string lastname, string email,
            string address, UserType userType)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Address = address;
            UserType = userType; 
            _messageModels = new List<MessageModel>();
        }
        #endregion

        #region Fields
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _address;
        private UserType _usertype;
        private List<MessageModel> _messageModels;

        #endregion

        //TODO Try/Catch block for Database exceptions
        #region Properties

        /// <summary>
        /// Firstname of the current user
        /// </summary>
        public string Firstname
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        /// <summary>
        /// Lastname of the current user
        /// </summary>
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
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
        #endregion
    }
}
