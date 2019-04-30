using System.ComponentModel.DataAnnotations;

namespace CarnGo.Database.Models
{
    public class Message 
    {
        [Key]
        public int MessageID { get; set; }
        [Required]
        public bool HaveBeenSeen { get; set; }
        public bool Confirmation { get; set; }
        public string TheMessage { get; set; }
        //public BitmapImage CarPicture { get; set; }
        public string Lessor { get; set; }
        public string Renter { get; set; }
        public int MsgType { get; set; }
    }
}
