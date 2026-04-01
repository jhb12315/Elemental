
namespace Elemental.Framework.Pool
{
    // 풀에서 꺼낼 때, 반납될 때 PoolManager가 호출
    public interface IPoolable
    {
        void OnSpawn();
        void OnDespawn();
    }
}