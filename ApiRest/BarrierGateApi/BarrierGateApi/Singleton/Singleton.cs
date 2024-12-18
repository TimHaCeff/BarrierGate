namespace BarrierGateApi.Singleton
{
    public class Singleton<T> where T : class
    {
        protected static T instance;
        protected static readonly object threadSafeLocker = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (threadSafeLocker)
                    {
                        if (instance == null)
                        {
                            instance = (T)Activator.CreateInstance(typeof(T), true);
                        }
                    }
                }
                return instance;
            }
        }
    }
}
