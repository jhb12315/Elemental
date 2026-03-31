using Elemental.Framework.Pool;
using UnityEngine;

namespace Elemental.Gameplay.Resource.item
{
    public class ItemReturner : MonoBehaviour, IPoolInitialize
    {
        PooledObject pooledObject;

        bool isReturned;

        void Awake()
        {
            pooledObject = GetComponent<PooledObject>();
            isReturned = true;
        }

        public void Initialize()
        {
            pooledObject = new PooledObject();
            isReturned = false;
        }

        public void OnCollected()
        {
            if (isReturned) return;
            pooledObject.PoolReturn(gameObject);
            isReturned = true;
        }
    }
}