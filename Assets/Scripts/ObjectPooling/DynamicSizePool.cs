using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPooling
{
    public class DynamicSizePool : Pool
    {
        private readonly List<PooledObject> _objectsInPool;

        public DynamicSizePool(GameObject objectToPool, int size)
        {
            _firstUnusedObjectIndex = 0;
            _objectsInPool = new PooledObject[size].ToList();
            InitializePoolDefaultValues(objectToPool, size);
        }

        private void InitializePoolDefaultValues(GameObject objectToPool, int size)
        {
            for (int i = 0; i < size; i++)
            {
                _objectsInPool[i] = new PooledObject(Object.Instantiate(objectToPool), objectToPool);
                _objectsInPool[i].Index = i;
            }
        }

        public override PooledObject PoolObject()
        {
            if (_firstUnusedObjectIndex == _objectsInPool.Count)
            {
                _objectsInPool.Add(new PooledObject(Object.Instantiate(_objectsInPool[0].Prefab), _objectsInPool[0].Prefab));
            }
            
            var firstFreeObject = _objectsInPool[_firstUnusedObjectIndex++];
            firstFreeObject.GetPooled();
            
            return firstFreeObject;
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
            for (int i = _objectsInPool.Count - 1; i >= 0; i--)
            {
                _objectsInPool[i].SelfDestruct();
            }
        }
        
        public override void Populate(int amount)
        {
            
        }
    }
}