using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class Pools
    {
        
        private Dictionary<GameObject, Pool> _objectToPoolMap = new Dictionary<GameObject, Pool>();
        
        public PooledObject GetPooledObject(GameObject objectToPool)
        {
            return GetPool(objectToPool).GetPooledObject();
        }

        private Pool GetPool(GameObject keyGameObject)
        {
            Pool outPool;
            
            _objectToPoolMap.TryGetValue(keyGameObject, out outPool);

            if (outPool == null)
            {
                outPool = new DynamicSizePool(keyGameObject, 5);
                _objectToPoolMap.Add(keyGameObject, outPool);
            }

            return outPool;
        }
    }   
}