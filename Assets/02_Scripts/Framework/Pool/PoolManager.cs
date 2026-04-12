using Elemental.Framework.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Framework.Pool
{
    public class PoolManager : Singleton<PoolManager>
    {
        Dictionary<GameObject, ObjectPool<GameObject>> pools = new Dictionary<GameObject, ObjectPool<GameObject>>();

        [SerializeField] List<GameObject> dropItemPrefabs;

        protected override void Awake()
        {
            base.Awake();
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

            if (gameObject.TryGetComponent(out IPoolObject poolObject))
            {
                poolObject.OnCreated(pool);
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
            if(gameObject.TryGetComponent(out IPoolable poolable)) poolable.OnSpawn();
        }

        void OnRelease(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IPoolable poolable)) poolable.OnDespawn();
            gameObject.SetActive(false);
        }

        void OnDestroyObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }

    }
}