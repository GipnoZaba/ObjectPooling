using System;
using UnityEngine;

namespace ObjectPooling
{
    [Serializable]
    public class PoolObject
    {
        [SerializeField] private PoolType _poolType;
    }
}