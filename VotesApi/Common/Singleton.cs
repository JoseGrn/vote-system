namespace Votes.Common
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();

        public Dictionary<string, int> votesDictionary = new Dictionary<string, int>();

        public List<int> dpiuser = new List<int>();

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
