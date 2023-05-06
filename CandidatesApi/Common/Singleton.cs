namespace Candidates.Common
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();

        public List<string> candidatesList = new List<string>();

        public string state = "";

        public bool wasopen = false;

        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
