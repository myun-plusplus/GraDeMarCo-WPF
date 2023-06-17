using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraDeMarCoWPF.Views
{
    /// <summary>
    /// MyPictureBox.xaml の相互作用ロジック
    /// </summary>
    public partial class MyPictureBox : UserControl
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(MyPictureBox));
        public static readonly DependencyProperty ZoomScaleProperty = DependencyProperty.Register("ZoomScale", typeof(double), typeof(MyPictureBox));
        //public static readonly DependencyProperty CanvasWidthProperty = DependencyProperty.Register("CanvasWidth", typeof(double), typeof(MyPictureBox));
        //public static readonly DependencyProperty CanvasHeightProperty = DependencyProperty.Register("CanvasHeight", typeof(double), typeof(MyPictureBox));

        public ImageSource Source
        {
            get
            {
                return (ImageSource)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }

        public double ZoomScale
        {
            get
            {
                return (double)GetValue(ZoomScaleProperty);
            }
            set
            {
                SetValue(ZoomScaleProperty, value);
                //if (Source != null)
                //{
                //    CanvasWidth = Source.Width * value;
                //    CanvasHeight = Source.Height * value;
                //}
            }
        }

        //public double CanvasWidth
        //{
        //    get
        //    {
        //        return (double)GetValue(CanvasWidthProperty);
        //    }
        //    set
        //    {
        //        SetValue(CanvasWidthProperty, value);
        //    }
        //}

        //public double CanvasHeight
        //{
        //    get
        //    {
        //        return (double)GetValue(CanvasHeightProperty);
        //    }
        //    set
        //    {
        //        SetValue(CanvasHeightProperty, value);
        //    }
        //}

        public MyPictureBox()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawEllipse(new SolidColorBrush(Colors.Transparent), new Pen(new SolidColorBrush(Colors.Blue), 1.0), new Point(100, 100), 50, 50);
        }
    }
}
