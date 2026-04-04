using Elemental.Framework.Pool;
using Elemental.Gameplay.Resource.Drop;
using Elemental.Gameplay.Resource.Spawn;
using System.Collections.Generic;
using UnityEngine;
namespace Elemental.Gameplay.Resource.Pool
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] PoolManager poolManager;
        [SerializeField] ResourceSpawnData[] resourceDatas;

        List<GameObject> resourcePrefabs;

        [SerializeField] Bounds spawnArea;

        void Awake()
        {
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
                spawnTreeObject.GetComponent<ResourceDrop>().Initialize(poolManager);
                spawnRockObject.GetComponent<ResourceDrop>().Initialize(poolManager);
            }
        }

        Vector2 GetRandomPoint()
        {
            float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
            float y = Random.Range(spawnArea.min.y, spawnArea.max.y);
            return new Vector2(x, y);
        }
    }
}