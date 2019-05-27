using System.Collections.Generic;
using System.Windows.Media.Imaging;
using CarnGo.Database.Models;

namespace CarnGo
{
    public interface IDbToAppModelConverter
    {
        UserModel Convert(User dbUser);
        CarProfileModel Convert(CarProfile dbCarProfile);
        List<MessageModel> Convert(List<Message> dbMessages);
        List<CarProfileModel> Convert(List<CarProfile> carProfiles);
        List<PossibleToRentDayModel> Convert(List<PossibleToRentDay> possibleToRentDays);
        List<DayThatIsRentedModel> Convert(List<DayThatIsRented> dayThatIsRented);
    }
}