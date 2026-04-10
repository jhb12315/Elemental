using Elemental.Framework.Pool;
using Elemental.Gameplay.Resource.Drop;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Gameplay.Resource.Cut
{
    public class ResourceReturner : MonoBehaviour, ICuttable, IPoolObject, IPoolable
    {
        PooledObject pooledObject;
        ResourceDrop resourceDrop;
        Collider2D coll;

        public event Action<Collider2D> OnReturned;

        public int cutCount;
        public int currentCutCount;

        public bool IsReturned { get; private set; }

        void Awake()
        {
            pooledObject = new PooledObject();
            resourceDrop = GetComponent<ResourceDrop>();
            coll = GetComponent<Collider2D>();
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
            IsReturned = false;
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
            if (IsReturned) return;
            OnReturned?.Invoke(coll);
            resourceDrop.Drop();
            pooledObject.PoolReturn(gameObject);
        }

        public void OnDespawn()
        {
            IsReturned = true;
        }
    }
}