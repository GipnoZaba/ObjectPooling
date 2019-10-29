using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PoolingSystem
{
    public class Pool
    {

        public int CountAll => _allObjects.Count;
        public int CountActive => _firstUnusedIndex;
        public int CountInactive => CountAll - CountActive;
        public int MaximumSize { get; private set; }
        
        public readonly GameObject prefab;
        private readonly List<PooledObject> _allObjects = new List<PooledObject>();
        private int _firstUnusedIndex;

        private Action<PooledObject>[] _onGetActions;
        private Action<PooledObject>[] _onReleaseActions;
        private readonly Dictionary<CallbackType, List<Action<PooledObject>>> _callbackTypeToActions;
        
        public Pool(GameObject prefab, int maximumSize = int.MaxValue, Action<PooledObject> onGet = null, Action<PooledObject> onRelease = null)
        {
            this.prefab = prefab;
            MaximumSize = maximumSize;
            _onGetActions = new[] { onGet };
            _onReleaseActions = new[] { onRelease };
        }

        public Pool(PoolBuilder builder)
        {
            prefab = builder.prefab;
            _onGetActions = builder.onGetCallbacks.ToArray();
            _onReleaseActions = builder.onReleaseCallbacks.ToArray();
            MaximumSize = builder.maxSize;
            _callbackTypeToActions = builder.callbackTypeToActions;
            
            Add(builder.prewarmCount);
            
            Pooling.InitNewPool(this);
        }

        public PooledObject Get()
        {
            if (CountAll >= MaximumSize)
            {
                Debug.LogWarning("Pool maximum size reached.");
                return null;
            }
            
            if (_firstUnusedIndex == CountAll)
            {
                Add();
            }
            
            PooledObject pooledObject = _allObjects[_firstUnusedIndex++];
            pooledObject.objectInScene.SetActive(true);

            foreach (var action in _onGetActions)
            {
                action?.Invoke(pooledObject);
            }

            return pooledObject;
        }

        public void Release(PooledObject pooledObject)
        {
            if (_firstUnusedIndex == 0 ||
                prefab != pooledObject.prefab)
            {
                return;
            }
            
            int reclaimedIndex = _allObjects.IndexOf(pooledObject);
            if (reclaimedIndex == -1) return;
            
            foreach (var action in _onReleaseActions)
            {
                action?.Invoke(pooledObject);
            }
            
            pooledObject.objectInScene.SetActive(false);
            _allObjects[reclaimedIndex] = _allObjects[--_firstUnusedIndex];
            _allObjects[_firstUnusedIndex] = pooledObject;
        }

        public void Add(int count = 1)
        {
            GameObject obj;
            PooledObject pooledObject;
            PooledObjectCallback objectCallback;
            
            for (int i = 0; i < count; i++)
            {
                obj = Object.Instantiate(prefab);
                obj.SetActive(false);
                
                pooledObject = new PooledObject(prefab, obj);
                
                objectCallback = obj.AddComponent<PooledObjectCallback>();
                objectCallback.pooledObject = pooledObject;
                objectCallback.callbackTypeToActions = _callbackTypeToActions;
                
                _allObjects.Add(pooledObject);
            }
        }
    }
}