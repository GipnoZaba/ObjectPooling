using UnityEngine;

namespace ObjectPooling
{
    public class FixedSizePool : Pool
    {

        private readonly PooledObject[] _objectsInPool;
        public override int Capacity => _maxPoolSize;
        public override int UsedObjectsCount => _firstUnusedObjectIndex;
        public override int FreeObjectsCount => _maxPoolSize - _firstUnusedObjectIndex;

        public FixedSizePool(GameObject objectToPool, int size)
        {
            _firstUnusedObjectIndex = 0;
            _maxPoolSize = size;
            _objectsInPool = new PooledObject[size];
            InitializePoolDefaultValues(objectToPool, size);
        }

        private void InitializePoolDefaultValues(GameObject objectToPool, int size)
        {
            for (int i = 0; i < size; i++)
            {
                _objectsInPool[i] = CreatePooledObject(objectToPool);
                _objectsInPool[i].Index = i;
            }
        }
        
        public override PooledObject GetPooledObject()
        {
            if (_firstUnusedObjectIndex >= _objectsInPool.Length)
            {
                Debug.LogError("No available objects");
                return null;
            }

            var firstFreeObject = _objectsInPool[_firstUnusedObjectIndex++];
            firstFreeObject.GetPooled();

            return firstFreeObject;
        }

        public override PooledObject[] GetRange(int amount)
        {
            if (FreeObjectsCount >= amount)
            {
                PooledObject[] pooledObjects = new PooledObject[amount];
                for (int i = 0; i < amount; i++)
                {
                    pooledObjects[i] = GetPooledObject();
                }

                return pooledObjects;
            }
            
            Debug.LogWarning("Not enough free objects in pool");
            return null;
        }

        public override void ReleasePooledObject(PooledObject objectToRelease)
        {
            var objectToReleaseTmp = _objectsInPool[objectToRelease.Index].ReturnToPool();

            _objectsInPool[objectToReleaseTmp.Index] = _objectsInPool[_firstUnusedObjectIndex - 1];
            _objectsInPool[objectToReleaseTmp.Index].Index = objectToReleaseTmp.Index;
            
            _objectsInPool[_firstUnusedObjectIndex - 1] = objectToReleaseTmp;
            _objectsInPool[_firstUnusedObjectIndex - 1].Index = _firstUnusedObjectIndex - 1;
            
            _firstUnusedObjectIndex -= 1;
        }
        
        public override void Clear()
        {
            for (int i = _objectsInPool.Length - 1; i >= 0; i--)
            {
                _objectsInPool[i].SelfDestruct();
            }
        }

        public override void Populate(int amount)
        {
            Debug.LogWarning("Trying to populate fixed size pool");
        }

        protected override GameObject GetObjectPrefab()
        {
            return _objectsInPool[0].Prefab;
        }
    }
}