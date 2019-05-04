﻿using System.Collections.Generic;
using CarnGo.Database.Models;

namespace CarnGo
{
    public interface IAppToDbModelConverter
    {
        User Convert(UserModel appUser);
        List<Message> Convert(List<MessageModel> appMessages);
    }
}