using Elemental.Framework.Pool;
using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Gameplay.item
{
    public class ItemReturner : MonoBehaviour, IPoolObject, IPoolable
    {
        bool isReturned;

        public PooledObject pooledObject;

        void Awake()
        {
            pooledObject = new PooledObject();
        }

        public void OnCreated(ObjectPool<GameObject> pool)
        {
            pooledObject.PoolMemorise(pool);
        }

        public void OnSpawn()
        {
            isReturned = false;
        }

        public void OnCollected()
        {
            if (isReturned) return;
            pooledObject.PoolReturn(gameObject);
        }

        public void OnDespawn()
        {
            isReturned = true;
        }
    }
}