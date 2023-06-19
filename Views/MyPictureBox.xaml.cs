using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraDeMarCoWPF.Views
{
    /// <summary>
    /// MyPictureBox.xaml の相互作用ロジック
    /// </summary>
    public partial class MyPictureBox : UserControl
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(MyPictureBox));
        public static readonly DependencyProperty ZoomScaleProperty = DependencyProperty.Register("ZoomScale", typeof(double), typeof(MyPictureBox));

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
            }
        }

        public MyPictureBox()
        {
            InitializeComponent();
        }
    }
}
