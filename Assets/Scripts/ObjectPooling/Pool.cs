namespace ObjectPooling
{
    public abstract class Pool
    {
        protected int _firstUnusedObjectIndex;

        public abstract PooledObject PoolObject();
        public abstract void ReleasePooledObject(PooledObject objectToRelease);
        public abstract void Clear();
        public abstract void Populate(int amount);
    }   
}