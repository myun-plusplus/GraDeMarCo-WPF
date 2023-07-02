using GraDeMarCoWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraDeMarCoWPF.Views
{
    /// <summary>
    /// MyRenderBox.xaml の相互作用ロジック
    /// </summary>
    public partial class MyRenderBox : UserControl
    {
        private ImageViewModel viewModel
        {
            get { return this.DataContext as ImageViewModel; }
        }

        public static readonly DependencyProperty ZoomScaleProperty = DependencyProperty.Register("RenderZoomScale", typeof(double), typeof(MyRenderBox));
        
        public double RenderZoomScale
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

        public MyRenderBox()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = RenderZoomScale;
            scaleTransform.ScaleY = RenderZoomScale;
            drawingContext.PushTransform(scaleTransform);

            drawingContext.DrawEllipse(new SolidColorBrush(Colors.Transparent), new Pen(new SolidColorBrush(Colors.Blue), 1.0), new Point(100, 100), 50, 50);
            drawingContext.DrawEllipse(new SolidColorBrush(Colors.Red), new Pen(new SolidColorBrush(Colors.Red), 0.0), new Point(200, 100), 5.0, 5.0);

            viewModel.DrawOnRenderCommand.Execute(drawingContext);

            drawingContext.Pop();
        }
    }
}
