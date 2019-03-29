using Prism.Commands;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Input;

namespace CarnGo
{
    public class NotificationViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public NotificationViewModel()
        {
            #region Dummy Data (Notification)
            var User1 = new UserModel("Martin", "Gildberg", "xXxGitMazterxXx@hotmail.com", "Gellerup", UserType.Lessor);
            var User2 = new UserModel("Marcus", "Gasberg", "xXxGitMazterxXx@hotmail.com", "Gellerup", UserType.OrdinaryUser);
            var Car = new CarProfileModel(User1, "X-360", "BMW", 1989, "1234567");

            var message = new MessageFromLessorModel(User2, User1, Car, "Du kommer bare :)", true);
            var Notification = new NotificationItemViewModel(message);

            #endregion
            Messages = new List<NotificationItemViewModel>
            {
                Notification,
                Notification,
                Notification
            };
        }
        #endregion

        #region Properties
        private List<NotificationItemViewModel> _messages;

        public List<NotificationItemViewModel> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }
        #endregion
    }
}
