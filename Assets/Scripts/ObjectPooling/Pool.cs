using System.Collections.Generic;

namespace ObjectPooling
{
    public abstract class Pool
    {
        protected int _lastUnusedObjectIndex;

        public abstract PooledObject GetPooledObject();
    }   
}