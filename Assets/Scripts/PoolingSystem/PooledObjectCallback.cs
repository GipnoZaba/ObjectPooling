using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolingSystem
{
    public class PooledObjectCallback : MonoBehaviour
    {
        public Dictionary<CallbackType, List<Action<PooledObject>>> callbackTypeToActions = new Dictionary<CallbackType, List<Action<PooledObject>>>();
        public PooledObject pooledObject;
        
        private void OnCollisionEnter(Collision other)
        {
            if (callbackTypeToActions.TryGetValue(CallbackType.OnCollisionEnter, out var actions))
            {
                foreach (var action in actions)
                {
                    action.Invoke(pooledObject);
                }
            }
        }
    }
}
