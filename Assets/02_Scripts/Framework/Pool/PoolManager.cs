using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Framework.Pool
{
    public class PoolManager : MonoBehaviour
    {
        Dictionary<GameObject, ObjectPool<GameObject>> pools = new Dictionary<GameObject, ObjectPool<GameObject>>();

        [SerializeField] List<GameObject> dropItemPrefabs;

        void Awake()
        {
            CreatePool(dropItemPrefabs, transform);
        }

        public void CreatePool(List<GameObject> prefabs, Transform parent)
        {
            foreach (GameObject prefab in prefabs)
            {
                if (pools.TryGetValue(prefab, out ObjectPool<GameObject> objectPool)) continue;

                ObjectPool<GameObject> pool = null;

                pool = new ObjectPool<GameObject>(
                    createFunc: () => CreateInstance(prefab, parent, pool),
                    actionOnGet: OnGet,
                    actionOnRelease: OnRelease,
                    actionOnDestroy: OnDestroyObject,
                    collectionCheck: true,
                    defaultCapacity: 30,
                    maxSize: 200
                    );

                pools[prefab] = pool;
            }
        }

        GameObject CreateInstance(GameObject prefab, Transform parent, ObjectPool<GameObject> pool)
        {
            GameObject gameObject = Instantiate(prefab, parent);

            if (gameObject.TryGetComponent(out IPoolMemorable po))
            {
                po.PoolMemorise(pool);
            }

            return gameObject;
        }

        public GameObject PooledSpawnSetPos(GameObject prefab, Vector2 pos)
        {
            if (pools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                GameObject spawnedObject = pool.Get();

                spawnedObject.transform.position = pos;
                spawnedObject.transform.rotation = Quaternion.identity;

                spawnedObject.SetActive(true);

                return spawnedObject;
            }
            return null;
        }

        public GameObject PooledSpawn(GameObject prefab)
        {
            if (pools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                GameObject spawnedObject = pool.Get();

                spawnedObject.SetActive(true);

                return spawnedObject;
            }
            return null;
        }

        void OnGet(GameObject gameObject)
        {

        }

        void OnRelease(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        void OnDestroyObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }

    }
}