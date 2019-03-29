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

            var message1 = new MessageFromLessorModel(User2, User1, Car, "Du kommer bare :)", true);
            var message2 = new MessageFromLessorModel(User2, User1, Car, "Det kan du godt glemme", false);
            var Notification1 = new NotificationItemViewModel(message1);
            var Notification2 = new NotificationItemViewModel(message2);
            Messages = new List<NotificationItemViewModel>
            {
                Notification1,
                Notification2
            };
            #endregion

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
