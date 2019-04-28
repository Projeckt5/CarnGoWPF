using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo
{ 
    public class MessageBoxViewModel : BaseViewModel
    {
        public MessageBoxViewModel()
        {
            #region Dummy Data (Notification)
            var User1 = new UserModel("Martin", "Gildberg", "xXxGitMazterxXx@hotmail.com", "Gellerup", UserType.Lessor);
            var User2 = new UserModel("Marcus", "Gasberg", "xXxGitMazterxXx@hotmail.com", "Gellerup", UserType.OrdinaryUser);
            var Car = new CarProfileModel(User1, "X-360", "BMW", 1989, "1234567", "Aarhus", 2, DateTime.Today, DateTime.Today, 1);
            //Car.CarPicture = new BitmapImage(new Uri("../../Images/Bilfoto.jpg"));


            var message1 = new MessageFromLessorModel(User2, User1, Car, "Du kommer bare :)", true);
            var message2 = new MessageFromLessorModel(User2, User1, Car, "Det kan du godt glemme makker! Det kan du godt glemme makker! Det kan du godt glemme makker!", false);
            var message3 = new MessageFromRenterModel(User2, User1, Car, "Må jeg godt låne din flotte bil?");
            var Notification1 = new NotificationItemViewModel(IoCContainer.Resolve<IApplication>(),message1);
            var Notification2 = new NotificationItemViewModel(IoCContainer.Resolve<IApplication>(),message2);
            var Notification3 = new NotificationItemViewModel(IoCContainer.Resolve<IApplication>(),message3);
            Messages = new List<NotificationItemViewModel>
            {
                Notification1,
                Notification2,
                Notification3
            };
            #endregion
        }

        #region Properties
        private List<NotificationItemViewModel> _messages;

        public List<NotificationItemViewModel> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }
        #endregion
    }
}
