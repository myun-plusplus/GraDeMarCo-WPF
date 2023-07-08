using System.Windows.Media;

namespace GraDeMarCoWPF.Services
{
    public interface IWindowService
    {
        void Open();
        void Close();
    }

    public interface IDialogService
    {
        bool? ShowDialog();
    }

    public interface IOpenFileDialogService : IDialogService
    {
        string Filename { get;set; }
    }

    public interface ISaveFileDialogService : IDialogService
    {
        string Filename { get; set; }
    }

    public interface IColorDialogService : IDialogService
    {
        Color Color { get; set; }
    }
}
