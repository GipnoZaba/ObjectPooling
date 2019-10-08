using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class Pools
    {
        
        private readonly Dictionary<GameObject, Pool> _objectToPoolMap = new Dictionary<GameObject, Pool>();
        
        public PooledObject GetPooledObject(GameObject objectToPool)
        {
            return GetPool(objectToPool).PoolObject();
        }

        public void ReleasePooledObject(PooledObject objectToRelease)
        {
            _objectToPoolMap.TryGetValue(objectToRelease.Prefab, out var outPool);

            if (outPool == null)
            {
                Debug.LogError("Trying to release object from non-existing pool");
                return;
            }
            
            outPool.ReleasePooledObject(objectToRelease);
        }

        private Pool GetPool(GameObject keyGameObject)
        {
            _objectToPoolMap.TryGetValue(keyGameObject, out var outPool);

            if (outPool == null)
            {
                outPool = new DynamicSizePool(keyGameObject, 5);
                _objectToPoolMap.Add(keyGameObject, outPool);
            }

            return outPool;
        }
        
        public void ClearPool(GameObject objectKeyToPool)
        {
            _objectToPoolMap.TryGetValue(objectKeyToPool, out var outPool);

            if (outPool != null)
            {
                outPool.Clear();
            }
        }
        
        public void ClearPool(PooledObject objectKeyToPool)
        {
            _objectToPoolMap.TryGetValue(objectKeyToPool.Prefab, out var outPool);

            outPool?.Clear();
            _objectToPoolMap.Remove(objectKeyToPool.Prefab);
        }
    }   
}