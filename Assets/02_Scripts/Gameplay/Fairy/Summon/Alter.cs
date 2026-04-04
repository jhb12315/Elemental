using Elemental.Framework.Pool;
using System.Collections.Generic;
using UnityEngine;

public class Alter : MonoBehaviour
{
    [SerializeField] PoolManager poolManager;
    [SerializeField] List<GameObject> fairyPrefabs;

    void Awake()
    {
        poolManager.CreatePool(fairyPrefabs, transform);
    }

    public void SummonFairy(GameObject fairy)
    {
        poolManager.PooledSpawnSetPos(fairy, transform.position);
    }
}
