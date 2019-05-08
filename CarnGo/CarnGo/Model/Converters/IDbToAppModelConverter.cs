using System.Collections.Generic;
using CarnGo.Database.Models;

namespace CarnGo
{
    public interface IDbToAppModelConverter
    {
        UserModel Convert(User dbUser);
        List<MessageModel> Convert(List<Message> dbMessages);
        List<CarProfileModel> Convert(List<CarProfile> carProfiles);
    }
}