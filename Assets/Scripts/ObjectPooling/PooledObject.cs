using UnityEngine;

namespace ObjectPooling
{
    public class PooledObject
    {
        public GameObject Prefab { get; }
        public GameObject ObjectInScene { get => _objectInScene; }
        public int Index { get; set; }
        
        private GameObject _objectInScene;
        
        public PooledObject(GameObject objectToPool, GameObject prefab)
        {
            Prefab = prefab;
            _objectInScene = objectToPool;
            _objectInScene.SetActive(false);
        }

        public PooledObject ReturnToPool()
        {
            _objectInScene.SetActive(false);
            return this;
        }

        public void GetPooled()
        {
            _objectInScene.SetActive(true);
        }

        public void SelfDestruct()
        {
            Object.Destroy(_objectInScene);
        }
    }
}