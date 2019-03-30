using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo
{
    public class CarDetailsViewModel:BaseViewModel
    {
        private int _year;
        private string _model;
        private bool? _audioPlayer;
        private bool? _childSeats;
        private bool? _smoking;
        private bool? _gps;

        public int Year
        {
            get => _year;
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged(nameof(Year));

                }

            }

        
        }
        public string Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged(nameof(Model));

                }

            }


        }
        public bool? AudioPlayer
        {
            get => _audioPlayer;
            set
            {
                if (_audioPlayer != value)
                {
                    _audioPlayer = value;
                    OnPropertyChanged(nameof(AudioPlayer));

                }

            }


        }
        public bool? ChildSeats
        {
            get => _childSeats;
            set
            {
                if (_childSeats != value)
                {
                    _childSeats = value;
                    OnPropertyChanged(nameof(ChildSeats));

                }

            }


        }
        public bool? Smoking
        {
            get => _smoking;
            set
            {
                if (_smoking != value)
                {
                    _smoking = value;
                    OnPropertyChanged(nameof(Smoking));

                }

            }


        }

        public bool? Gps
        {
            get => _gps;
            set
            {
                if (_gps != value)
                {
                    _gps = value;
                    OnPropertyChanged(nameof(Gps));

                }

            }


        }
        }
}
