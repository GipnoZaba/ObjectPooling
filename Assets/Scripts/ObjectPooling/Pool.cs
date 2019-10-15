﻿using UnityEngine;

namespace ObjectPooling
{
    public abstract class Pool
    {
        protected int _firstUnusedObjectIndex;
        protected int _maxPoolSize;
        protected abstract int FreeObjectsCount { get; }
        
        public abstract PooledObject GetPooledObject();
        public abstract PooledObject[] GetRange(int amount);
        public abstract void ReleasePooledObject(PooledObject objectToRelease);
        public abstract void Clear();
        public abstract void Populate(int amount);
        protected abstract GameObject GetObjectPrefab();
        protected PooledObject CreatePooledObject(GameObject objectToPool) =>
            new PooledObject(Object.Instantiate(objectToPool), objectToPool);
    }   
}