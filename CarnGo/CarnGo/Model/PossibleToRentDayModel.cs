using System;

namespace CarnGo
{
    public class PossibleToRentDayModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CarProfileModel CarProfile { get; set; }
    }
}