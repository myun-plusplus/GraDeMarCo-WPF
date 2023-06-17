using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// MyRenderBox.xaml の相互作用ロジック
    /// </summary>
    public partial class MyRenderBox : UserControl
    {
        public MyRenderBox()
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
