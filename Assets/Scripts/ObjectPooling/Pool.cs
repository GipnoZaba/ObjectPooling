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
        protected HashSet<ReleaseCallbackType> _releaseCallbacks;
        
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
            
            foreach (var type in _releaseCallbacks)
            {
                ReleaseCallback callback;
                
                switch (type)
                {
                    case ReleaseCallbackType.None:
                        break;
                    case ReleaseCallbackType.OnCollision:
                        callback = gameObjectPooled.AddComponent<ReleaseOnColliderCallback>();
                        ReleaseOnColliderCallback onCollisionCallback = (ReleaseOnColliderCallback) callback;
                        onCollisionCallback.isTrigger = false;
                        callback.pooledObject = newPooledObject;
                        break;
                    case ReleaseCallbackType.OnCollision2D:
                        break;
                    case ReleaseCallbackType.OnTrigger:
                        callback = gameObjectPooled.AddComponent<ReleaseOnColliderCallback>();
                        ReleaseOnColliderCallback onTriggerCallback = (ReleaseOnColliderCallback) callback;
                        onTriggerCallback.isTrigger = true;
                        callback.pooledObject = newPooledObject;
                        break;
                    case ReleaseCallbackType.OnTrigger2D:
                        break;
                    case ReleaseCallbackType.OnDestroy:
                        break;
                }
            }

            return newPooledObject;
        }
    }   
}