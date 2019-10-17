using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(menuName = "Object Pooling/Pools")]
    public class PoolsObject : ScriptableObject
    {
        [SerializeField] private PoolObject[] _pools;
    }
}