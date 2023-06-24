namespace GraDeMarCoWPF.Models
{
    public class Workspace
    {
        public static Workspace Instance
        {
            get
            {
                return _instance;
            }
        }

        private static Workspace _instance = new Workspace();

        public ImageData ImageData { get; private set; }
        public ImageDisplay ImageDisplay { get; private set; }
        public ImageArea ImageArea { get; private set; }
        public OutlineDrawingTool ImageAreaDrawingTool { get; private set; }
        public ImageAreaSelecting ImageAreaSelecting { get; private set; }

        private Workspace()
        {
            ImageData = new ImageData();
            ImageDisplay = new ImageDisplay(ImageData);
            ImageArea = new ImageArea();
            ImageAreaDrawingTool = new OutlineDrawingTool();
            ImageAreaSelecting = new ImageAreaSelecting(ImageDisplay, ImageArea, ImageAreaDrawingTool);
        }
    }
}
