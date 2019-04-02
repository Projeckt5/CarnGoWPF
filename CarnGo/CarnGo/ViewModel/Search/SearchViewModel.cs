using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{
    public class CarProfileDataEvent : PubSubEvent<CarProfileModel> { }

    public class SearchViewModel : CarProfileModel
    {
        #region Constructor

        public SearchViewModel()
        {
        }

        #endregion

    }
}