using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class Pools
    {
        
        private readonly Dictionary<GameObject, Pool> _objectToPoolMap = new Dictionary<GameObject, Pool>();
        
        public PooledObject GetPooledObject(GameObject objectToPool)
        {
            return GetPool(objectToPool).GetPooledObject();
        }

        public PooledObject[] GetRange(GameObject objectToPool, int amount)
        {
            if (IsObjectMapped(objectToPool, out var outPool))
            {
                return outPool.GetRange(amount);
            }

            return new PooledObject[0];
        }
        
        public PooledObject[] GetRange(PooledObject objectToPool, int amount)
        {
            return GetRange(objectToPool.Prefab, amount);
        }

        public void ReleasePooledObject(PooledObject objectToRelease)
        {
            if (IsObjectMapped(objectToRelease.Prefab, out var outPool) == false)
            {
                Debug.LogWarning("Trying to release object from non-existing pool");
                return;
            }
            
            outPool.ReleasePooledObject(objectToRelease);
        }

        public void ClearPools()
        {
            foreach(var elem in _objectToPoolMap)
            {
                elem.Value.Clear();
            }
            
            _objectToPoolMap.Clear();
        }
        
        public void ClearPool(GameObject objectKeyToPool)
        {
            if (IsObjectMapped(objectKeyToPool, out var outPool))
            {
                outPool.Clear();
                _objectToPoolMap.Remove(objectKeyToPool);
            }
        }
        
        public void ClearPool(PooledObject objectKeyToPool)
        {
            ClearPool(objectKeyToPool.Prefab);
        }

        public void PopulatePool(GameObject objectKeyToPool, int amount)
        {
            if (IsObjectMapped(objectKeyToPool, out var outPool))
            {
                outPool.Populate(amount);
            }
        }
        
        public void PopulatePool(PooledObject objectKeyToPool, int amount)
        {
            PopulatePool(objectKeyToPool.Prefab, amount);
        }

        public Pool CreatePool(GameObject keyGameObject, int startCapacity = 0, PoolType poolType = PoolType.DynamicSize)
        {
            if (IsObjectMapped(keyGameObject, out var pool))
            {
                Debug.LogWarning("Trying to create pool for object that is already being pulled");
                return pool;
            }
            
            switch (poolType)
            {
                case PoolType.FixedSize:
                    pool = new FixedSizePool(keyGameObject, startCapacity);
                    break;
                case PoolType.DynamicSize:
                    pool = new DynamicSizePool(keyGameObject, startCapacity);
                    break;
                case PoolType.FixedSizeReusable:
                    pool = new DynamicSizePool(keyGameObject, startCapacity); // todo
                    break;
                case PoolType.DynamicSizeReusable:
                    pool = new DynamicSizePool(keyGameObject, startCapacity); // todo
                    break;
                default:
                    pool = new DynamicSizePool(keyGameObject, startCapacity);
                    break;
            }

            _objectToPoolMap.Add(keyGameObject, pool);
            return pool;
        }

        private Pool CreateDefaultPool(GameObject keyGameObject)
        {
            var newPool = new DynamicSizePool(keyGameObject, 0);
            _objectToPoolMap.Add(keyGameObject, newPool);
            return newPool;
        }
        
        private Pool GetPool(GameObject keyGameObject)
        {
            if (IsObjectMapped(keyGameObject, out var outPool) == false)
            {
                outPool = CreateDefaultPool(keyGameObject);
            }

            return outPool;
        }
        
        private bool IsObjectMapped(GameObject objectKeyToPool, out Pool pool)
        {
            _objectToPoolMap.TryGetValue(objectKeyToPool, out pool);
            return pool != null;
        }
        
        private bool IsObjectMapped(PooledObject objectKeyToPool, out Pool pool)
        {
            return IsObjectMapped(objectKeyToPool.Prefab, out pool);
        }
    }   
}