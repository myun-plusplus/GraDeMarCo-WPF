using System;
using System.IO;
using System.Reflection;
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
            AppData = new AppData();
            ImageData = new ImageData();
            ImageDisplay = new ImageDisplay(ImageData);
            ImageArea = new ImageArea();
            ImageAreaDrawingTool = new OutlineDrawingTool();
            ImageAreaSelecting = new ImageAreaSelecting(ImageDisplay, ImageArea, ImageAreaDrawingTool);
            PlanimetricCircle = new PlanimetricCircle();
            PlanimetricCircleDrawingTool = new OutlineDrawingTool();
            PlanimetricCircleDrawing = new PlanimetricCircleDrawing(ImageDisplay, PlanimetricCircle, PlanimetricCircleDrawingTool);
            AppStateHandler = new AppStateHandler(AppData, ImageAreaSelecting, PlanimetricCircleDrawing);
        }

        public AppData AppData;

        [NonSerialized]
        public ImageData ImageData;

        [NonSerialized]
        public ImageDisplay ImageDisplay;

        public ImageArea ImageArea;

        [NonSerialized]
        public OutlineDrawingTool ImageAreaDrawingTool;

        [NonSerialized]
        public ImageAreaSelecting ImageAreaSelecting;

        public PlanimetricCircle PlanimetricCircle;

        [NonSerialized]
        public OutlineDrawingTool PlanimetricCircleDrawingTool;

        [NonSerialized]
        public PlanimetricCircleDrawing PlanimetricCircleDrawing;

        [NonSerialized]
        public AppStateHandler AppStateHandler;

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
        public void Load(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var formatter = new BinaryFormatter();
            Workspace workspace;

            byte[] data = new byte[fileInfo.Length];
            using (var stream = fileInfo.OpenRead())
            {
                stream.Read(data, 0, data.Length);
            }

            using (var ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                workspace = (Workspace)formatter.Deserialize(ms);
            }

            Copy(workspace.AppData, this.AppData);
            Copy(workspace.ImageArea, this.ImageArea);
        }

        private static void Copy<T>(T source, T destination)
        {
            var type = typeof(T);
            foreach (var sourceProperty in type.GetProperties())
            {
                var targetProperty = type.GetProperty(sourceProperty.Name);
                if (targetProperty.GetSetMethod() != null)
                {
                    targetProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
            //foreach (var sourceField in type.GetFields())
            //{
            //    var targetField = type.GetField(sourceField.Name);
            //    targetField.SetValue(destination, sourceField.GetValue(source));
            //}
        }
    }
}
