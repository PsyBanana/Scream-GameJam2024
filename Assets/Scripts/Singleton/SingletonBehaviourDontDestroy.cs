using UnityEngine;

namespace StimpakEssentials
{
    public class SingletonBehaviourDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;

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
                if (!_instance)
                {
                    _instance = FindAndRemoveDuplicated();
                    if (!_instance)
                    {
                        GameObject go = new GameObject(typeof(T).Name, typeof(T));
                        _instance = go.GetComponent<T>();
                    }

                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        static T FindAndRemoveDuplicated()
        {
            T[] managers = FindObjectsOfType(typeof(T)) as T[];

            if (managers != null)
            {
                if (managers.Length > 1)
                {
                    for (int i = 1; i < managers.Length; i++)
                    {
                        T manager = managers[i];
                        Destroy(manager.gameObject);
                    }

                    return managers[0];
                }
                else if (managers.Length == 1)
                {
                    return managers[0];
                }
            }

            return null;
        }
    }
}
