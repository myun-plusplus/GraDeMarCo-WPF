using GraDeMarCoWPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraDeMarCoWPF.Views
{
    /// <summary>
    /// PlanimetricCircleDrawingPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class PlanimetricCircleDrawingPanel : UserControl
    {
        private PlanimetricCircleDrawingViewModel viewModel
        {
            get { return this.DataContext as PlanimetricCircleDrawingViewModel; }
        }

        public PlanimetricCircleDrawingPanel()
        {
            InitializeComponent();
        }
    }
}
