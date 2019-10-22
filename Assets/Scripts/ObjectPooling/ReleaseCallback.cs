using UnityEngine;

namespace ObjectPooling
{
    public abstract class ReleaseCallback : MonoBehaviour
    {
        public PooledObject pooledObject;

        protected virtual void Release()
        {
            Pooling.Release(pooledObject);
        }
    }
}