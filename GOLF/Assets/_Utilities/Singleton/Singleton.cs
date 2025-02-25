namespace UnityEngine
{
    /// <summary>
    /// This is meant to be used with Interfaces, not classes.
    /// </summary>
    /// <typeparam name="I">Interface to mask.</typeparam>
    public class Singleton<I> : MonoBehaviour where I : class
    {
        public static I Source { get; private set; }
        
        protected virtual void Awake()
        {
            if (Source != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            
            Source = this as I;
            DontDestroyOnLoad(gameObject);
        }
    }
}
