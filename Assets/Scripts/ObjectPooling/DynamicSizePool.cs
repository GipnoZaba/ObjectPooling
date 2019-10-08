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
            _lastUnusedObjectIndex = size - 1;
            _objectsInPool = new PooledObject[size].ToList();
            InitializePoolDefaultValues(objectToPool, size);
        }

        private void InitializePoolDefaultValues(GameObject objectToPool, int size)
        {
            for (int i = 0; i < size; i++)
            {
                _objectsInPool[i] = new PooledObject(Object.Instantiate(objectToPool));
            }
        }

        public override PooledObject GetPooledObject()
        {
            if (_lastUnusedObjectIndex == _objectsInPool.Count)
            {
                _objectsInPool.Add(new PooledObject(Object.Instantiate(_objectsInPool[0]._objectInScene)));
            }
            
            var firstFreeObject = _objectsInPool[_lastUnusedObjectIndex++];

            return firstFreeObject;
        }
    }
}