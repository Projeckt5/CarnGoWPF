using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace CarnGo
{
    /// <summary>
    /// Interaction logic for RegisterCarProfile.xaml
    /// </summary>
    public partial class CarLeaseView : BasePage<CarLeaseViewModel>
    {
        public CarLeaseView()
        {
            InitializeComponent();
        }

        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "jpg images | *.jpg"
            };
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                    return;
                var filename = openFileDialog1.FileName;
                PageViewModel.CarPicture = File.ReadAllBytes(filename);
            }
        }
    }
}
