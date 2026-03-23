using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Framework.Pool
{
    public interface IPoolMemorable
    {
        void PoolMemorise(ObjectPool<GameObject> pool);
    }
}