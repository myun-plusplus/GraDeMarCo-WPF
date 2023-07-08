using System.Windows.Forms;

namespace GraDeMarCoWPF.Services
{
    public class ColorDialogService : IColorDialogService
    {
        private ColorDialog colorDialog;

        public System.Windows.Media.Color Color
        {
            get
            {
                return System.Windows.Media.Color.FromArgb(
                    colorDialog.Color.A,
                    colorDialog.Color.R,
                    colorDialog.Color.G,
                    colorDialog.Color.B);
            }
            set {
                colorDialog.Color = System.Drawing.Color.FromArgb(
                    value.A,
                    value.R,
                    value.G,
                    value.B);
            }
        }

        public ColorDialogService()
        {
            colorDialog = new ColorDialog();
            this.colorDialog.CustomColors = new int[] {
                0x150088, 0x241CED, 0x277FFF, 0x00F2FF, 0x4CB122, 0xE8A200, 0xCC483F, 0xA449A3,
                0x577AB9, 0xC9AEFF, 0x0EC9FF, 0xB0E4EF, 0x1DE6B5, 0xEAD999, 0xBE9270, 0xE7BFC8 };
            this.colorDialog.FullOpen = true;
        }

        public bool? ShowDialog()
        {
            DialogResult dialogResult = colorDialog.ShowDialog();
            return dialogResult == DialogResult.OK;
        }
    }
}
