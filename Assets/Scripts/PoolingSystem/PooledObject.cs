using UnityEngine;

namespace PoolingSystem
{
    public class PooledObject
    {
        public readonly GameObject prefab;
        public readonly GameObject objectInScene;

        public PooledObject(GameObject prefab, GameObject objectInScene)
        {
            this.prefab = prefab;
            this.objectInScene = objectInScene;
        }
    }
}
