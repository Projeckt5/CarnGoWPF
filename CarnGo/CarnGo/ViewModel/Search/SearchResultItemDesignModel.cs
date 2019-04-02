using System;

namespace CarnGo
{
    public class SearchResultItemDesignModel : SearchResultItemViewModel
    {
        public static SearchResultItemDesignModel Instance => new SearchResultItemDesignModel();

        #region Constructor
        public SearchResultItemDesignModel()
        {
            Model = "CLA 250";
            Brand = "Mercedes";
            Age = 2010;
            Regnr = "CA86304";
            Location = "Aarhus";
            Seats = 2;
            Price = 400;
            StartLeaseTime = DateTime.Today;
            EndLeaseTime = DateTime.Today;
            Owner = new UserModel
            {
                Firstname = "Jens",
                Lastname = "Jensen",
                Address = "Finlandsgade 1",
                Email = "hmm@gmail.com",
                UserType = UserType.Lessor
            };
        }
    #endregion
    }
}