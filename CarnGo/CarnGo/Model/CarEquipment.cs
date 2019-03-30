using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo
{
    
    public class CarEquipment
    {
        public CarEquipment(bool audioPlayer, bool childSeats,bool smoking,bool gps )
        {
            AudioPlayer = audioPlayer;
            ChildSeats = childSeats;
            Smoking = smoking;
            Gps = gps;
        }
        public bool AudioPlayer { get; set; }
        public bool ChildSeats { get; set; }
        public bool Smoking { get; set; }
        public bool Gps { get; set; }
    }
}
