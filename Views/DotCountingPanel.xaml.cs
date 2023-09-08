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
    /// DotCountingPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class DotCountingPanel : UserControl
    {
        public DotCountingPanel()
        {
            InitializeComponent();
        }
    }

    //private void lvw_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    foreach (var item in e.RemovedItems.Cast<Item>())
    //        item.IsSelected = false;
    //    foreach (var item in e.AddedItems.Cast<Item>())
    //        item.IsSelected = true;
    //}
}
