using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectPooling
{
    public abstract class Pool
    {
        protected int _firstUnusedObjectIndex;
        protected int _maxPoolSize;
        protected HashSet<ReleaseCallback> releaseCallbacks;
        
        public abstract int Capacity { get; }
        public abstract int FreeObjectsCount { get; }
        public abstract int UsedObjectsCount { get; }
        
        public abstract PooledObject GetPooledObject();
        public abstract PooledObject[] GetRange(int amount);
        public abstract void ReleasePooledObject(PooledObject objectToRelease);
        public abstract void Clear();
        public abstract void Populate(int amount);
        protected abstract GameObject GetObjectPrefab();

        protected PooledObject CreatePooledObject(GameObject objectToPool)
        {
            GameObject gameObjectPooled = Object.Instantiate(objectToPool); 
            PooledObject newPooledObject = new PooledObject(gameObjectPooled, objectToPool);
            
            foreach (var releaseCallback in releaseCallbacks)
            {
                ReleaseCallback callback = gameObjectPooled.AddComponent(releaseCallback.GetType()) as ReleaseCallback;
                callback.pooledObject = newPooledObject;
            }

            return newPooledObject;
        }
    }   
}