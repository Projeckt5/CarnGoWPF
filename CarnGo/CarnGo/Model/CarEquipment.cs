using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo
{
    
    public class CarEquipment:BaseViewModel
    {
        private bool _childSeat;
        private bool _audioplayer;
        private bool _gps;
        private bool _smoking;
        public bool ChildSeat
        {
            get => _childSeat;
            set
            {
                _childSeat = value;
                OnPropertyChanged(nameof(ChildSeat));
            }

        }
        public bool AudioPlayer
        {
            get => _audioplayer;
            set
            {
                _audioplayer = value;
                OnPropertyChanged(nameof(AudioPlayer));
            }
        }
        public bool Gps
        {
            get => _gps;
            set
            {
                _gps = value;
                OnPropertyChanged(nameof(Gps));
            }
        }
        public bool Smoking
        {
            get => _smoking;
            set
            {
                _smoking = value;
                OnPropertyChanged(nameof(Smoking));
            }
        }


    }
}
