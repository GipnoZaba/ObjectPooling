using UnityEngine;

namespace ObjectPooling
{
    public class FixedSizePool : Pool
    {

        private readonly PooledObject[] _objectsInPool;

        public FixedSizePool(GameObject objectToPool, int size)
        {
            _lastUnusedObjectIndex = size - 1;
            _objectsInPool = new PooledObject[size];
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
            if (_lastUnusedObjectIndex < 0)
            {
                Debug.LogError("No available objects");
                return null;
            }
            
            var firstFreeObject = _objectsInPool[_lastUnusedObjectIndex--];

            return firstFreeObject;
        }
    }
}