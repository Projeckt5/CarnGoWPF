using System;
using Prism.Events;

namespace CarnGo
{
    public class SearchResultItemDesignModel : SearchResultItemViewModel
    {
        public static SearchResultItemDesignModel Instance => new SearchResultItemDesignModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>());

        #region Constructor
        public SearchResultItemDesignModel(IEventAggregator eventAggregator, IApplication application)
            :base(eventAggregator, application)
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
                FirstName = "Jens",
                LastName = "Jensen",
                Address = "Finlandsgade 1",
                Email = "hmm@gmail.com",
                UserType = UserType.Lessor
            };

        }
    #endregion
    }
}