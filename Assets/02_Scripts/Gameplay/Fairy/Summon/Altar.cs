using Elemental.Framework.Pool;
using Elemental.Gameplay.Fairy;
using Elemental.Gameplay.Fairy.Harvest;
using Elemental.Gameplay.Fairy.Harvest.Behaviour;
using Elemental.Gameplay.Resource.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay
{
    public class Altar : MonoBehaviour
    {
        [SerializeField] PoolManager poolManager;
        [SerializeField] ResourceSpawner resourceSpawner;
        [SerializeField] List<GameObject> fairyPrefabs;

        ResourceTargetTool resourceTargetTool;

        [SerializeField] Vector2 safeZone;

        void Awake()
        {
            resourceTargetTool = new ResourceTargetTool(transform.position, safeZone);
            poolManager.CreatePool(fairyPrefabs, transform);
            resourceSpawner.OnResourceSpawned += resourceTargetTool.CollectHarvestTargets;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, safeZone);
        }

        public void SummonFairy(HarvestFairyData fairyData)
        {
            GameObject spawnFairy = poolManager.PooledSpawnSetPos(fairyData.fairyPrefab, transform.position);
            spawnFairy.GetComponent<HarvestFairy>().Initialize(resourceTargetTool, fairyData);
        }

        void OnDestroy()
        {
            resourceSpawner.OnResourceSpawned -= resourceTargetTool.CollectHarvestTargets;
        }
    }
}