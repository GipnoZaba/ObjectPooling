using PoolingSystem;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;

    private void Start()
    {
        /*
        Pool poolBuilt = new PoolBuilder(prefab1)
            .Prewarm(5)
            .SetMaxSize(15)
            .AddOnGet((pooledObject) => Debug.Log("Got " + pooledObject))
            .AddOnRelease((pooledObject) => Debug.Log("Release " + pooledObject))
            .AddCallback(CallbackType.OnCollisionEnter, (pooledObject) => Pooling.Reclaim(pooledObject));
        poolBuilt.Get();
        */
    }
}
