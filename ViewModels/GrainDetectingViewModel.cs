using GraDeMarCoWPF.Commands;
using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace GraDeMarCoWPF.ViewModels
{
    public class GrainDetectingViewModel : ViewModelBase
    {
        private AppData appData;
        private GrainDetectingOptions grainDetectingOptions;
        private DotDrawingTool grainInCircleDotDrawingTool;
        private DotDrawingTool grainOnCircleDotDrawingTool;

        public int MinimumGrainPixels
        {
            get { return grainDetectingOptions.MinimumGrainPixels; }
            set
            {
                grainDetectingOptions.MinimumGrainPixels = value;
                NotifyPropertyChanged(GetName.Of(() => MinimumGrainPixels));
            }
        }

        public bool DetectsGrainsInCircle
        {
            get { return grainDetectingOptions.DetectsGrainsInCircle; }
            set
            {
                grainDetectingOptions.DetectsGrainsInCircle = value;
                NotifyPropertyChanged(GetName.Of(() => DetectsGrainsInCircle));
            }
        }

        public SolidColorBrush GrainInCircleDotColor
        {
            get { return grainInCircleDotDrawingTool.Brush; }
        }

        public double GrainInCircleDotSize
        {
            get { return grainInCircleDotDrawingTool.Size; }
            set
            {
                grainInCircleDotDrawingTool.Size = value;
                NotifyPropertyChanged(GetName.Of(() => GrainInCircleDotSize));
            }
        }

        public bool DetectsGrainsOnCircle
        {
            get { return grainDetectingOptions.DetectsGrainsOnCircle; }
            set
            {
                grainDetectingOptions.DetectsGrainsOnCircle = value;
                NotifyPropertyChanged(GetName.Of(() => DetectsGrainsOnCircle));
            }
        }

        public SolidColorBrush GrainOnCircleDotColor
        {
            get { return grainOnCircleDotDrawingTool.Brush; }
        }

        public double GrainOnCircleDotSize
        {
            get { return grainOnCircleDotDrawingTool.Size; }
            set
            {
                grainOnCircleDotDrawingTool.Size = value;
                NotifyPropertyChanged(GetName.Of(() => GrainOnCircleDotSize));
            }
        }

        public ICommand SelectGrainInCircleDotColor { get; private set; }
        public ICommand SelectGrainOnCircleDotColor { get; private set; }

        public GrainDetectingViewModel(
            IColorDialogService colorDialogService,
            AppData appData,
            GrainDetectingOptions grainDetectingOptions,
            DotDrawingTool grainInCircleDotDrawingTool,
            DotDrawingTool grainOnCircleDotDrawingTool)
        {
            this.appData = appData;
            this.grainDetectingOptions = grainDetectingOptions;
            this.grainInCircleDotDrawingTool = grainInCircleDotDrawingTool;
            this.grainOnCircleDotDrawingTool = grainOnCircleDotDrawingTool;

            grainDetectingOptions.PropertyChanged += grainDetectingOptions_PropertyChanged;
            grainInCircleDotDrawingTool.PropertyChanged += grainInCircleDotDrawingTool_PropertyChanged;
            grainOnCircleDotDrawingTool.PropertyChanged += grainOnCircleDotDrawingTool_PropertyChanged;

            SelectGrainInCircleDotColor = new SelectDetectedDotColor(appData, grainInCircleDotDrawingTool, colorDialogService);
            SelectGrainOnCircleDotColor = new SelectDetectedDotColor(appData, grainOnCircleDotDrawingTool, colorDialogService);
        }

        private void grainDetectingOptions_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        private void grainInCircleDotDrawingTool_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Color")
            {
                NotifyPropertyChanged("GrainInCircleDotColor");
            }
            else if (e.PropertyName == "Size")
            {
                NotifyPropertyChanged("GrainInCircleDotSize");
            }
            else
            {
                NotifyPropertyChanged(e.PropertyName);
            }
        }

        private void grainOnCircleDotDrawingTool_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Color")
            {
                NotifyPropertyChanged("GrainOnCircleDotColor");
            }
            else if (e.PropertyName == "Size")
            {
                NotifyPropertyChanged("GrainOnCircleDotSize");
            }
            else
            {
                NotifyPropertyChanged(e.PropertyName);
            }
        }
    }
}
