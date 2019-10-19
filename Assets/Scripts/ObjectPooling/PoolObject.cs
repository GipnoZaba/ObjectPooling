using System;
using UnityEngine;

namespace ObjectPooling
{
    [Serializable]
    public class PoolObject
    {
        [Tooltip("asdasdasdasd")]
        [SerializeField] private String _poolName;
        [SerializeField] private PoolType _poolType;
        [SerializeField] private int _maxSize;
    }
}