using System.Windows;

namespace GraDeMarCoWPF.Models
{
    public class ImageAreaSelecting
    {
        private ImageDisplay imageDisplay;
        private ImageArea imageArea;

        public Rect Area
        {
            get
            {
                return new Rect(firstLocation, secondLocation);
            }
        }

        private enum _State
        {
            NotActive,
            NoneSelected,
            FirstLocationSelected,
            AreaSelected
        }
        private _State state;

        private Point firstLocation
        {
            get { return _firstLocation; }
            set
            {
                _firstLocation = value;
                var rect = new Rect(
                    imageDisplay.GetAbsoluteLocation(_firstLocation),
                    imageDisplay.GetAbsoluteLocation(_secondLocation));
                imageArea.LowerX = (int)rect.TopLeft.X;
                imageArea.LowerY = (int)rect.TopLeft.Y;
                imageArea.UpperX = (int)rect.BottomRight.X;
                imageArea.UpperY = (int)rect.BottomRight.Y;
            }
        }

        private Point secondLocation
        {
            get { return _secondLocation; }
            set
            {
                _secondLocation = value;
                var rect = new Rect(
                    imageDisplay.GetAbsoluteLocation(_firstLocation),
                    imageDisplay.GetAbsoluteLocation(_secondLocation));
                imageArea.LowerX = (int)rect.TopLeft.X;
                imageArea.LowerY = (int)rect.TopLeft.Y;
                imageArea.UpperX = (int)rect.BottomRight.X;
                imageArea.UpperY = (int)rect.BottomRight.Y;
            }
        }

        private Point _firstLocation;
        private Point _secondLocation;

        public ImageAreaSelecting(ImageDisplay imageDisplay, ImageArea imageArea)
        {
            this.imageDisplay = imageDisplay;
            this.imageArea = imageArea;
        }

        public void StartSelecting()
        {
            state = _State.NoneSelected;
        }

        public void StopSelecting()
        {
            state = _State.NotActive;
        }

        public void Click(Point location)
        {
            if (state == _State.NotActive)
            {
                return;
            }
            else if (state == _State.NoneSelected)
            {
                state = _State.FirstLocationSelected;
                firstLocation = location;
                secondLocation = location;
            }
            else if (state == _State.FirstLocationSelected)
            {
                state = _State.AreaSelected;
                secondLocation = location;
            }
            else
            {
                state = _State.NoneSelected;
            }
        }

        public void MouseMove(Point location)
        {
            if (state == _State.FirstLocationSelected)
            {
                secondLocation = location;
            }
        }
    }
}
