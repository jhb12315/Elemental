using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Framework.Pool
{
    public class PooledObject
    {
        ObjectPool<GameObject> myPool;

        public void PoolMemorise(ObjectPool<GameObject> pool)
        {
            myPool = pool;
        }

        public void PoolReturn(GameObject gameObject)
        {
            myPool.Release(gameObject);
        }
    }
}