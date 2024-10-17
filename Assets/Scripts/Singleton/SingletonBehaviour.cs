using UnityEngine;

namespace StimpakEssentials
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;

        public static bool Exists
        {
            get
            {
                return _instance != null;
            }
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_2021_1_OR_NEWER
                    _instance = (T)FindObjectOfType(typeof(T), true);
#else
                    _instance = (T)FindObjectOfType(typeof(T));
#endif
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
        }
    }
}
