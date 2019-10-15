using UnityEngine;

namespace ObjectPooling
{
    public class Pooling : MonoBehaviour
    {
        private static readonly Pools _Pools = new Pools();

        #region Get overloads
        public static PooledObject Get(GameObject objectToPool, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            PooledObject pooledObject = _Pools.GetPooledObject(objectToPool);

            pooledObject.ObjectInScene.transform.parent = parent;
            if (parent == null)
            {
                pooledObject.ObjectInScene.transform.position = position;
                pooledObject.ObjectInScene.transform.rotation = rotation;
            }
            else
            {
                pooledObject.ObjectInScene.transform.localPosition = position;
                pooledObject.ObjectInScene.transform.localRotation = rotation;
            }

            return pooledObject;
        }

        public static PooledObject Get(GameObject objectToPool, Transform parent = null)
        {
            return Get(objectToPool, objectToPool.transform.position, objectToPool.transform.rotation, parent);
        }

        public static PooledObject Get(GameObject objectToPool, Transform parent, bool inWorldSpace)
        {
            PooledObject pooledObject = _Pools.GetPooledObject(objectToPool);

            pooledObject.ObjectInScene.transform.parent = parent;
            if (parent != null && inWorldSpace == false)
            {
                pooledObject.ObjectInScene.transform.localPosition = objectToPool.transform.position;
                pooledObject.ObjectInScene.transform.localRotation = objectToPool.transform.rotation;
            }
            else
            {
                pooledObject.ObjectInScene.transform.position = objectToPool.transform.position;
                pooledObject.ObjectInScene.transform.rotation = objectToPool.transform.rotation;
            }

            return pooledObject;
        }
        #endregion

        public static PooledObject[] GetRange(GameObject objectToPool, int amount)
        {
            return _Pools.GetRange(objectToPool, amount);
        }
        
        public static PooledObject[] GetRange(PooledObject objectToPool, int amount)
        {
            return _Pools.GetRange(objectToPool, amount);
        }
        
        public static void Release(PooledObject objectToRelease)
        {
            _Pools.ReleasePooledObject(objectToRelease);
        }

        public static void ClearPools()
        {
            _Pools.ClearPools();
        }

        public static void ClearPool(GameObject objectKeyToPool)
        {
            _Pools.ClearPool(objectKeyToPool);
        }
        
        public static void ClearPool(PooledObject objectKeyToPool)
        {
            _Pools.ClearPool(objectKeyToPool);
        }

        public static void PopulatePool(GameObject objectKeyToPool, int amount)
        {
            _Pools.PopulatePool(objectKeyToPool, amount);
        }
        
        public static void PopulatePool(PooledObject objectKeyToPool, int amount)
        {
            _Pools.PopulatePool(objectKeyToPool, amount);
        }

        public static void InitPool(GameObject objectToPool, int amount = 0, PoolType poolType = PoolType.DynamicSize)
        {
            _Pools.CreatePool(objectToPool, amount, poolType);
        }
    }
}