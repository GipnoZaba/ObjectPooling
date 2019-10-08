using UnityEngine;

namespace ObjectPooling
{
    public class PooledObject
    {
        public GameObject _objectInScene { get; private set; }

        public PooledObject(GameObject objectToPool)
        {
            _objectInScene = objectToPool;
            ReturnToPool();
        }

        public void ReturnToPool()
        {
            _objectInScene.SetActive(false);
        }

        public void GetPooled()
        {
            _objectInScene.SetActive(true);
        }
    }
}