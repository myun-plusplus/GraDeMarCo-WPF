using GraDeMarCoWPF.Models;
using System.ComponentModel;
using System.Windows.Media;

namespace GraDeMarCoWPF.ViewModels
{
    public class GrainDetectingViewModel : ViewModelBase
    {
        private AppData appData;

        public int MinimumGrainPixels
        {
            get { return 0; }
            set
            {

            }
        }

        public bool DetectsGrainsInCircle
        {
            get { return false; }
            set
            {

            }
        }

        public Color GrainInCircleDotColor
        {
            get { return Colors.Transparent; }
            set
            {

            }
        }

        public double GrainInCircleDotSize
        {
            get { return 0.0; }
            set
            {

            }
        }

        public bool DetectsGrainsOnCircle
        {
            get { return false; }
            set
            {

            }
        }

        public Color GrainOnCircleDotColor
        {
            get { return Colors.Transparent; }
            set
            {

            }
        }

        public double GrainOnCircleDotSize
        {
            get { return 0.0; }
            set
            {

            }
        }

        public GrainDetectingViewModel(
            AppData appData)
        {
            this.appData = appData;
        }

        private void imageArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }
    }
}
