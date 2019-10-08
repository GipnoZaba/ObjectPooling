using UnityEngine;

namespace ObjectPooling
{
    public class Pooling : MonoBehaviour
    {
        private static readonly Pools _Pools = new Pools();

        public static PooledObject Get(GameObject objectToPool)
        {
            return _Pools.GetPooledObject(objectToPool);
        }
    }
}