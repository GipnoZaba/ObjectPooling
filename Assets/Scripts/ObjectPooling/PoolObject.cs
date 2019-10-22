using System;
using UnityEngine;

namespace ObjectPooling
{
    [Serializable]
    public class PoolObject
    {
        [SerializeField] private String _poolName;
        [SerializeField] private PoolType _poolType;
        [SerializeField] private GameObject _objectPrefab;
        [SerializeField] private int _startSize;
        [SerializeField] private int _maxSize;
    }
}