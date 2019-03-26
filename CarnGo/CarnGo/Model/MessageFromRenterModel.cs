﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarnGo
{
    public class MessageFromRenterModel:BaseViewModel
    {
        #region fields
        private UserModel _renter;
        private UserModel _leaser;
        private string _message;
        private DateTime _from;
        private DateTime _to;
        #endregion

        #region Constructor

        public MessageFromRenterModel(UserModel renter, UserModel leaser)
        {
            _renter = renter;
            _leaser = leaser;
        }
        

        #endregion

        #region Properties
        public string Message
        {
            get { return _message; }
            set
            {
                _ = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public DateTime From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }

        public DateTime To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }
#endregion

    }
}
