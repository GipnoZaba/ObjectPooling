using System;
using System.Collections.Generic;
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
        [SerializeField] private List<ReleaseCallbackType> _callbackTypes = new List<ReleaseCallbackType>();

        public String PoolName => _poolName;
        public PoolType PoolType => _poolType;
        public GameObject ObjectPrefab => _objectPrefab;
        public int StartSize => _startSize;
        public int MaxSize => _maxSize;
        public List<ReleaseCallbackType> CallbackTypes => _callbackTypes;
    }

    public enum ReleaseCallbackType
    {
        None,
        OnCollision,
        OnCollision2D,
        OnTrigger,
        OnTrigger2D,
        OnDestroy,
    }
}