using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraDeMarCoWPF.Models
{
    [Serializable]
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

        private Workspace()
        {
            ImageData = new ImageData();
            ImageDisplay = new ImageDisplay(ImageData);
            ImageArea = new ImageArea();
            ImageAreaDrawingTool = new OutlineDrawingTool();
            ImageAreaSelecting = new ImageAreaSelecting(ImageDisplay, ImageArea, ImageAreaDrawingTool);
        }

        [NonSerialized]
        public ImageData ImageData;

        [NonSerialized]
        public ImageDisplay ImageDisplay;

        public ImageArea ImageArea;

        [NonSerialized]
        public OutlineDrawingTool ImageAreaDrawingTool;

        [NonSerialized]
        public ImageAreaSelecting ImageAreaSelecting;

        public void Save(string filePath)
        {
            byte[] data;

            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, this);
                data = new byte[ms.Length];
                data = ms.GetBuffer();
            }

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(data, 0, data.Length);
            }
        }
    }
}
