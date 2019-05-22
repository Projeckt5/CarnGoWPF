using System;

namespace CarnGo
{
    public class DayThatIsRentedModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public UserModel Renter { get; set; }
        public CarProfileModel CarProfileModel { get; set; }
    }
}