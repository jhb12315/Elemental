using Elemental.Framework.Pool;
using Elemental.Framework.UI;
using Elemental.Gameplay.Fairy;
using Elemental.Gameplay.Fairy.Harvest;
using Elemental.Gameplay.Fairy.Harvest.Behaviour;
using Elemental.Gameplay.Fairy.Summon;
using Elemental.Gameplay.Interact;
using Elemental.Gameplay.Resource.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Elemental.Gameplay
{
    public class Altar : MonoBehaviour, IInteractable
    {
        [SerializeField] ResourceSpawner resourceSpawner;
        [SerializeField] List<GameObject> fairyPrefabs;
        [SerializeField] SummonAlterUI altarUI;
        IUIPanel AltarUIPanel => altarUI;

        SafeZoneResourceTargetTool resourceTargetTool;

        [SerializeField] Vector2 safeZone;

        void Awake()
        {
            resourceTargetTool = new SafeZoneResourceTargetTool(transform.position, safeZone);
            resourceSpawner.OnResourceSpawned += resourceTargetTool.CollectHarvestTargets;
        }

        void Start()
        {
            PoolManager.Instance.CreatePool(fairyPrefabs, transform);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, safeZone);
        }

        public void SummonFairy(HarvestFairyData fairyData)
        {
            GameObject spawnFairy = PoolManager.Instance.PooledSpawnSetPos(fairyData.fairyPrefab, transform.position);
            spawnFairy.GetComponent<HarvestFairy>().Initialize(resourceTargetTool, fairyData);
        }

        void OnDestroy()
        {
            resourceSpawner.OnResourceSpawned -= resourceTargetTool.CollectHarvestTargets;
        }

        public void OnInteracted()
        {
            UIManager.Instance.OpenUI(AltarUIPanel);
        }
    }
}