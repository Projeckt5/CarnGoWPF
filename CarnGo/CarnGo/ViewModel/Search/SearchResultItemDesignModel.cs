using System;

namespace CarnGo
{
    public class SearchResultItemDesignModel : SearchResultItemViewModel
    {
        public static SearchResultItemDesignModel Instance => new SearchResultItemDesignModel(IoCContainer.Resolve<IApplication>());

        #region Constructor
        public SearchResultItemDesignModel(IApplication application)
            :base(application)
        {
            Model = "CLA 250";
            Brand = "Mercedes";
            Location = "Aarhus";
            RegNr = "AF75903";
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