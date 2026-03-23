using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Framework.Pool
{
    public class PooledObject : MonoBehaviour, IPoolMemorable, IReturnable
    {
        ObjectPool<GameObject> myPool;

        public void PoolMemorise(ObjectPool<GameObject> pool)
        {
            myPool = pool;
        }

        public void PoolReturn()
        {
            myPool.Release(gameObject);
        }
    }
}