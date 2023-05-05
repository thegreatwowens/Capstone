using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class SingletonDontDestroyOnLoad<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected bool m_initialized = false;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;

                //Let you place your object anywhere in the scene, but make sure it is a root gameobject in runtime
                if (transform.parent != null)
                    transform.parent = null;

                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
