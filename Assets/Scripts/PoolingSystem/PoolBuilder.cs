using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolingSystem
{
    public class PoolBuilder
    {
        public GameObject prefab { get; private set; }
        public int prewarmCount { get; private set; }
        public int maxSize { get; private set; }
        public List<Action<PooledObject>> onGetCallbacks { get; private set; }
        public List<Action<PooledObject>> onReleaseCallbacks { get; private set; }
        public Dictionary<CallbackType, List<Action<PooledObject>>> callbackTypeToActions { get; private set; }

        public PoolBuilder(GameObject prefab)
        {
            this.prefab = prefab;
            maxSize = int.MaxValue;
            onGetCallbacks = new List<Action<PooledObject>>();
            onReleaseCallbacks = new List<Action<PooledObject>>();
            callbackTypeToActions = new Dictionary<CallbackType, List<Action<PooledObject>>>();
        }
        
        public PoolBuilder Prewarm(int prewarmCount)
        {
            this.prewarmCount = prewarmCount;
            return this;
        }

        public PoolBuilder SetMaxSize(int maxSize)
        {
            this.maxSize = maxSize;
            return this;
        }

        public PoolBuilder AddOnGet(Action<PooledObject> action)
        {
            onGetCallbacks.Add(action);
            return this;
        }

        public PoolBuilder AddOnRelease(Action<PooledObject> action)
        {
            onReleaseCallbacks.Add(action);
            return this;
        }

        public PoolBuilder AddCallback(CallbackType callbackType, Action<PooledObject> action)
        {
            if (callbackTypeToActions.ContainsKey(callbackType))
            {
                callbackTypeToActions[callbackType].Add(action);
            }
            else
            {
                callbackTypeToActions.Add(callbackType, new List<Action<PooledObject>>());
                callbackTypeToActions[callbackType].Add(action);
            }

            return this;
        }

        public static implicit operator Pool(PoolBuilder builder)
        {
            Pool pool = new Pool(builder);
            return pool;
        }
    }
}
