using System.Collections.Generic;
using CarnGo.Database.Models;

namespace CarnGo
{
    public interface IAppToDbModelConverter
    {
        User Convert(UserModel appUser);
        List<Message> Convert(List<MessageModel> appMessages);
        CarProfile Convert(CarProfileModel carProfile);
        Message Convert(MessageModel appMessage);
        List<PossibleToRentDay> Convert(List<PossibleToRentDayModel> carPossibleToRentDays);
        List<DayThatIsRented> Convert(List<DayThatIsRentedModel> carDayThatIsRented);
    }
}