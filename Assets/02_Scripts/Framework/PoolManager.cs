using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Framework.Pool
{
    public class PoolManager : MonoBehaviour
    {
        Dictionary<GameObject, ObjectPool<GameObject>> pools;

        void Awake()
        {
            pools = new Dictionary<GameObject, ObjectPool<GameObject>>();
        }

        public void PreloadPool(List<GameObject> prefabs, Transform parent, int preloadCount)
        {
            foreach (GameObject prefab in prefabs)
            {
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

                for (int i = 0; i <= preloadCount; i++)
                {
                    GameObject spawnedObject = CreateInstance(prefab, parent, pool);
                    pool.Release(spawnedObject);
                }
            }
        }

        GameObject CreateInstance(GameObject p, Transform parent, ObjectPool<GameObject> pool)
        {
            GameObject go = Instantiate(p, parent);

            if (go.TryGetComponent(out IPoolMemorable po))
            {
                po.PoolMemorise(pool);
            }

            return go;
        }

        public GameObject PooledSpawn(GameObject p, Vector2 pos)
        {
            if (pools.TryGetValue(p, out ObjectPool<GameObject> pool))
            {
                GameObject spawnedObject = pool.Get();

                spawnedObject.transform.position = pos;
                spawnedObject.transform.rotation = Quaternion.identity;

                spawnedObject.SetActive(true);

                return spawnedObject;
            }
            return null;
        }

        void OnGet(GameObject go)
        {

        }

        void OnRelease(GameObject go)
        {
            go.SetActive(false);
        }

        void OnDestroyObject(GameObject go)
        {
            Destroy(go);
        }

    }
}