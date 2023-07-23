using System.Windows.Media;

namespace GraDeMarCoWPF.Models
{
    public class OutlineDrawingTool : BindableBase
    {
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotifyPropertyChanged(GetName.Of(() => Color));

                Pen.Brush = new SolidColorBrush(value);
            }
        }

        public Pen Pen { get; private set; }

        private Color _color;

        public OutlineDrawingTool()
        {
            Pen = new Pen(null, 1.0);
        }
    }

    public class DotDrawingTool : BindableBase
    {
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotifyPropertyChanged(GetName.Of(() => Color));

                Brush.Color = value;
            }
        }

        public double Size
        {
            get { return _size; }
            set
            {
                _size = value;
                NotifyPropertyChanged(GetName.Of(() => Size));
            }
        }

        public SolidColorBrush Brush { get; private set; }

        private Color _color;
        private double _size;

        public DotDrawingTool()
        {
            Brush = new SolidColorBrush(Colors.Transparent);
        }
    }
}
