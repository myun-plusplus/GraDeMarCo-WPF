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

        private Workspace()
        {
            ModelA = new ModelA();
            ModelB = new ModelB(ModelA);
        }

        public ModelA ModelA { get; private set; }
        public ModelB ModelB { get; private set; }
    }

    public class ModelA
    {
        public string a { get; set; }

        public ModelA()
        {

        }
    }

    public class ModelB
    {
        private ModelA a;

        public ModelB(ModelA a)
        {
            this.a = a;
        }
    }
}
