using GraDeMarCoWPF.Models;
using GraDeMarCoWPF.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace GraDeMarCoWPF.ViewModels
{
    public class DotCountingItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush Color { get; set; }
        public string Count { get; set; }
        public ICommand SelectColor { get; set; }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class DotCountingViewModel : ViewModelBase
    {
        private AppData appData;

        public ObservableCollection<DotCountingItem> DotCountingItems { get; set; }

        public DotCountingItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged(GetName.Of(() => SelectedItem));
            }
        }

        public ICommand AddDotCountingItem { get; private set; }
        public ICommand EraseDotCountingItem { get; private set; }

        public DotCountingViewModel(
            IColorDialogService colorDialogService,
            AppData appData)
        {
            this.appData = appData;

            DotCountingItems = new ObservableCollection<DotCountingItem>();

            AddDotCountingItem = CreateCommand(
                _ =>
                {
                    var item = new DotCountingItem()
                    {
                        Color = new SolidColorBrush(Colors.Transparent),
                        Count = System.DateTime.Now.ToString()
                    };
                    item.SelectColor = CreateCommand(
                        __ =>
                        {
                            bool? dialogResult = colorDialogService.ShowDialog();
                            if (dialogResult ?? false)
                            {
                                item.Color = new SolidColorBrush(colorDialogService.Color);
                            }
                            item.NotifyPropertyChanged("");
                        });
                    DotCountingItems.Add(item);
                },
                _ => appData.CurrentState == AppState.ImageOpened || true
                );
            EraseDotCountingItem = CreateCommand(
                _ =>
                {
                    if (SelectedItem == null)
                    {
                        return;
                    }

                    DotCountingItems.Remove(SelectedItem);
                });
        }

        private DotCountingItem _selectedItem;
    }
}
