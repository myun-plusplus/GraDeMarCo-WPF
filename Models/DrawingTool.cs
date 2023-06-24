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
}
