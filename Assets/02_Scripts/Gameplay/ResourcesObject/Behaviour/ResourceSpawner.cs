using Elemental.Framework.Pool;
using Elemental.Gameplay.Resource.Drop;
using Elemental.Gameplay.Resource.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Elemental.Gameplay.Resource.Pool
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] ResourceSpawnData[] resourceDatas;

        PoolManager poolManager;

        public event Action OnResourceSpawned;

        List<GameObject> resourcePrefabs;

        [SerializeField] Bounds spawnArea;

        void Awake()
        {
            poolManager = PoolManager.Instance;
            resourcePrefabs = new List<GameObject>(resourceDatas.Length);
            
            foreach (var data in resourceDatas)
            {
                resourcePrefabs.Add(data.prefab);
            }

            poolManager.CreatePool(resourcePrefabs, transform);
        }

        void Start()
        {
            ResourceSpawn();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(spawnArea.center, spawnArea.size);
        }

        void ResourceSpawn()
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject spawnTreeObject = poolManager.PooledSpawnSetPos(resourcePrefabs[0], GetRandomPoint());
                GameObject spawnRockObject = poolManager.PooledSpawnSetPos(resourcePrefabs[1], GetRandomPoint());
                spawnTreeObject.GetComponent<ResourceDrop>().Initialize();
                spawnRockObject.GetComponent<ResourceDrop>().Initialize();
            }

            OnResourceSpawned?.Invoke();
        }

        Vector2 GetRandomPoint()
        {
            float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
            float y = Random.Range(spawnArea.min.y, spawnArea.max.y);
            return new Vector2(x, y);
        }
    }
}