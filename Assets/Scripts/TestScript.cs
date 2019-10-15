using ObjectPooling;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;

    private void Start()
    {
        Pooling.InitPool(_projectilePrefab, 2, PoolType.FixedSize);
        Pooling.PopulatePool(_projectilePrefab, 3);
        Pooling.GetRange(_projectilePrefab, 2);
        Pooling.GetRange(_projectilePrefab, 5);
        /*
        Pooling.GetAllPools();
        Pooling.GetPoolCapacity(_projectilePrefab);
        Pooling.GetPoolUsedObjectsCount(_projectilePrefab);
        Pooling.GetPoolUnusedObjectsCount(_projectilePrefab);
        Pooling.Get(_projectilePrefab, Vector3.one, Vector3.one * 4);
        Pooling.Get(_projectilePrefab, 1, 3);
        Pooling.Get(_projectilePrefab, Scale.Inherit/Original...);
        */
    }
}
