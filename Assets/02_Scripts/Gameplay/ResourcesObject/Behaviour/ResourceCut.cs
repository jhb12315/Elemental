using Elemental.Framework.Pool;
using Elemental.Gameplay.Resource.Drop;
using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Gameplay.Resource.Cut
{
    public class ResourceCut : MonoBehaviour, ICuttable, IPoolObject, IPoolable
    {
        PooledObject pooledObject;
        ResourceDrop resourceDrop;

        public int cutCount;
        public int currentCutCount;

        bool isReturned;

        void Awake()
        {
            pooledObject = new PooledObject();
            resourceDrop = GetComponent<ResourceDrop>();
        }

        public void Initialize()
        {
            currentCutCount = 0;
        }

        public void OnCreated(ObjectPool<GameObject> pool)
        {
            pooledObject.PoolMemorise(pool);
        }

        public void OnSpawn()
        {
            isReturned = false;
        }

        public void Cut()
        {
            currentCutCount++;
            if (cutCount <= currentCutCount)
            {
                CutComplete();
            }
        }

        // 채집 완료 후
        void CutComplete()
        {
            if (isReturned) return;
            resourceDrop.Drop();
            pooledObject.PoolReturn(gameObject);
        }

        public void OnDespawn()
        {
            isReturned = true;
        }
    }
}