using ObjectPooling;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;

    private void Start()
    {
        Pooling.InitPool(_projectilePrefab, 2, PoolType.FixedSize);
        Pooling.GetPoolCapacity(_projectilePrefab);
        Pooling.GetPoolUsedObjectsCount(_projectilePrefab);
        Pooling.GetPoolUnusedObjectsCount(_projectilePrefab);
    }
}
