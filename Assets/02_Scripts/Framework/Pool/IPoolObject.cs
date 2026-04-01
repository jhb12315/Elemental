using UnityEngine;
using UnityEngine.Pool;

namespace Elemental.Framework.Pool
{
    // 객체가 생성 될 때 PoolManager가 호출
    public interface IPoolObject
    {
        void OnCreated(ObjectPool<GameObject> pool);
    }
}
