using Elemental.Framework.Pool;
using Elemental.Gameplay.Fairy;
using Elemental.Gameplay.Fairy.Behaviour;
using System.Collections.Generic;
using UnityEngine;

public class Alter : MonoBehaviour
{
    [SerializeField] PoolManager poolManager;
    [SerializeField] List<GameObject> fairyPrefabs;

    [SerializeField] LayerMask resourceLayer;
    ContactFilter2D resourceFilter;
    List<Collider2D> harvestTargets;

    [SerializeField] Vector2 safeZone;

    void Awake()
    {
        resourceFilter = new ContactFilter2D
        {
            layerMask = resourceLayer,
            useLayerMask = true
        };

        harvestTargets = new List<Collider2D>(50);

        poolManager.CreatePool(fairyPrefabs, transform);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, safeZone);
    }

    public void SummonFairy(HarvestFairyData fairyData)
    {
        GameObject spawnFairy = poolManager.PooledSpawnSetPos(fairyData.fairyPrefab, transform.position);
        spawnFairy.GetComponent<HarvestFairy>().Initialize(GetHarvestTargets(), fairyData);
    }

    List<Collider2D> GetHarvestTargets()
    {
        harvestTargets.Clear();

        Physics2D.OverlapBox(transform.position, safeZone, 0f, resourceFilter, harvestTargets);

        return harvestTargets;
    }
}
