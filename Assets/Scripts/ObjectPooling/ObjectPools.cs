using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(menuName = "Object Pooling/Object Pools")]
    public class ObjectPools : ScriptableObject
    {
        [SerializeField] private FixedSizePool _fixedSizePool;
        [SerializeField] private DynamicSizePool _dynamicSizePool;
        
        private Pool[] _pools;
    }
}