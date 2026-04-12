using Elemental.Framework.Pool;
using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Gameplay.Fairy
{
    public class FairyPooled : MonoBehaviour, IPoolObject
    {
        PooledObject pooledObject;

        void Awake()
        {
            pooledObject = new PooledObject();
        }

        public void OnCreated(ObjectPool<GameObject> pool)
        {
            pooledObject.PoolMemorise(pool);
        }
    }
}