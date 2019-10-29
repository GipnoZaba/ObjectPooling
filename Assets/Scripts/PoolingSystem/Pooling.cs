using System.Collections.Generic;
using UnityEngine;

namespace PoolingSystem
{
    public class Pooling
    {
        private static Dictionary<GameObject, Pool> _PrefabToPool = new Dictionary<GameObject, Pool>();

        public static PooledObject Get(GameObject prefab)
        {
            Pool pool;
            if (PoolExists(prefab, out pool))
            {
                return pool.Get();
            }

            pool = InitNewPool(prefab);
            return pool.Get();
        }

        public static void Reclaim(PooledObject pooledObject)
        {
            if (_PrefabToPool.TryGetValue(pooledObject.prefab, out var pool))
            { 
                pool.Release(pooledObject);
            }
        }

        public static void InitNewPool(Pool pool)
        {
            if (_PrefabToPool.ContainsKey(pool.prefab)) return;
            
            _PrefabToPool.Add(pool.prefab, pool);
        }

        private static Pool InitNewPool(GameObject prefab)
        {
            Pool pool = new Pool(prefab);
            _PrefabToPool.Add(prefab, pool);
            return pool;
        }

        private static bool PoolExists(GameObject prefab, out Pool pool)
        {
            return _PrefabToPool.TryGetValue(prefab, out pool);
        }
    }
}