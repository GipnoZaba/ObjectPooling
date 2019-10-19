using System;

namespace ObjectPooling
{
    [Serializable]
    public enum PoolType{
        FixedSize,
        DynamicSize,
        FixedSizeReusable,
        DynamicSizeReusable
    }
}