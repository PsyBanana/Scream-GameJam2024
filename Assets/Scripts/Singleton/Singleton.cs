namespace StimpakEssentials
{
    public class Singleton<T> where T : class, new()
    {
        protected static T _instance;

        public static bool Exists
        {
            get
            {
                return _instance != null;
            }
        }

        protected Singleton()
        {

        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}